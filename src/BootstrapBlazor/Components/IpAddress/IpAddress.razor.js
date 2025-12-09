import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const selectCell = (el, index) => {
    if (index === -1) {
        index = 0
    }
    if (index > 3) {
        index = 3
    }
    const cell = el.querySelectorAll(".ipv4-cell")[index];
    cell.focus();
}

export function init(id, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const ip = { el, prevValues: [0, 0, 0, 0] }
    Data.set(id, ip)

    el.querySelectorAll(".ipv4-cell").forEach((c, index) => {
        EventHandler.on(c, 'compositionend', e => {
            e.preventDefault();
            c.value = ip.prevValues[index];
        });
        EventHandler.on(c, 'focus', e => {
            const input = e.target;
            input.select();
        });
        EventHandler.on(c, 'keydown', e => {
            const current = e.target;
            if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105)) {
                ip.prevValues[index] = c.value
                if (c.value === "0") {
                    c.value = ""
                }
                else if (c.selectionStart !== c.selectionEnd) {
                    const v1 = c.value.substring(0, c.selectionStart);
                    const v2 = c.value.substring(c.selectionEnd, c.value.length);
                    const newVal = `${v1}${e.key}${v2}`;
                    const num = Number(newVal)
                    if (num > 255) {
                        e.preventDefault()
                    }
                }
                else {
                    const num = Number(c.value + e.key)
                    if (num > 255) {
                        e.preventDefault()
                    }
                }
            }
            else if (e.key === '.') {
                e.preventDefault()
                selectCell(el, index + 1)
            }
            else if (e.key === 'Backspace') {
                if (c.value.length <= 1) {
                    c.value = "0"
                    e.preventDefault();
                    selectCell(el, index - 1)
                }
            }
            else if (current.selectionStart === current.value.length && (e.key === 'Space' || e.key === 'ArrowRight')) {
                e.preventDefault()
                selectCell(el, index + 1)
            }
            else if (current.selectionStart === 0 && e.key === 'ArrowLeft') {
                e.preventDefault()
                selectCell(el, index - 1)
            }
            else if (e.composed || e.key === 'Delete' || e.key === 'Tab' || e.key === 'ArrowLeft' || e.key === 'ArrowRight') {

            }
            else {
                e.preventDefault()
            }
        })

        EventHandler.on(c, 'keyup', e => {
            if (e.keyCode >= 48 && e.keyCode <= 57) {
                const val = c.value
                if (val.length === 3) {
                    selectCell(el, index + 1)
                }
            }
        })

        EventHandler.on(c, 'paste', e => {
            e.preventDefault();
            const raw = (e.clipboardData || window.clipboardData)?.getData('text') ?? '';
            if (!raw) {
                return;
            }
            const parts = raw.replace(/[^\d.]/g, '').split('.').filter(p => p.length);
            const cells = el.querySelectorAll(".ipv4-cell");
            let pos = 0;
            const args = [];
            parts.forEach(p => {
                if (pos > 3) {
                    return;
                }
                const num = Math.max(0, Math.min(255, parseInt(p, 10) || 0));
                args.push(num);
                cells[pos].value = num.toString();
                ip.prevValues[pos] = cells[pos].value;
                pos++;
            });

            invoke.invokeMethodAsync("TriggerUpdate", ...args);
        });
    })
}

export function dispose(id) {
    const ip = Data.get(id)
    Data.remove(id)

    if (ip) {
        ip.el.querySelectorAll(".ipv4-cell").forEach(c => {
            EventHandler.off(c, 'keyup')
            EventHandler.off(c, 'keydown')
        })
    }
}
