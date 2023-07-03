import { addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'

export async function init(id, src, scale, x, y) {
    await addScript("_content/BootstrapBlazor.Live2DDisplay/live2dcubismcore.min.js")
    await addScript("_content/BootstrapBlazor.Live2DDisplay/live2d.min.js")
    await addScript("_content/BootstrapBlazor.Live2DDisplay/pixi.min.js")
    await addScript("_content/BootstrapBlazor.Live2DDisplay/index.min.js")
    await addScript("_content/BootstrapBlazor.Live2DDisplay/extra.min.js")

    const model = await createModel(src, scale, x, y);

    var h = Math.ceil(model.height) + 'px';
    var w = Math.ceil(model.width) + 'px';

    const el = document.getElementById(id);
    el.style.width = w;
    el.style.height = h;
    el.style.zIndex = '99999';
    el.style.opacity = '1';
    el.style.pointerEvents = 'none';

    const canvas = document.createElement('canvas');
    canvas.style.width = w;
    canvas.style.height = h;

    el.appendChild(canvas);

    const app = new PIXI.Application({
        view: canvas,
        autoStart: true,
        resizeTo: el,
        backgroundAlpha: 0
    });
    app.stage.addChild(model);

    addHitAreaFrames(model);

    Data.set(id, { app, model, src, scale, el, canvas });
}

export async function reload(id, src, scale, x, y) {
    const op = Data.get(id);
    if (op.src != src) {
        op.src = src;
        op.app.stage.removeChild(op.model);
        op.model = await createModel(op.src, scale, x, y);
        op.app.stage.addChild(op.model);
    }
    if (op.scale != scale) {
        op.scale = scale;
        op.model.scale.set(scale);
    }

    var h = Math.ceil(op.model.height);
    var w = Math.ceil(op.model.width);

    op.el.style.width = w + 'px';
    op.el.style.height = h + 'px';

    op.canvas.style.width = w + 'px';
    op.canvas.style.height = h + 'px';

    op.canvas.width = w;
    op.canvas.height = h;
    op.app.resizeTo = op.el;
}

async function createModel(src, scale, x, y) {
    const model = await PIXI.live2d.Live2DModel.from(src);
    model.scale.set(scale);
    model.x = x;
    model.y = y;

    // handle tapping
    model.on("hit", (hitAreas) => {
        if (hitAreas.includes("Body")) {
            model.motion("Tap");
            model2.motion("tap_body");
        }

        if (hitAreas.includes("Head")) {
            model.expression();
        }
    });

    return model;
}

function addHitAreaFrames(model) {
    const hitAreaFrames = new PIXI.live2d.HitAreaFrames();

    model.addChild(hitAreaFrames);
}
