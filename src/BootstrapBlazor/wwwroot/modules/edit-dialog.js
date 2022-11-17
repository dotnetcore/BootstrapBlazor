import BlazorComponent from "./base/blazor-component.js"

export class EditDialog extends BlazorComponent {
    _execute(args) {
        var show = args[0]
        if (show) {
            this._element.classList.add('show')
        } else {
            this._element.classList.remove('show')
        }
    }
}
