import Data from "../../modules/data.js"
import Drag from "../../modules/drag.js"
import EventHandler from "../../modules/event-handler.js"

const setValue = (picker, point, value) => {
    const { el, val } = picker;
    const mode = el.getAttribute('data-bb-mode');

    if (mode === "Hour") {
        val.Hour = Math.floor(value)
        const isPM = el.querySelector('.bb-time-footer > .active').classList.contains('btn-pm');
        if (isPM) {
            val.Hour += 12;
        }
    }
    else if (mode === "Minute") {
        val.Minute = Math.floor(value * 5)
    }
    else {
        val.Second = Math.floor(value * 5)
    }
    setTime(picker);
    setPoint(picker, point);
}

const setTime = picker => {
    const { el, val } = picker;
    const mode = el.getAttribute('data-bb-mode');
    const hourEl = el.querySelector('.bb-time-text.hour');
    const minuteEl = el.querySelector('.bb-time-text.minute');
    const secondEl = el.querySelector('.bb-time-text.second');

    if (mode === "Hour") {
        let hour = Math.round(val.Hour)
        if (hour > 12) {
            hour = hour - 12;
        }
        hourEl.textContent = hour.toString().padStart(2, '0');
    }
    if (mode === "Minute") {
        minuteEl.textContent = val.Minute.toString().padStart(2, '0');
    }
    if (mode === "Second") {
        secondEl.textContent = val.Second.toString().padStart(2, '0');
    }
}

const setPoint = (picker, point) => {
    if (point.parentNode.classList.contains('bb-clock-panel-hour')) {
        setDeg(point, picker.val.Hour, 30)
    }
    else if (point.parentNode.classList.contains('bb-clock-panel-minute')) {
        setDeg(point, picker.val.Minute, 6)
    }
    else {
        setDeg(point, picker.val.Second, 6)
    }
}

const setDeg = (point, value, rate) => {
    const deg = value * rate;
    point.style.setProperty('transform', `rotate(${deg}deg)`);
}

const initDrag = (picker, pointers) => {
    const { el, invoke } = picker;
    pointers.forEach(p => {
        Drag.drag(p,
            e => {
                el.classList.add('dragging');
            },
            e => {
                const panel = p.parentNode;
                const rect = panel.getBoundingClientRect();
                const cent = {
                    left: rect.left + panel.offsetWidth / 2,
                    top: rect.top + panel.offsetHeight / 2
                };

                const x = (e.clientX || e.touches[0].clientX) - cent.left;
                const y = (e.clientY || e.touches[0].clientY) - cent.top;
                const deg = Math.atan2(y, x) * 6 / Math.PI + 15;
                const val = deg % 12;
                setValue(picker, p, val);
            },
            e => {
                el.classList.remove('dragging');
                console.log('drop-end')
                invoke.invokeMethodAsync('SetTime', picker.val.Hour, picker.val.Minute, picker.val.Second);
            }
        );

        setPoint(picker, p);
    })
}

export function init(id, options) {
    const { invoke, hour, minute, second } = options;
    const el = document.getElementById(id);
    const picker = {
        el, invoke,
        val:
        {
            Hour: hour, Minute: minute, Second: second
        }
    };
    Data.set(id, picker);

    initDrag(picker, [...el.querySelectorAll('.bb-clock-point')]);

    EventHandler.on(el, 'click', '.bb-time-body .bb-clock-panel > div', e => {
        const val = parseInt(e.delegateTarget.textContent);
        const point = e.delegateTarget.parentNode.querySelector('.bb-clock-point');

        if (e.delegateTarget.parentNode.classList.contains('bb-clock-panel-hour')) {
            picker.val.Hour = val;
            setDeg(point, val, 30);
        }
        if (e.delegateTarget.parentNode.classList.contains('bb-clock-panel-minute')) {
            picker.val.Minute = val;
            setDeg(point, val, 6);
        }
        if (e.delegateTarget.parentNode.classList.contains('bb-clock-panel-second')) {
            picker.val.Second = val;
            setDeg(point, val, 6);
        }
        invoke.invokeMethodAsync('SetTime', picker.val.Hour, picker.val.Minute, picker.val.Second);
    })
}

export function update(id, options) {
    const { hour, minute, second, version } = options;
    const picker = Data.get(id);
    if (picker) {
        const { el, val } = picker;
        if (version === 'NET6.0') {
            const pointers = [...el.querySelectorAll('.bb-clock-point')];
            pointers.forEach(p => {
                Drag.dispose(p);
            });
            initDrag(picker, pointers);
        }
        if (val.Hour !== hour) {
            val.Hour = hour;
            const point = el.querySelector('.bb-clock-panel-hour > .bb-clock-point')
            setDeg(point, hour, 30)
        }
        if (val.Minute !== minute) {
            val.Minute = minute;
            const point = el.querySelector('.bb-clock-panel-minute > .bb-clock-point')
            setDeg(point, minute, 6)
        }
        if (val.Second !== second) {
            val.Second = second;
            const point = el.querySelector('.bb-clock-panel-second > .bb-clock-point')
            setDeg(point, second, 6)
        }
    }
}

export function dispose(id) {
    const picker = Data.get(id);
    Data.remove(id);

    if (picker) {
        EventHandler.off(picker.body, 'click', '.bb-clock-panel > div');
        picker.pointers.forEach(p => {
            Drag.dispose(p);
        });
    }
}
