import Confirm from "./confirm.js"

export function execute(id, method, title) {
    const el = document.getElementById(id)

    if (method === 'showTooltip') {
        bootstrap.Tooltip.getOrCreateInstance(el, {
            title: title
        })
    }
    else if (method === 'removeTooltip') {
        const tooltip = bootstrap.Tooltip.getInstance(el)
        if (tooltip) {
            tooltip.dispose()
        }
    }
    else if (method === 'showConfirm') {
        let confirm = Confirm.getInstance(el)
        if (!confirm) {
            confirm = new Confirm(el)
            confirm.toggle()
        }
    }
    else if (method === 'submit') {
        const form = el.closest('form');
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
