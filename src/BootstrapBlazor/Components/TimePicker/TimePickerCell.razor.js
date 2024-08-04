import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, invoke) {
    const el = document.getElementById(id)
    const picker = {
        el, invoke,
        upCallback: 'OnClickUp',
        downCallback: 'OnClickDown',
        heightCallback: 'OnHeightCallback',
        spinnerSelector: '.time-spinner-list',
        spinnerItemSelector: '.time-spinner-item'
    }
    Data.set(id, picker);

    const item = el.querySelector(picker.spinnerItemSelector)
    loopCheckHeight(picker, item);

    EventHandler.on(el, 'mousewheel', picker.spinnerSelector, e => {
        e.preventDefault()
        e.stopPropagation()

        const margin = e.wheelDeltaY || -e.deltaY;
        if (margin > 0) {
            invoke.invokeMethodAsync(picker.upCallback);
        } else {
            invoke.invokeMethodAsync(picker.downCallback);
        }
    })
}

export function dispose(id) {
    const picker = Data.get(id)
    Data.remove(id)

    if(picker.loopHandler) {
        clearTimeout(picker.loopHandler);
    }
    EventHandler.off(picker.el, 'mousewheel', picker.spinnerSelector)
}

const loopCheckHeight = (picker, item) => {
    const handler = setTimeout(() => {
        const styles = getComputedStyle(item)
        const height = parseFloat(styles.getPropertyValue('height')) || 0;

        if(height > 0) {
            clearTimeout(handler);
            delete picker.loopHandler;
            picker.invoke.invokeMethodAsync(picker.heightCallback, height);
        }
    }, 200);
    picker.loopHandler = handler;
}
