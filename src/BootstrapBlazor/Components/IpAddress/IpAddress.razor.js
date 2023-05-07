import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const revert = (ip, index) => {
    ip.el.querySelectorAll(".ipv4-cell")[index].value = ip.prevValues[index]
}

const selectCell = (el, index) => {
    if (index === -1) {
        index = 0
    }
    if (index > 3) {
        index = 3
    }
    const c = el.querySelectorAll(".ipv4-cell")[index]
    c.focus()
    c.select()
}

const isValidIPStr = ipString => {
    if (typeof ipString !== "string") return false

    var ipStrArray = ipString.split(".")
    if (ipStrArray.length !== 4) return false

    return ipStrArray.reduce(function (prev, cur) {
        if (prev === false || cur.length === 0) return false
        return (Number(cur) >= 0 && Number(cur) <= 255) ? true : false
    }, true)
}

const getCurIPStr = () => {
    var str = ""
    this.find(".ipv4-cell").each(function (index, element) {
        str += (index == 0) ? $(element).val() : "." + $(element).val()
    })
    return str
}

// function for text input cell
const getCursorPosition = cell => {
    if ('selectionStart' in cell) {
        // Standard-compliant browsers
        return cell.selectionStart
    }
    else if (document.selection) {
        // IE
        cell.focus()
        const sel = document.selection.createRange()
        const selLen = document.selection.createRange().text.length
        sel.moveStart('character', -cell.value.length)
        return sel.text.length - selLen
    }
}

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const ip = { el, prevValues: [0, 0, 0, 0] }
    Data.set(id, ip)

    el.querySelectorAll('.ipv4-cell').forEach(c => {
        c.select()
        el.classList.add('selected')
    })

    el.querySelectorAll(".ipv4-cell").forEach((c, index) => {
        EventHandler.on(c, 'keydown', e => {
            if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode >= 96 && e.keyCode <= 105) {
                // numbers, backup last status
                ip.prevValues[index] = c.value
            }
            else if (e.keyCode === 37 || e.keyCode === 39) {
                // left key ,right key
                if (e.keyCode === 37 && getCursorPosition(c) === 0) {
                    selectCell(el, index - 1)
                    e.preventDefault()
                }
                else if (e.keyCode === 39 && getCursorPosition(c) === c.value.length) {
                    selectCell(el, index + 1)
                    e.preventDefault()
                }
            }
            else if (e.keyCode === 9) {	// allow tab
            }
            else if (e.keyCode === 8 || e.keyCode === 46) {	// allow backspace, delete
            }
            else {
                e.preventDefault()
            }
        })

        EventHandler.on(c, 'keyup', e => {
            if (e.keyCode >= 48 && e.keyCode <= 57 || e.keyCode >= 96 && e.keyCode <= 105) {
                // numbers
                const val = c.value
                const num = Number(val)

                if (num > 255)
                    revert(ip, index)
                else if (val.length > 1 && val[0] === "0")
                    revert(ip, index)
                else if (val.length === 3)
                    selectCell(el, index + 1)
            }
            if (e.key === 'Backspace') {
                if (c.value === '') {
                    c.value = '0'
                    selectCell(el, index - 1)
                }
            }
            if (e.key === '.') {
                selectCell(el, index + 1)
            }
            if (e.key === 'ArrowRight') {
                selectCell(el, index + 1)
            }
            if (e.key === 'ArrowLeft') {
                selectCell(el, index - 1)
            }
        })
    })
}

export function dispose(id) {
    const ip = Data.get(id)
    Data.remove(id)

    if (ip) {

    }
}
