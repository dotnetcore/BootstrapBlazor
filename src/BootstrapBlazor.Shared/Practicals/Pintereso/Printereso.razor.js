import EventHandler from "../../../BootstrapBlazor/modules/event-handler.js"

const scrollHandler = [];

export function init(invoke, method, el) {
    scrollHandler = () => {
        var b_height = document.body.clientHeight;
        if (parseInt(el.pageYOffset + el.innerHeight) > b_height - 10) {
            invoke.invokeMethodAsync(method);
        }
    }
    EventHandler.on(window, 'scroll', scrollHandler)
}

export function dispose(el) {
    EventHandler.off(window, 'scroll', scrollHandler)
}
