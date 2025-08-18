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

    let counter = 0;
    let totalMilliseconds = 0;
    let countDown = false;
    const getDate = () => {
        const view = options.viewMode;
        countDown = false;
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
            counter += 1000;
            totalMilliseconds = counter - options.startValue;
        }
        else if (view === "CountDown") {
            countDown = true;
            counter += 1000;
            totalMilliseconds = options.startValue - counter;
            if (totalMilliseconds < 0) totalMilliseconds = 0;
        }

        const seconds = Math.floor(totalMilliseconds / 1000) % 60;
        const minutes = Math.floor(totalMilliseconds / (1000 * 60)) % 60;
        const hours = Math.floor(totalMilliseconds / (1000 * 60 * 60)) % 24;
        const days = Math.floor(totalMilliseconds / (1000 * 60 * 60 * 24));
        const months = 0;
        const years = 0;
        return { years, months, days, hours, minutes, seconds };
    };

    const getConfig = () => [
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

    const go = () => {
        const d = getDate();
        const unitConfig = getConfig();
        unitConfig.forEach(({ key, list, digits }) => {
            if (list === null) {
                return;
            }

            setDigits(list, d[key], digits, countDown);
        });
        return d;
    };

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
