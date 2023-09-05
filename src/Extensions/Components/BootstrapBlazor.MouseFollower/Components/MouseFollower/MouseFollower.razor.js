import { addLink, addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import EventHandler from '../../../BootstrapBlazor/modules/event-handler.js'

const setNormal = (cursor, op) => {
    const el = op.options.container
    EventHandler.on(el, 'mouseenter', () => {
        cursor.show()
    })

    EventHandler.on(el, 'mouseleave', () => {
        cursor.hide()
    })

    EventHandler.on(el, 'mousedown', () => {
        cursor.addState(op.options.activeState)
    })

    EventHandler.on(el, 'mouseup', () => {
        cursor.removeState(op.options.activeState)
    })

    EventHandler.on(el, 'mousemoveOnce', () => {
        cursor.show()
    })
}

const setText = (cursor, op) => {
    const el = op.options.container
    EventHandler.on(el, 'mouseenter', () => {
        cursor.setText(op.content)
    })

    EventHandler.on(el, 'mouseleave', () => {
        cursor.removeText()
    })
}

const setIcon = (cursor, op) => {
    const el = op.options.container
    EventHandler.on(el, 'mouseenter', () => {
        cursor.setIcon(op.content)
    })

    EventHandler.on(el, 'mouseleave', () => {
        cursor.removeIcon()
    })
}

const setImage = (cursor, op) => {
    const el = op.options.container
    EventHandler.on(el, 'mouseenter', () => {
        cursor.setImg(op.content)
    })

    EventHandler.on(el, 'mouseleave', () => {
        cursor.removeImg()
    })
}

const setVideo = (cursor, op) => {
    const el = op.options.container
    EventHandler.on(el, 'mouseenter', () => {
        cursor.setVideo(op.content)
    })

    EventHandler.on(el, 'mouseleave', () => {
        cursor.removeVideo()
    })
}

export async function init(id, op) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    await addLink("./_content/BootstrapBlazor.MouseFollower/mouse-follower.min.css")
    await addScript("./_content/BootstrapBlazor/modules/gsap.min.js")
    await addScript("./_content/BootstrapBlazor.MouseFollower/mouse-follower.min.js")

    op.options.container = op.global ? document.body : el
    const cursor = new MouseFollower(op.options)
    Data.set(id, { el, cursor, op })

    const mode = op.mode
    if (mode === 'normal') {
        setNormal(cursor, op)
    }
    else if (mode === 'image') {
        setImage(cursor, op)
    }
    else if (mode === 'text') {
        setText(cursor, op)
    }
    else if (mode === 'icon') {
        setIcon(cursor, op)
    }
    else if (mode === 'video') {
        setVideo(cursor, op)
    }
}

export function dispose(id) {
    const mf = Data.get(id)
    Data.remove(id)

    if (mf) {
        mf.cursor.destroy()

        const el = mf.op.options.container
        const mode = mf.op.mode
        EventHandler.off(el, 'mouseenter')
        EventHandler.off(el, 'mouseleave')

        if (mode === 'normal') {
            EventHandler.off(el, 'mousedown')
            EventHandler.off(el, 'mouseup')
            EventHandler.off(el, 'mousemoveOnce')
        }
    }
}
