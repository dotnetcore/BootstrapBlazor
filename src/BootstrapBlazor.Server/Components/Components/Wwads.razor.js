export function init(id) {
    var el = document.getElementById(id)
    if (el) {
        if (el.getAttribute('data-bb-debug') !== 'true') {
            const ad = document.createElement('div')
            ad.setAttribute("data-id", 72)
            ad.classList.add("wwads-cn")
            ad.classList.add("wwads-horizontal")
            el.appendChild(ad);

            //    const cc = document.createElement('div');
            //    cc.classList.add("cc-ad")
            //    cc.innerHTML = `<a href="https://ccbpm.cn/index.html?Frm=argo" target="_blank"><img src='https://ccbpm.cn/img/GenerAD.png?Frm=argo' /></a>`
            //    el.appendChild(cc);
        }
    }
}
