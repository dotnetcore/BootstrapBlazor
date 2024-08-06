import Data from "../../modules/data.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    if (options.useElementViewport === false) {
        options.root = el;
    }
    const { root = null, rootMargin, threshold, autoUnobserve, callback } = options;
    const items = [...el.querySelectorAll(".bb-intersection-observer-item")];

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                if (autoUnobserve) {
                    observer.unobserve(entry.target);
                }
                const index = items.indexOf(entry.target);
                invoke.invokeMethodAsync(callback, index);
            }
        });
    }, { root, rootMargin, threshold });

    items.forEach(item => observer.observe(item));
    Data.set(id, observer);
}

export function dispose(id) {
    const observer = Data.get(id);
    Data.remove(id);

    if (observer) {
        observer.disconnect();
    }
}
