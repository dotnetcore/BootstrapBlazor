import "../../js/golden-layout.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")

    option.invokeVisibleChanged = (title, visible) => {
        invoke.invokeMethodAsync(option.visibleChangedCallback, title, visible)
    }
    option.invokeInitializedCallback = layout => {
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.initializedCallback)
    }
    option.invokeTabDropCallback = layout => {
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.tabDropCallback)
    }
    option.invokeSplitterStopCallback = layout => {
        saveConfig(option, layout)
        invoke.invokeMethodAsync(option.splitterStopCallback)
    }

    const layout = createGoldenLayout(option, el)
    const dock = { el, option, layout }
    Data.set(id, dock)

    layout.on('dockTabClosed', (component, title) => {
        component.classList.add('d-none')
        el.append(component)

        option.invokeVisibleChanged(title, false)
    })
    layout.on('dockTabDrop', () => {
        option.invokeTabDropCallback(layout)
    })
    layout.on('dockSplitterDragStop', () => {
        option.invokeSplitterStopCallback(layout)
    })
}

export function update(id, option) {
    const dock = Data.get(id)

    if (dock) {
        // 处理 toogle 逻辑
        const items = getAllContentItems(option.content)
        const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);

        // gt 没有 items 有时添加
        items.forEach(v => {
            const c = comps.find(i => i.id === v.id)
            if (c === undefined) {
                if (dock.layout.root.contentItems.length === 0) {
                    const compotentItem = dock.layout.createAndInitContentItem({ type: option.content[0].type, content: [] }, dock.layout.root)
                    dock.layout.root.addChild(compotentItem)
                }

                dock.layout.root.contentItems[0].addItem(v, 0)
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
    }
    saveConfig(option, dock.layout)
}

export function dispose(id) {
    const dock = Data.get(id)
    Data.remove(id)

    if (dock == null) {
        return
    }

    saveConfig(dock.option, dock.layout)
    dock.layout.destroy()
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
    hackGoldenLayout(option, layout)

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
    layout.init()
    hackGoldenLayoutOnDrop(layout)

    layout.resizeWithContainerAutomatically = true
    return layout
}

const closeItem = (el, component) => {
    const item = document.getElementById(component.id)
    item.classList.add('d-none')
    component.remove();
    el.append(item)
}

const getConfig = option => {
    let config = null
    option = {
        enableLocalStorage: false,
        name: 'default',
        ...option
    }
    if (option.enableLocalStorage) {
        const localConfig = localStorage.getItem(`uni_gl_layout_${option.name}_${option.version}`);
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
        ...(config || { content: option.content }),
        ...{
            dimensions: {
                borderWidth: 5,
                minItemHeight: 10,
                minItemWidth: 10,
                headerHeight: 26
            }
        }
    }
}

const saveConfig = (option, layout) => {
    option = {
        enableLocalStorage: false,
        ...option
    }
    if (option.enableLocalStorage) {
        removeConfig(option)
        localStorage.setItem(`uni_gl_layout_${option.name}_${option.version}`, JSON.stringify(layout.saveLayout()));
    }
}

const removeConfig = option => {
    for (let index = localStorage.length; index > 0; index--) {
        const k = localStorage.key(index - 1);
        if (k.indexOf(`uni_gl_layout_${option.name}_`) > -1) {
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
        const item = items.find(i => i.id === com.id)
        if (item) {
            com.componentState = item.componentState
            com.title = item.title
        }
        else {
            // 本地存储中有，配置中没有，需要显示这个组件，由于 ID 是变化的暂时通过 title 来定位新 Component
            const newEl = document.querySelector(`[data-bb-title='${com.title}']`)
            if (newEl) {
                option.invokeVisibleChanged(com.componentState.title, true)
                com.id = newEl.getAttribute('id')
                com.componentState.id = com.id
            }
            else {
                const component = items.find(i => i.title === com.componentState.title)
                if (component) {
                    com.id = component.id
                    com.title = component.title
                    com.componentState.id = component.id
                }
                else {
                    removeContent(config.root.content, com)
                }

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

    items.forEach(com => {
        // 更新服务器端组件可见状态
        const item = components.find(i => i.id === com.id)
        if (item === undefined) {
            const component = components.find(i => i.componentState.title === com.title)
            if (!component) {
                option.invokeVisibleChanged(com.title, false)
            }
        }
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

const hackGoldenLayout = (option, layout) => {
    if (goldenLayout.isHack === undefined) {
        goldenLayout.isHack = true

        goldenLayout.Tab.prototype.onCloseClick = function () {
            const component = document.getElementById(this._componentItem.id)
            const title = this._componentItem.title

            this.notifyClose();
            this._layoutManager.emit('dockTabClosed', component, title)
        }

        const originSplitterDragStop = goldenLayout.RowOrColumn.prototype.onSplitterDragStop;
        goldenLayout.RowOrColumn.prototype.onSplitterDragStop = function (splitter) {
            originSplitterDragStop.call(this, splitter)
            layout.emit('dockSplitterDragStop')
        }

        const originStackDrop = goldenLayout.Stack.prototype.onDrop;
        goldenLayout.Stack.prototype.onDrop = function (contentItem, area) {
            originStackDrop.call(this, contentItem, area);
            layout.emit('dockTabDrop')
        }

        const originSetTitle = goldenLayout.Tab.prototype.setTitle
        goldenLayout.Tab.prototype.setTitle = function (title) {
            originSetTitle.call(this, title)
            const showClose = this.contentItem.container.initialState.showClose
            if (!showClose) {
                this.closeElement.classList.add('d-none')
            }
        }

        const originBindEvents = goldenLayout.GoldenLayout.prototype.bindEvents
        goldenLayout.GoldenLayout.prototype.bindEvents = function () {
            layout.on("initialised", () => {
                option.invokeInitializedCallback(layout)
            })
            originBindEvents.call(this)
        }
    }
}

const hackGoldenLayoutOnDrop = layout => {
    const originRootDrop = layout.root.onDrop
    layout.root.onDrop = function (contentItem, area) {
        originRootDrop.call(this, contentItem, area)
        layout.emit('dockTabDrop')
    }
}
