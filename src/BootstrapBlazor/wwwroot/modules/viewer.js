import Drag from "./drag.js"
import EventHandler from "./event-handler.js"

export default {
    init(el, prevList, config) {
        const viewer = {
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
        viewer.show = index => {
            index = index || 0
            viewer.index = index
            viewer.originX = 0
            viewer.originY = 0
            viewer.pt = { top: 0, left: 0 }
            viewer.updateImage(viewer.index)

            // // 消除 body 滚动条
            viewer.body.classList.add('is-img-preview')
            viewer.el.classList.add('show')
        }

        viewer.updatePrevList = prevList => {
            viewer.prevList = prevList
        }

        viewer.resetImage = () => {
            viewer.prevImg.classList.add('transition-none')
            viewer.prevImg.style.setProperty('transform', 'scale(1) rotate(0deg)');
            viewer.prevImg.style.setProperty('margin', '0');
            const handler = window.setTimeout(() => {
                window.clearTimeout(handler)
                viewer.prevImg.classList.remove('transition-none')
            }, 300)
        }

        viewer.updateImage = index => {
            viewer.resetImage()
            const url = viewer.prevList[index]
            const img = viewer.el.querySelector('.bb-viewer-canvas > img')
            img.setAttribute('src', url)
        }

        viewer.processImage = (scaleCallback, rotateCallback) => {
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

            const transform = viewer.prevImg.style.transform
            let scale = getScale(transform)
            let rotate = getRotate(transform)
            if (scaleCallback) {
                scale = scaleCallback(scale)
            }
            if (rotateCallback) {
                rotate = rotateCallback(rotate)
            }
            viewer.prevImg.style.transform = `scale(${scale}) rotate(${rotate}deg)`

            const left = getValue(viewer.prevImg.style.marginLeft)
            const top = getValue(viewer.prevImg.style.marginTop)
            viewer.prevImg.style.marginLeft = `${left}px`
            viewer.prevImg.style.marginTop = `${top}px`
        }

        // 关闭按钮处理事件
        EventHandler.on(viewer.closeButton, 'click', () => {
            viewer.body.classList.remove('is-img-preview')
            // 恢复 Image
            viewer.resetImage()
            viewer.el.classList.remove('show')
        })

        // 上一张按钮处理事件
        EventHandler.on(el, 'click', '.bb-viewer-prev', () => {
            viewer.index--
            if (viewer.index < 0) {
                viewer.index = viewer.prevList.length - 1
            }
            viewer.updateImage(viewer.index)
        })

        // 下一张按钮处理事件
        EventHandler.on(el, 'click', '.bb-viewer-next', () => {
            viewer.index++
            if (viewer.index >= viewer.prevList.length) {
                viewer.index = 0
            }
            viewer.updateImage(viewer.index)
        })

        // 全屏/恢复按钮功能
        EventHandler.on(viewer.fullScreen, 'click', () => {
            viewer.resetImage()
            viewer.el.classList.toggle('active')
        })

        // 放大功能
        EventHandler.on(viewer.zoomOut, 'click', () => viewer.processImage(scale => scale + 0.2))

        // 缩小功能
        EventHandler.on(viewer.zoomIn, 'click', () => viewer.processImage(scale => Math.max(0.2, scale - 0.2)))

        // 左旋转功能
        EventHandler.on(viewer.rotateLeft, 'click', () => viewer.processImage(null, rotate => rotate - 90))

        // 右旋转功能
        EventHandler.on(viewer.rotateRight, 'click', () => viewer.processImage(null, rotate => rotate + 90))

        const handlerWheel = e => {
            e.preventDefault()
            const wheel = e.wheelDelta || -e.detail
            const delta = Math.max(-1, Math.min(1, wheel))
            if (delta > 0) {
                // 放大
                viewer.processImage(scale => scale + 0.015)
            } else {
                // 缩小
                viewer.processImage(scale => Math.max(0.195, scale - 0.015))
            }
        }
        // 鼠标放大缩小
        EventHandler.on(viewer.prevImg, 'mousewheel', handlerWheel)
        EventHandler.on(viewer.prevImg, 'DOMMouseScroll', handlerWheel)

        // 点击遮罩关闭功能
        EventHandler.on(viewer.mask, 'click', () => {
            viewer.closeButton.click()
        })

        // 增加键盘支持
        EventHandler.on(document, 'keydown', e => {
            if (e.key === "ArrowUp") {
                viewer.zoomOut.click()
            } else if (e.key === "ArrowDown") {
                viewer.zoomIn.click()
            } else if (e.key === "ArrowLeft") {
                const prevButton = viewer.el.querySelector('.bb-viewer-prev')
                if (prevButton) {
                    prevButton.click()
                }
            } else if (e.key === "ArrowRight") {
                const nextButton = viewer.el.querySelector('.bb-viewer-next')
                if (nextButton) {
                    nextButton.click()
                }
            } else if (e.key === "Escape") {
                viewer.closeButton.click()
            }
        })

        // 缩放处理
        EventHandler.on(viewer.prevImg, 'touchstart', e => {
            e.preventDefault()

            const touches = e.touches
            const events = touches[0]
            const events2 = touches[1]

            viewer.store.pageX = events.pageX
            viewer.store.pageY = events.pageY
            viewer.store.moveable = true

            if (events2) {
                viewer.store.pageX2 = events2.pageX
                viewer.store.pageY2 = events2.pageY
            }

            viewer.store.originScale = viewer.store.scale || 1
        })

        EventHandler.on(viewer.prevImg, 'touchmove', e => {
            if (!viewer.store.moveable) {
                return
            }

            const touches = e.touches
            const events = touches[0]
            const events2 = touches[1]

            if (events2) {
                e.preventDefault()
                if (!viewer.prevImg.classList.contains('transition-none')) {
                    viewer.prevImg.classList.add('transition-none')
                }

                if (!viewer.store.pageX2) {
                    viewer.store.pageX2 = events2.pageX
                }
                if (!viewer.store.pageY2) {
                    viewer.store.pageY2 = events2.pageY
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
                        x: viewer.store.pageX,
                        y: viewer.store.pageY
                    }, {
                        x: viewer.store.pageX2,
                        y: viewer.store.pageY2
                    })

                let newScale = viewer.store.originScale * zoom

                if (viewer.options.max != null && newScale > viewer.options.max) {
                    newScale = viewer.options.max
                }

                if (viewer.options.min != null && newScale < viewer.options.min) {
                    newScale = viewer.options.min
                }

                viewer.store.scale = newScale

                viewer.processImage(() => newScale)
            }
        })

        const touchEndHandler = () => {
            viewer.store.moveable = false
            viewer.prevImg.classList.remove('transition-none')

            delete viewer.store.pageX2
            delete viewer.store.pageY2
        }

        EventHandler.on(viewer.prevImg, 'touchend', touchEndHandler)

        EventHandler.on(viewer.prevImg, 'touchcancel', touchEndHandler)

        Drag.drag(viewer.prevImg,
            e => {
                viewer.originX = e.clientX || e.touches[0].clientX
                viewer.originY = e.clientY || e.touches[0].clientY

                // 偏移量
                viewer.pt.top = parseInt(viewer.prevImg.style.marginTop)
                viewer.pt.left = parseInt(viewer.prevImg.style.marginLeft)

                viewer.prevImg.classList.add('is-drag')
            },
            e => {
                if (viewer.prevImg.classList.contains('is-drag')) {
                    const eventX = e.clientX || e.changedTouches[0].clientX
                    const eventY = e.clientY || e.changedTouches[0].clientY

                    const newValX = viewer.pt.left + Math.ceil(eventX - viewer.originX)
                    const newValY = viewer.pt.top + Math.ceil(eventY - viewer.originY)

                    viewer.prevImg.style.marginLeft = `${newValX}px`
                    viewer.prevImg.style.marginRight = `${-newValX}px`
                    viewer.prevImg.style.marginTop = `${newValY}px`
                    viewer.prevImg.style.marginBottom = `${-newValY}px`
                }

            },
            () => {
                viewer.prevImg.classList.remove('is-drag')
            }
        )

        return viewer
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
        Drag.dispose(viewer.prevImg)

        // 点击遮罩关闭功能
        EventHandler.off(viewer.mask, 'click')

        // 增加键盘支持
        EventHandler.off(document, 'keydown')
    }
}
