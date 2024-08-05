import Data from "../../modules/data.js"

export function init(id, invoke, options) {
    console.log(options);

    const observer = new IntersectionObserver(entries => {

    }, options);

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
