﻿import "../../js/golden-layout.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke, callback) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")
    const layout = createGoldenLayout(option, el)
    const dock = { el, option, invoke, callback, layout }
    Data.set(id, dock)

    layout.on('tabClosed', componentItem => {
        if (componentItem) {
            componentItem.Element.classList.add('d-none')
            el.append(componentItem.Element)
            saveConfig(option, layout)

            invoke.invokeMethodAsync(callback, componentItem.title)
        }
    })
}

export function update(id, option) {
    const dock = Data.get(id)

    if (dock) {
        // 处理 toogle 逻辑
        const items = getAllContentItems(option.option.content)
        const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);

        // gt 没有 items 有时添加
        items.forEach(v => {
            const c = comps.find(i => i.id === v.id)
            if (c === undefined) {
                dock.layout.addItem(v, 0)
            }
        })

        // gt 有 items 没有时移除
        comps.forEach(v => {
            const c = items.find(i => i.id === v.id)
            if (c === undefined) {
                closeItem(dock.el, v)
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
    layout.registerComponentFactoryFunction("component", (container, state) => {
        const el = document.getElementById(state.id)
        if (el) {
            el.classList.remove('d-none')
            container.element.append(el)
        }
    })
    layout.init()
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
        option: {
            content: []
        },
        ...option
    }
    if (option.enableLocalStorage) {
        const localConfig = localStorage.getItem(`uni_gl_layout_${option.name}_${option.version}`);
        if (localConfig) {
            // 当tab全部关闭时，没有root节点
            const configItem = JSON.parse(localConfig)
            if (configItem.root) {
                config = configItem
                resetComponentId(config, option.option.content)
            }
        }
    }

    return {
        ...(config || { content: option.option.content }),
        ...{
            dimensions: {
                borderWidth: 5,
                minItemHeight: 10,
                minItemWidth: 10,
                headerHeight: 26,
                dragProxyWidth: 300,
                dragProxyHeight: 200
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
        if (k.indexOf(`uni_gl_layout_${option.name}`) > -1) {
            localStorage.removeItem(k);
        }
    }
}

const resetComponentId = (config, content) => {
    const components = getAllContentItems(config.root.content)
    const items = getAllContentItems(content)
    components.forEach((com, index) => {
        const item = items.find(i => i.id === com.id)
        if (item) {
            // 原有 Component
            com.componentState = item.componentState
            com.title = item.title
        }
        else {
            // 新的项目，由于 ID 是变化的暂时通过 title 来定位新 Component
            const newEl = document.querySelector(`[data-bb-title='${com.title}']`)
            if (newEl) {
                com.id = newEl.getAttribute('id')
                com.componentState.id = com.id
            }
        }
    })
}
