import Sortable from '../sortable.esm.js'
import Data from '../../BootstrapBlazor/modules/data.js'

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const op = getOptions(options);
    let element = el;
    if (op.rootSelector) {
        loopCheck(id, element, invoke, op);
    }
    else {
        initSortable(id, element, invoke, op);
    }
}

const loopCheck = (id, el, invoke, op) => {
    const check = () => {
        const element = el.querySelector(op.rootSelector);
        if (element === null) {
            op.loopCheckHeightHandler = requestAnimationFrame(check);
        }
        else {
            delete op.loopCheckHeightHandler;
            initSortable(id, element, invoke, op);
        }
    };
    check();
}

const initSortable = (id, element, invoke, op) => {
    delete op.rootSelector;

    op.group = {
        name: op.group
    };

    if (op.clone === true) {
        op.group = {
            ...op.group,
            pull: 'clone'
        };
        delete op.clone;
    }

    if (op.putback === false) {
        op.group = {
            ...op.group,
            pull: 'clone',
            put: false
        };
        delete op.putback;
    }

    op.onUpdate = event => {
        event.item.remove();
        event.to.insertBefore(event.item, event.to.childNodes[event.oldIndex]);
        invoke.invokeMethodAsync('TriggerUpdate', event.oldIndex, event.newIndex);
    }

    op.onRemove = event => {
        event.item.remove();
        event.from.insertBefore(event.item, event.from.childNodes[event.oldIndex]);
        invoke.invokeMethodAsync('TriggerRemove', event.oldIndex, event.newIndex);
    }

    const sortable = Sortable.create(element, op);
    Data.set(id, { element, sortable });
}

const getOptions = options => {
    return {
        rootSelector: null,
        animation: 150,
        ...options
    }
}

export function dispose(id) {
    const sortable = Data.get(id);
    Data.remove(id);
}
