import "../../js/golden-layout.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke, callback) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")
    const dock = { el, invoke, callback, layout: createGoldenLayout(option.content, el) }
    Data.set(id, dock)
}

export function update(id, option) {
    const dock = Data.get(id)
    console.log(option.content)

    if (dock) {
        // 处理 toogle 逻辑
        const items = getAllContentItems(option.content)
        const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);

        // gt 没有 items 有时添加
        items.forEach(v => {
            const c = comps.find(i => i.id == v.id)
            if (c === undefined) {
                dock.layout.root.contentItems[0].addItem(v, 0)
            }
        })

        // gt 有 items 没有时移除
        comps.forEach(v => {
            const c = items.find(i => i.id == v.id)
            if (c === undefined) {
                closeItem(dock.el, v)
            }
        })
    }
}

export function dispose(id) {

}

const getAllContentItems = content => {
    const items = []

    content.forEach(v => {
        if (v.type == 'component') {
            items.push(v)
        }

        if (v.content != null) {
            items.push.apply(items, getAllContentItems(v.content))
        }
    })
    return items
}

const createGoldenLayout = (content, el) => {
    const config = getConfig(content)

    const layout = new goldenLayout.GoldenLayout(config, el)
    layout.registerComponentFactoryFunction("component", (container, state) => {
        // 当从缓存拿config时, id 为没刷新前的 id，页面元素已经改变
        //const el = document.getElementById(state.id)
        //el.classList.remove('d-none')
        //container.element.append(el)
    })
    layout.init()
    layout.resizeWithContainerAutomatically = true

    layout.on("stateChanged", () => saveConfig(layout))
    layout.on("tabClosed", component => closeItem(el, component))
    return layout
}

const closeItem = (el, component) => {
    const item = document.getElementById(component.id)
    item.classList.add('d-none')
    component.remove();
    el.append(item)
}

const getConfig = content => {
    let config = null
    const localConfig = localStorage.getItem("uni_gl_layout");
    if (localConfig) {
        // 当tab全部关闭时，没有root节点
        const configItem = JSON.parse(localConfig)
        if (configItem.root) {
            config = configItem
        }
    }

    config = config || {
        dimensions: {
            borderWidth: 5,
            minItemHeight: 10,
            minItemWidth: 10,
            headerHeight: 26,
            dragProxyWidth: 300,
            dragProxyHeight: 200
        },
        content
    }
    return config
}

const saveConfig = layout => {
    localStorage.setItem("uni_gl_layout", JSON.stringify(layout.toConfig()));
}
