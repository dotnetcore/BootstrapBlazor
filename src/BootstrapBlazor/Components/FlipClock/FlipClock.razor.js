import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const listHour = el.querySelector('.bb-flip-clock-list.hour');
    const listMinute = el.querySelector('.bb-flip-clock-list.minute');
    const listSecond = el.querySelector('.bb-flip-clock-list.second');

    const getDate = () => {
        if (options.viewMode === "DateTime") {
            return new Date();
        }
        else if (options.viewMode === "Count") {
            options.startValue -= 1000;
            return new Date(new Date().getTimezoneOffset() * 60 * 1000 - options.startValue);
        }
        else if (options.viewMode === "CountDown") {

        }
    }

    const go = () => {
        el.classList.remove('flip');
        const date = getDate();
        setTime(listSecond, date.getSeconds());
        setTime(listMinute, date.getMinutes());
        setTime(listHour, date.getHours());
        el.classList.add('flip');
    }

    let start = void 0
    const flip = ts => {
        if (start === void 0) {
            start = ts;
            go();
        }
        const elapsed = ts - start;
        if (elapsed >= 1000) {
            start = ts;
            go();
        }
        requestAnimationFrame(flip);
    }

    requestAnimationFrame(flip);

    Data.set(id, { el, options });
}

export function dispose(id) {
    const clock = Data.get(id)
    if (clock) {

    }
}

const setTime = (list, time) => {
    if (list) {
        const leftIndex = Math.floor(time / 10);
        const rightIndex = time % 10;
        const leftFlip = list.children[0];
        const rightFlip = list.children[1];

        setFlip(leftFlip, leftIndex);
        setFlip(rightFlip, rightIndex);
    }
}

const setFlip = (flip, index) => {
    const before = flip.querySelector('.before');
    if (before) {
        before.classList.remove('before');
    }
    const active = flip.querySelector('.active');
    if (active) {
        active.classList.remove('active');
    }

    const items = flip.children;
    items[index].classList.add('active');
    index--
    if (index < 0) {
        index += items.length;
    }
    items[index].classList.add('before');
}
