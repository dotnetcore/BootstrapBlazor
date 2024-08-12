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
        element = el.querySelector(options.rootSelector);
        delete op.rootSelector;
    }
    if (op.clone === true) {
        op.group = {
            name: op.group,
            pull: 'clone'
        };
        delete op.clone;
    }

    if (element) {
        const sortable = Sortable.create(element, op);
        Data.set(id, { el, element, sortable });
    }
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

    if (sortable) {

    }
}
