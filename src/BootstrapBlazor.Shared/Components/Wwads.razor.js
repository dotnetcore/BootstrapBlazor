export function init(id) {
    const ad = document.createElement('div')
    ad.setAttribute("data-id", 72)
    ad.classList.add("wwads-cn")
    ad.classList.add("wwads-horizontal")
    var element = document.getElementById(id)
    if (element) {
        if (element.getAttribute('data-bb-debug') === 'true') {
            ad.classList.add('debug')
        }

        const parent = element.parentNode
        parent.insertBefore(ad, element.nextSibling)
    }
}

export function dispose(id) {
}

