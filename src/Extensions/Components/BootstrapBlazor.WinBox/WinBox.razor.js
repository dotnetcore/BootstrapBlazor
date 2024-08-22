import './js/winbox.min.js'
import Data from '../BootstrapBlazor/modules/data.js'
import EventHanlder from '../BootstrapBlazor/modules/event-handler.js'
import { addLink } from "../BootstrapBlazor/modules/utility.js"

export async function init(id, invoke, options) {
    await addLink('./_content/BootstrapBlazor.WinBox/css/winbox.bundle.css')
}

export function show(id, invoke, option) {
    const el = document.getElementById(id);
    const content = el.querySelector('.bb-win-box-content');
    const config = {
        ...option,
        title: 'Test',
        mount: content,
        onclose: () => {
            invoke.invokeMethodAsync("OnClose", option.id);
        }
    }
    if (option.triggerOnCreate) {
        config.oncreate = () => {
            invoke.invokeMethodAsync("OnCreate", option.id);
        }
    }
    if (option.triggerOnShown) {
        config.onshown = () => {
            invoke.invokeMethodAsync("OnShown", option.id);
        }
    }
    if (option.triggerOnHide) {
        config.onhide = () => {
            invoke.invokeMethodAsync("OnHide", option.id);
        }
    }
    if (option.triggerOnFocus) {
        config.onfocus = () => {
            invoke.invokeMethodAsync("OnFocus", option.id);
        }
    }
    if (option.triggerOnBlur) {
        config.onblur = () => {
            invoke.invokeMethodAsync("OnBlur", option.id);
        }
    }
    if (option.triggerOnFullscreen) {
        config.onfullscreen = () => {
            invoke.invokeMethodAsync("OnFullscreen", option.id);
        }
    }
    if (option.triggerOnRestore) {
        config.onrestore = () => {
            invoke.invokeMethodAsync("OnRestore", option.id);
        }
    }
    if (option.triggerOnMaximize) {
        config.onmaximize = () => {
            invoke.invokeMethodAsync("OnMaximize", option.id);
        }
    }
    if (option.triggerOnMinimize) {
        config.onminimize = () => {
            invoke.invokeMethodAsync("OnMinimize", option.id);
        }
    }
    new WinBox(config);
}

export function dispose(id) {

}
