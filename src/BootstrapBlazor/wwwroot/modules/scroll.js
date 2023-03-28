import BlazorComponent from "./base/blazor-component.js"

export class Scroll extends BlazorComponent {
    _init() {
    }

    _execute(args) {
        const method = args[0]
        const val = args[1]
        if (method === 'toBottom') {
            this._element.scrollTop = this._element.scrollHeight;
        } else if (method === 'to' && val) {
            this._element.scrollTop = val;
        } else if (method === 'toTop') {
            this._element.scrollTop = 0;
        } else if (method === 'toBottomByID' && val) {
            const element = document.getElementById(val)
            if (element) {
                this._toggleElement = document.getElementById(args[0]);
                this._toggleElement.scrollTop = txb.scrollHeight;
            }
        }
    }

    _dispose() {
    }
}
