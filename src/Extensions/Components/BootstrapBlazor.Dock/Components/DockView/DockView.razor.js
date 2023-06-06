import "../../js/golden-layout.min.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke, callback) {
    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-bb.css")

    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const config = {
        dimensions: {
            borderWidth: 5,
            minItemHeight: 10,
            minItemWidth: 10,
            headerHeight: 26,
            dragProxyWidth: 300,
            dragProxyHeight: 200
        },
        content: [option]
    }
    const dock = { el, config, invoke, callback }
    const layout = new goldenLayout.GoldenLayout(config, el)
    layout.registerComponentFactoryFunction("component", (container, state) => {
        const el = document.getElementById(state.id)
        container.element.append(el)
    })
    dock.layout = layout
    layout.init()
    layout.resizeWithContainerAutomatically = true
}

export function dispose(id) {

}
