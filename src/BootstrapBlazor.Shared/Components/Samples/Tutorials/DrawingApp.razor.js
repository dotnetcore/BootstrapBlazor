import Data from '../../../_content/BootstrapBlazor/modules/data.js'
import EventHandler from "../../../_content/BootstrapBlazor/modules/event-handler.js"
import { isMobile } from "../../../_content/BootstrapBlazor/modules/utility.js"

/**
 * 缓存绘图步骤
 * @param {any} drawingOptions - Options
 */
function saveState(drawingOptions) {
    drawingOptions.currentState++;
    drawingOptions.history[drawingOptions.currentState] = drawingOptions.canvas.toDataURL();
    if (drawingOptions.currentState < drawingOptions.history.length - 1) {
        drawingOptions.history = drawingOptions.history.slice(0, drawingOptions.currentState + 1);
    }
}

/**
 * 绘制圆点
 * @param {any} drawingOptions - Options
 * @param {any} x
 * @param {any} y
 */
function drawCircle(drawingOptions, x, y) {
    drawingOptions.ctx.beginPath();
    drawingOptions.ctx.arc(x, y, drawingOptions.lineThickness, 0, Math.PI * 2);
    drawingOptions.ctx.fillStyle = drawingOptions.drawingColor;
    drawingOptions.ctx.fill();
}

/**
 * 绘线
 * @param {any} drawingOptions - Options
 * @param {any} x1
 * @param {any} y1
 * @param {any} x2
 * @param {any} y2
 */
function drawLine(drawingOptions, x1, y1, x2, y2) {
    drawingOptions.ctx.beginPath();
    drawingOptions.ctx.moveTo(x1, y1);
    drawingOptions.ctx.lineTo(x2, y2);
    drawingOptions.ctx.strokeStyle = drawingOptions.drawingColor;
    drawingOptions.ctx.lineWidth = drawingOptions.lineThickness * 2;
    drawingOptions.ctx.stroke();
}

/**
 * 绘图
 * @param {any} drawingOptions - Options
 * @param {any} x2
 * @param {any} y2
 */
function draw(drawingOptions, x2, y2) {
    drawCircle(drawingOptions, x2, y2);
    drawLine(drawingOptions, drawingOptions.x, drawingOptions.y, x2, y2);

    drawingOptions.x = x2;
    drawingOptions.y = y2;
    drawingOptions.savedImageData = drawingOptions.canvas.toDataURL();
}

/**
 * 绑定触摸开始事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindTouchStartListener(drawingOptions, handler) {
    drawingOptions.touchStartListener = (e) => {
        e.preventDefault();
        drawingOptions.isPressed = true;

        drawingOptions.x = e.touches[0].clientX - drawingOptions.canvas.offsetLeft;
        drawingOptions.y = e.touches[0].clientY - drawingOptions.canvas.offsetTop;
    }

    EventHandler.on(drawingOptions.canvas, handler, drawingOptions.touchStartListener)
}

/**
 * 绑定触摸移动事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindTouchMoveListener(drawingOptions, handler) {
    drawingOptions.touchMoveListener = (e) => {
        if (drawingOptions.isPressed) {
            const x2 = e.touches[0].clientX - drawingOptions.canvas.offsetLeft;
            const y2 = e.touches[0].clientY - drawingOptions.canvas.offsetTop;
            draw(drawingOptions, x2, y2);
        }
    }

    EventHandler.on(drawingOptions.canvas, handler, drawingOptions.touchMoveListener)
}

/**
 * 绑定鼠标按下事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindMouseDownListener(drawingOptions, handler) {
    drawingOptions.mouseDownListener = (e) => {
        drawingOptions.isPressed = true;
        drawingOptions.x = e.offsetX;
        drawingOptions.y = e.offsetY;
    }

    EventHandler.on(drawingOptions.canvas, handler, drawingOptions.mouseDownListener)
}

/**
 * 绑定鼠标弹起事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindMouseUpListener(drawingOptions, handler) {
    drawingOptions.pressedListener = (e) => {
        drawingOptions.isPressed = false;
        drawingOptions.x = undefined;
        drawingOptions.y = undefined;

        saveState(drawingOptions);
    }

    EventHandler.on(drawingOptions.canvas, handler, drawingOptions.pressedListener)
}

/**
 * 绑定鼠标移动事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindMouseMoveListener(drawingOptions, handler) {
    drawingOptions.mouseMoveListener = (e) => {
        if (drawingOptions.isPressed) {
            const x2 = e.offsetX;
            const y2 = e.offsetY;

            draw(drawingOptions, x2, y2);
        }
    }

    EventHandler.on(drawingOptions.canvas, handler, drawingOptions.mouseMoveListener)
}

/**
 * 绑定窗口大小改变事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindWindowResizeListener(drawingOptions, handler) {
    drawingOptions.resizeListener = () => {
        const style = getComputedStyle(drawingOptions.canvas);
        const width = parseInt(style.width, 10);
        const height = parseInt(style.height, 10);

        drawingOptions.canvas.width = width;
        drawingOptions.canvas.height = height;

        if (drawingOptions.savedImageData) {
            const img = new Image();
            img.src = drawingOptions.savedImageData;
            img.onload = () => {
                drawingOptions.ctx.drawImage(img, 0, 0);
            };
        }
    };

    EventHandler.on(window, handler, drawingOptions.resizeListener)
}

/**
 * 绑定按键事件监听器
 * @param {Object} drawingOptions - Options
 * @param {string} handler - handlerName
 */
function bindKeyDownListener(drawingOptions, handler) {
    drawingOptions.keyDownListener = (e) => {
        if (e.ctrlKey && e.key === 'z') {
            e.preventDefault();
            undo(drawingOptions.id);
        } else if (e.ctrlKey && e.key === 'y') {
            e.preventDefault();
            redo(drawingOptions.id);
        }
    }

    EventHandler.on(drawingOptions.canvas, handler, drawingOptions.keyDownListener)
}

/**
 * 绑定事件监听器
 * @param {Object} drawingOptions - Options
 */
function bindEventListeners(drawingOptions) {
    if (drawingOptions.isMobile) {
        bindTouchStartListener(drawingOptions, 'touchstart');
        bindTouchMoveListener(drawingOptions, 'touchmove');
        bindMouseUpListener(drawingOptions, 'touchend');
    } else {
        bindWindowResizeListener(drawingOptions, 'resize');
        bindMouseDownListener(drawingOptions, 'mousedown');
        bindMouseUpListener(drawingOptions, 'mouseup');
        bindMouseMoveListener(drawingOptions, 'mousemove');
    }
    bindKeyDownListener(drawingOptions, 'keydown');
}

/**
 * 设置画布尺寸和上下文
 * @param {Object} drawingOptions - Options
 */
function setupCanvas(drawingOptions) {
    drawingOptions.canvas.setAttribute('tabindex', 0);
    drawingOptions.canvas.focus();

    const style = getComputedStyle(drawingOptions.canvas);
    drawingOptions.canvas.width = parseInt(style.width, 10);
    drawingOptions.canvas.height = parseInt(style.height, 10);

    saveState(drawingOptions);
}

/**
 * 撤销操作
 * @param {string} id - canvas dom id
 */
export const undo = (id) => {
    const drawingOptions = Data.get(id)
    if (drawingOptions) {
        if (drawingOptions.currentState > 0) {
            drawingOptions.currentState--;
            const img = new Image();
            img.src = drawingOptions.history[drawingOptions.currentState];
            img.onload = () => {
                drawingOptions.ctx.clearRect(0, 0, drawingOptions.canvas.width, drawingOptions.canvas.height);
                drawingOptions.ctx.drawImage(img, 0, 0);
            };
        }
    }
}

/**
 * 重做操作
 * @param {string} id - canvas dom id
 */
export const redo = (id) => {
    const drawingOptions = Data.get(id)
    if (drawingOptions) {
        if (drawingOptions.currentState < drawingOptions.history.length - 1) {
            drawingOptions.currentState++;
            const img = new Image();
            img.src = drawingOptions.history[drawingOptions.currentState];
            img.onload = () => {
                drawingOptions.ctx.clearRect(0, 0, drawingOptions.canvas.width, drawingOptions.canvas.height);
                drawingOptions.ctx.drawImage(img, 0, 0);
            };
        }
    }
}

/**
 * 更改画笔大小
 * @param {string} id - canvas dom id
 * @param {number} val - 新的画笔大小值
 */
export const changeSize = (id, val) => {
    const drawingOptions = Data.get(id);
    if (drawingOptions) {
        drawingOptions.lineThickness = val;
    }
}

/**
 * 更改画笔颜色
 * @param {string} id - canvas dom id
 * @param {string} val - 新的画笔颜色值
 */
export const changeColor = (id, val) => {
    const drawingOptions = Data.get(id);
    if (drawingOptions) {
        drawingOptions.drawingColor = val;
    }
}

/**
 * 清除画布内容
 * @param {string} id - canvas dom id
 */
export const clearRect = (id) => {
    const drawingOptions = Data.get(id)
    if (drawingOptions) {
        drawingOptions.savedImageData = drawingOptions.canvas.toDataURL();
        drawingOptions.ctx.clearRect(0, 0, drawingOptions.canvas.width, drawingOptions.canvas.height);
    }
}

/**
 * 导出画布内容为图像
 * @param {string} id - canvas dom id
 * @returns {string} - base64String
 */
export const exportImage = (id) => {
    const drawingOptions = Data.get(id)
    if (drawingOptions) {
        return drawingOptions.canvas.toDataURL('image/jpeg');
    }
}

/**
 * init 初始化
 * @param {string} id - canvas dom id
 * @param {number} thickness - 画笔大小
 * @param {string} color - 画笔颜色
 * @returns
 */
export const init = (id, thickness, color) => {
    const canvas = document.getElementById(id);

    if (!canvas) {
        console.error('Canvas element not found');
        return;
    }

    const drawingOptions = {
        id: id,
        lineThickness: thickness,
        drawingColor: color,
        x: 0,
        y: 0,
        isPressed: false,
        savedImageData: null,
        history: [],
        currentState: -1,
        canvas: canvas,
        ctx: canvas.getContext('2d'),
        isMobile: isMobile()
    }

    Data.set(id, drawingOptions);

    setupCanvas(drawingOptions);
    bindEventListeners(drawingOptions);
};

/**
 * dispose 释放资源
 * @param {string} id - canvas dom id
 */
export const dispose = (id) => {
    const drawingOptions = Data.get(id);

    if (drawingOptions.isMobile) {
        EventHandler.off(drawingOptions.canvas, 'touchstart', drawingOptions.touchStartListener);
        EventHandler.off(drawingOptions.canvas, 'touchmove', drawingOptions.touchMoveListener);
        EventHandler.off(drawingOptions.canvas, 'touchend', drawingOptions.pressedListener);
    } else {
        EventHandler.off(drawingOptions.canvas, 'mousedown', drawingOptions.mouseDownListener);
        EventHandler.off(drawingOptions.canvas, 'mouseup', drawingOptions.pressedListener);
        EventHandler.off(drawingOptions.canvas, 'mousemove', drawingOptions.mouseMoveListener);
        EventHandler.off(drawingOptions.canvas, 'keydown', drawingOptions.keyDownListener);
        EventHandler.off(window, 'resize', drawingOptions.resizeListener);
    }

    Data.remove(id)
}
