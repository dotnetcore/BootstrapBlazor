import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import { createDock } from "../../js/golden-layout-extensions.js"
import { saveConfig, removeConfig } from "../../js/golden-layout-config.js"
import { lockDock, resetDockLock, lockStack, unlockStack, lockTab, toggleComponent, getAllItemsByType } from "../../js/golden-layout-utility.js"
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")

    const eventsData = new Map()
    const dock = { el, template: el.querySelector('template'), eventsData, invoke, lock: option.lock, layoutConfig: option.layoutConfig }
    Data.set(id, dock)

    option.invokeVisibleChangedCallback = (title, visible) => {
        invoke.invokeMethodAsync(option.visibleChangedCallback, title, visible)
    }

    const layout = createDock(el, option);
    dock.layout = layout
    layout.on('initialised', () => {
        saveConfig(layout, option);
    })
    layout.on('tabCreated', tab => {
        var state = tab.contentItem.container.getState()
        if (state.titleClass) {
            tab.titleElement.classList.add(state.titleClass);
        }
        if (state.titleWidth) {
            tab.titleElement.style.setProperty('width', `${state.titleWidth}px`);
        }
        if (state.showClose === false) {
            tab.closeElement.classList.add('d-none');
        }
        if (state.hasTitleTemplate) {
            const gear = document.querySelector(`[data-bb-componentId="${state.id}"]`);
            if (gear) {
                tab.titleElement.append(gear);
            }

            const originalEvent = tab._dragStartEvent;
            tab._dragStartEvent = function (x, y, dragListener, item) {
                const gear = item.parentItem.element.querySelector(`[data-bb-componentId="${state.id}"]`)
                if (gear) {
                    document.body.appendChild(gear);
                }

                originalEvent(x, y, dragListener, item);
            }
        }
    });
    layout.on('stackCreated', stack => {
        const stackElement = stack.target;
        var lockElement = document.createElement('div');
        lockElement.classList = 'bb-dock-lock';
        lockElement.title = 'lock/unlock';
        lockElement.onclick = () => {
            const lock = eventsData.has(stackElement)
            if (lock) {
                unlockStack(stackElement, dock)
            }
            else {
                lockStack(stackElement, dock)
            }

            resetDockLock(dock)
            stackElement.layoutManager.emit('lockChanged')
        }
        var dropdown = stackElement.header.controlsContainerElement.querySelector('.lm_tabdropdown');
        dropdown.after(lockElement);
        stackElement.lockElement = lockElement;
    })
    layout.init()

    layout.on('tabClosed', (component, title) => {
        component.classList.add('d-none')
        el.append(component)

        saveConfig(layout, option);
        option.invokeVisibleChangedCallback(title, false)

        resetDockLock(dock)
    })
    layout.on('itemDropped', item => {
        const stack = item.parentItem
        if (eventsData.has(stack)) {
            lockTab(item.tab, eventsData)
        }
        resetDockLock(dock)
        saveConfig(layout, option);
        invoke.invokeMethodAsync(option.tabDropCallback)
    })
    layout.on('splitterDragStop', () => {
        saveConfig(layout, option);
        invoke.invokeMethodAsync(option.splitterCallback)
    })
    layout.on('lockChanged', state => {
        saveConfig(layout, option);
    })

    invoke.invokeMethodAsync(option.initializedCallback)
    dock.invokeLockAsync = state => {
        invoke.invokeMethodAsync(option.lockChangedCallback, state)
    }

    // lock stack
    const components = getAllItemsByType('component', option)
    layout.getAllContentItems().filter(i => i.isComponent).forEach(com => {
        const component = components.find(c => c.id === com.id)
        if (component && component.componentState.lock) {
            const tabs = com.parent.header.tabs
            if (tabs.find(i => !i.componentItem.container.initialState.lock) === void 0) {
                lockStack(com.parent, dock)
            }
        }
    })
}

export function update(id, option) {
    const dock = Data.get(id)

    if (dock) {
        if (dock.layoutConfig !== option.layoutConfig) {
            reset(id, option)
        }
        else if (dock.lock !== option.lock) {
            // 处理 Lock 逻辑
            dock.lock = option.lock
            lockDock(dock)
        }
        else {
            // 处理 toggle 逻辑
            toggleComponent(dock, option);
            saveConfig(dock.layout, option);
        }
    }
}

export function lock(id, lock) {
    const dock = Data.get(id)
    dock.lock = lock
    lockDock(dock)
}

export function getLayoutConfig(id) {
    let config = "";
    const dock = Data.get(id)
    if (dock) {
        const layout = dock.layout
        config = JSON.stringify(layout.saveLayout())
    }
    return config;
}

export function reset(id, option) {
    const dock = Data.get(id)
    if (dock) {
        removeConfig(option);

        const el = dock.el;
        const components = getAllItemsByType('component', option);
        components.forEach(i => {
            const item = document.getElementById(i.id);
            if (item) {
                item.classList.add("d-none");
                el.append(item);
            }
        })
        dispose(id)

        init(id, option, dock.invoke)
    }
}

export function dispose(id) {
    const dock = Data.get(id)
    Data.remove(id)

    if (dock) {
        dock.eventsData.clear();
        dock.layout.destroy();
    }
}
