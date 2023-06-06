import "../../js/golden-layout.min.js"
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, option, invoke, callback) {
    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-base.css")
    await addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-light-theme.css")

    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const config = { content: [option] }
    const dock = { el, config, invoke, callback }
    const layout = new goldenLayout.GoldenLayout(config, el)
    layout.registerComponent("component", (container, state) => {
        container.getElement().innerHTMl = `<h2>test1</h2>`
    })
    dock.layout = layout
    layout.init()
}

export function dispose(id) {

}
