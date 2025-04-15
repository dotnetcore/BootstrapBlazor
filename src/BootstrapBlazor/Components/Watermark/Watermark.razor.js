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
        const { el, ob } = watermark;
        ob.disconnect();

        delete watermark.ob;
        document.body.removeAttribute('data-bb-watermark');
        el.remove();
    }
}

const updateWatermark = (records, watermark) => {
    for (const record of records) {
        for (const dom of record.removedNodes) {
            if (dom.classList && dom.classList.contains('bb-watermark-bg')) {
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
        color: '#0000004d',
        zIndex: '9999'
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
    div.style.backgroundSize = `${styleSize.toFixed(2)}px ${styleSize.toFixed(2)}px`;
    div.style.backgroundRepeat = 'repeat';
    div.style.pointerEvents = 'none';
    div.style.opacity = '1';
    div.style.position = 'absolute';
    div.style.inset = '0';
    div.classList.add("bb-watermark-bg");

    if (options.zIndex === void 0) {
        options.zIndex = defaults.zIndex;
    }
    div.style.zIndex = options.zIndex;

    const mark = el.querySelector('.bb-watermark-bg');
    if (mark) {
        mark.remove();
    }
    el.appendChild(div);

    if (options.isPage) {
        document.body.setAttribute('data-bb-watermark', "true");
        document.body.appendChild(el);
    }

    options.bg = bg;
    requestAnimationFrame(() => monitor(watermark));
}

const monitor = watermark => {
    const { el, options } = watermark;
    if (el === null) {
        return;
    }

    if (options.isPage === false && el.children.length !== 2) {
        clearWatermark(watermark);
        return;
    }

    const mark = options.isPage ? el.children[0] : el.children[1];
    if (mark.className !== 'bb-watermark-bg') {
        clearWatermark(watermark);
        return;
    }

    const style = getComputedStyle(mark);
    const { display, opacity, position, inset, zIndex, zoom, transform, backgroundRepeat, backgroundImage, backgroundSize } = style;
    if (display !== 'block') {
        clearWatermark(watermark);
        return;
    }
    if (opacity !== '1') {
        clearWatermark(watermark);
        return;
    }
    if (position !== 'absolute') {
        clearWatermark(watermark);
        return;
    }
    if (inset !== '0px') {
        clearWatermark(watermark);
        return;
    }
    if (zIndex !== options.zIndex) {
        clearWatermark(watermark);
        return;
    }
    if (zoom !== '1') {
        clearWatermark(watermark);
        return;
    }
    if (transform !== 'none') {
        clearWatermark(watermark);
        return;
    }
    if (backgroundRepeat !== 'repeat') {
        clearWatermark(watermark);
        return;
    }
    if (backgroundImage !== `url("${options.bg.base64}")`) {
        clearWatermark(watermark);
        return;
    }

    const size = parseFloat(backgroundSize);
    if (Math.abs(size - options.bg.styleSize) > 1) {
        clearWatermark(watermark);
        return;
    }
    requestAnimationFrame(() => monitor(watermark));
}

const clearWatermark = watermark => {
    const { el, ob } = watermark;
    if (ob) {
        ob.disconnect();
    }
    if (el) {
        el.innerHTML = '';
    }
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
        styleSize: canvasSize / devicePixelRatio
    };
}
