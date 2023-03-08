import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"
import { drag, getHeight, getWidth } from "./base/utility.js"

export class Modal extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._invokerShownMethod = this._config.arguments[1]
        this._invokerCloseMethod = this._config.arguments[2]
        this._setEventListeners()
    }

    _setEventListeners() {
        EventHandler.on(this._element, 'shown.bs.modal', () => {
            this._invoker.invokeMethodAsync(this._invokerShownMethod)
        })
        EventHandler.on(this._element, 'hide.bs.modal', e => {
            e.stopPropagation();
            if (this._draggable) {
                this._dialog.style.width = ''
                this._dialog.style.margin = ''

                EventHandler.off(this._dialog, 'mousedown')
                EventHandler.off(this._dialog, 'touchstart')
            }
            this._invoker.invokeMethodAsync(this._invokerCloseMethod)
        })

        this._pop = () => {
            if (this._modal) {
                this._modal._dialog.remove()
                this._modal.dispose()
                this._modal = null
                document.body.classList.remove('modal-open');
                document.body.style.paddingRight = '';
                document.body.style.overflow = '';
            }
        }
        EventHandler.on(window, 'popstate', this._pop)
    }

    _execute(args) {
        const method = args[1]
        if (method === 'show') {
            this._show()
        } else if (method === 'hide') {
            this._hide()
        } else if (method === 'toggle') {
            this._toggle()
        }
    }

    _show() {
        const dialogs = this._element.querySelectorAll('.modal-dialog')
        if (dialogs.length === 1) {
            let backdrop = this._element.getAttribute('data-bs-backdrop') !== 'static'
            if (!backdrop) {
                backdrop = 'static'
            }
            if (!this._modal) {
                this._modal = bootstrap.Modal.getOrCreateInstance(this._element, { focus: false })
            }
            this._modal._config.keyboard = this._element.getAttribute('data-bs-keyboard') === 'true'
            this._modal._config.backdrop = backdrop
            this._modal.show()
        } else {
            this._invoker.invokeMethodAsync(this._invokerShownMethod)

            this._modal._config.keyboard = false
            this._modal._config.backdrop = 'static'

            this._handlerKeyboardAndBackdrop()
        }

        this._dialog = dialogs[dialogs.length - 1]
        this._draggable = this._dialog.classList.contains('is-draggable')
        if (this._draggable) {
            this._disposeDrag()

            this._originX = 0;
            this._originY = 0;
            this._dialogWidth = 0;
            this._dialogHeight = 0;
            this._pt = { top: 0, left: 0 };

            this._header = this._dialog.querySelector('.modal-header')
            drag(this._header,
                e => {
                    if (e.srcElement.closest('.modal-header-buttons')) {
                        return true
                    }
                    this._originX = e.clientX || e.touches[0].clientX;
                    this._originY = e.clientY || e.touches[0].clientY;

                    const rect = this._dialog.querySelector('.modal-content').getBoundingClientRect()
                    this._dialogWidth = rect.width
                    this._dialogHeight = rect.height
                    this._pt.top = rect.top
                    this._pt.left = rect.left

                    this._dialog.style.margin = `${this._pt.top}px 0 0 ${this._pt.left}px`
                    this._dialog.style.width = `${this._dialogWidth}px`
                    this._dialog.classList.add('is-drag')
                },
                e => {
                    if (this._dialog.classList.contains('is-drag')) {
                        const eventX = e.clientX || e.changedTouches[0].clientX;
                        const eventY = e.clientY || e.changedTouches[0].clientY;

                        let newValX = this._pt.left + Math.ceil(eventX - this._originX);
                        let newValY = this._pt.top + Math.ceil(eventY - this._originY);

                        if (newValX <= 0) newValX = 0;
                        if (newValY <= 0) newValY = 0;

                        if (newValX + this._dialogWidth < window.innerWidth) {
                            this._dialog.style.marginLeft = `${newValX}px`
                        }
                        if (newValY + this._dialogHeight < window.innerHeight) {
                            this._dialog.style.marginTop = `${newValY}px`
                        }
                    }
                },
                e => {
                    this._dialog.classList.remove('is-drag')
                })
        }
    }

    _handlerKeyboardAndBackdrop() {
        if (!this._hook_keyboard_backdrop) {
            this._hook_keyboard_backdrop = true;

            this._handlerEscape = e => {
                if (e.key === 'Escape') {
                    const keyboard = this._element.getAttribute('data-bs-keyboard')
                    if (keyboard === 'true') {
                        this._hide()
                    }
                }
            }

            EventHandler.on(document, 'keyup', this._handlerEscape)
            EventHandler.on(this._element, 'click', e => {
                if (e.target.closest('.modal-dialog') === null) {
                    const backdrop = this._element.getAttribute('data-bs-backdrop')
                    if (backdrop !== 'static') {
                        this._hide()
                    }
                }
            })
        }
    }

    _hide() {
        if (this._element.children.length === 1) {
            this._modal.hide()
        } else {
            this._invoker.invokeMethodAsync(this._invokerCloseMethod)
        }
    }

    _toggle() {
        if (this._modal) {
            this._modal.toggle()
        } else {
            this._show()
        }
    }

    _disposeDrag() {
        if (this._header) {
            EventHandler.off(this._header, 'mousedown')
            EventHandler.off(this._header, 'touchstart')
            this._header = null
        }
    }

    _dispose() {
        if (this._draggable) {
            this._disposeDrag()
        }

        EventHandler.off(this._element, 'shown.bs.modal')
        EventHandler.off(this._element, 'hide.bs.modal')
        EventHandler.off(this._element, 'click')

        if (this._hook_keyboard_backdrop) {
            EventHandler.off(document, 'keyup', this._handlerEscape)
        }

        EventHandler.off(window, 'popstate', this._pop)
        if (this._modal) {
            this._modal.dispose()
        }
    }
}
