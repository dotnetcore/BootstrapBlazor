import BaseComponent from "./base-component.js"
import { getElement } from "./index.js"

class BlazorComponent extends BaseComponent {
    constructor(element, config) {
        super(element, config)

        this._init()
    }

    _init() {
    }

    _dispose() {
    }

    dispose() {
        this._dispose()
        super.dispose()
    }

    static dispose(element) {
        const component = this.getInstance(element)
        if (component) {
            component.dispose()
        }
    }

    static init(element) {
        element = getElement(element)
        if (element) {
            this.getOrCreateInstance(element)
        }
    }

    static get NAME() {
        return this.name
    }
}

export default BlazorComponent
