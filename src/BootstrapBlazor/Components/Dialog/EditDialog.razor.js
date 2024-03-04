export function execute(id, show) {
    const el = document.getElementById(id)
    if (show) {
        el.classList.add('show')
    }
    else {
        el.classList.remove('show')
    }
}
