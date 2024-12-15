export function init() {
    const hash = decodeURI(location.hash)
    if (hash) {
        const anchor = hash.split('-')[0]
        const el = document.querySelector(anchor)
        if (el) {
            const handler = setTimeout(() => {
                el.scrollIntoView({ behavior: 'smooth', block: 'center' })
                clearTimeout(handler)
            }, 1000)
        }
    }
}
