import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"
import * as Input from "../Input/BootstrapInput.razor.js"

export const focus = Input.focus
export const clear = Input.clear
export const handleKeyUp = Input.handleKeyUp
export const select = Input.select
export const selectAllByFocus = Input.selectAllByFocus
export const selectAllByEnter = Input.selectAllByEnter

const sanitize = (value, tokens) => {
    let result = ''
    let index = 0

    while (index < value.length) {
        const character = value[index]
        if (character >= '0' && character <= '9') {
            result += character
            index++
            continue
        }

        const token = tokens.find(token => value.startsWith(token, index))
        if (token) {
            result += token
            index += token.length
        }
        else {
            index++
        }
    }

    return result
}

const normalizeTokens = tokens => [...new Set(tokens)]
    .filter(token => token)
    .sort((left, right) => right.length - left.length)

const sanitizeInput = state => {
    const { el, tokens } = state
    const value = sanitize(el.value, tokens)
    if (value === el.value) {
        return
    }

    const selectionStart = el.selectionStart ?? el.value.length
    const caret = sanitize(el.value.substring(0, selectionStart), tokens).length
    el.value = value
    el.setSelectionRange(caret, caret)
}

export function init(id, tokens) {
    const el = document.getElementById(`${id}_input`)
    if (!el) {
        return
    }

    const state = { el, tokens: normalizeTokens(tokens), isComposing: false }
    Data.set(id, state)

    EventHandler.on(el, 'beforeinput', e => {
        if (state.isComposing || e.isComposing) {
            e.preventDefault();
            return;
        }

        if (e.inputType?.startsWith('insert') && e.data && sanitize(e.data, state.tokens) !== e.data) {
            e.preventDefault()
        }
    })
    EventHandler.on(el, 'input', e => {
        if (!e.isComposing) {
            sanitizeInput(state)
        }
    })
    EventHandler.on(el, 'compositionstart', () => {
        state.isComposing = true
    })
    EventHandler.on(el, 'compositionend', () => {
        state.isComposing = false
        sanitizeInput(state)
    })
}

export function update(id, tokens) {
    const state = Data.get(id)
    if (state) {
        state.tokens = normalizeTokens(tokens)
    }
}

export function dispose(id) {
    const state = Data.get(id)
    Data.remove(id)

    if (state) {
        EventHandler.off(state.el, 'beforeinput')
        EventHandler.off(state.el, 'input')
        EventHandler.off(state.el, 'compositionstart')
        EventHandler.off(state.el, 'compositionend')
    }
    Input.dispose(`${id}_input`)
}
