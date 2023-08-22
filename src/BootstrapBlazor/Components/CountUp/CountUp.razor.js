import { CountUp } from "../../lib/countUp/countUp.min.js?v=$version"
import Data from "../../modules/data.js?v=$version"

export function init(id, invoke, val, callback, option) {
    var count = { invoke, callback }
    Data.set(id, count)

    initCountUp(id, invoke, val, callback, option)
}

export function update(id, value, option) {
    const count = Data.get(id)
    initCountUp(id, count.invoke, value, count.callback, option)
}

export function dispose(id) {
    Data.remove(id)
}

const initCountUp = (id, invoke, val, callback, option) => {
    option = option || {}
    if (callback !== null) {
        option.onCompleteCallback = () => {
            invoke.invokeMethodAsync(callback)
        }
    }

    const countUp = new CountUp(id, val, option)
    if (val > 0) {
        countUp.start()
    }
}
