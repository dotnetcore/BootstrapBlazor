const timeMode = {
    Hour: 'Hour',
    Min: 'Min',
    Sec: 'Sec',
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
    face.dataset.handAng = a;

    var handle = face.querySelector('.handle');
    handle.style.transform = 'rotate(' + (deg).toFixed(20) + 'deg) translateY(' + -l + 'em)';
    handle.classList.toggle('anim', anim);

    var handleBar = face.querySelector('.handle-bar');
    handleBar.style.transform = 'rotate(' + (deg).toFixed(20) + 'deg) scaleY(' + bl + ')';
    handleBar.classList.toggle('anim', !!anim);
}

export function init(id, invoke, hours, minutes, seconds) {
    const self = document.getElementById(id);
    let mouse = false;

    self.querySelectorAll('.face-wrap').forEach(el => el.dataset.handAng = 0);
    self.querySelector('.face-set.min').dataset.handAng = 1;

    document.body.addEventListener('mouseup', function () {
        mouse = false;
    });

    function setTime(t) {
        var faceWrap = document.querySelector('.face-wrap');
        var hms = faceWrap.getAttribute('data-bb-hms');
        if (hms === timeMode.Hour) {
            t = Math.round(t)
            if (t === 0) t = 12;
            self.querySelector('.bb-time-header .hour').textContent = t;
            setHandle(self.querySelector('.face-set.hour'), t, null, false);
            hours = t;
        }
        if (hms === timeMode.Min) {
            t = Math.round((t * 5))
            if (t === 60) t = 0;
            self.querySelector('.bb-time-header .min').textContent = String(t).padStart(2, '0');
            setHandle(self.querySelector('.face-set.min'), t / 5, null, false);
            minutes = t;
        }
        if (hms === timeMode.Sec) {
            t = Math.round((t * 5))
            if (t === 60) t = 0;
            self.querySelector('.bb-time-header .sec').textContent = String(t).padStart(2, '0');
            setHandle(self.querySelector('.face-set.sec'), t / 5, null, false);
            seconds = t;
        }
    }

    function handleMove(e) {
        if (!mouse) return;
        e.preventDefault();
        let wrap = self.querySelector('.face-wrap');
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

    self.querySelector('.face-wrap').addEventListener('mousedown', function () {
        mouse = true;
    });
    self.querySelector('.face-wrap').addEventListener('mousedown', handleMove);

    self.querySelector('.face-wrap').addEventListener('mouseup', function () {
        invoke.invokeMethodAsync('SetTime', hours, minutes, seconds);
    });

    document.body.addEventListener('mousemove', handleMove);
    self.querySelectorAll('*').forEach(el => el.style.transition = 'none');
    setTimeout(function () {
        self.querySelectorAll('*').forEach(el => el.style.transition = '');
    });
}
