import Data from '../../../BootstrapBlazor/modules/data.js'

export function init(el, invoker, callback) {
    const meta = {}
    Data.set(el, meta)

    window.BlazorDemoTopology = {}
    window.BlazorDemoTopology.handler = tagName => {
        const element = el.querySelector('.topology > div')
        invoker.invokeMethodAsync(callback, tagName)
    }
}

export function execute() {
    window.topology.setOptions({ hoverColor: '', hoverCursor: '', activeColor: '' });
}

export function dispose(el) {
    Data.remove(el)
}
