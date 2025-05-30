export function execute(id, title) {
    const el = document.getElementById(id);

    if (el) {
        const tip = bootstrap.Tooltip.getOrCreateInstance(el, { customClass: 'is-invalid', title })
        if (!tip._isShown()) {
            if (title !== tip._config.title) {
                tip._config.title = title;
            }

            if (showResult(el)) {
                tip.show();
            }
        }
    }
}

const showResult = el => {
    let ret = false;
    const form = el.closest('form');
    if (form) {
        const show = form.getAttribute("data-bb-invalid-result");
        if (show === "true") {
            ret = true;
        }
    }
    return ret;
}

export function dispose(id) {
    const el = document.getElementById(id)

    if (el) {
        const tip = bootstrap.Tooltip.getInstance(el)
        if (tip) {
            tip.dispose()
        }
    }
}

export function executeUpload(items, invalidItems, addId) {
    items.forEach(id => {
        const el = document.getElementById(id);
        if (el) {
            const item = invalidItems.find(i => i.id === id);
            if (item) {
                const { id, errorMessage } = item;
                execute(id, errorMessage);
                el.classList.remove('is-valid');
                el.classList.add('is-invalid');
            }
            else {
                dispose(id);
                el.classList.remove('is-invalid');
                el.classList.add('is-valid');
            }
        }
    });

    if (addId) {
        const el = document.getElementById(addId);
        if (el) {
            el.classList.remove('is-valid', 'is-invalid');
            dispose(addId);
        }
    }
}

export function disposeUpload(items) {
    items.forEach(id => {
        const el = document.getElementById(id);
        if (el) {
            dispose(id);
        }
    });
}
