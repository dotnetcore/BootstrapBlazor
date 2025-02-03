import Data from "../../modules/data.js"
import Typed from '../../lib/typedjs/typed.module.js'

const getOptions = (text, invoke, options, callbacks) => {
    options ??= {};

    if (text) {
        options.strings = [text];
    }
    if (options.strings === void 0) {
        options.strings = [""];
    }
    options.onComplete = function () {
        if (options.loop) {
            return;
        }
        invoke.invokeMethodAsync(callbacks.triggerComplete);
    }
    return options;
}

export function init(id, invoke, text, options, callbacks) {
    const el = document.getElementById(id);
    const typed = new Typed(el, getOptions(text, invoke, options, callbacks));
    Data.set(id, { el, invoke, callbacks, typed });
}

export function update(id, text, options) {
    const typedJs = Data.get(id);

    if (typedJs) {
        const { el, invoke, callbacks, typed } = typedJs;
        typed.destroy();

        typedJs.typed = new Typed(el, getOptions(text, invoke, options, callbacks));
    }
}

export function dispose(id) {
    const typedJs = Data.get(id);
    Data.remove(id);

    if (typedJs) {
        const { typed } = typedJs;
        typed.destroy();
    }
}
