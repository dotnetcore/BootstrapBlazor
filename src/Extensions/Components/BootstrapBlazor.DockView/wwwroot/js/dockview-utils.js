import { DockviewComponent } from "./dockview-core.esm.js"
import { DockviewPanelContent } from "./dockview-content.js"
import { onAddGroup, addGroupWithPanel, toggleLock } from "./dockview-group.js"
import { onAddPanel, onRemovePanel, getPanels, findPanelFunc } from "./dockview-panel.js"
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
        else if (dockview.params.options.lock !== options.lock) {
            dockview.params.options.lock = options.lock
            toggleGroupLock(dockview, options)
        }
        else {
            toggleComponent(dockview, options)
        }
    }

    dockview.reset = options => {
        reloadFromConfig(dockview, options)
    }

    dockview.dispose = () => {
    }

    dockview.onDidRemovePanel(onRemovePanel);

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
const toggleGroupLock = (dockview, options) => {
    dockview.groups.forEach(group => {
        toggleLock(group, group.header.rightActionsContainer, options.lock)
    })
}

export { cerateDockview };
