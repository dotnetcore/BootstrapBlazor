import BlazorComponent from "./base/blazor-component.js"

export class Textarea extends BlazorComponent {
    _execute(args) {
        const autoScroll = this._element.getAttribute('data-bb-scroll') === 'auto'
        const method = args[0]
        let position = args[1]
        if (method === 'toTop') {
            position = 0;
        }
        if (autoScroll || method === 'toBottom') {
            position = this._element.scrollHeight
        }
        this._element.scrollTop = position;
    }
}
