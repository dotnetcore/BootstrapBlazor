import "../../js/golden-layout.min.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke, callback) {
    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")

    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    console.log(option.content)

    //option = {
    //    type: 'row',
    //    content: [
    //        {
    //            type: 'component',
    //            componentName: 'component',
    //            componentState: { text: 'Component 1' }
    //        },
    //        {
    //            type: 'component',
    //            componentName: 'component',
    //            componentState: { text: 'Component 2' }
    //        },
    //        {
    //            type: 'component',
    //            componentName: 'component',
    //            componentState: { text: 'Component 3' }
    //        }
    //    ]
    //}

    const config = {
        dimensions: {
            borderWidth: 5,
            minItemHeight: 10,
            minItemWidth: 10,
            headerHeight: 26,
            dragProxyWidth: 300,
            dragProxyHeight: 200
        },
        content: option.content
    }

    const layout = new goldenLayout.GoldenLayout(config, el)
    layout.registerComponentFactoryFunction("component", (container, state) => {
        const el = document.getElementById(state.id)
        el.classList.remove('d-none')
        container.element.append(el)
    })
    layout.init()
    layout.resizeWithContainerAutomatically = true

    const dock = { el, config, invoke, callback, layout }
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
                const item = document.getElementById(v.id)
                item.classList.add('d-none')
                v.remove();
                dock.el.append(item)
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
