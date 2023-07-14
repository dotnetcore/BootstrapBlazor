import "../../js/golden-layout.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")

    option.invokeVisibleChangedCallback = (title, visible) => {
        invoke.invokeMethodAsync(option.visibleChangedCallback, title, visible)
    }

    hackGoldenLayout()
    const layout = createGoldenLayout(option, el)
    layout.on('initialised', () => {
        saveConfig(option, layout)
    })
    layout.init()

    const eventsData = new Map()
    const components = getAllContentItems(option.content)
    layout.getAllContentItems().filter(i => i.isComponent).forEach(com => {
        const component = components.find(c => c.id === com.id)
        if (component && component.componentState.lock) {
            lockTab(com.tab, eventsData)
        }
    })

    layout.on('tabClosed', (component, title) => {
        component.classList.add('d-none')
        el.append(component)

        saveConfig(option, layout)
        option.invokeVisibleChangedCallback(title, false)
    })
    layout.on('itemDropped', () => {
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.tabDropCallback)
    })
    layout.on('splitterDragStop', () => {
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.splitterCallback)
    })
    invoke.invokeMethodAsync(option.initializedCallback)

    const dock = { el, layout, lock: option.lock, eventsData }
    Data.set(id, dock)
}

export function update(id, option) {
    const dock = Data.get(id)

    if (dock) {
        if (dock.lock !== option.lock) {
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

export function dispose(id) {
    const dock = Data.get(id)
    Data.remove(id)

    if (dock == null) {
        return
    }

    dock.eventsData.clear()
    dock.layout.destroy()
}

const lockDock = dock => {
    const lock = dock.lock
    const stacks = dock.layout.getAllStacks()
    dock.eventsData = dock.eventsData || new Map()
    stacks.forEach(stack => {
        if (lock) {
            lockStack(stack, dock.eventsData)
        }
        else {
            unLockStack(stack, dock.eventsData)
        }
    })
}

const lockStack = (stack, eventsData) => {
    stack.header.tabs.forEach(tab => {
        lockTab(tab, eventsData)
    })
}

const unLockStack = (stack, eventsData) => {
    stack.header.tabs.forEach(tab => {
        tab.enableReorder()
        unLockTab(tab, eventsData)
    })
}

const lockTab = (tab, eventsData) => {
    if (!eventsData.has(tab)) {
        tab.disableReorder()
        eventsData.set(tab, tab.onCloseClick)
        tab.element.classList.add('bb-dock-tab-lock')
        tab.onCloseClick = () => {
            tab.enableReorder()
            unLockTab(tab, eventsData)
        }
    }
}

const unLockTab = (tab, eventsData) => {
    if (eventsData.has(tab)) {
        tab.element.classList.remove('bb-dock-tab-lock')
        tab.onCloseClick = eventsData.get(tab)
        eventsData.delete(tab)
    }
}

const toggleComponent = (dock, option) => {
    const items = getAllContentItems(option.content)
    const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);

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
                dock.layout.root.contentItems[0].addItem(v)
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

const getAllContentItems = content => {
    const items = []

    content.forEach(v => {
        if (v.type === 'component') {
            items.push(v)
        }

        if (v.content != null) {
            items.push.apply(items, getAllContentItems(v.content))
        }
    })
    return items
}

const createGoldenLayout = (option, el) => {
    const config = getConfig(option)

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
    let config = null
    option = {
        enableLocalStorage: false,
        name: 'default',
        ...option
    }
    if (option.enableLocalStorage) {
        const localConfig = localStorage.getItem(getLocalStorageKey(option));
        if (localConfig) {
            // 当tab全部关闭时，没有root节点
            const configItem = JSON.parse(localConfig)
            if (configItem.root) {
                config = configItem
                resetComponentId(config, option)
            }
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
    const components = getAllContentItems(config.root.content)
    // 服务器端配置
    const items = getAllContentItems(option.content)
    components.forEach(com => {
        const item = items.find(i => i.key === com.componentState.key)
        if (item) {
            com.componentState = item.componentState
            com.title = item.title
            com.id = item.id
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

    items.forEach(item => {
        // 更新服务器端组件可见状态
        const com = components.find(i => i.componentState.key === item.key)
        option.invokeVisibleChangedCallback(item.title, com !== undefined)
    })
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

const hackGoldenLayout = () => {
    if (!goldenLayout.isHack) {
        goldenLayout.isHack = true
        goldenLayout.Tab.prototype.onCloseClick = function () {
            const component = document.getElementById(this._componentItem.id)
            const title = this._componentItem.title

            this.notifyClose();
            this._layoutManager.emit('tabClosed', component, title)
        }

        const originSplitterDragStop = goldenLayout.RowOrColumn.prototype.onSplitterDragStop;
        goldenLayout.RowOrColumn.prototype.onSplitterDragStop = function (splitter) {
            originSplitterDragStop.call(this, splitter)
            this.layoutManager.emit('splitterDragStop')
        }

        const originSetTitle = goldenLayout.Tab.prototype.setTitle
        goldenLayout.Tab.prototype.setTitle = function (title) {
            originSetTitle.call(this, title)
            const showClose = this.contentItem.container.initialState.showClose
            if (!showClose) {
                this.closeElement.classList.add('d-none')
            }
        }
    }
}
