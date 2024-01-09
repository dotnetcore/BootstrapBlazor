export function execute(id, title) {
    const el = document.getElementById(id)

    if (el) {
        const tip = bootstrap.Tooltip.getOrCreateInstance(el, { customClass: 'is-invalid', title })
        if (!tip._isShown()) {
            if (title !== tip._config.title) {
                tip._config.title = title
            }
            tip.show()
        }
    }
}

export function dispose(id) {
    const el = document.getElementById(id)

    if (el) {
        const tip = bootstrap.Tooltip.getInstance(el)
        if (tip) {
            const handler = setTimeout(() => {
                clearTimeout(handler)
                if (tip && tip._element) {
                    tip.dispose()
                }
            }, 10)
        }
    }
}
