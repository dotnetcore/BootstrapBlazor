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
 * 根据当前Id获取对应的dom节点，加上传入的鼠标点击事件，通过坐标计算，得到当前点击位置相对于dom元素平面的宽高百分比
 * @param id
 * @param event
 * @returns {number[]}
 */
export function getElementClickLocation(id, event) {
    const current = document.getElementById(id);
    const rect = current.getBoundingClientRect();
    const xPercentage = (event.clientX - rect.left) / current.clientWidth;
    const yPercentage = (event.clientY - rect.top) / current.clientHeight;
    return [xPercentage, yPercentage];
}

/**
 * 暂时不知道有啥用
 * @param id
 */
export function dispose(id) {
    Data.remove(id)
}
