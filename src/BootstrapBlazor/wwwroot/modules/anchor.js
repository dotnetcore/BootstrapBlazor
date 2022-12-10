import EventHandler from "./base/event-handler.js"
import BlazorComponent from "./base/blazor-component.js"
import { getDescribedElement, getWindowScroll } from "./base/utility.js"

export class Anchor extends BlazorComponent {
    static get Default() {
        return {
            targetSelector: 'data-bb-target',
            containerSelector: 'data-bb-container',
            offsetSelector: 'data-bb-offset'
        }
    }

    _init() {
        this._animation = this._config.animation
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', e => {
            e.preventDefault()
            const target = getDescribedElement(this._element, this._config.targetSelector);
            if (target) {
                const container = getDescribedElement(this._element, this._config.containerSelector) || document.defaultView
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
                let winScroll = container;
                if (winScroll.scrollTop === undefined) {
                    winScroll = getWindowScroll(container)
                }
                if (this._animation) {
                    const top = margin + winScroll.scrollTop
                    margin = winScroll.scrollTop
                    var step = (top - margin) / 10
                    let handler = window.setInterval(() => {
                        if (margin == top) {
                            window.clearInterval(handler)
                            handler = null
                        }
                        else {
                            margin += step
                            if (step > 0 && margin >= top) {
                                margin = top
                            }
                            else if (step < 0 && margin <= top) {
                                margin = top
                            }
                            container.scrollTo(0, margin)
                        }
                    }, 10)
                }
                else {
                    container.scrollTo(0, margin + winScroll.scrollTop)
                }
            }
        });
    }

    _dispose() {
        EventHandler.off(this._element, 'click')
    }
}
