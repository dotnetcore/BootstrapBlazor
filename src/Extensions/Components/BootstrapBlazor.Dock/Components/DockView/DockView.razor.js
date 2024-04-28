import "../../js/golden-layout.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")

    const eventsData = new Map()
    const dock = { el, eventsData, invoke, lock: option.lock, layoutConfig: option.layoutConfig }
    Data.set(id, dock)

    option.invokeVisibleChangedCallback = (title, visible) => {
        invoke.invokeMethodAsync(option.visibleChangedCallback, title, visible)
    }

    hackGoldenLayout(dock)
    const layout = createGoldenLayout(option, el)
    dock.layout = layout
    layout.on('initialised', () => {
        saveConfig(option, layout)
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

    if (dock == null) {
        return
    }

    dock.eventsData.clear()
    dock.layout.destroy()

    if (goldenLayout.bb_docks !== void 0) {
        const index = goldenLayout.bb_docks.indexOf(dock);
        if (index > 0) {
            goldenLayout.bb_docks.splice(index, 1);
        }
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

        const header = stack.header
        header.controlsContainerElement.classList.add('bb-dock-lock')
        header.tabs.forEach(tab => {
            lockTab(tab, eventsData)
        })
    }
}

const unLockStack = (stack, dock) => {
    const eventsData = dock.eventsData

    if (eventsData.has(stack)) {
        eventsData.delete(stack)

        const header = stack.header
        header.controlsContainerElement.classList.remove('bb-dock-lock')
        header.tabs.forEach(tab => {
            unLockTab(tab, eventsData)
        })
    }
}

const resetDockLock = dock => {
    const unlocks = dock.layout.getAllContentItems().filter(com => com.isComponent && !com.container.initialState.lock)
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
        tab.componentItem.container.initialState.lock = true
    }
}

const unLockTab = (tab, eventsData) => {
    if (eventsData.has(tab)) {
        tab.enableReorder()
        tab.onCloseClick = eventsData.get(tab)
        eventsData.delete(tab)
        tab.componentItem.container.initialState.lock = false
    }
}

const toggleComponent = (dock, option) => {
    const items = getAllItemsByType('component', option);
    const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);
    const stacks = dock.layout.getAllContentItems().filter(s => s.isStack);

    // gt 没有 items 有时添加
    items.forEach(v => {
        const c = comps.find(i => i.id === v.id)
        if (c === undefined) {
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
                const stack = stacks.find(s => s.id == v.parent.id) || stacks.pop();
                if (stack) {
                    stack.addItem(v);
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
        if (c === undefined) {
            closeItem(dock.el, v)
        }
        else if (v.title !== c.title) {
            // 更新 Title
            v.setTitle(c.title)
        }
    })

    saveConfig(option, dock.layout)
}

const getAllItemsByType = (type, parent) => {
    const items = []

    parent.content.forEach(v => {
        if (v.type === type) {
            v.parent = parent;
            items.push(v)
        }

        if (v.content != null) {
            items.push.apply(items, getAllItemsByType(type, v))
        }
    })
    return items
}

const createGoldenLayout = (option, el) => {
    const config = getConfig(option)

    if (option.lock) {
        getAllItemsByType('component', option).forEach(i => {
            i.componentState.lock = option.lock
        })
    }

    const layout = new goldenLayout.GoldenLayout(config, el)

    layout.registerComponentFactoryFunction("component", (container, state) => {
        const el = document.getElementById(state.id)
        if (el) {
            el.classList.remove('d-none')
            if (state.class) {
                container.element.classList.add(state.class)
            }
            container.element.append(el)
        }
    })
    layout.resizeWithContainerAutomatically = true
    return layout
}

const closeItem = (el, component) => {
    const item = document.getElementById(component.id)
    if (item) {
        item.classList.add('d-none')
        el.append(item)
    }
    const parent = component.parent
    parent.removeChild(component)

}

const getConfig = option => {
    option = {
        enableLocalStorage: false,
        layoutConfig: null,
        name: 'default',
        ...option
    }

    let config = null
    let layoutConfig = option.layoutConfig;
    if (layoutConfig === null && option.enableLocalStorage) {
        layoutConfig = localStorage.getItem(getLocalStorageKey(option));
    }
    if (layoutConfig) {
        // 当tab全部关闭时，没有root节点
        const configItem = JSON.parse(layoutConfig)
        if (configItem.root) {
            config = configItem
            resetComponentId(config, option)
        }
    }

    return {
        ...(config || { content: [] }),
        ...{
            dimensions: {
                borderWidth: 5,
                minItemHeight: 10,
                minItemWidth: 10,
                headerHeight: 25
            },
            labels: {
                close: 'close',
                maximise: 'maximise',
                minimise: 'minimise',
                popout: 'lock/unlock'
            }
        },
        ...option
    }
}

const getLocalStorageKey = option => {
    return `${option.prefix}-${option.version}`
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
        localStorage.setItem(getLocalStorageKey(option), JSON.stringify(layout.saveLayout()));
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

const resetComponentId = (config, option) => {
    // 本地配置
    const localComponents = getAllItemsByType('component', config.root)
    // 服务器端配置
    const serverItems = getAllItemsByType('component', option)
    localComponents.forEach(com => {
        const item = serverItems.find(i => i.componentState.key === com.componentState.key)
        if (item) {
            com.id = item.id
            com.title = item.title
            com.componentState = item.componentState
            com.componentState.lock = com.componentState.lock || item.componentState.lock
        }
        else {
            // 本地存储中有，配置中没有，需要显示这个组件，通过 key 来定位新 Component
            const newEl = document.querySelector(`[data-bb-key='${com.componentState.key}']`) || document.querySelector(`[data-bb-title='${com.componentState.key}']`)
            if (newEl) {
                com.id = newEl.getAttribute('id')
                com.title = newEl.getAttribute('data-bb-title')
                com.componentState.id = com.id

                option.invokeVisibleChangedCallback(com.title, true)
            }
            else {
                removeContent(config.root.content, com)

                // remove empty stack
                config.root.content.filter(v => v.content.length === 0).forEach(v => {
                    const index = config.root.content.indexOf(v)
                    if (index > -1) {
                        config.root.content.splice(index, 1)
                    }
                })
            }
        }
    })

    serverItems.forEach(item => {
        // 更新服务器端组件可见状态
        const com = localComponents.find(i => i.componentState.key === item.componentState.key)
        option.invokeVisibleChangedCallback(item.title, com !== void 0)

        // 本地存储中没有，配置中有
        if (com === void 0) {
            if (config.root.content.length > 0) {
                config.root.content.push({
                    type: 'stack',
                    content: [{
                        type: 'component',
                        content: [],
                        title: item.title,
                        id: item.id,
                        componentType: 'component',
                        componentState: item.componentState
                    }]
                });
            }
        }
    })

    // set stack headers
    // 本地配置
    const localStacks = getAllItemsByType('stack', config.root)
    // 服务器端配置
    const serverStacks = getAllItemsByType('stack', option)

    localStacks.forEach(s => {
        const stack = {
            hasHeaders: true,
            ...findStack(s, serverStacks),
        };
        s.header = {
            ...s.header,
            show: stack.hasHeaders
        }
    });
}

const findStack = (stack, stacks) => {
    let find = null;
    for (let com of stack.content) {
        find = stacks.find(s => s.content.find(c => c.componentState.key === com.componentState.key));
        if (find) {
            break;
        }
    }
    return find;
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

const hackGoldenLayout = dock => {
    if (goldenLayout.bb_docks === void 0) {
        goldenLayout.bb_docks = [];
    }
    goldenLayout.bb_docks.push(dock);

    if (!goldenLayout.isHack) {
        goldenLayout.isHack = true

        // hack Tab
        goldenLayout.Tab.prototype.onCloseClick = function () {
            const component = document.getElementById(this.componentItem.id)
            const title = this.componentItem.title

            this.notifyClose();
            this._layoutManager.emit('tabClosed', component, title)
        }

        const originSetTitle = goldenLayout.Tab.prototype.setTitle
        goldenLayout.Tab.prototype.setTitle = function (title) {
            originSetTitle.call(this, title)
            const showClose = this.contentItem.container.initialState.showClose
            if (!showClose) {
                this.closeElement.classList.add('d-none')
            }
        }

        // hack RowOrColumn
        const originSplitterDragStop = goldenLayout.RowOrColumn.prototype.onSplitterDragStop
        goldenLayout.RowOrColumn.prototype.onSplitterDragStop = function (splitter) {
            originSplitterDragStop.call(this, splitter)
            this.layoutManager.emit('splitterDragStop')
        }

        // hack Header
        goldenLayout.Header.prototype.handleButtonPopoutEvent = function () {
            // find own dock
            const dock = goldenLayout.bb_docks.find(i => i.layout === this.layoutManager);
            const eventsData = dock.eventsData

            const stack = this.parent
            const lock = eventsData.has(stack)
            if (lock) {
                unLockStack(stack, dock)
            }
            else {
                lockStack(stack, dock)
            }

            resetDockLock(dock)
            this.layoutManager.emit('lockChanged')
        }

        const originprocessTabDropdownActiveChanged = goldenLayout.Header.prototype.processTabDropdownActiveChanged
        goldenLayout.Header.prototype.processTabDropdownActiveChanged = function () {
            originprocessTabDropdownActiveChanged.call(this)

            this._closeButton.onClick = function (ev) {
                // find own dock
                const dock = goldenLayout.bb_docks.find(i => i.layout === this._header.layoutManager);
                const eventsData = dock.eventsData

                const tabs = this._header.tabs.map(tab => {
                    return { element: tab.componentItem.element, title: tab.componentItem.title }
                })
                if (!eventsData.has(this._header.parent)) {
                    this._pushEvent(ev)

                    const handler = setTimeout(() => {
                        clearTimeout(handler)
                        tabs.forEach(tab => {
                            this._header.layoutManager.emit('tabClosed', tab.element, tab.title)
                        })
                    }, 100)
                }
            }
        }
    }
}
