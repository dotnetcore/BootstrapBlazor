import { computePosition as computePosition$1, autoUpdate, offset, hide, flip, shift } from '../lib/floating-ui/floating-ui.dom.esm.js'
export { arrow, autoPlacement, detectOverflow, flip, hide, inline, limitShift, offset, shift, size } from '../lib/floating-ui/floating-ui.core.esm.js';

export function createPopper(reference, floating, update, options) {
    return autoUpdate(reference, floating, update, options)
}

export function computePosition(reference, floating, options) {
    const op = {
        ...{
            middleware: [
                offset(),
                hide(),
                flip(),
                shift({ padding: 5 })
            ]
        },
        ...options
    }
    return computePosition$1(reference, floating, op)
}
