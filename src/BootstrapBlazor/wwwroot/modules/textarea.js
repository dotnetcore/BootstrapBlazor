import BlazorComponent from "./base/blazor-component.js"

export class Textarea extends BlazorComponent {
    _init() {
        this._prevMethod = '';
    }

    _execute(args) {
        const autoScroll = this._element.getAttribute('data-bb-scroll') === 'auto'
        let method = args[0]
        if (method === 'refresh') {
            method = this._prevMethod
        }
        let position = args[1]
        if (method === 'toTop') {
            position = 0;
        }
        if (autoScroll || method === 'toBottom') {
            position = this._element.scrollHeight
        }

        if (!isNaN(position)) {
            this._element.scrollTop = position;
        }

        if (method !== 'refresh') {
            this._prevMethod = method;
        }
    }
}
