export function init(id, title, content) {
    const el = document.getElementById(id)
    if (el) {
        new bootstrap.Popover(el, { title, content })
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
