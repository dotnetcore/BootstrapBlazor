import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"
import { copy, getDescribedElement, addLink, addScript, getHeight } from "../../../_content/BootstrapBlazor/modules/base/utility.js"

export class Pre extends BlazorComponent {
    _init() {
        addLink('_content/BootstrapBlazor.Shared/lib/highlight/vs.css')
        addScript('_content/BootstrapBlazor.Shared/lib/highlight/highlight.min.js')
        this._pre = this._element.querySelector('pre')
        if (this._pre) {
            this._code = this._pre.querySelector('code')
            this._setListeners()
        }
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', '.btn-copy', e => {
            const text = e.delegateTarget.parentNode.querySelector('code').textContent;
            copy(text)

            const tooltip = getDescribedElement(e.delegateTarget)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = this._config.title
            }
        })

        EventHandler.on(this._element, 'click', '.btn-plus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(this._pre)
            const codeHeight = getHeight(this._code)
            if (preHeight < codeHeight) {
                preHeight = Math.min(codeHeight, preHeight + 100)
            }
            this._pre.style.maxHeight = `${preHeight}px`
        })

        EventHandler.on(this._element, 'click', '.btn-minus', e => {
            e.preventDefault()
            e.stopPropagation();

            let preHeight = getHeight(this._pre)
            if (preHeight > 260) {
                preHeight = Math.max(260, preHeight - 100)
            }
            this._pre.style.maxHeight = `${preHeight}px`
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
        EventHandler.off(this._element, 'click', '.btn-copy')
        EventHandler.off(this._element, 'click', '.btn-plus')
        EventHandler.off(this._element, 'click', '.btn-minus')
    }
}
