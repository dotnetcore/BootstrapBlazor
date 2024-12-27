import EventHandler from "./event-handler.js"

export default {
    composition(el, callback) {
        if (el) {
            let isComposing = false
            EventHandler.on(el, 'compositionstart', e => {
                isComposing = true;
            });
            EventHandler.on(el, 'compositionend', e => {
                isComposing = false;
                callback(el.value);
            });
            EventHandler.on(el, 'input', e => {
                if (!isComposing) {
                    callback(el.value);
                }
            });
        }
    },

    dispose(el) {
        if (el) {
            EventHandler.off(el, 'compositionstart')
            EventHandler.off(el, 'compositionend')
            EventHandler.off(el, 'input')
        }
    }
}
