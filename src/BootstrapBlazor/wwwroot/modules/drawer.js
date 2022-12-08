import BlazorComponent from "./base/blazor-component.js"

export class Drawer extends BlazorComponent {
    _init() {
        this._body = document.querySelector('body')
    }

    _execute(args) {
        const open = args[0]
        if (open) {
            this._element.classList.add('is-open')
            this._body.classList.add('overflow-hidden')
        }
        else {
            if (this._element.classList.contains('is-open')) {
                this._element.classList.remove('is-open')
                this._element.classList.add('is-close')
                let handler = window.setTimeout(() => {
                    window.clearTimeout(handler)
                    handler = null
                    this._element.classList.remove('is-close')
                    this._body.classList.remove('overflow-hidden')
                }, 350)
            }
        }
    }
}
