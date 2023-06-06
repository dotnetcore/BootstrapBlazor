import "../../js/golden-layout.min.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, config, invoke, callback) {
    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-base.css")
    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-light-theme.css")

    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const dock = { el, config, invoke, callback }

    const components = []
    expandConfig(dock)

    const layout = new goldenLayout.GoldenLayout(config, el)

    // 循环注册 component
    for (var i = 0; i < components.length; i++) {
        layout.registerComponent(components[i], (container, componentState) => {
        })
    }
    layout.init()
    dock.layout = layout
}

export function dispose(id) {

}

function expandConfig(val, components) {
    if (val.content == null) {
        components.push(val.componentName)
    }
    else {
        if (val.content) {
            for (var i = 0; i < val.content.length; i++) {
                expandConfig(val.content[i], components)
            }
        }
    }
}
