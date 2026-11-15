import assert from 'node:assert/strict'
import { readFile } from 'node:fs/promises'
import { test } from 'node:test'

const sourceUrl = new URL('../../src/BootstrapBlazor/Components/Modal/Modal.razor.js', import.meta.url)
const modulesUrl = new URL('../../src/BootstrapBlazor/wwwroot/modules/', import.meta.url)
const source = (await readFile(sourceUrl, 'utf8')).replaceAll('../../modules/', modulesUrl.href)
const { init, execute, dispose } = await import(`data:text/javascript;base64,${Buffer.from(source).toString('base64')}`)

function createDialog(hidden = false) {
    return { classList: { contains: name => hidden && name === 'd-none' } }
}

function createModal(t, dialogCount, options = {}) {
    const el = new EventTarget()
    const classes = new Set()
    const attributes = new Map([
        ['data-bs-keyboard', 'true'],
        ['data-bs-backdrop', 'static']
    ])
    el.dialogs = Array.from({ length: dialogCount }, () => createDialog())
    Object.defineProperty(el, 'children', { get: () => el.dialogs })
    el.querySelectorAll = () => el.dialogs
    el.getAttribute = name => attributes.get(name) ?? null
    el.closest = () => null
    el.classList = {
        add: name => classes.add(name),
        remove: name => classes.delete(name),
        contains: name => classes.has(name)
    }
    el.style = { display: 'none' }

    globalThis.window = new EventTarget()
    globalThis.document = new EventTarget()
    document.getElementById = () => el
    document.body = {
        style: { cssText: '' },
        hasAttribute: () => false,
        classList: { contains: () => false }
    }

    const callbacks = []
    let creations = 0
    let shows = 0
    let backdropTransitions = 0
    let nativeKeydowns = 0
    let nativeMousedowns = 0
    const instance = {
        _config: {},
        _focustrap: {},
        _isShown: false,
        _isTransitioning: false,
        show() {
            if (this._isShown || this._isTransitioning) {
                return
            }
            shows++
            this._isShown = true
            this._isTransitioning = true
            el.style.display = 'block'
        },
        hide() {
            if (!this._isShown || this._isTransitioning) {
                return
            }
            if (!el.dispatchEvent(new Event('hide.bs.modal', { cancelable: true }))) {
                return
            }
            this._isShown = false
            this._isTransitioning = true
        },
        _triggerBackdropTransition() {
            if (!classes.has('modal-static')) {
                classes.add('modal-static')
                backdropTransitions++
            }
        },
        dispose() { }
    }
    globalThis.bootstrap = {
        Modal: {
            getOrCreateInstance() {
                creations++
                el.addEventListener('keydown', e => {
                    nativeKeydowns++
                    if (e.key === 'Escape') {
                        if (instance._config.keyboard) {
                            instance.hide()
                        }
                        else {
                            instance._triggerBackdropTransition()
                        }
                    }
                })
                el.addEventListener('mousedown', e => {
                    nativeMousedowns++
                    el.addEventListener('click', click => {
                        if (e.target === el && click.target === el) {
                            if (instance._config.backdrop === 'static') {
                                instance._triggerBackdropTransition()
                            }
                            else if (instance._config.backdrop) {
                                instance.hide()
                            }
                        }
                    }, { once: true })
                })
                return instance
            }
        }
    }

    init('modal-test', {
        invokeMethodAsync(name) {
            callbacks.push(name)
            if (name === 'CloseCallback') {
                return Promise.resolve(options.onClose?.())
            }
            return Promise.resolve(name === 'BeforeCloseCallback' ? options.canClose !== false : undefined)
        }
    }, 'ShownCallback', 'CloseCallback')

    t.after(() => {
        dispose('modal-test')
        delete globalThis.window
        delete globalThis.document
        delete globalThis.bootstrap
    })

    return {
        el,
        instance,
        callbacks,
        attributes,
        get creations() { return creations },
        get shows() { return shows },
        get backdropTransitions() { return backdropTransitions },
        get nativeKeydowns() { return nativeKeydowns },
        get nativeMousedowns() { return nativeMousedowns },
        show: () => execute('modal-test', 'show'),
        waitForHide: () => execute('modal-test', 'waitForHide'),
        finishShowing() {
            instance._isTransitioning = false
            el.dispatchEvent(new Event('shown.bs.modal'))
        },
        finishHiding() {
            instance._isTransitioning = false
            el.style.display = 'none'
            el.dispatchEvent(new Event('hidden.bs.modal'))
        }
    }
}

test('shows the outer modal when rapid first-use calls render multiple dialogs', t => {
    const modal = createModal(t, 2)

    modal.show()

    assert.equal(modal.creations, 1)
    assert.equal(modal.shows, 1)
    assert.equal(modal.el.style.display, 'block')
    assert.deepEqual(modal.callbacks, [])

    modal.finishShowing()

    assert.deepEqual(modal.callbacks, ['ShownCallback'])
})

test('reopens a hidden modal when rapid calls render multiple dialogs', async t => {
    const modal = createModal(t, 1)
    modal.show()
    modal.finishShowing()
    modal.instance.hide()
    const hidden = modal.waitForHide()
    modal.finishHiding()
    await hidden
    modal.el.dialogs.push(createDialog())

    modal.show()

    assert.equal(modal.creations, 1)
    assert.equal(modal.shows, 2)
    assert.equal(modal.el.style.display, 'block')
})

test('notifies a nested dialog without reopening the visible outer modal', t => {
    const modal = createModal(t, 1)
    modal.show()
    modal.finishShowing()
    modal.el.dialogs.push(createDialog())

    modal.show()

    assert.equal(modal.shows, 1)
    assert.deepEqual(modal.callbacks, ['ShownCallback', 'ShownCallback'])
})

test('waits for the outer show transition before notifying rapid calls', t => {
    const modal = createModal(t, 1)
    modal.show()
    modal.el.dialogs.push(createDialog())

    modal.show()

    assert.equal(modal.shows, 1)
    assert.deepEqual(modal.callbacks, [])

    modal.finishShowing()

    assert.deepEqual(modal.callbacks, ['ShownCallback'])
})

test('targets the top visible dialog instead of a hidden previous or trailing dialog', t => {
    const modal = createModal(t, 3)
    modal.el.dialogs[0] = createDialog(true)
    modal.el.dialogs[2] = createDialog(true)

    modal.show()

    assert.equal(modal.instance._dialog, modal.el.dialogs[1])
})

test('does not initialize an empty modal or one without a visible dialog', t => {
    const modal = createModal(t, 0)
    modal.show()
    modal.el.dialogs.push(createDialog(true))
    modal.show()

    assert.equal(modal.creations, 0)
    assert.equal(modal.shows, 0)
    assert.deepEqual(modal.callbacks, [])
})

test('waits for the hide transition and close callback before allowing another show', async t => {
    let finishClose
    const closing = new Promise(resolve => finishClose = resolve)
    const modal = createModal(t, 1, { onClose: () => closing })
    modal.show()
    modal.finishShowing()
    modal.instance.hide()
    let hidden = false
    const waiting = modal.waitForHide().then(() => hidden = true)
    await Promise.resolve()
    assert.equal(hidden, false)

    modal.finishHiding()
    await Promise.resolve()
    assert.equal(hidden, false)

    finishClose()
    await waiting
    assert.equal(hidden, true)
    modal.show()
    assert.equal(modal.shows, 2)
})

test('allows an OnClose callback to open another dialog without waiting on itself', async t => {
    let modal
    modal = createModal(t, 1, {
        onClose: async () => {
            await modal.waitForHide()
            modal.show()
        }
    })
    modal.show()
    modal.finishShowing()
    modal.instance.hide()
    const hidden = modal.waitForHide()
    modal.finishHiding()
    await hidden

    assert.equal(modal.shows, 2)
    assert.equal(modal.el.style.display, 'block')
})

test('does not wait forever when a native hide event is canceled', async t => {
    const modal = createModal(t, 1)
    modal.show()
    modal.finishShowing()
    modal.el.addEventListener('hide.bs.modal', e => e.preventDefault())

    modal.instance.hide()
    await modal.waitForHide()

    assert.equal(modal.instance._isShown, true)
    assert.equal(modal.el.style.display, 'block')
})

test('releases pending hide waits when the component is disposed', async t => {
    const modal = createModal(t, 1)
    modal.show()
    modal.finishShowing()
    modal.instance.hide()
    const hidden = modal.waitForHide()

    dispose('modal-test')
    await hidden
})

test('prevents native Escape feedback while closing an enabled nested layer', async t => {
    const modal = createModal(t, 2)
    modal.show()
    modal.finishShowing()

    const keydown = new Event('keydown')
    keydown.key = 'Escape'
    modal.el.dispatchEvent(keydown)
    const keyup = new Event('keyup')
    keyup.key = 'Escape'
    document.dispatchEvent(keyup)
    await Promise.resolve()

    assert.equal(modal.nativeKeydowns, 0)
    assert.equal(modal.backdropTransitions, 0)
    assert.deepEqual(modal.callbacks, ['ShownCallback', 'BeforeCloseCallback', 'CloseCallback'])
})

test('prevents native backdrop feedback while closing an enabled nested layer', async t => {
    const modal = createModal(t, 2)
    modal.attributes.delete('data-bs-backdrop')
    modal.show()
    modal.finishShowing()

    modal.el.dispatchEvent(new Event('mousedown'))
    modal.el.dispatchEvent(new Event('click'))
    await Promise.resolve()

    assert.equal(modal.nativeMousedowns, 0)
    assert.equal(modal.backdropTransitions, 0)
    assert.deepEqual(modal.callbacks, ['ShownCallback', 'BeforeCloseCallback', 'CloseCallback'])
})

test('preserves locked backdrop and keyboard behavior without closing the active layer', t => {
    const modal = createModal(t, 2)
    modal.attributes.set('data-bs-keyboard', 'false')
    modal.show()
    modal.finishShowing()
    const keydown = new Event('keydown')
    keydown.key = 'Escape'
    modal.el.dispatchEvent(keydown)
    const keyup = new Event('keyup')
    keyup.key = 'Escape'
    document.dispatchEvent(keyup)

    modal.el.dispatchEvent(new Event('click'))

    assert.equal(modal.nativeKeydowns, 1)
    assert.equal(modal.backdropTransitions, 1)
    assert.deepEqual(modal.callbacks, ['ShownCallback'])
})

test('does not intercept native dismissal after returning to a single layer', t => {
    const modal = createModal(t, 2)
    modal.show()
    modal.finishShowing()
    modal.el.dialogs.pop()
    modal.show()
    const keydown = new Event('keydown')
    keydown.key = 'Escape'
    modal.el.dispatchEvent(keydown)
    const keyup = new Event('keyup')
    keyup.key = 'Escape'
    document.dispatchEvent(keyup)
    modal.el.dispatchEvent(new Event('click'))

    assert.equal(modal.nativeKeydowns, 1)
    assert.equal(modal.instance._config.keyboard, true)
    assert.equal(modal.backdropTransitions, 0)
    assert.deepEqual(modal.callbacks, ['ShownCallback'])
})
