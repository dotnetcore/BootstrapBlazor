import { CountUp } from "../../lib/countUp/countUp.min.js"
import Data from "../../modules/data.js"

export function init(id, invoke, val, callback, option) {
    option = option || {}
    if (callback !== null) {
        option.onCompleteCallback = () => {
            invoke.invokeMethodAsync(callback)
        }
    }

    const countUp = new CountUp(id, val, option)
    countUp.start()
}
