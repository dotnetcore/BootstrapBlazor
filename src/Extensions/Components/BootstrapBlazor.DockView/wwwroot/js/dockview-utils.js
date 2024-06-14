import { DockviewComponent } from "./dockview-core.esm.js"
import { DockviewPanelContent } from "./dockview-content.js"
import { createGroupActions } from "./dockview-group.js"
import { updateDockviewPanel } from "./dockview-panel.js"
import { fixObject, getConfigFromStorage, savePanel, deletePanel, loadPanelsFromLocalstorage } from './dockview-extensions.js'

const cerateDockview = (el, options) => {
    const template = el.querySelector('template');
    const dockview = new DockviewComponent({
        parentElement: el,
        createComponent: option => new DockviewPanelContent(option)
    });
    initDockview(dockview, options, template);

    dockview.init();
    return dockview
}

const initDockview = (dockview, options, template) => {
    dockview.params = { panels: [], options, template, observer: null };
    loadPanelsFromLocalstorage(dockview);

    dockview.init = () => {
        const config = options.enableLocalStorage ? getConfigFromStorage(options.localStorageKey) : getConfigFromOptions(options);
        dockview.fromJSON(config);
    }

    dockview.update = options => {
        if (options.layoutConfig) {
            reloadDockview(dockview, options)
        }
        else if (dockview.locked !== options.lock) {
            // TODO: 循环所有 Group 锁定 Group
        }
        else {
            toggleComponent(dockview, options)
        }
    }

    dockview.reset = options => {
        reloadDockview(dockview, options)
    }

    dockview.dispose = () => {
    }

    dockview.onDidRemovePanel(event => {
        let panel = {
            id: event.id,
            title: event.title,
            component: event.view.contentComponent,
            groupId: event.group.id,
            params: {
                ...event.params,
                currentPosition: {
                    width: event.group.element.parentElement.offsetWidth,
                    height: event.group.element.parentElement.offsetHeight,
                    top: parseFloat(event.group.element.parentElement.style.top || 0),
                    left: parseFloat(event.group.element.parentElement.style.left || 0)
                }
            }
        }
        if (event.params.groupInvisible) {
            panel.groupInvisible = event.params.groupInvisible
        }
        savePanel(dockview, panel)

        // 在group上存储已删除的panel标识
        !event.group.children && (event.group.children = [])
        event.group.children = event.group.children.filter(p => findPanel(p, event) !== null);
        event.group.children.push({
            id: event.id,
            title: event.title,
            params: event.params
        })

        if (event.view.content.element) {
            if (event.titleMenuEle) {
                event.view.content.element.append(event.titleMenuEle)
            }
            if (dockview.params.template) {
                dockview.params.template.append(event.view.content.element)
            }
        }
    })

    dockview.onDidAddPanel(updateDockviewPanel);

    dockview.onDidAddGroup(group => {
        Object.defineProperties(group, {
            type: {
                get() { return model.location.type }
            },
            params: {
                get() { return JSON.parse(JSON.stringify(group.activePanel?.params || {})) }
            }
        })

        const { floatingGroups = [] } = dockview;
        let floatingGroup = floatingGroups.find(item => item.data.id === group.id)
        if (floatingGroup) {
            let { width, height, top, left } = floatingGroup.position
            setTimeout(() => {
                let style = group.element.parentElement.style
                style.width = width + 'px'
                style.height = height + 'px'
                style.top = top + 'px'
                style.left = left + 'px'
            }, 0)
        }

        createGroupActions(group);
    });

    dockview.onDidRemoveGroup(event => {
        console.log('remove-group', event);
    })

    dockview.onWillDragPanel(event => {
        if (event.panel.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    dockview._onDidMovePanel.event(event => { })

    dockview.onWillDragGroup(event => {
        if (event.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    dockview.gridview.onDidChange(event => { })

    dockview.onDidLayoutFromJSON(event => {
        setTimeout(() => {
            dockview._initialized?.fire()
        }, 0)
        dockview.groups.forEach(group => {
            if (group.panels.length === 0) {
                dockview.setVisible(group, false)
            }
        })
    })
    dockview.onDidRemove(() => {
    })
}

const reloadDockview = (dockview, options) => {
    dockview.clear()
    dockview.params.panels = [];

    const jsonData = getConfigFromOptions(options);
    dockview.fromJSON(jsonData);
}

const toggleComponent = (dockview, options) => {
    const panels = getPanels(options.content)
    const localPanels = dockview.panels
    panels.forEach(p => {
        const pan = localPanels.find(findPanelFunc(p));
        if (pan === void 0) {
            const panel = dockview.params.panels.find(findPanelFunc(p));
            addDelPanel(dockview, panel || p);
        }
    })

    localPanels.forEach(item => {
        let pan = panels.find(findPanelFunc(item));
        if (pan === void 0) {
            dockview.removePanel(item)
        }
    })
}

const getGroupIdFunc = () => {
    let currentId = 0;
    return () => `${currentId++}`;
}

const getConfigFromOptions = options => options.layoutConfig ? getConfigFromLayoutString(options) : getConfigFromContent(options);

const getConfigFromLayoutString = options => {
    let config = JSON.parse(options.layoutConfig);
    const panels = getPanels(options.content);
    Object.values(config.panels).forEach(value => {
        const contentPanel = panels.find(findPanelFunc(value));
        if (contentPanel) {
            value.params = {
                ...value.params,
                ...contentPanel.params
            }
        }
    });
    return fixObject(config);
}

const getConfigFromContent = options => {
    const { width, height } = { width: 800, height: 600 };
    const getGroupId = getGroupIdFunc()
    const panels = {}
    const orientation = options.content[0].type === 'row' ? 'VERTICAL' : 'HORIZONTAL';
    return fixObject({
        activeGroup: '1',
        grid: {
            width,
            height,
            orientation,
            root: {
                type: 'branch',
                data: [getTree(options.content[0], { width, height, orientation }, options, panels, getGroupId)]
            },
        },
        panels
    });
}

const addDelPanel = (dockview, panel) => {
    let group
    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    if (panel.groupId) {
        group = dockview.api.getGroup(panel.groupId)
        if (!group) {
            group = dockview.createGroup({ id: panel.groupId })
            const floatingGroupPosition = isMaximized ? {
                x: 0, y: 0,
                width: dockview.width,
                height: dockview.height
            } : {
                x: currentPosition?.left || 0,
                y: currentPosition?.top || 0,
                width: currentPosition?.width,
                height: currentPosition?.height
            }
            dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })
            createGroupActions(group);
        }
        else {
            if (group.api.location.type === 'grid') {
                let isVisible = dockview.isVisible(group)
                if (isVisible === false) {
                    dockview.setVisible(group, true)
                    isMaximized && group.api.maximize();
                }
            }
        }
        dockview.addPanel({
            id: panel.id,
            title: panel.title,
            component: panel.component,
            position: { referenceGroup: group },
            params: { ...panel.params, isPackup, height, isMaximized, position }
        })
    }
    else {
        group = dockview.groups.find(group => group.children?.[panel.id])
        if (!group) {
            let curentPanel = dockview.panels.findLast(item => item.params.parentId === panel.params.parentId)
            let direction = getOrientation(dockview.gridview.root, curentPanel.group) === 'VERTICAL' ? 'below' : 'right'
            dockview.addPanel({
                id: panel.id,
                title: panel.title,
                component: panel.component,
                position: { referenceGroup: curentPanel.group, direction },//direction: "bottom"
                params: { ...panel.params, isPackup, height, isMaximized, position }
            });
        }
        else {
            if (group.api.location.type === 'grid') {
                let isVisible = dockview.isVisible(group)
                if (isVisible === false) {
                    dockview.setVisible(group, true)
                    isMaximized && group.api.maximize()
                }
            }
            dockview.addPanel({
                id: panel.id,
                title: panel.title,
                component: panel.component,
                position: { referenceGroup: group },
                params: { ...panel.params, isPackup, height, isMaximized, position }
            })
        }
    }
    deletePanel(dockview, panel)
}

const getOrientation = function (child, group) {
    if (child.children) {
        let targetGroup = child.children.find(item => !item.children && item.element === group.element)
        if (targetGroup) {
            return child.orientation
        }
        else {
            for (const item of child.children) {
                let orientation = getOrientation(item, group)
                if (orientation) return orientation
            }
        }
    } else {
        return false
    }
}

const saveConfig = dockview => {
    if (dockview.hasMaximizedGroup()) return;

    if (dockview.params.options.enableLocalStorage) {
        const json = dockview.toJSON()
        localStorage.setItem(dockview.params.options.localStorageKey, JSON.stringify(json));
    }
}

const getTree = (contentItem, { width, height, orientation }, parent, panels, getGroupId) => {
    let length = parent.content.length || 1
    let obj = {}, boxSize = orientation === 'HORIZONTAL' ? width : height, size
    let hasSizeList = parent.content.filter(item => item.width || item.height)
    let hasSizeLen = hasSizeList.length
    if (hasSizeLen === 0) {
        size = (1 / length * boxSize).toFixed(2) * 1
    } else {
        size = hasSizeList.reduce((pre, cur) => pre + (cur.width || cur.height), 0)
        size = ((boxSize - size) / (length - hasSizeLen)).toFixed(2) * 1
    }
    orientation === 'HORIZONTAL' ? width = size : height = size
    orientation = orientation === 'HORIZONTAL' ? 'VERTICAL' : 'HORIZONTAL'

    if (contentItem.type === 'row' || contentItem.type === 'column') {
        obj.type = 'branch'
        obj.size = contentItem.width || contentItem.height || size
        obj.data = contentItem.content.map(item => getTree(item, { width, height, orientation }, contentItem, panels, getGroupId))
    }
    else if (contentItem.type === 'group') {
        obj.type = 'leaf'
        obj.size = contentItem.width || contentItem.height || size
        obj.visible = contentItem.content.some(item => item.visible !== false)
        obj.data = {
            id: getGroupId(),
            activeView: contentItem.content[0].id,
            hideHeader: contentItem.content.length === 1 && contentItem.content[0].showHeader === false,
            views: contentItem.content.filter(item => item.visible !== false).map(item => {
                panels[item.id] = {
                    id: item.id,
                    title: item.title,
                    tabComponent: item.componentName,
                    contentComponent: item.componentName,
                    params: { ...item, parentId: parent.id }
                }
                return item.id
            })
        }
    }
    else if (contentItem.type === 'component') {
        obj.type = 'leaf'
        obj.visible = contentItem.visible !== false
        obj.size = contentItem.width || contentItem.height || size
        obj.data = {
            id: getGroupId(),
            activeView: contentItem.id,
            hideHeader: contentItem.showHeader === false,
            views: obj.visible ? [contentItem.id] : []
        }
        if (obj.visible) {
            panels[contentItem.id] = {
                id: contentItem.id,
                title: contentItem.title,
                tabComponent: contentItem.componentName,
                contentComponent: contentItem.componentName,
                params: { ...contentItem, parentId: parent.id }
            }
        }
    }
    return obj
}

const getPanels = content => {
    return getPanel(content[0])
}

const getPanel = (contentItem, parent = {}, panels = []) => {
    if (contentItem.type === 'component') {
        panels.push({
            id: contentItem.id,
            groupId: contentItem.groupId,
            title: contentItem.title,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
            params: { ...contentItem, parentId: parent.id }
        })
    } else {
        contentItem.content?.forEach(item => getPanel(item, contentItem, panels))
    }
    return panels
}

const findPanelFunc = v => p => findPanel(p, v);

const findPanel = (p, v) => (p.params.key && p.params.key === v.params.key) || p.id === v.id || p.title === v.title;

export { cerateDockview };
