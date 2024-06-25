import { addLink } from '../../_content/BootstrapBlazor/modules/utility.js'

export async function init(id, options) {
    var el = document.getElementById(id)
    if (el) {
        await addLink("../../css/wwads.css");

        const { isVertical, isDebug } = options;

        if (isDebug === false) {
            const ad = document.createElement('div')
            ad.setAttribute("data-id", 72)
            ad.classList.add("wwads-cn")
            ad.classList.add(isVertical ? "wwads-vertical" : "wwads-horizontal");
            el.appendChild(ad);

            //    const cc = document.createElement('div');
            //    cc.classList.add("cc-ad")
            //    cc.innerHTML = `<a href="https://ccbpm.cn/index.html?Frm=argo" target="_blank"><img src='https://ccbpm.cn/img/GenerAD.png?Frm=argo' /></a>`
            //    el.appendChild(cc);
        }
    }
}
