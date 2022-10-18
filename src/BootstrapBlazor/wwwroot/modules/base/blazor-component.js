import BaseComponent from "./base-component.js"
import { getElement } from "./index.js"

const getElementById = object => {
    if (typeof object === 'string' && object.length > 0) {
        object = `#${object}`
    }

    return getElement(object);
}

export default class BlazorComponent extends BaseComponent {
    _execute(args) {

    }

    _hackPopover() {
        if (this._popover) {
            this._popover._isWithContent = () => true

            const getTipElement = this._popover._getTipElement
            let fn = tip => {
                tip.classList.add(this._config.class)
            }
            this._popover._getTipElement = () => {
                let tip = getTipElement.call(this._popover)
                fn(tip)
                return tip
            }
        }
    }

    static dispose(element) {
        element = getElementById(element)
        super.dispose(element)
    }

    static init(element) {
        element = getElementById(element)
        if (element) {
            new this(element, { arguments: [].slice.call(arguments, 1) })
        }
    }

    static execute(element) {
        element = getElementById(element)
        if (element) {
            const instance = this.getOrCreateInstance(element)
            instance._execute([].slice.call(arguments, 1))
        }
    }
}
