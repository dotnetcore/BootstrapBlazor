import Data from "../../modules/data.js?v=$version"

export function init(id, options) {
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const listHour = el.querySelector('.bb-flip-clock-list.hour');
    const listMinute = el.querySelector('.bb-flip-clock-list.minute');
    const listSecond = el.querySelector('.bb-flip-clock-list.second');

    //if (options.viewMode === "CountDown") {
    //    listSecond.children[0].querySelectorAll('.inn').forEach(v => {
    //        v.innerHTML = `${5 - parseInt(v.innerHTML)}`
    //    })
    //    listSecond.children[1].querySelectorAll('.inn').forEach(v => {
    //        v.innerHTML = `${9 - parseInt(v.innerHTML)}`
    //    })
    //    listMinute.children[0].querySelectorAll('.inn').forEach(v => {
    //        v.innerHTML = `${5 - parseInt(v.innerHTML)}`
    //    })
    //    listMinute.children[1].querySelectorAll('.inn').forEach(v => {
    //        v.innerHTML = `${9 - parseInt(v.innerHTML)}`
    //    })
    //    listHour.children[0].querySelectorAll('.inn').forEach(v => {
    //        v.innerHTML = `${2 - parseInt(v.innerHTML)}`
    //    })
    //    listHour.children[1].querySelectorAll('.inn').forEach(v => {
    //        v.innerHTML = `${3 - parseInt(v.innerHTML)}`
    //    })
    //}

    let counter = 0;
    const getDate = () => {
        if (options.viewMode === "DateTime") {
            const now = new Date();
            return { Hours: now.getHours(), Minutes: now.getMinutes(), Seconds: now.getSeconds() };
        }
        else if (options.viewMode === "Count") {
            counter += 1000;
            const now = new Date(new Date().getTimezoneOffset() * 60 * 1000 - options.startValue + counter);
            return { Hours: now.getHours(), Minutes: now.getMinutes(), Seconds: now.getSeconds() };
        }
        else if (options.viewMode === "CountDown") {
            counter += 1000;
            const now = new Date(new Date().getTimezoneOffset() * 60 * 1000 + options.startValue - counter);
            return { Hours: now.getHours(), Minutes: now.getMinutes(), Seconds: now.getSeconds() };
        }
    }

    const go = () => {
        el.classList.remove('flip');
        const date = getDate();
        setTime(listSecond, date.Seconds);
        setTime(listMinute, date.Minutes);
        setTime(listHour, date.Hours);
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
