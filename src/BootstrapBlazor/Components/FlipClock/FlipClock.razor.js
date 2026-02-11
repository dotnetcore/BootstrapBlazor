export function init(id, invoke, options) {
    options = {
        ...{
            viewMode: 'DateTime',
            startValue: 0,
            onCompleted: null,
            counter: 0,
            totalMilliseconds : 0,
            countDown: false
        },
        ...options
    }
    const el = document.getElementById(id);
    if (el === null) {
        return;
    }

    let start = void 0
    let current;

    const flip = ts => {
        if (start === void 0) {
            start = ts;
            current = go(el, options);
        }
        const elapsed = ts - start;
        if (elapsed >= 1000) {
            start = ts;
            current = go(el, options);
        }

        if (options.countDown && current.hours === 0 && current.minutes === 0 && current.seconds === 0) {
            invoke.invokeMethodAsync(options.onCompleted);
            return;
        }
        requestAnimationFrame(flip);
    }

    requestAnimationFrame(flip);
}

const go = (el, options) => {
    const d = getDate(options);
    const unitConfig = getConfig(el);
    unitConfig.forEach(({ key, list, digits }) => {
        if (list === null) {
            return;
        }

        setDigits(list, d[key], digits, options.countDown);
    });
    return d;
};

const getDate = (options) => {
    const view = options.viewMode;
    options.countDown = false;
    if (view === "DateTime") {
        const now = new Date();
        return {
            years: now.getFullYear(),
            months: now.getMonth() + 1,
            days: now.getDate(),
            hours: now.getHours(),
            minutes: now.getMinutes(),
            seconds: now.getSeconds()
        };
    }
    else if (view === "Count") {
        options.counter += 1000;
        options.totalMilliseconds = options.counter - options.startValue;
    }
    else if (view === "CountDown") {
        options.countDown = true;
        options.counter += 1000;
        options.totalMilliseconds = options.startValue - options.counter;
        if (options.totalMilliseconds < 0) options.totalMilliseconds = 0;
    }

    const seconds = Math.floor(options.totalMilliseconds / 1000) % 60;
    const minutes = Math.floor(options.totalMilliseconds / (1000 * 60)) % 60;
    const hours = Math.floor(options.totalMilliseconds / (1000 * 60 * 60)) % 24;
    const days = Math.floor(options.totalMilliseconds / (1000 * 60 * 60 * 24));
    const months = 0;
    const years = 0;
    return { years, months, days, hours, minutes, seconds };
};

const getConfig = el => [
    { key: 'years', list: el.querySelector('.bb-flip-clock-list.year'), digits: 4 },
    { key: 'months', list: el.querySelector('.bb-flip-clock-list.month'), digits: 2 },
    { key: 'days', list: el.querySelector('.bb-flip-clock-list.day'), digits: 2 },
    { key: 'hours', list: el.querySelector('.bb-flip-clock-list.hour'), digits: 2 },
    { key: 'minutes', list: el.querySelector('.bb-flip-clock-list.minute'), digits: 2 },
    { key: 'seconds', list: el.querySelector('.bb-flip-clock-list.second'), digits: 2 },
];

const setDigits = (list, value, digits, countDown) => {
    list.classList.remove('flip');
    for (let i = 0; i < digits; i++) {
        const place = digits - 1 - i;
        const digit = Math.floor(value / 10 ** place) % 10;
        setFlip(list.children[i], digit, countDown);
    }
    list.classList.add('flip');
};

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
