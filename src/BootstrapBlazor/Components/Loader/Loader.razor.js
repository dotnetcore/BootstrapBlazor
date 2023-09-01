import { addLink, addScript } from '../../modules/utility.js?v=$version'

/**
 * 初始化js,css并加载动画
 * @param {any} id DOM元素ID
 * @param {Number} cols 分割数量
 */
export async function init(id, cols) {
    await addLink('./_content/BootstrapBlazor/lib/splitting/splitting-cells.css')
    await addScript('./_content/BootstrapBlazor/lib/splitting/splitting.min.js')
    await addScript('./_content/BootstrapBlazor/modules/gsap.min.js')

    // 加载动画
    load(id, cols);
}

/**
 * 分割元素，并加载动画
 * @param {any} id DOM元素ID
 * @param {Number} cols 分割数量
 */
export function load(id, cols) {
    // 获取DOM元素
    const el = document.getElementById(id);
    // 获取子元素
    const cell = el.querySelectorAll(".loader-flip");

    // 使用Splitting插件对cell进行分割
    const results = Splitting({
        target: cell,//目标元素
        by: 'cells',//分割方式
        image: true,//不知道干啥的。。。
        columns: cols,//分割列数
        rows: 1//分割行数
    });

    // 获取分割后的cells
    const cells = results[0].cells;

    // 获取DOM元素
    var tl = gsap.timeline({ repeat: -1, repeatDelay: 0.75 });

    // 为每个cell添加动画
    tl.from(cells, {
        scale: 0, // 初始化时缩放为0
        transformOrigin: "center", // 变换Origin为中心
        x: "1.5rem", // 初始时横坐标为1.5rem
        duration: 0.25, // 动画持续时间
        ease: "circ.out", // 缓动函数
        stagger: {
            amount: 3, // 逐个元素动画延迟时间
            from: "start" // 从起始位置开始
        }
    });
    tl.to(cells, {
        scale: 0, // 缩放为0
        xPercent: -900, // 横坐标为-900%
        duration: 0.25, // 动画持续时间
        stagger: { amount: 0.75, from: "start" }, // 逐个元素动画延迟时间
    }, "+=0.5"); // 动画结束后的延迟时间
}

/**
 * 分割元素，并加载动画
 * @param {any} id DOM元素ID
 * @param {Number} cols 分割数量
 */
export function update(id, cols) {
    // 获取DOM元素
    const el = document.getElementById(id);
    const flip = el.querySelectorAll(".loader-flip");

    // 删除第一个flip元素的🍌属性
    delete flip[0]['🍌'];

    // 获取cells，删除所有子元素
    const cells = el.querySelectorAll(".cell-grid");
    cells.forEach(x => x.remove());

    // 加载动画
    load(id, cols);
}
