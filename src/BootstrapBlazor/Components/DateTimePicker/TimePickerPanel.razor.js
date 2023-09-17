import Data from "../../modules/data.js?v=$version"
import Drag from "../../modules/drag.js?v=$version"

const setTime = picker => {
    const { el, val } = picker;
    const mode = el.getAttribute('data-bb-mode');
    if (mode === "Hour") {
        let hour = Math.round(val.Hour)
        if (hour > 12) {
            hour = hour - 12;
        }
        el.querySelector('.bb-time-text.hour').textContent = hour.toString().padStart(2, '0');
    }
    if (mode === "Minute") {
        el.querySelector('.bb-time-text.minute').textContent = val.Minute.toString().padStart(2, '0');
    }
    if (mode === "Second") {
        el.querySelector('.bb-time-text.second').textContent = val.Second.toString().padStart(2, '0');
    }
}

const setValue = (picker, point, value) => {
    const { el, val } = picker;
    const mode = el.getAttribute('data-bb-mode');

    if (mode === "Hour") {
        val.Hour = value
    }
    else if (mode === "Minute") {
        val.Minute = value
    }
    else {
        val.Second = value
    }
    setPoint(picker, point);
    setTime(picker);
}

const setPoint = (picker, point) => {
    if (point.parentNode.classList.contains('bb-clock-panel-hour')) {
        const deg = picker.val.Hour * 30;
        point.style.setProperty('transform', `rotate(${deg}deg)`);
    }
    else if (point.parentNode.classList.contains('bb-clock-panel-minute')) {
        const deg = picker.val.Minute * 6;
        point.style.setProperty('transform', `rotate(${deg}deg)`);
    }
    else {
        const deg = picker.val.Second * 6;
        point.style.setProperty('transform', `rotate(${deg}deg)`);
    }
}

export function init(id, invoke, hour, minute, second) {
    const el = document.getElementById(id);
    const picker = {
        el, invoke,
        val:
        {
            Hour: hour, Minute: minute, Second: second
        },
        hourEl: el.querySelector('.bb-time-text.hour'),
        minuteEl: el.querySelector('.bb-time-text.minute'),
        secondEl: el.querySelector('.bb-time-text.second')
    };

    picker.pointers = [...el.querySelectorAll('.bb-clock-point')];
    picker.pointers.forEach(p => {
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

                const x = e.clientX - cent.left;
                const y = e.clientY - cent.top;
                const deg = Math.atan2(y, x) * 6 / Math.PI + 15;
                const val = Math.floor(deg % 12);

                setValue(picker, p, val);
            },
            e => {
                el.classList.remove('dragging');
                invoke.invokeMethodAsync('SetTime', picker.val.Hour, picker.val.Minute, picker.val.Second);
            }
        );

        setPoint(picker, p);
    })
    Data.set(id, picker);
}

export function update(id, hour, minute, second) {
    const picker = Data.get(id);
    if (picker) {
        const { el, val } = picker;
        if (val.Hour !== hour) {
            val.Hour = hour;
            const point = el.querySelector('.bb-clock-panel-hour > .bb-clock-point')
            setPoint(picker, point)
        }
        if (val.Minute !== minute) {
            val.Minute = minute;
            const point = el.querySelector('.bb-clock-panel-minute > .bb-clock-point')
            setPoint(picker, point)
        }
        if (val.Second !== second) {
            val.Second = second;
            const point = el.querySelector('.bb-clock-panel-second > .bb-clock-point')
            setPoint(picker, point)
        }
    }
}

export function dispose(id) {
    const picker = Data.get(id);
    Data.remove(id);

    if (picker) {
        picker.pointers.forEach(p => {
            Drag.dispose(p);
        });
    }
}
