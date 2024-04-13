import EventHandler from "./event-handler.js"

export default {
    drag: (el, start, move, end) => {
        const handleDragStart = e => {
            let notDrag = start(e) || false
            if (!notDrag) {
                e.preventDefault()
                e.stopPropagation()

                EventHandler.on(document, 'mousemove', handleDragMove)
                EventHandler.on(document, 'touchmove', handleDragMove)
                EventHandler.on(document, 'mouseup', handleDragEnd)
                EventHandler.on(document, 'touchend', handleDragEnd)
            }
        }

        const handleDragMove = e => {
            if (e.touches && e.touches.length > 1) {
                return
            }
            move(e)
        }

        const handleDragEnd = e => {
            end(e)
            const handler = setTimeout(() => {
                clearTimeout(handler)
                EventHandler.off(document, 'mousemove', handleDragMove)
                EventHandler.off(document, 'touchmove', handleDragMove)
                EventHandler.off(document, 'mouseup', handleDragEnd)
                EventHandler.off(document, 'touchend', handleDragEnd)
            }, 10)
        }

        EventHandler.on(el, 'mousedown', handleDragStart)
        EventHandler.on(el, 'touchstart', handleDragStart)
    },

    dispose: el => {
        if (el) {
            EventHandler.off(el, 'mousedown')
            EventHandler.off(el, 'touchstart')
        }
    }
}
