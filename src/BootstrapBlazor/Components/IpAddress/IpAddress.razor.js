﻿import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const selectCell = (el, index) => {
    if (index === -1) {
        index = 0
    }
    if (index > 3) {
        index = 3
    }
    const c = el.querySelectorAll(".ipv4-cell")[index]
    c.focus()
    return c
}

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const ip = { el, prevValues: [0, 0, 0, 0] }
    Data.set(id, ip)

    el.querySelectorAll(".ipv4-cell").forEach((c, index) => {
        EventHandler.on(c, 'keydown', e => {
            const current = selectCell(el, index)
            if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105)) {
                // numbers, backup last status
                ip.prevValues[index] = c.value
                if (c.value === "0") {
                    c.value = ""
                }
                else if (c.selectionStart !== c.selectionEnd) {
                    const v = c.value.substring(c.selectionStart, c.selectionEnd)
                    const newVal = c.value.replace(v, e.key)
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
                const c = selectCell(el, index + 1)
                c.select()
            }
            else if (e.key === 'Backspace') {
                if (c.value.length <= 1) {
                    c.value = "0"
                    e.preventDefault();
                    const prevCell = selectCell(el, index - 1)
                    prevCell.selectionStart = prevCell.value.length
                    prevCell.selectionEnd = prevCell.value.length
                }
            }
            // 空格或右箭头（光标一定要在内容最后面）向后换一格
            else if (current.selectionStart === current.value.length && (e.code === 'Space' || e.code === 'ArrowRight')) {
                e.preventDefault()
                selectCell(el, index + 1)
            }
            // 左箭头（光标一定要在内容最前面）向前换一格
            else if (current.selectionStart === 0 && e.code === 'ArrowLeft') {
                e.preventDefault()
                selectCell(el, index - 1)
            }
            else if (e.key === 'Delete' || e.key === 'Tab' || e.key === 'ArrowLeft' || e.key === 'ArrowRight') {
                //原逻辑不做修改
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
