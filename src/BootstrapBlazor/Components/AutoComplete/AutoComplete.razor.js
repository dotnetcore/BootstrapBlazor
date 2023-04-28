import { getHeight } from "../../modules/utility.js"
import Data from '../../modules/data.js'
import Debounce from '../../modules/debounce.js'
import EventHandler from "../../modules/event-handler.js"
import Input from '../../modules/input.js'

export function init(el) {
    var ac = { el }
    Data.set(el, ac)
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

export function debounce(el, input, ms) {
    const ac = Data.get(el)
    if (ac) {
        ac.input = input
        ac.debounce = true
    }
    Debounce.init(input, ms)
}

export function composition(el, input, invoke, method) {
    const ac = Data.get(el)
    if (ac) {
        ac.input = input
        ac.composition = true
    }
    Input.composition(input, invoke, method)
}

export function dispose(el) {
    const ac = Data.get(el)
    Data.remove(el)

    if (ac) {
        if (ac.composition) {
            Input.dispose(ac.input)
        }
        if (ac.debounce) {
            Debounce.dispose(ac.input)
        }
    }
}
