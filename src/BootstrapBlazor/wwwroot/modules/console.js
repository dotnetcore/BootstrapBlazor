import BlazorComponent from "./base/blazor-component.js"
import { getHeight } from "./base/utility.js"

export class Console extends BlazorComponent {
    _init() {
        this._body = this._element.querySelector('.card-body')
        this._window = this._element.querySelector('.console-window')
    }

    _execute() {
        const scroll = this._element.getAttribute('data-bb-scroll') === 'auto'
        if (scroll) {
            this._body.scrollTo(0, getHeight(this._window))
        }
    }
}
