import Data from "../../modules/data.js?v=$version"
import Drag from "../../modules/drag.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

const setTime = picker => {
    const { el, val } = picker;
    const mode = el.getAttribute('data-bb-mode');
    if (mode === "Hour") {
        let hour = Math.round(val.Hour)
        if (hour > 12) {
            hour = hour - 12;
        }
        el.querySelector('.bb-time-text.hour').textContent = hour.toString().padStart(2, '0');
        //setHandle(el.querySelector('.bb-clock-panel-hour'), t, null, false);
    }
    if (mode === "Minute") {
        el.querySelector('.bb-time-text.minute').textContent = val.Minute.toString().padStart(2, '0');
        //setHandle(el.querySelector('.bb-clock-panel-min'), t / 5, null, false);
    }
    if (mode === "Second") {
        el.querySelector('.bb-time-text.second').textContent = val.Second.toString().padStart(2, '0');
        //setHandle(el.querySelector('.bb-clock-panel-sec'), t / 5, null, false);
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
                let wrap = el.querySelector('.bb-time-body');
                var pos = wrap.getBoundingClientRect();
                var cent = {
                    left: pos.left + wrap.offsetWidth / 2,
                    top: pos.top + wrap.offsetHeight / 2
                };

                var x = e.clientX - cent.left;
                var y = e.clientY - cent.top;
                var hrs = Math.atan2(y, x) / Math.PI * 6 + 3;

                hrs += 12;
                hrs %= 12;
                setTime(picker);
            },
            e => {
                el.classList.remove('dragging');
                invoke.invokeMethodAsync('SetTime', picker.val.Hour, picker.val.Minute, picker.val.Second);
            }
        );

        if (p.parentNode.classList.contains('bb-clock-panel-hour')) {
            const deg = picker.val.Hour * 30;
            p.style.setProperty('transform', `rotate(${deg}deg)`);
        }
        else if (p.parentNode.classList.contains('bb-clock-panel-minute')) {
            const deg = picker.val.Minute * 6;
            p.style.setProperty('transform', `rotate(${deg}deg)`);
        }
        else {
            const deg = picker.val.Second * 6;
            p.style.setProperty('transform', `rotate(${deg}deg)`);
        }
    })
    Data.set(id, picker);
}

export function setHandle(face, a, l, anim) {
    if (a == null) {
        a = face.dataset.handAng;
    }
    if (l == 'hidden') {
        l = face.classList.contains('min') ? 7 : 4;
    }
    if (l == null) {
        l = 5.7;
    }

    var bl = a % 1 == 0 ? l - 0.25 : l;
    var deg = a * 30;

    var handle = face.querySelector('.bb-clock-point');
    handle.style.transform = 'rotate(' + (deg).toFixed(20) + 'deg)';
    handle.classList.toggle('anim', anim);
}

export function update(id, hour, minute, second) {
    const picker = Data.get(id);
    if (picker) {

    }
}

export function dispose(id) {
    const picker = Data.get(id);
    data.remove(id);

    if (picker) {
        picker.pointers.forEach(p => {
            Drag.dispose(p);
        });
    }
}
