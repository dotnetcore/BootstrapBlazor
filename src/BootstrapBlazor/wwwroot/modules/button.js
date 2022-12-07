import BlazorComponent from "./base/blazor-component.js"
import { Confirm } from "./confirm.js"

export class Button extends BlazorComponent {
    _execute(args) {
        const method = args[0]
        if (method === 'showTooltip') {
            const title = args[1]
            bootstrap.Tooltip.getOrCreateInstance(this._element, {
                title: title
            })
        }
        else if (method === 'removeTooltip') {
            const tooltip = bootstrap.Tooltip.getInstance(this._element)
            if (tooltip) {
                tooltip.dispose()
            }
        }
        else if (method === 'showConfirm') {
            let confirm = Confirm.getInstance(this._element)
            if (!confirm) {
                confirm = new Confirm(this._element)
                confirm.toggle()
            }
        }
        else if (method === 'submit') {
            const form = this._element.closest('form');
            if (form !== null) {
                const button = document.createElement('button');
                button.setAttribute('type', 'submit');
                button.setAttribute('hidden', 'true');
                form.append(button);
                button.click();
                button.remove();
            }
        }
    }
}
