import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"

export class TimePicker extends BlazorComponent {
    static get Default() {
        return {
            upCallback: 'OnClickUp',
            downCallback: 'OnClickDown',
            heightCallback: 'OnHeightCallback',
            spinnerSelector: '.time-spinner-list',
            spinnerItemSelector: '.time-spinner-item'
        }
    }

    _init() {
        this._invoker = this._config.arguments[0]
        this._caclHeight()
        this._setListeners()
    }

    _caclHeight() {
        const item = this._element.querySelector(this._config.spinnerItemSelector)
        const styles = getComputedStyle(item)
        const height = parseFloat(styles.getPropertyValue('height')) || 0
        this._invoker.invokeMethodAsync(this._config.heightCallback, height)
    }

    _setListeners() {
        EventHandler.on(this._element, 'mousewheel', this._config.spinnerSelector, e => {
            e.preventDefault()
            e.stopPropagation()

            const margin = e.wheelDeltaY || -e.deltaY;
            if (margin > 0) {
                this._invoker.invokeMethodAsync(this._config.upCallback);
            } else {
                this._invoker.invokeMethodAsync(this._config.downCallback);
            }
        })
    }

    _dispose() {
        EventHandler.off(this._element, 'mousewheel', this._config.spinnerSelector)
    }
}
