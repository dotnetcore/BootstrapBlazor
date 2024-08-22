import './js/winbox.min.js'
import Data from '../BootstrapBlazor/modules/data.js'
import EventHanlder from '../BootstrapBlazor/modules/event-handler.js'
import { addLink } from "../BootstrapBlazor/modules/utility.js"

export async function init(id, invoke, options) {
    await addLink('./_content/BootstrapBlazor.WinBox/css/winbox.min.css')
}

export function show(id, invoke, option) {
    const el = document.getElementById(id);
    const content = el.querySelector('.bb-win-box-content');
    const config = {
        ...option,
        title: 'Test',
        mount: content,
        onclose: () => {
            invoke.invokeMethodAsync("OnClose", option.id)
        }
    }
    new WinBox(config);
}

export function dispose(id) {

}
