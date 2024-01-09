export function init(invoke, callback) {
    window.BlazorDemoTopology = {
        handler: tagName => {
            invoke.invokeMethodAsync(callback, tagName)
        }
    }
}

export function execute() {
    window.meta2d.setOptions({ hoverColor: '', hoverCursor: '', activeColor: '' });
}
