﻿import { getDescribedElement } from "../../modules/utility.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, options) {
    const el = document.getElementById(id)
    if (el) {
        createPopover(el, options);
    }
}

export function dispose(id) {
    const el = document.getElementById(id)
    if (el) {
        const pop = bootstrap.Popover.getInstance(el)
        if (pop) {
            pop.dispose();
        }
    }
}

const createPopover = (el, options) => {
    const { content, template } = options;
    const pop = bootstrap.Popover.getOrCreateInstance(el);
    if (template) {
        EventHandler.on(el, 'inserted.bs.popover', () => insertedPopover(el));
        EventHandler.on(el, 'hide.bs.popover', () => hidePopover(el));
    }
    else if (content === void 0 || content === '') {
        pop.disable();
    }
    else {
        pop.enable();
        pop._config.content = content;
    }
}

const insertedPopover = el => {
    const popover = getDescribedElement(el)
    if (popover) {
        const body = document.createElement('div');
        body.className = 'popover-body';
        const template = el.querySelector('template');
        [...template.children].forEach(i => {
            body.appendChild(i);
        });
        popover.appendChild(body);
    }
}

const hidePopover = el => {
    const popover = getDescribedElement(el)
    if (popover) {
        const body = popover.querySelector('.popover-body');
        const template = el.querySelector('template');
        [...body.children].forEach(i => {
            template.appendChild(i);
        });
        body.remove();
    }
}