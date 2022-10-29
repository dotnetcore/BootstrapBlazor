import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"
import { copy, getDescribedElement, addLink, addScript } from "../../../_content/BootstrapBlazor/modules/base/utility.js"
import { Tooltip } from "../../../_content/BootstrapBlazor/modules/tooltip.js"

export class Pre extends BlazorComponent {
    _init() {
        addLink('_content/BootstrapBlazor.Shared/lib/highlight/vs.css')
        addScript('_content/BootstrapBlazor.Shared/lib/highlight/highlight.min.js')
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', 'button', e => {
            const text = e.delegateTarget.previousElementSibling.querySelector('code').textContent;
            copy(text)

            const tooltip = getDescribedElement(e.delegateTarget)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = this._config.title
            }
        })
    }

    _execute(args) {
        if (args[0] === 'highlight') {
            if (window.hljs) {
                this._highlight()
            }
            else {
                this._handler = window.setInterval(() => {
                    if (window.hljs) {
                        this._clearInterval()
                        this._highlight()
                    }
                }, 100)
            }
        }
    }

    _highlight() {
        if (this._element) {
            const code = this._element.querySelector('code')
            if (code) {
                window.hljs.highlightBlock(code)
            }
        }
    }

    _clearInterval() {
        if (this._handler) {
            window.clearInterval(this._handler)
            delete this._handler
        }
    }

    _dispose() {
        this._clearInterval()
        EventHandler.off(this._element, 'click', 'button');
    }
}
