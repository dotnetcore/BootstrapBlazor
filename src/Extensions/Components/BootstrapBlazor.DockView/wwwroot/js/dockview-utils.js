import { DockviewComponent } from "./dockview-core.esm.js"
import { DockviewPanelContent } from "./dockview-content.js"
import { onAddGroup, addGroupWithPanel } from "./dockview-group.js"
import { onAddPanel, getPanels, findPanelFunc, findPanel } from "./dockview-panel.js"
import { getConfig, reloadFromConfig, loadPanelsFromLocalstorage } from './dockview-config.js'
import './dockview-extensions.js'

const cerateDockview = (el, options) => {
    const template = el.querySelector('template');
    const dockview = new DockviewComponent({
        parentElement: el,
        createComponent: option => new DockviewPanelContent(option)
    });
    initDockview(dockview, options, template);

    dockview.init();
    return dockview;
}

const initDockview = (dockview, options, template) => {
    dockview.params = { panels: [], options, template, observer: null };
    loadPanelsFromLocalstorage(dockview);

    dockview.init = () => {
        const config = getConfig(options);
        dockview.fromJSON(config);
    }

    dockview.update = options => {
        if (options.layoutConfig) {
            reloadFromConfig(dockview, options)
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

    dockview.onDidAddPanel(onAddPanel);

    dockview.onDidAddGroup(onAddGroup);

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

const toggleComponent = (dockview, options) => {
    const panels = getPanels(options.content)
    const localPanels = dockview.panels
    panels.forEach(p => {
        const pan = localPanels.find(findPanelFunc(p));
        if (pan === void 0) {
            const panel = dockview.params.panels.find(findPanelFunc(p));
            addGroupWithPanel(dockview, panel || p);
        }
    })

    localPanels.forEach(item => {
        let pan = panels.find(findPanelFunc(item));
        if (pan === void 0) {
            dockview.removePanel(item)
        }
    })
}

export { cerateDockview };
