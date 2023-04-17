import { CountUp } from "../../lib/countUp/countUp.min.js"
import Data from "../../modules/data.js"

export function init(id) {
    var count = {}
    Data.set(id, count)

    var val = parseFloat(document.getElementById(id).innerHTML)
    if (val) {
        count._countUp = new CountUp(id, 5234)
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

