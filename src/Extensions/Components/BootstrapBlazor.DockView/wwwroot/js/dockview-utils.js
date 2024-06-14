import { DockviewComponent } from "../js/dockview-core.esm.js"
import { DockviewPanelContent } from "../js/dockview-content.js"
import { createGroupActions } from "../js/dockview-group-extensions.js"
import { updateDockviewPanel } from "../js/dockview-panel-extensions.js"
import '../js/dockview-extensions.js'

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

const reloadDockview = (dockview, options) => {
    dockview.clear()
    dockview.params.panels = [];

    const jsonData = getJson(getConfigByOptions(options));
    dockview.fromJSON(jsonData);
}

const toggleComponent = (dockview, options) => {
    const panels = getPanels(options.content)
    const localPanels = dockview.panels || {}
    panels.forEach(panel => {
        let pan = localPanels.find(item => item.title === panel.title)
        if (pan === void 0) {
            let storagePanels = getLocal(`${dockview.params.options.localStorageKey}-panels`) || []
            let storagePanel = storagePanels.find(item => (panel.params.key && panel.params.key === item.params.key) || item.id === panel.id || item.title === panel.title)
            addDelPanel(storagePanel || panel, [], dockview)
        }
    })

    localPanels.forEach(item => {
        let pan = panels.find(panel => panel.title === item.title)
        if (pan === void 0) {
            dockview.removePanel(item)
        }
    })
}

const initDockview = (dockview, options, template) => {
    dockview.params = { panels: [], options, template };

    dockview.init = () => {
        const config = options.enableLocalStorage ? getLocal(options.localStorageKey) : getConfigByOptions(options);
        const jsonData = getJson(config)
        dockview.fromJSON(jsonData);
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
        event.group.children = event.group.children.filter(panel => {
            return !((event.params.key && event.params.key === panel.params.key) || panel.id === event.id || panel.title === event.title)
        })
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

    dockview.onDidAddGroup(event => {
        // 给每个Group实例添加type和params属性
        Object.defineProperties(event, {
            type: {
                get() { return this.model.location.type }
            },
            params: {
                get() { return JSON.parse(JSON.stringify(event.activePanel?.params || {})) }
            }
        })
        if (0) {
            event.header.hidden = true
        }

        const { floatingGroups = [] } = dockview;
        let floatingGroup = floatingGroups.find(item => item.data.id === event.id)
        if (floatingGroup) {
            let { width, height, top, left } = floatingGroup.position
            setTimeout(() => {
                let style = event.element.parentElement.style
                style.width = width + 'px'
                style.height = height + 'px'
                style.top = top + 'px'
                style.left = left + 'px'
            }, 0)
        }

        createGroupActions(event);
        observer.observe(event.header.element)
    })

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

const setWidth = (observerList) => {
    observerList.forEach(observer => {
        let header = observer.target
        let tabsContainer = header.querySelector('.tabs-container')
        let voidWidth = header.querySelector('.void-container').offsetWidth
        let dropdown = header.querySelector('.right-actions-container>.dropdown')
        if (!dropdown) return
        let dropMenu = dropdown.querySelector('.dropdown-menu')
        if (voidWidth === 0) {
            if (tabsContainer.children.length <= 1) return
            let lastTab = header.querySelector('.tabs-container>.inactive-tab:not(:has(+ .inactive-tab))')
            let aEle = document.createElement('a')
            let liEle = document.createElement('li')
            aEle.className = 'dropdown-item'
            liEle.setAttribute('tabWidth', lastTab.offsetWidth)
            liEle.addEventListener('click', () => {
                liEle.setAttribute('tabWidth', tabsContainer.children[0].offsetWidth)
                liEle.children[0].append(tabsContainer.children[0])
                tabsContainer.append(liEle.children[0].children[0])
            })
            aEle.append(lastTab)
            liEle.append(aEle)
            dropMenu.insertAdjacentElement("afterbegin", liEle)
        } else {
            let firstLi = dropMenu.children[0]
            if (firstLi) {
                let firstTab = firstLi.querySelector('.tab')
                if (voidWidth > firstLi.getAttribute('tabWidth')) {
                    firstTab && tabsContainer.append(firstTab)
                    firstLi.remove()
                }
            }
        }
    })
}

const observer = new ResizeObserver(setWidth)

const getGroupIdFunc = () => {
    let currentId = 0;
    return () => `${currentId++}`;
}

const getConfigByOptions = options => options.layoutConfig ? getConfigByLayoutString(options) : getConfigByContent(options);

const getConfigByLayoutString = options => {
    let config = JSON.parse(options.layoutConfig)
    const panels = getPanels(content)
    Object.values(config.panels).forEach(value => {
        let contentPanel = panels.find(panel => (panel.params.key && panel.params.key === value.params.key) || panel.id === value.id || panel.title === value.title)
        value.params = {
            ...value.params,
            class: contentPanel.params.class,
            height: contentPanel.params.height,
            parentId: contentPanel.params.parentId,
            showClose: contentPanel.params.showClose,
            showHeader: contentPanel.params.showHeader,
            showLock: contentPanel.params.showLock,
            titleClass: contentPanel.params.titleClass,
            titleWidth: contentPanel.params.titleWidth,
            type: contentPanel.params.type,
            width: contentPanel.params.width
        }
    });
    return config;
}

const getConfigByContent = options => {
    const { width, height } = { width: 800, height: 600 };
    const getGroupId = getGroupIdFunc()
    const panels = {}
    const orientation = options.content[0].type === 'row' ? 'VERTICAL' : 'HORIZONTAL';
    return {
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
    };
}

export function addDelPanel(panel, delPanels, dockview) {
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
            let newPanel = dockview.addPanel({
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

export function getJson(data) {
    // 修正JSON
    // 修改浮动框的宽高
    data.floatingGroups?.forEach(item => {
        let { width, height } = item.position
        item.position.width = width - 2
        item.position.height = height - 2
    })
    // 修改隐藏Group的父级size可能为0
    let { grid: { root }, orientation } = data
    correctBranch(root)
    return data
}

const correctBranch = branch => {
    if (branch.type === 'leaf') {
        if (branch.visible === false) {
            delete branch.visible
        }
        return
    }
    branch.data.forEach(item => {
        correctBranch(item)
    })
}

export function getLocal(key) {
    return JSON.parse(localStorage.getItem(key))
}

const savePanel = (dockview, panel) => {
    const { panels, options } = dockview.params;
    panels.push(panel)
    if (options.enableLocalStorage) {
        localStorage.setItem(`${options.localStorageKey}-panels`, JSON.stringify(panels))
    }
}

const deletePanel = (dockview, panel) => {
    const { panels, options } = dockview.params;
    let index = panels.indexOf(panel);
    if (index > -1) {
        panels.splice(index, 1);
    }
    panels.slice(panel);
    if (options.enableLocalStorage) {
        localStorage.setItem(`${options.localStorageKey}-panels`, JSON.stringify(panels))
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

export { cerateDockview };
