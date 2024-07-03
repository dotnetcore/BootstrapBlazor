export function init() {
    const main = document.querySelector('.main')
    if (main) {
        main.classList.add('dock-layout')
    }
}

export function dispose() {
    const main = document.querySelector('.main')
    if (main) {
        main.classList.remove('dock-layout')
    }
}
