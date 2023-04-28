import EventHandler from "./event-handler.js"

export default {
    composition(el, invoke, callback) {
        var isInputZh = false;
        EventHandler.on(el, 'compositionstart', e => {
            isInputZh = true;
        });
        EventHandler.on(el, 'compositionend', e => {
            isInputZh = false;
        });
        EventHandler.on(el, 'input', e => {
            if (isInputZh) {
                e.stopPropagation();
                e.preventDefault();
                const handler = setTimeout(() => {
                    if (!isInputZh) {
                        clearTimeout(handler)
                        invoke.invokeMethodAsync(callback, el.value);
                    }
                }, 15);
            }
        });
    },

    dispose(el) {
        EventHandler.off(el, 'compositionstart')
        EventHandler.off(el, 'compositionend')
        EventHandler.off(el, 'input')
    }
}
