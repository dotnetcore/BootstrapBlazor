import { getHeight } from "../../modules/utility.js"
import { handleKeyup, select, selectAllByFocus, selectAllByEnter } from '../Input/BootstrapInput.razor.js'
import Data from '../../modules/data.js'
import Debounce from '../../modules/debounce.js'
import EventHandler from "../../modules/event-handler.js"
import Input from '../../modules/input.js'

export function init(id) {
    const el = document.getElementById(id)
    var ac = { el }
    Data.set(id, ac)
}

export function autoScroll(el, index) {
    const menu = el.querySelector('.dropdown-menu')
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
    }
    Input.composition(id, invoke, method)
}

export function dispose(id) {
    const ac = Data.get(id)
    Data.remove(id)

    if (ac) {
        if (ac.el) {
            EventHandler.off(ac.el, 'keyup')
            EventHandler.off(ac.el, 'focus')
        }
        if (ac.composition) {
            Input.dispose(id)
        }
        if (ac.debounce) {
            Debounce.dispose(id)
        }
    }
}

export { handleKeyup, select, selectAllByFocus, selectAllByEnter }
