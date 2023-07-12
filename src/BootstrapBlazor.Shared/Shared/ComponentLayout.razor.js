export function init() {
    const hash = location.hash
    if (hash) {
        const el = document.querySelector(hash)
        if (el) {
            const rect = el.getBoundingClientRect()
            const top = rect.top - 50
            scrollTo(0, top)
        }
    }
}
