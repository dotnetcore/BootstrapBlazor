import BaseComponent from "./base-component.js"
import { getElementById } from "./index.js"

export default class BlazorComponent extends BaseComponent {
    static dispose(element) {
        element = getElementById(element)
        super.dispose(element)
    }

    static init(selector) {
        const element = getElementById(selector)
        if (element) {
            new this(element, { arguments: [].slice.call(arguments, 1) })
        }
        else {
            console.warn(`the elment ${selector} of ${this.NAME} not found`)
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
