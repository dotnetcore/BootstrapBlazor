import { CountUp } from "../../lib/countUp/countUp.min.js"
import Data from "../../modules/data.js"

export function init(id, invoker, val, callback) {
    var count = { invoker, callback }
    Data.set(id, count)

    const option = {
        onCompleteCallback: () => {
            invoker.invokeMethodAsync(callback)
        }
    }

    const countUp = new CountUp(id, val, option)
    count._countUp = countUp

    if (val > 0) {
        countUp.start()
    }
}

export function update(id, value) {
    const count = Data.get(id)
    if (count._countUp) {
        count._countUp.update(value)
    }
}

export function dispose(id) {
    Data.remove(id)
}

