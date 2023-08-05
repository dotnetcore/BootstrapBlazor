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
                    console.log(el, hash)
                }, 1000)
            }
            else {
                console.log(hash)
            }
        }
        catch (e) {
            console.log(e)
        }
    }
}
