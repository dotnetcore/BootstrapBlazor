import { getHeight } from "../../modules/utility.js?v=$version"
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js?v=$version"
import Data from "../../modules/data.js?v=$version"
import Debounce from "../../modules/debounce.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"
import Input from "../../modules/input.js?v=$version"
import Popover from "../../modules/base-popover.js?v=$version"

export function init(id) {
    const el = document.getElementById(id)
    const menu = el.querySelector('.dropdown-menu')
    var ac = { el, menu }
    Data.set(id, ac)

    if (el.querySelector('[data-bs-toggle="bb.dropdown"]')) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' })
    }
}

export function autoScroll(id, index) {
    const ac = Data.get(id)
    const el = ac.el
    const menu = ac.menu
    const styles = getComputedStyle(menu)
    const maxHeight = parseInt(styles.maxHeight) / 2
    const itemHeight = getHeight(menu.querySelector('li'))
    const height = itemHeight * index
    const count = Math.floor(maxHeight / itemHeight)

    const active = menu.querySelector('.active')
    if (active) {
        active.classList.remove('active')
    }

    var len = menu.children.length
    if (index < len) {
        menu.children[index].classList.add('active')
    }

    if (height > maxHeight) {
        menu.scrollTop = itemHeight * (index > count ? index - count : index)
    }
    else if (index <= count) {
        menu.scrollTop = 0
    }
}

export function debounce(id, ms) {
    const ac = Data.get(id)
    if (ac) {
        ac.debounce = true
    }
    Debounce.init(id, ms)
}

export function composition(id, invoke, method) {
    const ac = Data.get(id)
    if (ac) {
        ac.composition = true
        Input.composition(`${id}_input`, invoke, method)
    }
}

export function dispose(id) {
    const ac = Data.get(id)
    Data.remove(id)

    if (ac) {
        if (ac.popover) {
            Popover.dispose(ac.popover)
        }
        if (ac.el) {
            EventHandler.off(ac.el, 'keyup')
            EventHandler.off(ac.el, 'focus')
        }
        if (ac.composition) {
            Input.dispose(`${id}_input`)
        }
        if (ac.debounce) {
            Debounce.dispose(id)
        }
    }
}

export { handleKeyUp, select, selectAllByFocus, selectAllByEnter }
