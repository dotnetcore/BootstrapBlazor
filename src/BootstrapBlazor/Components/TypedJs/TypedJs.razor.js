import Data from "../../modules/data.js"
import Typed from '../../lib/typedjs/typed.module.js'

export function init(id, invoke, text, options, callbacks) {
    const el = document.getElementById(id);
    options ??= {};
    if (text) {
        options.strings = [text];
    }
    if (options.strings === void 0) {
        options.strings = [""];
    }

    const typed = new Typed(el, options);
    Data.set(id, { el, invoke, callbacks, typed });
}

export function update(id, text, options) {
    const typedJs = Data.get(id);

    if (typedJs) {
        const { el, typed } = typedJs;
        typed.destroy();

        if (text) {
            options.strings = [text];
        }
        if (options.strings === void 0) {
            options.strings = [""];
        }
        typedJs.typed = new Typed(el, options);
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
