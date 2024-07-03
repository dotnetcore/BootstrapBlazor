import { addLink, addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import EventHandler from '../../../BootstrapBlazor/modules/event-handler.js'

export async function init(op) {
    await addLink("./_content/BootstrapBlazor.Live2DDisplay/BootstrapBlazor.Live2DDisplay.bundle.scp.css")
    await addScript("./_content/BootstrapBlazor.Live2DDisplay/live2dcubismcore.min.js")
    await addScript("./_content/BootstrapBlazor.Live2DDisplay/live2d.min.js")
    await addScript("./_content/BootstrapBlazor.Live2DDisplay/pixi.min.js")
    await addScript("./_content/BootstrapBlazor.Live2DDisplay/index.min.js")
    await addScript("./_content/BootstrapBlazor.Live2DDisplay/extra.min.js")

    const model = await createModel(op);
    if (model) {
        const el = document.getElementById(op.id);
        el.style.zIndex = '9999';
        el.style.opacity = '1';

        const canvas = document.createElement('canvas');

        el.appendChild(canvas);

        const app = new PIXI.Application({
            view: canvas,
            autoStart: true
        });
        app.stage.addChild(model);

        const data = { app, model, el, canvas, op };
        Data.set(op.id, data);

        resizeTo(data);
        changeDraggable(op.id, op.isDraggable);
        changeBackground(op.id, op.backgroundAlpha, op.backgroundColor);
    }
}


function createHitAreaFrames(op, model) {
    model.children = [];
    if (op.addHitAreaFrames) {
        // handle tapping
        model.on("hit", (hitAreas) => {
            if (hitAreas.includes("Body")) {
                model.motion("Tap");
                model.motion("tap_body");
            }

            if (hitAreas.includes("Head")) {
                model.expression();
            }
        });
        const hitAreaFrames = new PIXI.live2d.HitAreaFrames();
        model.addChild(hitAreaFrames);
        hitAreaFrames.visible = op.addHitAreaFrames;
    }
}

function resizeTo(data) {
    if (data.op.isDraggble) {
        data.app.resizeTo = window;
    }
    else {
        var h = Math.ceil(data.model.height);
        var w = Math.ceil(data.model.width);

        data.el.style.width = w + 'px';
        data.el.style.height = h + 'px';

        data.canvas.style.width = data.el.style.width;
        data.canvas.style.height = data.el.style.height;

        data.canvas.width = w;
        data.canvas.height = h;

        data.app.resizeTo = data.el;
    }
}

export async function changeSource(op) {
    const data = Data.get(op.id);
    data.op = op;
    data.app.stage.removeChild(data.model);
    data.model = await createModel(op);
    if (data.model) {
        data.app.stage.addChild(data.model);
        data.model.scale.set(data.op.scale);
        resizeTo(data);
    }
}

export function changeScale(id, scale) {
    const data = Data.get(id);
    data.op.scale = scale;
    data.model.scale.set(data.op.scale);
    resizeTo(data);
}

export function changeXY(id, x, y) {
    const data = Data.get(id);
    data.op.x = x;
    data.op.y = y;
    data.model.x = x;
    data.model.y = y;
    resizeTo(data);
}

export function changeBackground(id, backgroundAlpha, backgroundColor) {
    if (backgroundColor) {
        if (backgroundColor.startsWith('#')) {
            const data = Data.get(id);
            backgroundColor = backgroundColor.replace("#", "0x");
            data.op.backgroundAlpha = backgroundAlpha;
            data.op.backgroundColor = backgroundColor;
            data.app.renderer.backgroundColor = backgroundColor;
            data.app.renderer.backgroundAlpha = backgroundAlpha;
        }
    }
}

export function changeDraggable(id, draggble) {
    const data = Data.get(id);
    data.op.isDraggble = draggble;

    // 获取要拖动的元素
    var el = data.el;

    if (!draggble) {
        EventHandler.off(el, 'mousedown');
        EventHandler.off(document, 'mousemove');
        EventHandler.off(document, 'mouseup');

        el.style.cursor = 'none';
        el.style.pointerEvents = 'none';
    } else {
        el.style.cursor = 'move';
        el.style.pointerEvents = 'auto';
        // 初始化拖动的起始位置
        var offsetX = 0;
        var offsetY = 0;

        // 当鼠标按下时触发
        EventHandler.on(el, 'mousedown', (e) => {
            // 计算鼠标在元素内的相对位置
            offsetX = e.clientX - el.offsetLeft;
            offsetY = e.clientY - el.offsetTop;

            // 添加鼠标移动和释放事件监听器
            // 当鼠标移动时触发
            EventHandler.on(document, 'mousemove', (e) => {
                // 阻止默认的拖动行为
                e.preventDefault();

                // 计算元素的新位置
                var x = e.clientX - offsetX;
                var y = e.clientY - offsetY;

                // 设置元素的新位置
                el.style.left = x + "px";
                el.style.top = y + "px";
            });

            // 当鼠标释放时触发
            EventHandler.on(document, 'mouseup', () => {
                // 移除鼠标移动和释放事件监听器
                EventHandler.off(document, 'mousemove');
                EventHandler.off(document, 'mouseup');
            });
        })
    }
}

export function addHitAreaFrames(id, isaddHitAreaFrames) {
    const data = Data.get(id);
    data.op.addHitAreaFrames = isaddHitAreaFrames;
    createHitAreaFrames(data.op, data.model);
}

async function createModel(op) {
    const model = await PIXI.live2d.Live2DModel.from(op.source);
    model.scale.set(op.scale);
    model.x = op.x;
    model.y = op.y;
    model.dragging = op.isDraggble;
    if (op.addHitAreaFrames) {
        createHitAreaFrames(op, model);
    }
    return model;
}
