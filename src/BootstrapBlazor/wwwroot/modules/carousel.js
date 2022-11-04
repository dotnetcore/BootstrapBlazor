import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"

export class Carousel extends BlazorComponent {
    _init() {
        this._config.delay = 10
        this._controls = this._element.querySelectorAll('[data-bs-slide]')
        this._carousel = bootstrap.Carousel.getOrCreateInstance(this._element, this._config)
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'mouseenter', () => {
            this._enterHandler = window.setTimeout(() => {
                window.clearTimeout(this._enterHandler)
                this._enterHandler = null
                this._element.classList.add('hover')
            }, this._config.delay)
        });
        EventHandler.on(this._element, 'mouseleave', () => {
            this._leaveHandler = window.setTimeout(() => {
                window.clearTimeout(this._leaveHandler)
                this._leaveHandler = null
                this._element.classList.remove('hover')
            }, this._config.delay)
        })
    }

    _dispose() {
        if (this._carousel !== null) {
            this._carousel.dispose();
        }

        if (this._enterHandler) {
            window.clearTimeout(this._enterHandler);
        }
        if (this._leaveHandler) {
            window.clearTimeout(this._leaveHandler);
        }
        EventHandler.off(this._element, 'mouseenter');
        EventHandler.off(this._element, 'mouseleave');

        if(this._carousel) {
            this._carousel.dispose()
        }
    }
}
