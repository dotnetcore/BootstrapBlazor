﻿import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, options) {
    const { invoke, method, isVirtualize } = options
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const tree = { el, invoke, isVirtualize };
    Data.set(id, tree)

    EventHandler.on(el, 'keydown', '.tree-root', e => {
        if (e.key === 'ArrowDown' || e.key === 'ArrowUp' || e.key === 'ArrowLeft' || e.key === 'ArrowRight') {
            const v = el.getAttribute('data-bb-keyboard');
            if (v === "true") {
                e.preventDefault();
                invoke.invokeMethodAsync(method, e.key);
            }
        }
        else if (e.keyCode === 32) {
            const v = el.getAttribute('data-bb-keyboard');
            if (v === "true") {
                const checkbox = el.querySelector(".active .form-check-input");
                if (checkbox) {
                    e.preventDefault();
                    checkbox.click();
                }
            }
        }
    });
}

export function scroll(id, options) {
    const tree = Data.get(id)
    if (tree) {
        const { el } = tree;
        const item = el.querySelector(".active .tree-node");
        if (item) {
            item.scrollIntoView(options);
        }
    }
}

export function toggleLoading(id, index, state) {
    const tree = Data.get(id)
    if (tree) {
        const { el } = tree;
        const node = el.querySelector(`[data-bb-tree-view-index="${index}"]`);
        if (node) {
            if (state) {
                node.classList.add('loading');
            }
            else {
                node.classList.remove('loading');
            }
        }
    }
}

export function setChildrenState(id, index, state) {
    const tree = Data.get(id)
    if (tree) {
        const { el } = tree;
        const node = el.querySelector(`[data-bb-tree-view-index="${index}"]`);
        const level = parseInt(node.style.getPropertyValue('--bb-tree-view-level'));
        if (node) {
            let next = node.nextElementSibling;
            while (next) {
                const currentLevel = parseInt(next.style.getPropertyValue('--bb-tree-view-level'));
                if (currentLevel <= level) {
                    break;
                }
                const checkbox = next.querySelector('.form-check-input');
                if (checkbox) {
                    checkbox.indeterminate = false;
                    checkbox.checked = state === 1;
                    EventHandler.trigger(checkbox, "statechange.bb.checkbox", { state });
                }
                next = next.nextElementSibling;
            }
        }
    }
}

export async function setParentState(id, index, state) {
    const tree = Data.get(id)
    if (tree) {
        const { el, invoke } = tree;
        const node = el.querySelector(`[data-bb-tree-view-index="${index}"]`);
        let level = parseInt(node.style.getPropertyValue('--bb-tree-view-level'));
        if (node) {
            const parents = [];
            let prev = node.previousElementSibling;
            while (prev) {
                const currentLevel = parseInt(prev.style.getPropertyValue('--bb-tree-view-level'));
                if (currentLevel < level) {
                    level = currentLevel;
                    parents.push(prev);
                }
                prev = prev.previousElementSibling;
            }

            if (parents.length > 0) {
                const results = await invoke.invokeMethodAsync('GetParentsState', parents.map(p => parseInt(p.getAttribute('data-bb-tree-view-index'))), index, state);
                for (let index = 0; index < parents.length; index++) {
                    const checkbox = parents[index].querySelector('.form-check-input');
                    const result = results[index];
                    checkbox.indeterminate = false;
                    if (result === 0) {
                        checkbox.checked = false;
                    }
                    else if (result === 1) {
                        checkbox.checked = true;
                    }
                    else {
                        checkbox.indeterminate = true;
                    }
                    EventHandler.trigger(checkbox, "statechange.bb.checkbox", { state: result });
                }
            }
        }
    }
}

export function dispose(id) {
    const tree = Data.get(id)
    Data.remove(id);

    if (tree) {
        const { el } = tree;
        EventHandler.off(el, 'keyup', '.tree-root');
    }
}
