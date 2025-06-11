import { insertBefore, getUID } from "../../modules/utility.js"
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
        const modalBody = [...modal.querySelectorAll('.modal-body')];
        if (modalBody.length > 0) {
            modal.classList.add('d-none');

            const dialog = modalBody.pop();
            const body = document.querySelector('body')
            body.classList.add('bb-printview-open')

            const printContentEl = createPrintContent(dialog);
            body.appendChild(printContentEl);

            const handler = setTimeout(() => {
                clearTimeout(handler)
                window.print();
                restoreCanvas(printContentEl);
                body.classList.remove('bb-printview-open')
                printContentEl.remove()
                modal.classList.remove('d-none')
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

const createPrintContent = content => {
    content.querySelectorAll("input").forEach(ele => {
        const id = ele.getAttribute('id')
        if (!id) {
            ele.setAttribute('id', getUID())
        }
    })
    const dialog = document.createElement('div')
    dialog.classList.add('bb-printview')
    dialog.innerHTML = content.innerHTML

    const elements = ["input", "textarea"];
    elements.forEach(tag => {
        dialog.querySelectorAll(tag).forEach(ele => {
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
        });
    });

    const canvas = [...content.querySelectorAll('canvas')];
    const targetCanvas = [...dialog.querySelectorAll('canvas')];
    for (var i = 0; i < canvas.length; i++) {
        const canvasEl = canvas[i];
        createCanvasPlaceholder(canvasEl);
        moveCanvas(canvasEl, targetCanvas[i]);
    }

    return dialog;
}

const createCanvasPlaceholder = canvas => {
    const sectionEl = document.createElement('section');
    sectionEl.classList.add('bb-print-canvas-placeholder');
    insertBefore(canvas, sectionEl);
}

const moveCanvas = (canvas, target) => {
    insertBefore(target, canvas);
    target.remove();
}

const restoreCanvas = printContentEl => {
    const canvas = [...printContentEl.querySelectorAll('canvas')];
    const targetCanvas = [...document.querySelectorAll('.bb-print-canvas-placeholder')];
    for (var i = 0; i < canvas.length; i++) {
        const canvasEl = canvas[i];
        const target = targetCanvas[i];
        moveCanvas(canvasEl, target);
        target.remove();
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
