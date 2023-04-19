import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"
import { drag } from "./base/utility.js"

export class ImagePreviewer extends BlazorComponent {
    _init() {
        this._prevList = this._config.arguments[0] || []
        this._body = document.querySelector('body')
        this._prevImg = this._element.querySelector('.bb-viewer-canvas > img')
        this._closeButton = this._element.querySelector('.bb-viewer-close')
        this._zoomOut = this._element.querySelector('.plus-icon')
        this._zoomIn = this._element.querySelector('.minus-icon')
        this._rotateLeft = this._element.querySelector('.rotate-left')
        this._rotateRight = this._element.querySelector('.rotate-right')
        this._fullScreen = this._element.querySelector('.bb-viewer-full-screen')
        this._mask = this._element.querySelector('.bb-viewer-mask')

        this._store = {
            scale: 1
        }

        // 放大缩小配置
        this._options = { max: null, min: 0.195 }

        this._setListeners()
    }

    show(index = 0) {
        this._index = index
        this._originX = 0
        this._originY = 0
        this._pt = { top: 0, left: 0 }
        this._updateImage(this._index)

        // // 消除 body 滚动条
        this._body.classList.add('is-img-preview')
        this._element.classList.add('show')
    }

    _setListeners() {
        // 关闭按钮处理事件
        EventHandler.on(this._closeButton, 'click', () => {
            this._body.classList.remove('is-img-preview')
            // 恢复 Image
            this._resetImage()
            this._element.classList.remove('show')
        })

        // 上一张按钮处理事件
        EventHandler.on(this._element, 'click', '.bb-viewer-prev', () => {
            this._index--
            if (this._index < 0) {
                this._index = this._prevList.length - 1
            }
            this._updateImage(this._index)
        })

        // 下一张按钮处理事件
        EventHandler.on(this._element, 'click', '.bb-viewer-next', () => {
            this._index++
            if (this._index >= this._prevList.length) {
                this._index = 0
            }
            this._updateImage(this._index)
        })

        // 全屏/恢复按钮功能
        EventHandler.on(this._fullScreen, 'click', () => {
            this._resetImage()
            this._element.classList.toggle('active')
        })

        // 放大功能
        EventHandler.on(this._zoomOut, 'click', () => this._processImage(scale => scale + 0.2))

        // 缩小功能
        EventHandler.on(this._zoomIn, 'click', () => this._processImage(scale => Math.max(0.2, scale - 0.2)))

        // 左旋转功能
        EventHandler.on(this._rotateLeft, 'click', () => this._processImage(null, rotate => rotate - 90))

        // 右旋转功能
        EventHandler.on(this._rotateRight, 'click', () => this._processImage(null, rotate => rotate + 90))

        const handlerWheel = e => {
            e.preventDefault()
            const wheel = e.wheelDelta || -e.detail
            const delta = Math.max(-1, Math.min(1, wheel))
            if (delta > 0) {
                // 放大
                this._processImage(scale => scale + 0.015)
            } else {
                // 缩小
                this._processImage(scale => Math.max(0.195, scale - 0.015))
            }
        }
        // 鼠标放大缩小
        EventHandler.on(this._prevImg, 'mousewheel', handlerWheel)
        EventHandler.on(this._prevImg, 'DOMMouseScroll', handlerWheel)

        // 点击遮罩关闭功能
        EventHandler.on(this._mask, 'click', () => {
            this._closeButton.click()
        })

        // 增加键盘支持
        EventHandler.on(document, 'keydown', e => {
            if (e.key === "ArrowUp") {
                this._zoomOut.click()
            } else if (e.key === "ArrowDown") {
                this._zoomIn.click()
            } else if (e.key === "ArrowLeft") {
                const prevButton = this._element.querySelector('.bb-viewer-prev')
                if(prevButton) {
                    prevButton.click()
                }
            } else if (e.key === "ArrowRight") {
                const nextButton = this._element.querySelector('.bb-viewer-next')
                if(nextButton) {
                    nextButton.click()
                }
            } else if (e.key === "Escape") {
                this._closeButton.click()
            }
        })

        // 缩放处理
        EventHandler.on(this._prevImg, 'touchstart', e => {
            e.preventDefault()

            const touches = e.touches
            const events = touches[0]
            const events2 = touches[1]

            this._store.pageX = events.pageX
            this._store.pageY = events.pageY
            this._store.moveable = true

            if (events2) {
                this._store.pageX2 = events2.pageX
                this._store.pageY2 = events2.pageY
            }

            this._store.originScale = this._store.scale || 1
        })

        EventHandler.on(this._prevImg, 'touchmove', e => {
            if (!this._store.moveable) {
                return
            }

            const touches = e.touches
            const events = touches[0]
            const events2 = touches[1]

            if (events2) {
                e.preventDefault()
                if (!this._prevImg.classList.contains('transition-none')) {
                    this._prevImg.classList.add('transition-none')
                }

                if (!this._store.pageX2) {
                    this._store.pageX2 = events2.pageX
                }
                if (!this._store.pageY2) {
                    this._store.pageY2 = events2.pageY
                }

                const getDistance = (start, stop) => Math.hypot(stop.x - start.x, stop.y - start.y)

                const zoom = getDistance({
                    x: events.pageX,
                    y: events.pageY
                }, {
                    x: events2.pageX,
                    y: events2.pageY
                }) /
                    getDistance({
                        x: this._store.pageX,
                        y: this._store.pageY
                    }, {
                        x: this._store.pageX2,
                        y: this._store.pageY2
                    })

                let newScale = this._store.originScale * zoom

                if (this._options.max != null && newScale > this._options.max) {
                    newScale = this._options.max
                }

                if (this._options.min != null && newScale < this._options.min) {
                    newScale = this._options.min
                }

                this._store.scale = newScale

                this._processImage(() => newScale)
            }
        })

        const touchEndHandler = () => {
            this._store.moveable = false
            this._prevImg.classList.remove('transition-none')

            delete this._store.pageX2
            delete this._store.pageY2
        }

        EventHandler.on(this._prevImg, 'touchend', touchEndHandler)

        EventHandler.on(this._prevImg, 'touchcancel', touchEndHandler)

        drag(this._prevImg,
            e => {
                this._originX = e.clientX || e.touches[0].clientX
                this._originY = e.clientY || e.touches[0].clientY

                // 偏移量
                this._pt.top = parseInt(this._prevImg.style.marginTop)
                this._pt.left = parseInt(this._prevImg.style.marginLeft)

                this._prevImg.classList.add('is-drag')
            },
            e => {
                if (this._prevImg.classList.contains('is-drag')) {
                    const eventX = e.clientX || e.changedTouches[0].clientX
                    const eventY = e.clientY || e.changedTouches[0].clientY

                    const newValX = this._pt.left + Math.ceil(eventX - this._originX)
                    const newValY = this._pt.top + Math.ceil(eventY - this._originY)

                    this._prevImg.style.marginLeft = `${newValX}px`
                    this._prevImg.style.marginTop = `${newValY}px`
                }

            },
            () => {
                this._prevImg.classList.remove('is-drag')
            })
    }

    _resetImage() {
        this._prevImg.classList.add('transition-none')
        this._prevImg.style.transform = 'scale(1) rotate(0deg)'
        this._prevImg.style.marginLeft = '0px'
        this._prevImg.style.marginTop = '0px'
        const handler = window.setTimeout(() => {
            window.clearTimeout(handler)
            this._prevImg.classList.remove('transition-none')
        }, 300)
    }

    _updateImage(index) {
        this._resetImage()
        const url = this._prevList[index]
        const img = this._element.querySelector('.bb-viewer-canvas > img')
        img.setAttribute('src', url)
    }

    _processImage(scaleCallback, rotateCallback) {
        const getScale = v => {
            let scale = 1
            if (v) {
                const arr = v.split(' ')
                scale = parseFloat(arr[0].split('(')[1])
            }
            return scale
        }

        const getRotate = v => {
            let rotate = 0
            if (v) {
                const arr = v.split(' ')
                rotate = parseFloat(arr[1].split('(')[1])
            }
            return rotate
        }

        const getValue = v => {
            let value = 0
            if (v) {
                value = parseFloat(v)
            }
            return value
        }

        const transform = this._prevImg.style.transform
        let scale = getScale(transform)
        let rotate = getRotate(transform)
        if (scaleCallback) {
            scale = scaleCallback(scale)
        }
        if (rotateCallback) {
            rotate = rotateCallback(rotate)
        }
        this._prevImg.style.transform = `scale(${scale}) rotate(${rotate}deg)`

        const left = getValue(this._prevImg.style.marginLeft)
        const top = getValue(this._prevImg.style.marginTop)
        this._prevImg.style.marginLeft = `${left}px`
        this._prevImg.style.marginTop = `${top}px`
    }

    _execute(args) {
        this._prevList = args[0]
    }

    _dispose() {
        this._element.classList.remove('show')

        // 关闭按钮处理事件
        EventHandler.off(this._closeButton, 'click')

        // 上一张按钮处理事件
        EventHandler.off(this._element, 'click', '.bb-viewer-prev')

        // 下一张按钮处理事件
        EventHandler.off(this._element, 'click', '.bb-viewer-next')

        // 全屏/恢复按钮功能
        EventHandler.off(this._fullScreen, 'click')

        // 放大功能
        EventHandler.off(this._zoomOut, 'click')

        // 缩小功能
        EventHandler.off(this._zoomIn, 'click')

        // 左旋转功能
        EventHandler.off(this._rotateLeft, 'click')

        // 右旋转功能
        EventHandler.off(this._rotateRight, 'click')

        // 鼠标放大缩小
        EventHandler.off(this._prevImg, 'mousewheel')
        EventHandler.off(this._prevImg, 'DOMMouseScroll')

        // 触摸放大缩小
        EventHandler.off(this._prevImg, 'touchstart')
        EventHandler.off(this._prevImg, 'touchmove')
        EventHandler.off(this._prevImg, 'touchend')
        EventHandler.off(this._prevImg, 'touchcancel')

        //drag
        EventHandler.off(this._prevImg, 'mousedown')
        EventHandler.off(this._prevImg, 'touchstart')

        // 点击遮罩关闭功能
        EventHandler.off(this._mask, 'click')

        // 增加键盘支持
        EventHandler.off(document, 'keydown')
    }
}
