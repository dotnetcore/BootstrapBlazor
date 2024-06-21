import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import { createDock, getAllItemsByType } from "../../js/golden-layout-extensions.js"
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
        saveConfig(option, layout)
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
                unLockStack(stackElement, dock)
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

        saveConfig(option, layout)
        option.invokeVisibleChangedCallback(title, false)

        resetDockLock(dock)
    })
    layout.on('itemDropped', item => {
        const stack = item.parentItem
        if (eventsData.has(stack)) {
            lockTab(item.tab, eventsData)
        }
        resetDockLock(dock)
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.tabDropCallback)
    })
    layout.on('splitterDragStop', () => {
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.splitterCallback)
    })
    layout.on('lockChanged', state => {
        saveConfig(option, layout)
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
            toggleComponent(dock, option)
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

const lockDock = dock => {
    const stacks = dock.layout.getAllStacks()
    stacks.forEach(stack => {
        if (dock.lock) {
            lockStack(stack, dock)
        }
        else {
            unLockStack(stack, dock)
        }
    })
    dock.layout.emit('lockChanged')
}

const lockStack = (stack, dock) => {
    const eventsData = dock.eventsData

    if (!eventsData.has(stack)) {
        eventsData.set(stack, stack)

        stack.lockElement.classList.add('lock')
        stack.header.tabs.forEach(tab => {
            lockTab(tab, eventsData)
        })
    }
}

const unLockStack = (stack, dock) => {
    const eventsData = dock.eventsData

    if (eventsData.has(stack)) {
        eventsData.delete(stack)

        stack.lockElement.classList.remove('lock')
        stack.header.tabs.forEach(tab => {
            unLockTab(tab, eventsData)
        })
    }
}

const resetDockLock = dock => {
    const unlocks = dock.layout.getAllContentItems().filter(com => com.isComponent && !com.container.getState().lock)
    const lock = unlocks.length === 0
    if (dock.lock !== lock) {
        dock.lock = lock
        dock.invokeLockAsync(lock)
    }
}

const lockTab = (tab, eventsData) => {
    if (!eventsData.has(tab)) {
        tab.disableReorder()
        tab.onCloseClick = () => { }
        eventsData.set(tab, tab.onCloseClick)
        tab.componentItem.container.getState().lock = true
    }
}

const unLockTab = (tab, eventsData) => {
    if (eventsData.has(tab)) {
        tab.enableReorder()
        tab.onCloseClick = eventsData.get(tab)
        eventsData.delete(tab)
        tab.componentItem.container.getState().lock = false
    }
}

const toggleComponent = (dock, option) => {
    const items = getAllItemsByType('component', option);
    const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);
    const stacks = dock.layout.getAllContentItems().filter(s => s.isStack);

    // gt 没有 items 有时添加
    items.forEach(v => {
        const c = comps.find(i => i.id === v.id)
        if (c === void 0) {
            if (dock.layout.root.contentItems.length === 0) {
                const componentItem = dock.layout.createAndInitContentItem({ type: option.content[0].type, content: [] }, dock.layout.root)
                dock.layout.root.addChild(componentItem)
            }
            if (dock.layout.root.contentItems[0].isStack) {
                const typeConfig = goldenLayout.ResolvedItemConfig.createDefault(option.content[0].type)
                const rowOrColumn = dock.layout.root.layoutManager.createContentItem(typeConfig, dock.layout.root)
                const stack = dock.layout.root.contentItems[0]
                dock.layout.root.replaceChild(stack, rowOrColumn)
                rowOrColumn.addChild(stack)
                rowOrColumn.addItem(v)
                rowOrColumn.updateSize()
            }
            else {
                const stack = stacks.find(s => s.id == v.parent.id);
                if (stack) {
                    stack.addItem(v);
                }
                else if (v.parent.type === 'stack' && stacks.length > 0) {
                    stacks.pop().addItem(v);
                }
                else {
                    dock.layout.root.contentItems[0].addItem(v);
                }
            }

            if (v.componentState.lock) {
                const component = dock.layout.getAllContentItems().find(i => i.isComponent && i.id === v.id)
                lockStack(component.parentItem, dock)
            }
        }
    })

    // gt 有 items 没有时移除
    comps.forEach(v => {
        const c = items.find(i => i.id === v.id)
        if (c === void 0) {
            closeItem(dock, v)
        }
        else if (v.title !== c.title) {
            // 更新 Title
            v.setTitle(c.title)
        }
    })

    saveConfig(option, dock.layout)
}

const closeItem = (dock, component) => {
    const { template } = dock;
    const item = document.getElementById(component.id)
    if (item) {
        template.append(item)
    }
    const parent = component.parent
    parent.removeChild(component)

}

const indexOfKey = (key, option) => {
    return key.indexOf(`${option.prefix}-`) > -1
}

const saveConfig = (option, layout) => {
    option = {
        enableLocalStorage: false,
        ...option
    }
    if (option.enableLocalStorage) {
        removeConfig(option)
        localStorage.setItem(option.localStorageKey, JSON.stringify(layout.saveLayout()));
    }
}

const removeConfig = option => {
    for (let index = localStorage.length; index > 0; index--) {
        const k = localStorage.key(index - 1);
        if (indexOfKey(k, option)) {
            localStorage.removeItem(k);
        }
    }
}

const removeContent = (content, item) => {
    content.forEach(v => {
        if (Array.isArray(v.content)) {
            const index = v.content.indexOf(item)
            if (index > -1) {
                v.content.splice(index, 1)
            }
            else {
                removeContent(v.content, item)
            }
        }
    })
}
