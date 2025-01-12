import Data from "../../modules/data.js"

export function init(id, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }
    const watermark = { el, options };
    createWatermark(watermark);

    const observer = ob => {
        ob.observe(el, {
            childList: true,
            attributes: true,
            subtree: true
        });
    }

    const observerCallback = records => {
        ob.disconnect();
        updateWatermark(records, watermark);
        observer(ob);
    };

    const ob = new MutationObserver(observerCallback);
    observer(ob);
    watermark.ob = ob;

    Data.set(id, watermark);
}

export function update(id, options) {
    const watermark = Data.get(id);
    watermark.options = options;

    createWatermark(watermark);
}

export function dispose(id) {
    const watermark = Data.get(id);
    Data.remove(id);

    if (watermark) {
        const { ob } = watermark;
        ob.disconnect();

        delete watermark.ob;
    }
}

const updateWatermark = (records, watermark) => {
    for (const record of records) {
        for (const dom of record.removedNodes) {
            if (dom.classList.contains('bb-watermark-bg')) {
                createWatermark(watermark);
                return;
            }
        }

        if (record.target.classList.contains('bb-watermark-bg')) {
            createWatermark(watermark);
            return;
        }
    }
}

const createWatermark = watermark => {
    const { el, options } = watermark;
    const defaults = {
        gap: 40,
        fontSize: 16,
        text: 'BootstrapBlazor',
        rotate: -40,
        color: '#0000004d'
    };

    for (const key in options) {
        if (options[key] === void 0 || options[key] === null) {
            delete options[key];
        }
    }

    const bg = getWatermark({ ...defaults, ...options });
    const div = document.createElement('div');
    const { base64, styleSize } = bg;
    div.style.backgroundImage = `url(${base64})`;
    div.style.backgroundSize = `${styleSize}px ${styleSize}px`;
    div.style.backgroundRepeat = 'repeat';
    div.style.pointerEvents = 'none';
    div.style.opacity = '1';
    div.style.position = 'absolute';
    div.style.inset = '0';
    div.style.zIndex = '999';
    div.classList.add("bb-watermark-bg");

    const mark = el.querySelector('.bb-watermark-bg');
    if (mark) {
        mark.remove();
    }
    el.appendChild(div);
}

const getWatermark = props => {
    const canvas = document.createElement('canvas');
    const devicePixelRatio = window.devicePixelRatio || 1;

    const fontSize = props.fontSize * devicePixelRatio;
    const font = fontSize + 'px serif';
    const ctx = canvas.getContext('2d');

    ctx.font = font;
    const { width } = ctx.measureText(props.text);
    const canvasSize = Math.max(100, width) + props.gap * devicePixelRatio;
    canvas.width = canvasSize;
    canvas.height = canvasSize;
    ctx.translate(canvas.width / 2, canvas.height / 2);

    ctx.rotate((Math.PI / 180) * props.rotate);
    ctx.fillStyle = props.color;
    ctx.font = font;
    ctx.textAlign = 'center';
    ctx.textBaseline = 'middle';

    ctx.fillText(props.text, 0, 0);
    return {
        base64: canvas.toDataURL(),
        size: canvasSize,
        styleSize: canvasSize / devicePixelRatio,
    };
}
