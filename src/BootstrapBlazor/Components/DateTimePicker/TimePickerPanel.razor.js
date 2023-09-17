const timeMode = Object.freeze({
    Hour: 'Hour',
    Min: 'Min',
    Sec: 'Sec',
})

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

export function init(id, invoke, hours, minutes, seconds) {
    const self = document.getElementById(id);
    let mouse = false;

    document.body.addEventListener('mouseup', function () {
        mouse = false;
    });

    function setTime(t) {
        var faceWrap = document.querySelector('.bb-time-body');
        var hms = faceWrap.getAttribute('data-bb-hms');
        if (hms === timeMode.Hour) {
            t = Math.round(t)
            if (t === 0) t = 12;
            self.querySelector('.bb-time-header .hour').textContent = t;
            setHandle(self.querySelector('.bb-clock-panel-hour'), t, null, false);
            hours = t;
        }
        if (hms === timeMode.Min) {
            t = Math.round((t * 5))
            if (t === 60) t = 0;
            self.querySelector('.bb-time-header .min').textContent = String(t).padStart(2, '0');
            setHandle(self.querySelector('.bb-clock-panel-min'), t / 5, null, false);
            minutes = t;
        }
        if (hms === timeMode.Sec) {
            t = Math.round((t * 5))
            if (t === 60) t = 0;
            self.querySelector('.bb-time-header .sec').textContent = String(t).padStart(2, '0');
            setHandle(self.querySelector('.bb-clock-panel-sec'), t / 5, null, false);
            seconds = t;
        }
    }

    function handleMove(e) {
        if (!mouse) return;
        e.preventDefault();
        let wrap = self.querySelector('.bb-time-body');
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
        setTime(hrs);
    }

    self.querySelector('.bb-time-body').addEventListener('mousedown', function () {
        mouse = true;
    });
    self.querySelector('.bb-time-body').addEventListener('mousedown', handleMove);

    self.querySelector('.bb-time-body').addEventListener('mouseup', function () {
        invoke.invokeMethodAsync('SetTime', hours, minutes, seconds);
    });

    document.body.addEventListener('mousemove', handleMove);
    self.querySelectorAll('*').forEach(el => el.style.transition = 'none');
    setTimeout(function () {
        self.querySelectorAll('*').forEach(el => el.style.transition = '');
    });
}
