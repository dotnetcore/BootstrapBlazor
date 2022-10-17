import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js";
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js";
import { copy, getDescribedElement } from "../../../_content/BootstrapBlazor/modules/base/utility.js";

export class Pre extends BlazorComponent {
    _init() {
        this._setListeners()
    }

    _setListeners() {
        EventHandler.on(this._element, 'click', '.btn-clipboard', e => {
            const text = e.delegateTarget.previousElementSibling.querySelector('code').textContent;
            copy(text)

            const tooltip = getDescribedElement(e.delegateTarget)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = this._config.title
            }
        })
    }

    _dispose() {
        EventHandler.off(this._element, 'click', '.btn-clipboard');
    }
}
