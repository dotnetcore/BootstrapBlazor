import '../../lib/pickr/pickr.es5.min.js'
import { addLink } from "../../modules/utility.js"
import Data from "../../modules/data.js"

export async function init(id, invoke, options) {
    const obj = { invoke, pickr: null };
    Data.set(id, obj);
    await getOrCreatePickr(id, obj, options);
}

export async function update(id, options) {
    const obj = Data.get(id);
    await getOrCreatePickr(id, obj, options);
}

export function dispose(id) {
    const data = Data.get(id);
    Data.remove(id);
    if (data && data.pickr) {
        data.pickr.destroyAndRemove();
    }
}

const getOrCreatePickr = async (id, picker, options) => {
    const { isSupportOpacity } = options;

    if (isSupportOpacity === true) {
        await addLink("./_content/BootstrapBlazor/css/nano.min.css");

        const { invoke, pickr } = picker;
        if (pickr) {
            const { value, disabled } = options;
            const original = formatColorString(pickr.getColor());
            if (original !== value) {
                pickr.setColor(value, true);
            }
            if (pickr.options.disabled !== disabled) {
                if (disabled) {
                    pickr.disable();
                }
                else {
                    pickr.enable();
                }
            }
        }
        else {
            const el = document.getElementById(id);
            const config = getOptions(el, options)
            const pickr = Pickr.create(config);

            pickr.on('save', (color, instance) => {
                instance.hide();
                invoke.invokeMethodAsync('OnColorChanged', formatColorString(color));
            }).on('swatchselect', color => {
                invoke.invokeMethodAsync('OnColorChanged', formatColorString(color));
            });

            picker.pickr = pickr;
        }
    }
    else if (picker.pickr) {
        picker.pickr.destroyAndRemove();
        picker.pickr = null;
    }
}

const formatColorString = color => {
    if (color === null) {
        return "#FFFFFF";
    }
    else {
        const hex = color.toRGBA();
        let val = `#${formatHexString(hex[0])}${formatHexString(hex[1])}${formatHexString(hex[2])}`;
        if (hex[3] !== 1) {
            val = `${val}${formatHexString(hex[3] * 255)}`;
        }
        return val.toUpperCase();
    }
};

const formatHexString = hex => Math.round(hex).toString(16).padStart(2, '0');

const getOptions = (el, options) => {
    delete options.isSupportOpacity;
    if (options.value) {
        options.default = options.value;
        delete options.value;
    }
    if (options.swatches === null) {
        delete options.swatches;
    }
    const config = {
        el,
        theme: 'nano',
        useAsButton: true,
        swatches: [
            'rgba(244, 67, 54, 1)',
            'rgba(233, 30, 99, 0.95)',
            'rgba(156, 39, 176, 0.9)',
            'rgba(103, 58, 183, 0.85)',
            'rgba(63, 81, 181, 0.8)',
            'rgba(33, 150, 243, 0.75)',
            'rgba(3, 169, 244, 0.7)',
            'rgba(0, 188, 212, 0.7)',
            'rgba(0, 150, 136, 0.75)',
            'rgba(76, 175, 80, 0.8)',
            'rgba(139, 195, 74, 0.85)',
            'rgba(205, 220, 57, 0.9)',
            'rgba(255, 235, 59, 0.95)',
            'rgba(255, 193, 7, 1)'
        ],
        defaultRepresentation: 'HEXA',
        components: {
            preview: true,
            opacity: true,
            hue: true,

            interaction: {
                hex: false,
                rgba: false,
                hsva: false,
                input: true,
                clear: true,
                save: true
            }
        },
        ...options
    }
    if (config.lang === "zh-CN") {
        config.i18n = {
            'btn:save': '保存',
            'btn:clear': '清除'
        }
    }
    return config;
}
