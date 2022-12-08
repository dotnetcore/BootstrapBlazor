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
                container.scrollTo(0, margin + winScroll.scrollTop)
            }
        });
    }

    _dispose() {
        EventHandler.off(this._element, 'click')
    }
}
