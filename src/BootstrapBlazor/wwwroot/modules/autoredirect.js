import EventHandler from "./base/event-handler.js";
import BaseComponent from "./base/base-component.js";

export class AutoRedirect extends BaseComponent {
    constructor(element, config = {}) {
        super(element)

        this._config = config
        this._init()
    }

    _init() {
        this._invoker = this._config.arguments[0]
        this._interval = this._config.arguments[1]
        this._invokeMethodName = this._config.arguments[2]
        this._mousePosition = {};
        this._count = 1000;
        this._setListeners()
    }

    _setListeners() {
        this._fnMouseHandler = e => {
            if (this._mousePosition.screenX !== e.screenX || this._mousePosition.screenY !== e.screenY) {
                this._mousePosition.screenX = e.screenX;
                this._mousePosition.screenY = e.screenY;
                this._count = 1000;
            }
        }

        this._fnKeyHandler = () => {
            this._count = 1000;
        }

        EventHandler.on(document, 'mousemove', this._fnMouseHandler);
        EventHandler.on(document, 'keydown', this._fnKeyHandler)

        this._lockHandler = window.setInterval(() => {
            this._count += 1000
            if (this._count > this._interval) {
                window.clearInterval(this._lockHandler);
                this._lockHandler = null

                this._invoker.invokeMethodAsync(this._invokeMethodName);
                this.dispose();
            }
        }, 1000);
    }

    dispose() {
        EventHandler.off(document, 'mousemove', this._fnMouseHandler);
        EventHandler.off(document, 'keydown', this._fnKeyHandler)

        if (this._lockHandler) {
            window.clearInterval(this._lockHandler);
        }
    }

    static init(element) {
        new this(element, { arguments: [].slice.call(arguments, 1) })
    }
}
