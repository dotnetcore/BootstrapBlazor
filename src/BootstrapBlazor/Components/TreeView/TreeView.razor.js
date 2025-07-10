import EventHandler from "../../modules/event-handler.js"
import { insertBefore } from "../../modules/utility.js"

export function init(id, options) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const { invoke, method, allowDrag, triggerDragEnd } = options
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

    if (allowDrag) {
        resetTreeViewRow(id);

        EventHandler.on(el, 'dragstart', e => {
            el.targetItem = e.target;
            el.targetItem.classList.add('drag-item');

            e.dataTransfer.setData('text/plain', '');
            e.dataTransfer.effectAllowed = 'move';
            el.classList.add('dragging');
        });

        EventHandler.on(el, 'dragend', e => {
            el.classList.remove('dragging');
            el.targetItem.classList.remove('drag-item');

            const item = el.targetItem.closest('.tree-content');
            const originalIndex = parseInt(item.getAttribute("data-bb-tree-view-index"));

            let isChildren = false;
            let targetItem = null;
            const overItem = el.querySelector('.tree-drag-inside-over');
            if (overItem) {
                overItem.classList.remove('tree-drag-inside-over');
                isChildren = true;
                targetItem = overItem.closest('.tree-content');
            }
            else {
                const belowItem = el.querySelector('.tree-node-placeholder');
                if (belowItem) {
                    targetItem = belowItem.closest('.tree-content');
                    belowItem.remove();
                }
            }
            delete el.targetItem;

            const currentIndex = parseInt(targetItem.getAttribute("data-bb-tree-view-index"));
            invoke.invokeMethodAsync(triggerDragEnd, originalIndex, currentIndex, isChildren);
        });

        EventHandler.on(el, 'dragenter', '.tree-drop-child-inside', e => {
            e.preventDefault();

            const item = e.delegateTarget;
            item.classList.add('tree-drag-inside-over');
        });
        EventHandler.on(el, 'dragenter', '.tree-drop-child-below', e => {
            e.preventDefault()

            const item = e.delegateTarget;
            const placeholder = createPlaceholder();
            item.appendChild(placeholder);
        });

        EventHandler.on(el, 'dragleave', '.tree-drop-child-inside', e => {
            e.preventDefault()

            const item = e.delegateTarget;
            item.classList.remove('tree-drag-inside-over');
        });
        EventHandler.on(el, 'dragleave', '.tree-drop-child-below', e => {
            e.preventDefault()

            const item = e.delegateTarget;
            item.classList.remove('tree-drag-below-over');
            item.innerHTML = "";
        });

        EventHandler.on(el, 'dragover', '.tree-drop-zone', e => {
            e.preventDefault()
        });
    }
}

export function resetTreeViewRow(id) {
    const el = document.getElementById(id);
    const rows = [...el.querySelectorAll('.tree-content')];
    rows.forEach(row => {
        const node = row.querySelector('.tree-node');
        if (node) {
            node.setAttribute('draggable', 'true');
            const prevElement = node.previousElementSibling;
            if (prevElement && !prevElement.classList.contains('tree-drop-zone')) {
                const dropzone = createDropzone();
                insertBefore(node, dropzone);
            }
        }
    });
}

const createDropzone = () => {
    const div = document.createElement('div');
    div.classList.add(`tree-drop-zone`);

    const inside = document.createElement('div');
    inside.classList.add(`tree-drop-child-inside`);

    const below = document.createElement('div');
    below.classList.add(`tree-drop-child-below`);

    div.appendChild(inside);
    div.appendChild(below);

    return div
}

const createPlaceholder = () => {
    const div = document.createElement('div');
    div.classList.add(`tree-node-placeholder`);

    const circle = document.createElement('div');
    circle.classList.add(`tree-node-ph-circle`);

    const line = document.createElement('div');
    line.classList.add(`tree-node-ph-line`);

    div.appendChild(circle);
    div.appendChild(line);

    return div
}

export function scroll(id, options) {
    const el = document.getElementById(id);
    const item = el.querySelector(".tree-content.active");
    if (item) {
        item.scrollIntoView(options ?? { behavior: 'smooth', block: 'nearest', inline: 'start' });
    }
}

export function toggleLoading(id, index, state) {
    const el = document.getElementById(id);
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

export function setChildrenState(id, index, state) {
    const el = document.getElementById(id);
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

export async function setParentState(id, invoke, method, index) {
    const el = document.getElementById(id);
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
            const results = await invoke.invokeMethodAsync(method, parents.map(p => parseInt(p.getAttribute('data-bb-tree-view-index'))));
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

export function dispose(id) {
    const el = document.getElementById(id);

    if (el) {
        EventHandler.off(el, 'keyup', '.tree-root');
        EventHandler.off(el, 'dragstart');
        EventHandler.off(el, 'dragend');
        EventHandler.off(el, 'dragenter');
        EventHandler.off(el, 'dragleave');
        EventHandler.off(el, 'dragover');
    }
}
