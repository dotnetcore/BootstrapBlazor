import './js/winbox.min.js'
import Data from '../BootstrapBlazor/modules/data.js'
import EventHanlder from '../BootstrapBlazor/modules/event-handler.js'
import { addLink } from "../BootstrapBlazor/modules/utility.js"

export async function init(id) {
    await addLink('./_content/BootstrapBlazor.WinBox/css/winbox.bundle.css')
}

export function show(id, invoke, option) {
    const el = document.getElementById(id);
    const content = el.querySelector('.bb-winbox-content');
    const config = {
        ...option,
        mount: content,
        onclose: () => {
            invoke.invokeMethodAsync("OnClose", config.id);
        }
    }
    if (config.triggerOnCreate) {
        delete config.triggerOnCreate;
        config.oncreate = () => {
            invoke.invokeMethodAsync("OnCreate", config.id);
        }
    }
    if (config.triggerOnShow) {
        delete config.triggerOnShow;
        config.onshow = () => {
            invoke.invokeMethodAsync("OnShow", config.id);
        }
    }
    if (config.triggerOnHide) {
        delete config.triggerOnHide;
        config.onhide = () => {
            invoke.invokeMethodAsync("OnHide", config.id);
        }
    }
    if (config.triggerOnFocus) {
        delete config.triggerOnFocus;
        config.onfocus = () => {
            invoke.invokeMethodAsync("OnFocus", config.id);
        }
    }
    if (config.triggerOnBlur) {
        delete config.triggerOnBlur;
        config.onblur = () => {
            invoke.invokeMethodAsync("OnBlur", config.id);
        }
    }
    if (config.triggerOnFullscreen) {
        delete config.triggerOnFullscreen;
        config.onfullscreen = () => {
            invoke.invokeMethodAsync("OnFullscreen", config.id);
        }
    }
    if (config.triggerOnRestore) {
        delete config.triggerOnRestore;
        config.onrestore = () => {
            invoke.invokeMethodAsync("OnRestore", config.id);
        }
    }
    if (config.triggerOnMaximize) {
        delete config.triggerOnMaximize;
        config.onmaximize = () => {
            invoke.invokeMethodAsync("OnMaximize", config.id);
        }
    }
    if (config.triggerOnMinimize) {
        delete config.triggerOnMinimize;
        config.onminimize = () => {
            invoke.invokeMethodAsync("OnMinimize", config.id);
        }
    }
    const winbox = new WinBox(config);
    Data.set(id, { el, winbox });
}

export function execute(id, method, args) {
    const instance = Data.get(id);
    if (instance) {
        const { winbox } = instance;
        if (winbox) {
            winbox[method].call(winbox, args);
        }
    }
}

export function stack() {
    WinBox.stack();
}

export function dispose(id) {
    Data.remove(id);
}
