import BlazorComponent from "./base/blazor-component.js"

export class Tooltip extends BlazorComponent {
    static get Default() {
        return {
            arguments: [""]
        }
    }

    _init() {
        this._config.title = this._config.arguments[0]
        this._tooltip = new bootstrap.Tooltip(this._element, this._config)
    }

    _dispose() {
        if (this._tooltip) {
            const delay = 10
            const tooltip = this._tooltip
            const handler = window.setTimeout(() => {
                window.clearTimeout(handler)
                if (tooltip) {
                    tooltip.dispose()
                }
            }, delay)
        }
    }
}
