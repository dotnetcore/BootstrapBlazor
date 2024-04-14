import Data from "../../modules/data.js"

export function init(id, options) {
    options = {
        ...{
            viewMode: 'DateTime',
            startValue: 0,
            onCompleted: null
        },
        ...options
    }
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    const listHour = el.querySelector('.bb-flip-clock-list.hour');
    const listMinute = el.querySelector('.bb-flip-clock-list.minute');
    const listSecond = el.querySelector('.bb-flip-clock-list.second');
    const countDown = options.viewMode === "CountDown";

    let counter = 0;
    const getDate = () => {
        let now;
        if (options.viewMode === "Count") {
            counter += 1000;
            now = new Date(new Date().getTimezoneOffset() * 60 * 1000 - options.startValue + counter);
        }
        else if (countDown) {
            counter += 1000;
            now = new Date(new Date().getTimezoneOffset() * 60 * 1000 + options.startValue - counter);
        }
        else {
            now = new Date();
        }
        return { hours: now.getHours(), minutes: now.getMinutes(), seconds: now.getSeconds() };
    }

    let lastHour;
    let lastMinute;
    let lastSecond;
    const go = () => {
        const { hours, minutes, seconds } = getDate();

        if (lastSecond !== seconds) {
            lastSecond = seconds;
            setTime(listSecond, seconds, countDown);
        }
        if (lastMinute !== minutes) {
            lastMinute = minutes;
            setTime(listMinute, minutes, countDown);
        }
        if (lastHour !== hours) {
            lastHour = hours;
            setTime(listHour, hours, countDown);
        }
        return { hours, minutes, seconds }
    }

    let start = void 0
    let current;
    const flip = ts => {
        if (start === void 0) {
            start = ts;
            current = go();
        }
        const elapsed = ts - start;
        if (elapsed >= 1000) {
            start = ts;
            current = go();
        }

        if (countDown && current.hours === 0 && current.minutes === 0 && current.seconds === 0) {
            options.invoke.invokeMethodAsync(options.onCompleted);
            return;
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

const setTime = (list, time, countDown) => {
    if (list) {
        const leftIndex = Math.floor(time / 10);
        const rightIndex = time % 10;
        const leftFlip = list.children[0];
        const rightFlip = list.children[1];

        list.classList.remove('flip');
        setFlip(leftFlip, leftIndex, countDown);
        setFlip(rightFlip, rightIndex, countDown);
        list.classList.add('flip');
    }
}

const setFlip = (flip, index, countDown) => {
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
    if (countDown) {
        index++;
        if (index >= items.length) {
            index = 0;
        }
    }
    else {
        index--;
        if (index < 0) {
            index += items.length;
        }
    }
    items[index].classList.add('before');
}
