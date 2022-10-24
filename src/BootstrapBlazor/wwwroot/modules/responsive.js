import SimpleComponent from "./base/simple-component.js"
import EventHandler from "./base/event-handler.js"

export class Responsive extends SimpleComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._invokerMethod = this._config.arguments[1]
        this._currentBreakpoint = Responsive.getResponsive()
        this._setListeners()
    }

    _setListeners() {
        const resizeHandler = () => {
            let lastBreakpoint = Responsive.getResponsive()
            if (lastBreakpoint !== this._currentBreakpoint) {
                this._currentBreakpoint = lastBreakpoint
                this._invoker.invokeMethodAsync(this._invokerMethod, lastBreakpoint)
            }
        };

        EventHandler.on(window, 'resize', resizeHandler)
    }

    _dispose() {
        EventHandler.off(window, 'resize')
    }

    static getResponsive() {
        return window.getComputedStyle(document.body, ':before').content.replace(/\"/g, '')
    }
}
