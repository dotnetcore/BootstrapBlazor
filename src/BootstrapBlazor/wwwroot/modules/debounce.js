import EventHandler from "./event-handler.js"

export default {
    init(id, waitMs) {
        const el = document.getElementById(id)
        if (el) {
            // ReaZhuang贡献
            let timer
            var allowKeys = ['ArrowUp', 'ArrowDown', 'Escape', 'Enter']

            EventHandler.on(el, 'keyup', event => {
                if (allowKeys.indexOf(event.key) < 1 && timer) {
                    // 清空计时器的方法
                    clearTimeout(timer)

                    // 阻止事件冒泡，使之不能进入到c#
                    event.stopPropagation()

                    // 创建一个计时器，开始倒计时，倒计时结束后执行内部的方法
                    timer = setTimeout(() => {
                        // 清除计时器，使下次事件不能进入到if中
                        timer = null
                        // 手动激发冒泡事件
                        event.target.dispatchEvent(event)
                    }, waitMs)
                }
                else {
                    // 创建一个空的计时器，在倒计时期间内，接收的事件将全部进入到if中
                    timer = setTimeout(() => { }, waitMs)
                }
            })
        }
    },

    dispose(id) {
        const el = document.getElementById(id)
        if (el) {
            EventHandler.off(el, 'keyup')
        }
    }
}
