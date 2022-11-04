import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"
import { copy } from "./base/utility.js"

export class IconDialog extends BlazorComponent {
    _init() {
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', 'button', e => {
            const text = e.delegateTarget.parentNode.querySelector('.is-display').textContent
            if (text) {
                this._copy(e.delegateTarget, text)
            }
        })
    }

    _copy(element, text) {
        copy(text);

        this._tooltip = bootstrap.Tooltip.getInstance(element)
        if (this._tooltip) {
            this._reset(element)
        } else {
            this._show(element)
        }
    }

    _show(element) {
        this._tooltip = new bootstrap.Tooltip(element, {
            title: this._config.title
        })
        this._tooltip.show()
        this._tooltipHandler = window.setTimeout(() => {
            window.clearTimeout(this._tooltipHandler);
            if (this._tooltip) {
                this._tooltip.dispose();
            }
        }, 1000);
    }

    _reset(element) {
        if (this._tooltipHandler) {
            window.clearTimeout(this._tooltipHandler)
        }
        if (this._tooltip) {
            this._tooltipHandler = window.setTimeout(() => {
                window.clearTimeout(this._tooltipHandler)
                this._tooltip.dispose();
                this._show()
            }, 10)
        } else {
            this._show(element)
        }
    }

    _dispose() {
        EventHandler.off(this._element, 'click', 'button')
    }
}
