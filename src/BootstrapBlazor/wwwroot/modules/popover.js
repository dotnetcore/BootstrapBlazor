import { Tooltip } from "./tooltip.js"

export class Popover extends Tooltip {
    _init() {
        this._config.title = this._config.arguments[0]
        this._config.content = this._config.arguments[1]
        this._popover = new bootstrap.Popover(this._element, this._config)
    }

    _dispose() {
        if (this._popover) {
            this._popover.dispose();
        }
    }
}
