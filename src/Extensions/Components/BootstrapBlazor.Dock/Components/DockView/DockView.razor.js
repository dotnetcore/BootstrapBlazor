import Data from '../../../BootstrapBlazor/modules/data.js'
import { addLink } from '../../../BootstrapBlazor/modules/utility.js'
import "../../js/golden-layout.js"

let components = [];

export function init(el, config) {
    addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-base.css");
    addLink("./_content/BootstrapBlazor.Dock/css/goldenlayout-light-theme.css")
    try {
        console.log(config)
        expandConfig(config);
        console.log(components)
        const layout = new goldenLayout.GoldenLayout(config, el);
        // 循环注册 component
        for (var i = 0; i < components.length; i++) {
            layout.registerComponent(components[i], (container, componentState) => {
            })
        }
        layout.init();
    } catch (e) {
        console.error(e)
    }
}

function expandConfig(val) {
    if (val.content == null) {
        components.push(val.componentName)
    } else {
        if (val.content) {
            for (var i = 0; i < val.content.length; i++) {
                expandConfig(val.content[i])
            }
        }
    }
}
