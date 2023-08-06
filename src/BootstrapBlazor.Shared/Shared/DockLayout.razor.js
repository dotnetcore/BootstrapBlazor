export function init() {
    document.body.classList.add("overflow-hidden")
    const main = document.querySelector('.main')
    if (main) {
        main.classList.add('dock-layout')
    }
}

export function dispose() {
    document.body.classList.remove("overflow-hidden")
    const main = document.querySelector('.main')
    if (main) {
        main.classList.remove('dock-layout')
    }
}
