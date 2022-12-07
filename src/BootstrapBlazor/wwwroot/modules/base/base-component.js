import Data from './data.js'
import EventHandler from "./event-handler.js"
import SimpleComponent from "./simple-component.js"
import { getElement } from "./index.js"

/**
 * Class definition
 */

export default class BaseComponent extends SimpleComponent {
    constructor(element, config) {
        element = getElement(element)
        if (!element) {
            return
        }

        super(element, config)
    }

    // Public
    dispose() {
        EventHandler.off(this._element, this.constructor.EVENT_KEY)

        super.dispose()
    }

    // Static
    static getInstance(element) {
        return Data.get(getElement(element), this.DATA_KEY)
    }

    static get EVENT_KEY() {
        return `.${this.DATA_KEY}`
    }

    static eventName(name) {
        return `${name}${this.EVENT_KEY}`
    }
}
