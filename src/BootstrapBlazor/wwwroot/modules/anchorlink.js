import EventHandler from "./base/event-handler.js"
import BlazorComponent from "./base/blazor-component.js"
import { copy } from "./base/utility.js"

export class AnchorLink extends BlazorComponent {
    _init() {
        this._hash = this._element.getAttribute('id')
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', e => {
            e.preventDefault()
            if (this._hash) {
                const href = window.location.origin + window.location.pathname + '#' + this._hash
                copy(href);

                if (this._config.title) {
                    this._tooltip = bootstrap.Tooltip.getOrCreateInstance(this._element, this._config);
                    this._tooltip.show()
                    const handler = window.setTimeout(() => {
                        window.clearTimeout(handler);
                        this._tooltip.dispose();
                    }, 1000);
                }
            }
        })
    }

    _dispose() {
        EventHandler.off(this._element, 'click')
    }
}
