import Data from "../../modules/data.js"

/**
 * 暂时不知道有啥用
 * @param id
 */
export function init(id) {
    const el = document.getElementById(id)
    Data.set(id, el)
}

/**
 * 暂时不知道有啥用
 * @param id
 */
export function dispose(id) {
    Data.remove(id)
}
