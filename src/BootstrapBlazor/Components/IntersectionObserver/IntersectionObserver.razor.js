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
    if (options.threshold && options.threshold.indexOf(' ') > 0) {
        options.threshold = options.threshold.split(' ');
    }
    const { root, rootMargin, threshold, autoUnobserveWhenIntersection, autoUnobserveWhenNotIntersection, callback } = options;
    const option = { root, rootMargin: rootMargin ?? '0px 0px 0px 0px', threshold: threshold ?? 0 };

    const observer = new IntersectionObserver(entries => {
        entries.forEach(entry => {
            if(entry.isIntersecting && autoUnobserveWhenIntersection) {
                observer.unobserve(entry.target);
            }
            else if(!entry.isIntersecting && autoUnobserveWhenNotIntersection) {
                observer.unobserve(entry.target);
            }
            const index = items.indexOf(entry.target);
            invoke.invokeMethodAsync(callback, {
                isIntersecting: entry.isIntersecting,
                index,
                time: entry.time,
                intersectionRatio: entry.intersectionRatio
            });
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
