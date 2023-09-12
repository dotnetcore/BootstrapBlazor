﻿export function init(id, invoke, hourValue, minValue, isAM) {
    const el = document.getElementById(id);

    const self = el;
    var activeFace = self.querySelector('.face-set.hour'),
        min = false,
        am = false,
        mouse = false;

    self.querySelectorAll('.face-set').forEach(el => el.dataset.handAng = 0);
    self.querySelector('.face-set.min').dataset.handAng = 1;
    function setHandle(face, a, l, anim) {
        if (a == null) a = face.dataset.handAng;
        if (l == 'hidden') l = face.classList.contains('min') ? 7 : 4;
        if (l == null) l = 5.5;
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
    function minMode(yes) {
        var cl = yes ? 'min' : 'hour';
        min = yes;
        activeFace.classList.add('face-off');
        setHandle(activeFace, null, 'hidden', true);
        activeFace = self.querySelector('.face-set.' + cl);
        activeFace.classList.remove('face-off');
        setHandle(activeFace, null, null, true);
        self.querySelector('.bb-time-header .active').classList.remove('active');
        self.querySelector('.bb-time-header .part.' + cl).classList.add('active');
    }
    minMode(true);
    minMode(false);
    document.body.addEventListener('mouseup', function () {
        if (mouse && !min) minMode(true);
        mouse = false;
    });
    function setHour(hour) {
        if (hour == 0) hour = 12;
        self.querySelector('.bb-time-header .hour').textContent = hour;
        setHandle(self.querySelector('.face-set.hour'), hour, null, false);
        invoke.invokeMethodAsync('SetHour', hour);
    }
    function setMin(min) {
        if (min == 60) min = 0;
        self.querySelector('.bb-time-header .min').textContent = String(min).padStart(2, '0');
        setHandle(self.querySelector('.face-set.min'), min / 5, null, false);
        invoke.invokeMethodAsync('SetMin', min);
    }
    function setAmPm(to_am) {
        am = to_am;
        self.querySelector('.am-pm-btn.am').classList.toggle('active', am);
        self.querySelector('.am-pm-btn.pm').classList.toggle('active', !am);
        self.querySelector('.bb-time-header .am-pm').textContent = am ? 'AM' : 'PM';
        invoke.invokeMethodAsync('SetAmPm', am);
    }
    function handleMove(e) {
        if (!mouse) return;
        e.preventDefault();
        var $this = self.querySelector('.face-wrap');
        var pos = $this.getBoundingClientRect();
        var cent = { left: $this.offsetWidth / 2 + pos.left, top: $this.offsetHeight / 2 + pos.top };
        var hrs = Math.atan2(e.pageY - cent.top, e.pageX - cent.left) / Math.PI * 6 + 3;
        hrs += 12;
        hrs %= 12;
        if (min) {
            setMin(Math.round((hrs * 5)));
        } else {
            setHour(Math.round((hrs)));
        }
    }
    self.querySelector('.face-wrap').addEventListener('mousedown', function () {
        mouse = true;
    });
    self.querySelector('.face-wrap').addEventListener('mousedown', handleMove);
    document.body.addEventListener('mousemove', handleMove);
    self.querySelector('.face-set.min').classList.add('face-off');
    self.querySelector('.bb-time-header .part.min').addEventListener('click', minMode.bind(this, true));
    self.querySelector('.bb-time-header .part.hour').addEventListener('click', minMode.bind(this, false));
    self.querySelector('.am-pm-btn.am').addEventListener('click', setAmPm.bind(this, true));
    self.querySelector('.am-pm-btn.pm').addEventListener('click', setAmPm.bind(this, false));
    self.querySelector('.bb-time-header .am-pm').addEventListener('click', function () {
        setAmPm(!am);
    });
    self.querySelectorAll('*').forEach(el => el.style.transition = 'none');
    setTimeout(function () {
        self.querySelectorAll('*').forEach(el => el.style.transition = '');
    });

    //设置默认值
    setMin(minValue);
    setHour(hourValue);
    setAmPm(isAM);
}
