export function init(id) {
    const ad = document.createElement('div')
    ad.setAttribute("data-id", 72)
    ad.classList.add("wwads-cn")
    ad.classList.add("wwads-horizontal")
    var el = document.getElementById(id)
    if (el) {
        if (el.getAttribute('data-bb-debug') === 'true') {
            ad.classList.add('debug')
        }

        const parent = el.parentNode
        parent.insertBefore(ad, el.nextSibling)
    }
}
