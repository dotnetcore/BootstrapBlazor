import BlazorComponent from "./base/blazor-component.js"

export class Drawer extends BlazorComponent {
    _init() {
        this._body = document.querySelector('body')
        this._drawerBody = this._element.querySelector('.drawer-body')
    }

    _execute(args) {
        const open = args[0]
        if (open) {
            this._element.classList.add('show')
            this._body.classList.add('overflow-hidden')
            let handler = window.setTimeout(() => {
                this._drawerBody.classList.add('show')
                window.clearTimeout(handler)
                handler = null
            }, 20);
        }
        else {
            if (this._element.classList.contains('show')) {
                this._drawerBody.classList.remove('show')
                let handler = window.setTimeout(() => {
                    window.clearTimeout(handler)
                    handler = null
                    this._element.classList.remove('show')
                    this._body.classList.remove('overflow-hidden')
                }, 320)
            }
        }
    }

    _dispose() {
        if (this._element.classList.contains('show')) {
            this._element.classList.remove('show')
            this._drawerBody.classList.remove('show')
            this._body.classList.remove('overflow-hidden')
        }
    }
}
