import './js/winbox.min.js'
import Data from '../BootstrapBlazor/modules/data.js'
import EventHanlder from '../BootstrapBlazor/modules/event-handler.js'
import { addLink } from "../BootstrapBlazor/modules/utility.js"

export async function init(id, invoke, options) {
    await addLink('./_content/BootstrapBlazor.WinBox/css/winbox.min.css')
}

export function show(options) {
    new WinBox({
        title: 'Test'
    });
}

export function dispose(id) {

}
