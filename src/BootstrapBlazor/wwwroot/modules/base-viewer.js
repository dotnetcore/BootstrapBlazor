import { drag } from "./utility.js"
import EventHandler from "./event-handler.js"

export default {
    init(el, prevList, config) {
        const previewer = {
            ...{
                el,
                prevList,
                body: document.querySelector('body'),
                prevImg: el.querySelector('.bb-viewer-canvas > img'),
                closeButton: el.querySelector('.bb-viewer-close'),
                zoomOut: el.querySelector('.plus-icon'),
                zoomIn: el.querySelector('.minus-icon'),
                rotateLeft: el.querySelector('.rotate-left'),
                rotateRight: el.querySelector('.rotate-right'),
                fullScreen: el.querySelector('.bb-viewer-full-screen'),
                mask: el.querySelector('.bb-viewer-mask'),
                store: {
                    scale: 1
                },
                options: { max: null, min: 0.195 }
            },
            ...config || {}
        }
        previewer.show = index => {
            index = index || 0
            viewer.index = index
            viewer.originX = 0
            viewer.originY = 0
            viewer.pt = { top: 0, left: 0 }
            viewer.updateImage(viewer.index)

            // // 消除 body 滚动条
            viewer.body.classList.add('is-img-preview')
            viewer.element.classList.add('show')
        }

        previewer.resetImage = () => {
            viewer.prevImg.classList.add('transition-none')
            viewer.prevImg.style.transform = 'scale(1) rotate(0deg)'
            viewer.prevImg.style.marginLeft = '0px'
            viewer.prevImg.style.marginTop = '0px'
            const handler = window.setTimeout(() => {
                window.clearTimeout(handler)
                viewer.prevImg.classList.remove('transition-none')
            }, 300)
        }

        previewer.updateImage = index => {
            viewer.resetImage()
            const url = viewer.prevList[index]
            const img = viewer.element.querySelector('.bb-viewer-canvas > img')
            img.setAttribute('src', url)
        }

        previewer.processImage = (scaleCallback, rotateCallback) => {
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

            const transform = previewer.prevImg.style.transform
            let scale = getScale(transform)
            let rotate = getRotate(transform)
            if (scaleCallback) {
                scale = scaleCallback(scale)
            }
            if (rotateCallback) {
                rotate = rotateCallback(rotate)
            }
            previewer.prevImg.style.transform = `scale(${scale}) rotate(${rotate}deg)`

            const left = getValue(previewer.prevImg.style.marginLeft)
            const top = getValue(previewer.prevImg.style.marginTop)
            previewer.prevImg.style.marginLeft = `${left}px`
            previewer.prevImg.style.marginTop = `${top}px`
        }

        // 关闭按钮处理事件
        EventHandler.on(previewer.closeButton, 'click', () => {
            previewer.body.classList.remove('is-img-preview')
            // 恢复 Image
            previewer.resetImage()
            previewer.element.classList.remove('show')
        })

        // 上一张按钮处理事件
        EventHandler.on(el, 'click', '.bb-viewer-prev', () => {
            previewer.index--
            if (previewer.index < 0) {
                previewer.index = previewer.prevList.length - 1
            }
            previewer.updateImage(viewer.index)
        })

        // 下一张按钮处理事件
        EventHandler.on(el, 'click', '.bb-viewer-next', () => {
            previewer.index++
            if (previewer.index >= previewer.prevList.length) {
                previewer.index = 0
            }
            previewer.updateImage(previewer.index)
        })

        // 全屏/恢复按钮功能
        EventHandler.on(previewer.fullScreen, 'click', () => {
            previewer.resetImage()
            previewer.element.classList.toggle('active')
        })

        // 放大功能
        EventHandler.on(previewer.zoomOut, 'click', () => viewer.processImage(scale => scale + 0.2))

        // 缩小功能
        EventHandler.on(previewer.zoomIn, 'click', () => viewer.processImage(scale => Math.max(0.2, scale - 0.2)))

        // 左旋转功能
        EventHandler.on(previewer.rotateLeft, 'click', () => viewer.processImage(null, rotate => rotate - 90))

        // 右旋转功能
        EventHandler.on(previewer.rotateRight, 'click', () => viewer.processImage(null, rotate => rotate + 90))

        const handlerWheel = e => {
            e.preventDefault()
            const wheel = e.wheelDelta || -e.detail
            const delta = Math.max(-1, Math.min(1, wheel))
            if (delta > 0) {
                // 放大
                previewer.processImage(scale => scale + 0.015)
            } else {
                // 缩小
                previewer.processImage(scale => Math.max(0.195, scale - 0.015))
            }
        }
        // 鼠标放大缩小
        EventHandler.on(previewer.prevImg, 'mousewheel', handlerWheel)
        EventHandler.on(previewer.prevImg, 'DOMMouseScroll', handlerWheel)

        // 点击遮罩关闭功能
        EventHandler.on(previewer.mask, 'click', () => {
            viewer.closeButton.click()
        })

        // 增加键盘支持
        EventHandler.on(document, 'keydown', e => {
            if (e.key === "ArrowUp") {
                previewer.zoomOut.click()
            } else if (e.key === "ArrowDown") {
                previewer.zoomIn.click()
            } else if (e.key === "ArrowLeft") {
                const prevButton = previewer.element.querySelector('.bb-viewer-prev')
                if (prevButton) {
                    prevButton.click()
                }
            } else if (e.key === "ArrowRight") {
                const nextButton = previewer.element.querySelector('.bb-viewer-next')
                if (nextButton) {
                    nextButton.click()
                }
            } else if (e.key === "Escape") {
                previewer.closeButton.click()
            }
        })

        // 缩放处理
        EventHandler.on(previewer.prevImg, 'touchstart', e => {
            e.preventDefault()

            const touches = e.touches
            const events = touches[0]
            const events2 = touches[1]

            previewer.store.pageX = events.pageX
            previewer.store.pageY = events.pageY
            previewer.store.moveable = true

            if (events2) {
                previewer.store.pageX2 = events2.pageX
                previewer.store.pageY2 = events2.pageY
            }

            previewer.store.originScale = previewer.store.scale || 1
        })

        EventHandler.on(previewer.prevImg, 'touchmove', e => {
            if (!previewer.store.moveable) {
                return
            }

            const touches = e.touches
            const events = touches[0]
            const events2 = touches[1]

            if (events2) {
                e.preventDefault()
                if (!previewer.prevImg.classList.contains('transition-none')) {
                    previewer.prevImg.classList.add('transition-none')
                }

                if (!previewer.store.pageX2) {
                    previewer.store.pageX2 = events2.pageX
                }
                if (!previewer.store.pageY2) {
                    previewer.store.pageY2 = events2.pageY
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
                        x: previewer.store.pageX,
                        y: previewer.store.pageY
                    }, {
                        x: previewer.store.pageX2,
                        y: previewer.store.pageY2
                    })

                let newScale = previewer.store.originScale * zoom

                if (previewer.options.max != null && newScale > previewer.options.max) {
                    newScale = previewer.options.max
                }

                if (previewer.options.min != null && newScale < previewer.options.min) {
                    newScale = previewer.options.min
                }

                previewer.store.scale = newScale

                previewer.processImage(() => newScale)
            }
        })

        const touchEndHandler = () => {
            previewer.store.moveable = false
            previewer.prevImg.classList.remove('transition-none')

            delete previewer.store.pageX2
            delete previewer.store.pageY2
        }

        EventHandler.on(previewer.prevImg, 'touchend', touchEndHandler)

        EventHandler.on(previewer.prevImg, 'touchcancel', touchEndHandler)

        drag(previewer.prevImg,
            e => {
                previewer.originX = e.clientX || e.touches[0].clientX
                previewer.originY = e.clientY || e.touches[0].clientY

                // 偏移量
                previewer.pt.top = parseInt(previewer.prevImg.style.marginTop)
                previewer.pt.left = parseInt(previewer.prevImg.style.marginLeft)

                previewer.prevImg.classList.add('is-drag')
            },
            e => {
                if (previewer.prevImg.classList.contains('is-drag')) {
                    const eventX = e.clientX || e.changedTouches[0].clientX
                    const eventY = e.clientY || e.changedTouches[0].clientY

                    const newValX = previewer.pt.left + Math.ceil(eventX - previewer.originX)
                    const newValY = previewer.pt.top + Math.ceil(eventY - previewer.originY)

                    previewer.prevImg.style.marginLeft = `${newValX}px`
                    previewer.prevImg.style.marginTop = `${newValY}px`
                }

            },
            () => {
                previewer.prevImg.classList.remove('is-drag')
            }
        )

        return previewer
    },

    dispose(viewer) {

        // 关闭按钮处理事件
        EventHandler.off(viewer.closeButton, 'click')

        // 上一张按钮处理事件
        EventHandler.off(viewer.element, 'click', '.bb-viewer-prev')

        // 下一张按钮处理事件
        EventHandler.off(viewer.element, 'click', '.bb-viewer-next')

        // 全屏/恢复按钮功能
        EventHandler.off(viewer.fullScreen, 'click')

        // 放大功能
        EventHandler.off(viewer.zoomOut, 'click')

        // 缩小功能
        EventHandler.off(viewer.zoomIn, 'click')

        // 左旋转功能
        EventHandler.off(viewer.rotateLeft, 'click')

        // 右旋转功能
        EventHandler.off(viewer.rotateRight, 'click')

        // 鼠标放大缩小
        EventHandler.off(viewer.prevImg, 'mousewheel')
        EventHandler.off(viewer.prevImg, 'DOMMouseScroll')

        // 触摸放大缩小
        EventHandler.off(viewer.prevImg, 'touchstart')
        EventHandler.off(viewer.prevImg, 'touchmove')
        EventHandler.off(viewer.prevImg, 'touchend')
        EventHandler.off(viewer.prevImg, 'touchcancel')

        //drag
        EventHandler.off(viewer.prevImg, 'mousedown')
        EventHandler.off(viewer.prevImg, 'touchstart')

        // 点击遮罩关闭功能
        EventHandler.off(viewer.mask, 'click')

        // 增加键盘支持
        EventHandler.off(document, 'keydown')
    }
}
