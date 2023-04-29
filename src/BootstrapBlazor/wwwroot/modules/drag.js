import EventHandler from './event-handler.js'

export default {
    init: element => {

    },

    drag: (element, start, move, end) => {
        const handleDragStart = e => {
            let notDrag = false
            if (isFunction(start)) {
                notDrag = start(e) || false
            }

            if (!notDrag) {
                e.preventDefault()
                e.stopPropagation()

                document.addEventListener('mousemove', handleDragMove)
                document.addEventListener('touchmove', handleDragMove)
                document.addEventListener('mouseup', handleDragEnd)
                document.addEventListener('touchend', handleDragEnd)
            }
        }

        const handleDragMove = e => {
            if (e.touches && e.touches.length > 1) {
                return;
            }

            if (isFunction(move)) {
                move(e)
            }
        }

        const handleDragEnd = e => {
            if (isFunction(end)) {
                end(e)
            }

            const handler = window.setTimeout(() => {
                window.clearTimeout(handler)
                document.removeEventListener('mousemove', handleDragMove)
                document.removeEventListener('touchmove', handleDragMove)
                document.removeEventListener('mouseup', handleDragEnd)
                document.removeEventListener('touchend', handleDragEnd)
            }, 10)
        }

        element.addEventListener('mousedown', handleDragStart)
        element.addEventListener('touchstart', handleDragStart)
    },

    dispose: el => {

    }
}
