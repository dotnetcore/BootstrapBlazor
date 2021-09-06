(function ($) {
    $.extend({
        bb_toggleFullscreen: function (el, id) {
            var ele = el;
            if (!ele || ele === '') {
                if (id) {
                    ele = document.getElementById(id);
                }
                else {
                    ele = document.documentElement;
                }
            }
            if ($.bb_IsFullscreen()) {
                $.bb_ExitFullscreen();
                ele.classList.remove('fs-open');
            }
            else {
                $.bb_Fullscreen(ele);
                ele.classList.add('fs-open');
            }
        },
        bb_Fullscreen: function (ele) {
            ele.requestFullscreen() ||
                ele.webkitRequestFullscreen ||
                ele.mozRequestFullScreen ||
                ele.msRequestFullscreen;
        },
        bb_ExitFullscreen: function () {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            }
            else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            }
            else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
            else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            }
        },
        bb_IsFullscreen: function () {
            return document.fullscreen ||
                document.webkitIsFullScreen ||
                document.webkitFullScreen ||
                document.mozFullScreen ||
                document.msFullScreent;
        }
    });
})(jQuery);
