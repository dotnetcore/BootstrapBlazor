import BlazorComponent from "./base/blazor-component.js"

export class Scroll extends BlazorComponent {
    _init() {
    }

    _execute(args) {
        const scroll = this._element.getAttribute('data-bb-scroll') === 'auto';
        const method = args[0]
        const val = args[1]
        if (method === 'to' && val) {
            this._element.scrollTop = val;
        } else if (method === 'toTop') {
            this._element.scrollTop = 0;
        } else if (method === 'toBottomByID' && val) {
            const element = document.getElementById(val)
            if (element) {
                this._toggleElement = document.getElementById(args[0]);
                this._toggleElement.scrollTop = txb.scrollHeight;
            }
        } else if (scroll || method === 'toBottom') {
            this._element.scrollTop = this._element.scrollHeight;
        }
    }

    _dispose() {
    }
}
