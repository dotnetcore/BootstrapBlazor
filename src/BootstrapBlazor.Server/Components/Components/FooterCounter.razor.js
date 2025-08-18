import Data from "../../_content/BootstrapBlazor/modules/data.js"

export function init(id, totalSeconds) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const tick = () => {
        const counter = Data.get(id);

        counter.totalSeconds = (counter.totalSeconds || 0) + 1;
        const now = Math.round(counter.totalSeconds, 0);

        const days = Math.floor(now / (24 * 3600));
        const hours = Math.floor((now % (24 * 3600)) / 3600);
        const minutes = Math.floor((now % 3600) / 60);
        const seconds = now % 60;

        const pad = num => num.toString().padStart(2, '0');
        el.innerHTML = `Run ${pad(days)}.${pad(hours)}:${pad(minutes)}:${pad(seconds)}`;
    }

    const handler = setInterval(tick, 1000);
    Data.set(id, {
        el,
        handler,
        totalSeconds
    });
}

export function updateFooterCounter(id, totalSeconds) {
    const counter = Data.get(id);
    if (counter) {
        counter.totalSeconds = totalSeconds;
        console.log(`FooterCounter updated: ${id}, totalSeconds: ${totalSeconds}`);
    }
}

export function dispose(id) {
    const counter = Data.get(id);
    if (counter) {
        clearInterval(counter.handler);
        Data.remove(id);
    }
}
