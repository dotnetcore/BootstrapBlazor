export function init(id, content) {
    const el = document.getElementById(id)
    if (el) {
        const pop = bootstrap.Popover.getInstance(el)
        if (pop) {
            pop._config.content = content
        }
        else {
            new bootstrap.Popover(el, {
                content,
                title: () => {
                    return el.getAttribute('data-bs-original-title')
                }
            })
        }
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    if (el) {
        const pop = bootstrap.Popover.getInstance(el)
        if (pop) {
            pop.dispose();
        }
    }
}
