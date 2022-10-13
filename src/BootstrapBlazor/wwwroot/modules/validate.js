import { Tooltip } from "./tooltip.js"

export class Validate extends Tooltip {
    _execute(args) {
        this._config.title = args[0]
        if (!this._tooltip) {
            this._config.customClass = "is-invalid"
            this._config.arguments = args
            this._init()
        }
        else {
            this._tooltip._config.title = this._config.title
        }
        if (!this._tooltip._isShown()) {
            this._tooltip.show()
        }
    }
}
