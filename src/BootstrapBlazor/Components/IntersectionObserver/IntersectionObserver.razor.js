import Data from "../../modules/data.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const items = [...el.querySelectorAll(".bb-intersection-observer-item")];

    if (options.useElementViewport === false) {
        options.root = el;
    }
    const { root, rootMargin, threshold, autoUnobserve, callback } = options;
    const option = { root, rootMargin: rootMargin ?? '0px 0px 0px 0px', threshold: threshold ?? 0 };

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (autoUnobserve) {
                observer.unobserve(entry.target);
            }
            const index = items.indexOf(entry.target);
            invoke.invokeMethodAsync(callback, entry.isIntersecting, index);
        });
    }, option);

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
