import { getUID } from "../../modules/utility.js"
import { showTooltip, removeTooltip } from "../Button/Button.razor.js"
import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id) {
    const el = document.getElementById(id)
    const p = { el }
    Data.set(id, p)

    const href = el.getAttribute('href')
    if (href) {
        el.setAttribute('target', '_blank')
    }
    else {
        el.removeAttribute('target')
        EventHandler.on(el, 'click', () => {
            print(el)
        })
    }
}

const print = el => {
    const modal = el.closest('.modal-content')
    if (modal) {
        const modalBody = modal.querySelectorAll('.modal-body')
        if (modalBody.length > 0) {
            modalBody[0].querySelectorAll("input").forEach(ele => {
                const id = ele.getAttribute('id')
                if (!id) {
                    ele.setAttribute('id', getUID())
                }
            })
            const printContent = modalBody[0].innerHTML
            const body = document.querySelector('body')
            body.classList.add('bb-printview-open')
            const dialog = document.createElement('div')
            dialog.classList.add('bb-printview')
            dialog.innerHTML = printContent
            body.appendChild(dialog)

            // assign value
            dialog.querySelectorAll("input").forEach(ele => {
                const id = ele.getAttribute('id')
                const vEl = document.getElementById(id)
                if (vEl) {
                    if (ele.getAttribute('type') === 'checkbox') {
                        const v1 = vEl.checked
                        if (v1 === true) {
                            ele.setAttribute('checked', 'checked')
                        }
                    }
                    else {
                        ele.value = vEl.value
                    }
                }
            })

            const handler = setTimeout(() => {
                clearTimeout(handler)
                window.print()
                body.classList.remove('bb-printview-open')
                dialog.remove()
            }, 50)
        }
    }
    else {
        const handler = setTimeout(() => {
            clearTimeout(handler)
            window.print()
        }, 50)
    }
}

export function dispose(id) {
    const p = Data.get(id)
    Data.remove(id)

    removeTooltip(id)
    if (p) {
        EventHandler.off(p.el, 'click')
    }
}

export {
    showTooltip,
    removeTooltip,
}
