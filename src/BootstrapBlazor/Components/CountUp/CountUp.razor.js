import { CountUp } from "../../lib/countUp/countUp.min.js"
import Data from "../../modules/data.js"

export function init(id, invoke, val, callback) {
    var count = { invoke, callback }
    Data.set(id, count)

    const option = {}

    if (callback !== null) {
        option.onCompleteCallback = () => {
            const count = Data.get(id)
            if (count) {
                count.invoke.invokeMethodAsync(callback)
            }
        }
    }

    const countUp = new CountUp(id, val, option)
    count.countUp = countUp

    if (val > 0) {
        countUp.start()
    }
}

export function update(id, value) {
    const count = Data.get(id)
    if (count.countUp) {
        count.countUp.update(value)
    }
}

export function dispose(id) {
    Data.remove(id)
}

