export function init() {
    const hash = decodeURI(location.hash)
    if (hash) {
        var anchor = hash.split('-')[0]
        try {
            const el = document.querySelector(anchor)
            if (el) {
                const handler = setTimeout(() => {
                    el.scrollIntoView({ behavior: 'smooth', block: 'center' })
                    clearTimeout(handler)
                }, 1000)
            }
        }
        catch (e) {
            console.log(e)
        }
    }
}
