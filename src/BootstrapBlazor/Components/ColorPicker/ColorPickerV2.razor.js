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
 * 整个页面生命周期内所有颜色选择器的全局缓存
 * key是colorPickerV2Id，value代表各个组件的当前百分比
 * @type {Map<string, {
 * colorSliderRoundBlockLock: boolean,
 * colorSliderPercentage: number,
 * colorPaletteRoundBlockLock: boolean,
 * colorPaletteXPercentage: number,
 * colorPaletteYPercentage: number,
 * alphaSliderRoundBlockLock: boolean,
 * alphaSliderPercentage: number,
 * result: {h: number, s: number, l: number, a: number},
 * refresh: function}>}
 */
const globalColorPickerV2CacheMap = new Map();

/**
 * 暂时不知道有啥用
 * @param id
 */
export function dispose(id) {
    Data.remove(id)
}

/**
 * 对不包含透明通道的颜色选择器组件的初始化，
 * 通过razor中给div赋值id属性，并将id传入当前方法，定位到指定的div元素
 * @param {string} colorPickerV2Id
 * @param {string} colorPaletteId
 * @param {string} colorPaletteRoundBlockId
 * @param {string} colorSliderId
 * @param {string} colorSliderRoundBlockId
 * @param {string} alphaSliderId
 * @param {string} alphaSliderRoundBlockId
 */
export function initColorPicker(
    colorPickerV2Id,
    colorPaletteId,
    colorPaletteRoundBlockId,
    colorSliderId,
    colorSliderRoundBlockId,
    alphaSliderId,
    alphaSliderRoundBlockId) {
    //根据id获取需要操纵的dom元素
    const colorPalette = document.getElementById(colorPaletteId);
    const colorPaletteRoundBlock = document.getElementById(colorPaletteRoundBlockId);
    const colorSlider = document.getElementById(colorSliderId);
    const colorSliderRoundBlock = document.getElementById(colorSliderRoundBlockId);
    const alphaSlider = document.getElementById(alphaSliderId);
    const alphaSliderRoundBlock = document.getElementById(alphaSliderRoundBlockId);
    let colorPickerV2Cache = globalColorPickerV2CacheMap.get(colorPickerV2Id);
    if (!colorPickerV2Cache) {
        colorPickerV2Cache = {
            colorSliderRoundBlockLock: false, //色相滑块的圆形块是否锁定
            colorSliderPercentage: 0, //色相滑块的当前横轴位置比例值，0-1
            colorPaletteRoundBlockLock: false, //饱和度/明度区域的圆形块是否锁定
            colorPaletteXPercentage: 0.5, //饱和度/明度区域的当前横轴位置比例值，0-1
            colorPaletteYPercentage: 0.5,//饱和度/明度区域的当前纵轴位置比例值，0-1
            alphaSliderRoundBlockLock: false, //透明通道滑块的圆形块是否锁定
            alphaSliderPercentage: 1, //透明通道滑块的当前横轴位置比例值，0-1
            result: {h: 0, s: 0, l: 0, a: 1}, //当前最新的结果颜色
            refresh() {
                //求出色相
                this.colorSliderPercentage = clamp(this.colorSliderPercentage, 0, 1);
                const hue = this.colorSliderPercentage * 360;
                //求出混合后的结果色
                this.colorPaletteXPercentage = clamp(this.colorPaletteXPercentage, 0, 1);
                const xColor = {h: hue, s: this.colorPaletteXPercentage, l: (1 - this.colorPaletteXPercentage) / 2 + 0.5};
                this.colorPaletteYPercentage = clamp(this.colorPaletteYPercentage, 0, 1);
                const yColor = {h: hue, s: 0, l: 1 - this.colorPaletteYPercentage};
                const blendColor = grbMultiplicativeBlending(hslToRgb(xColor), hslToRgb(yColor));
                //对结果色应用透明通道
                const resultColor = rgbToHsl(blendColor);
                this.alphaSliderPercentage = clamp(this.alphaSliderPercentage, 0, 1);
                this.result = {h: resultColor.h, s: resultColor.s, l: resultColor.l, a: this.alphaSliderPercentage};
                //对对应的元素样式进行修改
                colorSliderRoundBlock.style.cssText = `
                    left: ${doubleToPercentage(this.colorSliderPercentage)};
                    background-color: hsl(${hue}, 100%, 50%);`;
                colorPalette.style.cssText = `
                    background-image:
                         linear-gradient(to bottom, hsl(${hue}, 0%, 100%), hsl(${hue}, 0%, 0%)),
                         linear-gradient(to right, hsl(${hue}, 0%, 100%), hsl(${hue}, 100%, 50%));`;
                colorPaletteRoundBlock.style.cssText = `
                    left: ${doubleToPercentage(this.colorPaletteXPercentage)};
                    top: ${doubleToPercentage(this.colorPaletteYPercentage)};
                    background-color: hsl(${resultColor.h}, ${doubleToPercentage(resultColor.s)}, ${doubleToPercentage(resultColor.l)});`;
                alphaSlider.style.cssText = `
                    background-image:
                         linear-gradient(to right, hsl(${hue}, 0%, 100%), hsl(${hue}, 0%, 0%));`;
                alphaSliderRoundBlock.style.cssText = `
                    left: ${doubleToPercentage(this.alphaSliderPercentage)};
                    background-color: hsl(${hue}, 100%, 50%);`;
                }
            };
        //缓存cache
        globalColorPickerV2CacheMap.set(colorPickerV2Id, colorPickerV2Cache);

        //防止同一个dom元素意外产生多次事件挂载，不用addEventListener
        //设置colorPalette事件
        colorPalette.onclick = (e) =>
            refreshColorPalettePercentageData(e);
        colorPalette.onmousemove = (e) =>
        {if (colorPickerV2Cache.colorPaletteRoundBlockLock) {refreshColorPalettePercentageData(e)}};
        colorPalette.onmousedown = (_) =>
        {colorPickerV2Cache.colorPaletteRoundBlockLock = true;};
        colorPalette.onmouseup = (_) =>
        {colorPickerV2Cache.colorPaletteRoundBlockLock = false;};
        colorPalette.onmouseout = (_) =>
        {colorPickerV2Cache.colorPaletteRoundBlockLock = false};
        //设置colorSlider事件
        colorSlider.onclick = (e) =>
        {refreshColorSliderPercentageData(e)};
        colorSlider.onmousemove = (e) =>
        {if (colorPickerV2Cache.colorSliderRoundBlockLock) { refreshColorSliderPercentageData(e)}};
        colorSlider.onmousedown = (_) =>
        {colorPickerV2Cache.colorSliderRoundBlockLock = true};
        colorSlider.onmouseup = (_) =>
        {colorPickerV2Cache.colorSliderRoundBlockLock = false};
        colorSlider.onmouseout = (_) =>
        {colorPickerV2Cache.colorSliderRoundBlockLock = false};
        //设置alphaSlider事件
        alphaSlider.onclick = (e) =>
        {refreshAlphaSliderPercentageData(e)};
        alphaSlider.onmousemove = (e) =>
        {if (colorPickerV2Cache.alphaSliderRoundBlockLock) { refreshAlphaSliderPercentageData(e)}};
        alphaSlider.onmousedown = (_) =>
        {colorPickerV2Cache.alphaSliderRoundBlockLock = true};
        alphaSlider.onmouseup = (_) =>
        {colorPickerV2Cache.alphaSliderRoundBlockLock = false};
        alphaSlider.onmouseout = (_) =>
        {colorPickerV2Cache.alphaSliderRoundBlockLock = false};
    }
    //根据预设的四个Percentage先初始化一次
    colorPickerV2Cache.refresh();

    /**
     * 刷新ColorPalette的X和Y对应的Percentage值
     * @param {MouseEvent} e
     */
    function refreshColorPalettePercentageData(e) {
        const location = getMousePointLocationInElement(colorPalette, e);
        colorPickerV2Cache.colorPaletteXPercentage = location.x;
        colorPickerV2Cache.colorPaletteYPercentage = location.y;
        colorPickerV2Cache.refresh();
    }

    /**
     * 刷新ColorSlider的XPercentage值
     * @param {MouseEvent} e
     */
    function refreshColorSliderPercentageData(e) {
        const location = getMousePointLocationInElement(colorSlider, e);
        colorPickerV2Cache.colorSliderPercentage = location.x;
        colorPickerV2Cache.refresh();
    }

    /**
     * 刷新AlphaSlider的XPercentage值
     * @param {MouseEvent} e
     */
    function refreshAlphaSliderPercentageData(e) {
        const location = getMousePointLocationInElement(alphaSlider, e);
        colorPickerV2Cache.alphaSliderPercentage = location.x;
        colorPickerV2Cache.refresh();
    }
}

/**
 * 注销当前id对应的颜色选择器缓存
 * @param {string} colorPickerV2Id
 */
export function disposeColorPicker(
    colorPickerV2Id) {
    globalColorPickerV2CacheMap.delete(colorPickerV2Id);
}

/**
 * 获取当前id对应的颜色选择器结果颜色值
 * @param {string} colorPickerV2Id
 * @returns {number[]}
 */
export function getColorPickerResult(
    colorPickerV2Id) {
    const cache = globalColorPickerV2CacheMap.get(colorPickerV2Id);
    return cache ? [cache.result.h, cache.result.s, cache.result.l, cache.result.a] : [];
}

/**
 * 设置当前id对应的颜色选择器结果颜色值
 * @param {string} colorPickerV2Id
 * @param {number} h
 * @param {number} s
 * @param {number} l
 * @param {number} a
 */
export function setColorPicker(
    colorPickerV2Id,
    h, s, l ,a) {
    const cache = globalColorPickerV2CacheMap.get(colorPickerV2Id);
    if (cache.result.h === h && cache.result.s === s && cache.result.l === l && cache.result.a === a)
        return;
    //确定透明度圆形块位置
    cache.alphaSliderPercentage = a;
    //确定色相圆形块位置
    cache.colorSliderPercentage = h / 360;
    //确定Y分量的hsl值
    const rgb = hslToRgb({h: h, s: s, l: l});
    const xy = findXYForTargetRGB(rgb, h, 0.001);
    cache.colorPaletteXPercentage = Number(xy.x.toFixed(2));
    cache.colorPaletteYPercentage = Number(xy.y.toFixed(2));
    cache.refresh();
}

/**
 *
 * @param {number} value
 * @param {number} min
 * @param {number} max
 * @returns {number}
 */
function clamp(value, min, max) {
    if (value < min) {
        return min;
    } else if (value > max) {
        return max;
    } else {
        return value;
    }
}

/**
 * 将小数转变为百分数
 * @param {number} source
 * @returns {string}
 */
function doubleToPercentage(source) {
    return `${(source * 100).toFixed(2)}%`
}

/**
 * 通过坐标计算，得到当前点击位置相对于元素平面的宽高百分比
 * @param element
 * @param event
 * @returns {{x:number, y:number}}
 */
function getMousePointLocationInElement(element, event) {
    const rect = element.getBoundingClientRect();
    const xPercentage = (event.clientX - rect.left) / element.clientWidth;
    const yPercentage = (event.clientY - rect.top) / element.clientHeight;
    return {x:xPercentage, y:yPercentage};
}

/**
 * 给定一个目标rgb值和色相，匹配一组x和y
 * @param {{r: number, g: number, b: number}} targetRGB
 * @param {number} hue
 * @param {number} step
 * @returns {{x: number, y: number}}
 */
function findXYForTargetRGB(targetRGB, hue, step = 0.01) {
    let bestMatch;
    let minError = Infinity;

    for (let x = 0; x <= 1; x += step) {
        for (let y = 0; y <= 1; y += step) {
            const xColor = { h: hue, s: x, l: (1 - x) / 2 + 0.5 };
            const yColor = { h: hue, s: 0, l: 1 - y };

            const blendColor = grbMultiplicativeBlending(hslToRgb(xColor), hslToRgb(yColor));

            const error = Math.sqrt(
                Math.pow(blendColor.r - targetRGB.r, 2) +
                Math.pow(blendColor.g - targetRGB.g, 2) +
                Math.pow(blendColor.b - targetRGB.b, 2)
            );
            if (error < minError) {
                minError = error;
                bestMatch = { x: x, y: y };
            }
            if (error < 0.001) {
                return bestMatch;
            }
        }
    }
    return bestMatch;
}

/**
 * hsl格式转rgb格式，
 * h区间0-360，s区间0-1，l区间0-1,
 * r区间0-1，g区间0-1，b区间0-1
 * @param {{h: number, s: number, l: number}} hsl
 * @returns {{r: number, g: number, b: number}} rgb
 */
function hslToRgb(
    hsl) {
    const c = (1 - Math.abs(2 * hsl.l - 1)) * hsl.s;
    const x = c * (1 - Math.abs((hsl.h / 60) % 2 - 1));
    const m = hsl.l - c / 2;
    let result;
    if (hsl.h >= 0 && hsl.h < 60)
        result = {r: c, g: x, b: 0};
    if (hsl.h >= 60 && hsl.h < 120)
        result = {r: x, g: c, b: 0};
    if (hsl.h >= 120 && hsl.h < 180)
        result = {r: 0, g: c, b: x};
    if (hsl.h >= 180 && hsl.h < 240)
        result = {r: 0, g: x, b: c};
    if (hsl.h >= 240 && hsl.h < 300)
        result = {r: x, g: 0, b: c};
    if (hsl.h >= 300 && hsl.h <= 360)
        result = {r: c, g: 0, b: x};
    return {r: (result.r + m), g: (result.g + m), b: (result.b + m)};
}

/**
 * 求两个rgb格式的颜色值乘法混合的rgb格式结果
 * @param {{r: number, g: number, b: number}} rgbX
 * @param {{r: number, g: number, b: number}} rgbY
 * @returns {{r: number, g: number, b: number}} rgb
 */
function grbMultiplicativeBlending(
    rgbX,
    rgbY) {
    return {r: rgbX.r * rgbY.r, g: rgbX.g * rgbY.g, b: rgbX.b * rgbY.b};
}

/**
 * rgb格式转hsl格式，
 * r区间0-1，g区间0-1，b区间0-1
 * h区间0-360，s区间0-1，l区间0-1,
 * @param {{r: number, g: number, b: number}} rgb
 * @returns {{h: number, s: number, l: number}} hsl
 */
function rgbToHsl(
    rgb) {
    const max = Math.max(rgb.r, Math.max(rgb.g, rgb.b));
    const min = Math.min(rgb.r, Math.min(rgb.g, rgb.b));
    const delta = max - min;
    let h = 0;
    if (delta !== 0)
    {
        if (Math.abs(max - rgb.r) < 0.0000000001)
            h = ((rgb.g - rgb.b) / delta + (rgb.g < rgb.b ? 6 : 0)) * 60;
        else if (Math.abs(max - rgb.g) < 0.0000000001)
            h = ((rgb.b - rgb.r) / delta + 2) * 60;
        else
            h = ((rgb.r - rgb.g) / delta + 4) * 60;
    }
    const l = (max + min) / 2;
    const s = delta === 0 ? 0 : delta / (1 - Math.abs(2 * l - 1));
    return {h: h, s: s, l: l};
}
