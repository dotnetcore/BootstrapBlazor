import { debounce, getHeight } from "../../modules/utility.js"
import { handleKeyUp, select, selectAllByFocus, selectAllByEnter } from "../Input/BootstrapInput.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import Input from "../../modules/input.js"
import Popover from "../../modules/base-popover.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    const menu = el.querySelector('.dropdown-menu')
    const input = document.getElementById(`${id}_input`)
    var ac = { el, invoke, menu, input }
    Data.set(id, ac)

    if (el.querySelector('[data-bs-toggle="bb.dropdown"]')) {
        ac.popover = Popover.init(el, { toggleClass: '[data-bs-toggle="bb.dropdown"]' })
    }

    // debounce
    const duration = parseInt(input.getAttribute('data-bb-debounce') || '0');
    if (duration > 0) {
        ac.debounce = true
        EventHandler.on(input, 'keyup', debounce(e => {
            invoke.invokeMethodAsync('OnKeyUp', e.code)
        }, duration, e => {
            return ['ArrowLeft', 'ArrowRight', 'ArrowUp', 'ArrowDown', 'Escape', 'Enter'].indexOf(e.key) > -1
        }))
    }
    else {
        EventHandler.on(input, 'keyup', e => {
            invoke.invokeMethodAsync('OnKeyUp', e.code)
        })
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

export function composition(id) {
    const ac = Data.get(id)
    if (ac) {
        ac.composition = true
        Input.composition(`${id}_input`, ac.invoke, 'TriggerOnChange')
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
            EventHandler.off(ac.el, 'focus')
        }
        if (ac.composition) {
            Input.dispose(`${id}_input`)
        }
        if (ac.input) {
            EventHandler.off(ac.input, 'keyup')
        }
    }
}

export { handleKeyUp, select, selectAllByFocus, selectAllByEnter }
