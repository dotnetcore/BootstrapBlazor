import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js";

export class Wwads extends BlazorComponent {
    _init() {
        const ad = document.createElement('div')
        ad.setAttribute("data-id", 72)
        ad.classList.add("wwads-cn")
        ad.classList.add("wwads-horizontal")

        if (this._element.getAttribute('data-bb-debug') === 'true') {
            ad.classList.add('debug')
        }

        const parent = this._element.parentNode
        parent.insertBefore(ad, this._element.nextSibling)
    }
}
