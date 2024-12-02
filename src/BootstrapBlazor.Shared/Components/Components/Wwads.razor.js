export function init(id, options) {
    var el = document.getElementById(id)
    if (el) {
        const { isVertical, isDebug } = options;

        if (isDebug === false) {
            const ad = document.createElement('div')
            ad.setAttribute("data-id", 72)
            ad.classList.add("wwads-cn")
            ad.classList.add(isVertical ? "wwads-vertical" : "wwads-horizontal");
            el.appendChild(ad);
        }
    }
}
