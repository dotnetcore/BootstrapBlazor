import { CountUp } from "../../lib/countUp/countUp.min.js"
import Data from "../../modules/data.js"

export function init(id, val) {
    var count = {}
    Data.set(id, count)

    if (val) {
        const countUp = new CountUp(id, val)
        count._countUp = countUp

        if (!countUp.error) {
            countUp.start()
        }
        else {
            console.error(countUp.error)
        }
    }
}

export function update(id, value) {
    const count = Data.get(id)
    if (count._countUp) {
        couont._countUp.update(value)
    }
}

export function dispose(id) {
    Data.remove(id)
}

