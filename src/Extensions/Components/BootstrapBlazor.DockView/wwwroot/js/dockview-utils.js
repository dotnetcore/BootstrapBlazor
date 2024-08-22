import { DockviewComponent } from "./dockview-core.esm.js"
import { DockviewPanelContent } from "./dockview-content.js"
import { onAddGroup, addGroupWithPanel, toggleLock, observeFloatingGroupLocationChange } from "./dockview-group.js"
import { onAddPanel, onRemovePanel, getPanelsFromOptions, findContentFromPanels } from "./dockview-panel.js"
import { getConfig, reloadFromConfig, loadPanelsFromLocalstorage, saveConfig } from './dockview-config.js'
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
        dockview.params.floatingGroups = config.floatingGroups || []
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

    dockview.onDidRemovePanel(onRemovePanel);

    dockview.onDidAddPanel(onAddPanel);

    dockview.onDidAddGroup(onAddGroup);

    dockview.onWillDragPanel(event => {
        if (event.panel.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    dockview.onWillDragGroup(event => {
        if (event.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    dockview.onDidLayoutFromJSON(() => {
        const handler = setTimeout(() => {
            clearTimeout(handler);

            const panels = dockview.panels
            const delPanelsStr = localStorage.getItem(dockview.params.options.localStorageKey + '-panels')
            const delPanels = delPanelsStr && JSON.parse(delPanelsStr) || []
            panels.forEach(panel => {
                dockview._panelVisibleChanged?.fire({ title: panel.title, status: true });
            })
            delPanels.forEach(panel => {
                dockview._panelVisibleChanged?.fire({ title: panel.title, status: false });
            })
            const { floatingGroups } = dockview.params
            floatingGroups.forEach(floatingGroup => {
                const group = dockview.groups.find(g => g.id == floatingGroup.data.id)
                if (!group) return
                const { top, left } = floatingGroup.position
                const style = group.element.parentElement.style
                style.top = top + 'px'
                style.left = left + 'px'

                observeFloatingGroupLocationChange(group)
            })

            dockview._inited = true;
            dockview._initialized?.fire()
            dockview.groups.forEach(group => {
                observeGroup(group)
            })
        }, 100);
    })

    dockview.gridview.onDidChange(event => {
        dockview._groupSizeChanged.fire()
        saveConfig(dockview)
    })

    dockview._rootDropTarget.onDrop(() => {
        saveConfig(dockview)
    })

}

export const observeGroup = (group) => {
    const dockview = group.api.accessor
    if (dockview.params.observer === null) {
        dockview.params.observer = new ResizeObserver(observerList => resizeObserverHandle(observerList, dockview));
    }
    dockview.params.observer.observe(group.header.element)
    dockview.params.observer.observe(group.header.tabContainer)
    for (let panel of group.panels) {
        if (panel.params.isActive) {
            panel.api.setActive()
            break
        }
    }
}

const resizeObserverHandle = (observerList, dockview) => {
    observerList.forEach(({ target }) => {
        setWidth(target, dockview)
    })
}
const setWidth = (target, dockview) => {
    let header, tabsContainer
    if (target.classList.contains('tabs-container')) {
        header = target.parentElement
        tabsContainer = target
    }
    else {
        header = target
        tabsContainer = header.querySelector('.tabs-container')
    }
    if (header.offsetWidth == 0) return
    let voidWidth = header.querySelector('.void-container').offsetWidth
    let dropdown = header.querySelector('.right-actions-container>.dropdown')
    if (!dropdown) return
    let dropMenu = dropdown.querySelector('.dropdown-menu')
    if (voidWidth === 0) {
        if (tabsContainer.children.length <= 1) return
        const inactiveTabs = header.querySelectorAll('.tabs-container>.tab')
        const lastTab = inactiveTabs[inactiveTabs.length - 1]
        const aEle = document.createElement('a')
        const liEle = document.createElement('li')
        aEle.className = 'dropdown-item'
        liEle.tabWidth = lastTab.offsetWidth;
        aEle.append(lastTab)
        liEle.append(aEle)
        dropMenu.insertAdjacentElement("afterbegin", liEle)
    }
    else {
        let firstLi = dropMenu.querySelector('li:has(.tab)') || dropMenu.children[0]
        if (firstLi) {
            let firstTab = firstLi.querySelector('.tab')
            if (voidWidth > firstLi.tabWidth || tabsContainer.children.length == 0) {
                firstTab && tabsContainer.append(firstTab)
                firstLi.remove()
            }
        }
    }
    setTimeout(() => {
        if ([...tabsContainer.children].every(tab => !tab.classList.contains('active-tab'))) {
            const group = dockview.groups.find(g => g.element === header.parentElement)
            group.panels[0].api.setActive()
        }
    }, 100);
}

const toggleComponent = (dockview, options) => {
    const panels = getPanelsFromOptions(options).filter(p => p.params.visible)
    const localPanels = dockview.panels
    panels.forEach(p => {
        const pan = findContentFromPanels(localPanels, p);
        if (pan === void 0) {
            const panel = findContentFromPanels(dockview.params.panels, p);
            const groupPanels = panels.filter(p1 => p1.params.parentId == p.params.parentId)
            let indexOfOptions = groupPanels.findIndex(p => p.params.key == panel.params.key)
            indexOfOptions = indexOfOptions == -1 ? 0 : indexOfOptions
            const index = panel && panel.params.index
            addGroupWithPanel(dockview, panel || p, panels, index ?? indexOfOptions);
        }
    })

    localPanels.forEach(item => {
        let pan = findContentFromPanels(panels, item);
        if (pan === void 0) {
            item.group.delPanelIndex = item.group.panels.findIndex(p => p.params.key == item.params.key)
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
