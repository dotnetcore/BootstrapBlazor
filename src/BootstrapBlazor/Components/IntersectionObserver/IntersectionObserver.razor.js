import Data from "../../modules/data.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const { root, rootMargin, threshold, autoUnobserver } = options;
    const items = [...el.querySelectorAll(".bb-intersection-observer-item")];

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                if (autoUnobserver) {
                    observer.unobserve(entry.target);
                }
                const index = items.indexOf(entry.target);
                invoke.invokeMethodAsync('OnIntersecting', index);
            };
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
        observer = null;
    }
}
