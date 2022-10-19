import { Tooltip } from "./tooltip.js"

export class Validate extends Tooltip {
    _execute(args) {
        this._tooltip._config.customClass = "is-invalid"
        this._tooltip._config.title = args[0]
        if (!this._tooltip._isShown()) {
            this._tooltip.show()
        }
    }
}
