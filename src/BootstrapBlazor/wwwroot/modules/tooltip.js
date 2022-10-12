import BlazorComponent from "./base/blazor-component.js"

export class Tooltip extends BlazorComponent {
    _init() {
        this._config.title = this._config.arguments[0]
        this._tooltip = new bootstrap.Tooltip(this._element, this._config)
    }

    _dispose() {
        if (this._tooltip && this._tooltip.tip !== null) {
            this._tooltip.dispose();
        }
    }
}
