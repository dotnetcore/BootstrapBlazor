import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js"
import { copy } from "../../../_content/BootstrapBlazor/modules/base/utility.js"

export class IconList extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._updateMethod = this._config.arguments[1]
        this._showDialogMethod = this._config.arguments[2]
        this._copyIcon = this._config.arguments[3]

        if (this._element.classList.contains('is-catalog')) {
            this._body = this._element.querySelector('.icons-body')
            this._target = this._body.getAttribute('data-bs-target')
            this._scrollspy = new bootstrap.ScrollSpy(this._body, {
                target: this._target
            })
            this._base = this._element.querySelector('#bb-fa-top')
        }

        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', '.nav-link', e => {
            e.preventDefault()
            e.stopPropagation()

            const targetId = e.delegateTarget.getAttribute('href')
            const target = this._element.querySelector(targetId)

            const rect = target.getBoundingClientRect()
            let margin = rect.top
            let marginTop = getComputedStyle(target).getPropertyValue('margin-top').replace('px', '')
            if (marginTop) {
                margin = margin - parseInt(marginTop)
            }
            const offset = this._element.getAttribute(this._config.offsetSelector)
            if (offset) {
                margin = margin - parseInt(offset)
            }
            margin = margin - this._base.getBoundingClientRect().top
            this._body.scrollTo(0, margin)
        })

        EventHandler.on(this._element, 'click', '.icons-body a', e => {
            e.preventDefault()
            e.stopPropagation()

            const i = e.delegateTarget.querySelector('i')
            const css = i.getAttribute('class')
            this._invoker.invokeMethodAsync(this._updateMethod, css)
            const dialog = this._element.classList.contains('is-dialog')
            if (dialog) {
                this._invoker.invokeMethodAsync(this._showDialogMethod, css)
            } else if (this._copyIcon) {
                this._copy(e.delegateTarget, css)
            }
        })
    }

    _copy(element, text) {
        copy(text);

        this._tooltip = bootstrap.Tooltip.getInstance(element)
        if (this._tooltip) {
            this._reset(element)
        } else {
            this._show(element)
        }
    }

    _show(element) {
        this._tooltip = new bootstrap.Tooltip(element, {
            title: this._config.title
        })
        this._tooltip.show()
        this._tooltipHandler = window.setTimeout(() => {
            window.clearTimeout(this._tooltipHandler)
            if (this._tooltip) {
                this._tooltip.dispose()
            }
        }, 1000)
    }

    _reset(element) {
        if (this._tooltipHandler) {
            window.clearTimeout(this._tooltipHandler)
        }
        if (this._tooltip) {
            this._tooltipHandler = window.setTimeout(() => {
                window.clearTimeout(this._tooltipHandler)
                this._tooltip.dispose();
                this._show()
            }, 10)
        } else {
            this._show(element)
        }
    }

    _dispose() {
        EventHandler.off(this._element, 'click', '.nav-link')
        EventHandler.off(this._element, 'click', '.icons-body a')

        if (this._scrollspy) {
            this._scrollspy.dispose()
        }
    }
}
