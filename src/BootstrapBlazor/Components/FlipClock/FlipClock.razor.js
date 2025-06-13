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

    const listYear = el.querySelector('.bb-flip-clock-list.year');
    const listMonth = el.querySelector('.bb-flip-clock-list.month');
    const listDay = el.querySelector('.bb-flip-clock-list.day');
    const listHour = el.querySelector('.bb-flip-clock-list.hour');
    const listMinute = el.querySelector('.bb-flip-clock-list.minute');
    const listSecond = el.querySelector('.bb-flip-clock-list.second');
    const countDown = options.viewMode === "CountDown";

    let counter = 0;
    const getDate = () => {
        let totalMilliseconds = 0;
        let now;

        if (options.viewMode === "Count") {
            counter += 1000;
            totalMilliseconds = counter - options.startValue;
        }
        else if (countDown) {
            counter += 1000;
            totalMilliseconds = options.startValue - counter;
            if (totalMilliseconds < 0) totalMilliseconds = 0;
        }
        else {
            now = new Date();
            return {
                years: now.getFullYear(),
                months: now.getMonth() + 1,
                days: now.getDate(),
                hours: now.getHours(),
                minutes: now.getMinutes(),
                seconds: now.getSeconds()
            };
        }

        const seconds = Math.floor(totalMilliseconds / 1000) % 60;
        const minutes = Math.floor(totalMilliseconds / (1000 * 60)) % 60;
        const hours = Math.floor(totalMilliseconds / (1000 * 60 * 60)) % 24;
        const days = Math.floor(totalMilliseconds / (1000 * 60 * 60 * 24));
        const months = new Date().getMonth() + 1;
        const years = new Date().getFullYear();
        return { years, months, days, hours, minutes, seconds };
    }

    let lastYear;
    let lastMonth;
    let lastDay;
    let lastHour;
    let lastMinute;
    let lastSecond;
    const go = () => {
        const { years, months, days, hours, minutes, seconds } = getDate();

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
        if (lastDay !== days) {
            lastDay = days;
            setTime(listDay, days, countDown);
        }
        if (lastMonth !== months) {
            lastMonth = months;
            setTime(listMonth, months, countDown);
        }
        if (lastYear !== years) {
            lastYear = years;
            setYear(listYear, years, countDown);
        }
        return { years, months, days, hours, minutes, seconds }
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

const setYear = (list, year, countDown) => {
    if (list) {
        list.classList.remove('flip');
        for (var index = 0; index < 4; index++) {
            const flip = list.children[index];
            const flipIndex = Math.floor(year / Math.pow(10, 3 - index)) % 10;
            setFlip(flip, flipIndex, countDown);
        }
        list.classList.add('flip');
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
