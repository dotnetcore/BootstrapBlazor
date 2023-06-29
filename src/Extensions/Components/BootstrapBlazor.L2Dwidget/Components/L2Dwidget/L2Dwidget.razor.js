import { L2Dwidget } from '../../../BootstrapBlazor.L2Dwidget/L2Dwidget.min.js'

export function init(op) {
    const config = {
        model: op.model,
        display: op.display,
        mobile: op.mobile,
        name: op.name,
        dialog: op.dialog
    };

    L2Dwidget.init(config);
}

export function dispose() {

}
