import { Tooltip } from "./tooltip.js"
import { getTransitionDelayDurationFromElement } from "./base/utility.js"

export class Validate extends Tooltip {
    _init() {
        this._config.title = this._config.arguments[0]
        if (this._config.arguments.length > 1) {
            const method = this._config.arguments[1]
            if (method === 'Invalid') {
                this._reset()
            }
        }
        else {
            this._create()
        }
    }

    _reset() {
        this._config.customClass = 'is-invalid'
        this._tooltip = bootstrap.Tooltip.getInstance(this._element)
        if (this._tooltip !== null && this._tooltip._isShown()) {
            this._tooltip.hide();

            const duration = getTransitionDelayDurationFromElement(this._element);
            const handler = window.setTimeout(() => {
                window.clearTimeout(handler);
                this._tooltip.dispose();
                this._create(true);
            }, duration);
        } else {
            this._create(true);
        }
    }

    _create(toggle = false) {
        this._tooltip = new bootstrap.Tooltip(this._element, this._config)
        if (toggle) {
            this._tooltip.toggle()
        }
    }

    _dispose() {
        if (this._tooltip && this._tooltip.tip !== null) {
            this._tooltip.dispose();
        }
    }
}
