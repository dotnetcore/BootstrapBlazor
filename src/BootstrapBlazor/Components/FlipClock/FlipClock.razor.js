import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

export function init(id, options) {
    console.log(options);

    const { invoke, onCompleted, useLocaleTimeZone } = options;
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    if (useLocaleTimeZone) {

    }

    const listHour = el.querySelector('.bb-flip-clock-list.hour');
    const listMinute = el.querySelector('.bb-flip-clock-list.minute');
    const listSecond = el.querySelector('.bb-flip-clock-list.second');

    const go = () => {
        el.classList.remove('flip');
        const date = new Date();
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
}

export function dispose(id) {
    const clock = Data.get(id)
    if (clock) {
    }
}

const setTime = (list, time) => {
    const leftIndex = Math.floor(time / 10);
    const rightIndex = time % 10;
    const leftFlip = list.children[0];
    const rightFlip = list.children[1];

    setFlip(leftFlip, leftIndex);
    setFlip(rightFlip, rightIndex);
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
