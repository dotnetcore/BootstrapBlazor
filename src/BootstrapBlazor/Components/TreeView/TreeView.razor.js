import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

export function init(id, options) {
    const { invoke, method, isVirtualize } = options
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const tree = { el, invoke, isVirtualize };
    Data.set(id, tree)

    EventHandler.on(el, 'mouseenter', '.tree-content', e => {
        const ele = e.delegateTarget.parentNode
        ele.classList.add('hover')
    })

    EventHandler.on(el, 'mouseleave', '.tree-content', e => {
        const ele = e.delegateTarget.parentNode
        ele.classList.remove('hover')
    })

    EventHandler.on(el, 'click', '.tree-node', e => {
        const node = e.delegateTarget
        const prev = node.previousElementSibling;
        const radio = prev.querySelector('[type="radio"]')
        if (radio && radio.getAttribute('disabled') !== 'disabled') {
            radio.click();
        }
    })

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
                const checkbox = el.querySelector(".active > .tree-content > .form-check > .form-check-input");
                if (checkbox) {
                    e.preventDefault();
                    checkbox.click();
                }
            }
        }
    });

    if (isVirtualize) {
        EventHandler.on(el, 'click', '.form-check-input', async e => {
            const checkbox = e.delegateTarget;
            const state = checkbox.getAttribute('data-bb-state');
            if (state) {
                const row = checkbox.closest('.tree-content');
                const index = row.getAttribute('data-bb-tree-view-index');
                await invoke.invokeMethodAsync('SetNodeStateByIndex', parseInt(index), state === '1' ? 0 : 1);
                await setParentState(id, index);
            }
        });
    }
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
                    checkbox.checked = state === 1;
                    checkbox.setAttribute('data-bb-state', state);
                    if (state === 1) {
                        checkbox.parentElement.classList.add('is-checked');
                    }
                    else {
                        checkbox.parentElement.classList.remove('is-checked');
                    }
                }
                next = next.nextElementSibling;
            }
        }
    }
}

export async function setParentState(id, index) {
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
                const results = await invoke.invokeMethodAsync('GetParentsState', parents.map(p => parseInt(p.getAttribute('data-bb-tree-view-index'))));
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
                }
            }
        }
    }
}

export function dispose(id) {
    const tree = Data.get(id)
    Data.remove(id);

    if (tree) {
        const { el, isVirtualize } = tree;
        EventHandler.off(el, 'mouseenter');
        EventHandler.off(el, 'mouseleave');
        EventHandler.off(el, 'click', '.tree-node');
        EventHandler.off(el, 'keyup', '.tree-root');

        if (isVirtualize) {
            EventHandler.off(el, 'click', '.form-check-input');
        };
    }
}
