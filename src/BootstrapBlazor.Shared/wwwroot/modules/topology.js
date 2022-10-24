import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"

export class Topology extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._invokerMethod = this._config.arguments[1]

        window.BlazorDemoTopology = {}
        window.BlazorDemoTopology.handler = tagName => {
            const element = document.querySelector('.topology > div')
            const topology = Topology.getInstance(element)
            if (topology) {
                topology.invoke(tagName)
            }
        }
    }

    invoke(tagName) {
        this._invoker.invokeMethodAsync(this._invokerMethod, tagName)
    }

    _execute() {
        window.topology.setOptions({hoverColor: '', hoverCursor: '', activeColor: ''});
    }

    _dispose() {
        if (window.BlazorDemoTopology) {
            delete window.BlazorDemoTopology
        }
    }
}
