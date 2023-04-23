import Data from ".modules/data.js"

export function init(id, title, content) {
    const el = document.getElementById(id)
    const config = { title, content }
    const pop = {
        el, popover: new bootstrap.Popover(el, config)
    }
}

export function dispose(id) {
    const pop = Data.get(id)
    Data.remove(id)
    if (pop.popover) {
        pop.popover.dispose();
    }
}
