export function scrollToAnchor() {
    const hash = decodeURI(location.hash)
    if (hash) {
        const anchor = hash.split('-')[0]
        const el = document.querySelector(anchor)
        if (el) {
            const handler = setTimeout(() => {
                clearTimeout(handler)
                el.scrollIntoView({ behavior: 'smooth', block: 'start' })
            }, 1000)
        }
    }
}
