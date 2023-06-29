import { addLink, addScript } from '../../../BootstrapBlazor/modules/utility.js'
import Data from '../../../BootstrapBlazor/modules/data.js'
import L2Dwidget from '../../../BootstrapBlazor.L2Dwidget/L2Dwidget.min.js'

export async function init(id, op) {
    const widget = L2Dwidget.init({
        "model": {
            "jsonPath": "https://unpkg.com/live2d-widget-model-shizuku@1.0.5/assets/shizuku.model.json",
            "scale": 1,
            "hHeadPos": 0.5,
            "vHeadPos": 0.618
        },
        "dialog": {
            enable: true,
            script: {
                'tap body': '哎呀！别碰我！',
                'tap face': '哎呀！讨厌！',
            }
        },
        "mobile": {
            "show": true,
            scale: 0.5
        },
        "display": {
            "position": "right",
        }
    });

    Data.set(id, { el, widget, op })
}

export function dispose(id) {
    const widget = Data.get(id)
    Data.remove(id)

}
