(function ($) {
    $.extend({
        bb_camera: function (el, obj, method, auto) {
            var $el = $(el);

            var stop = function (video, track) {
                video.pause();
                video.srcObject = null;
                track.stop();
            };

            if (method === 'stop') {
                var video = $el.find('video')[0];
                var track = $el.data('bb_video_track');
                if (track) {
                    stop(video, track);
                }
                return;
            }

            navigator.mediaDevices.enumerateDevices().then(function (videoInputDevices) {
                var videoInputs = videoInputDevices.filter(function (device) {
                    return device.kind === 'videoinput';
                });
                obj.invokeMethodAsync("InitDevices", videoInputs).then(() => {
                    if (auto && videoInputs.length > 0) {
                        $el.find('button[data-method="play"]').trigger('click');
                    }
                });

                // handler button click event
                var video = $el.find('video')[0];
                var canvas = $el.find('canvas')[0];
                var context = canvas.getContext('2d');
                var mediaStreamTrack;

                $el.on('click', 'button[data-method]', async function () {
                    var data_method = $(this).attr('data-method');
                    if (data_method === 'play') {
                        var front = $(this).attr('data-camera');
                        var deviceId = $el.find('.dropdown-item.active').attr('data-val');
                        var constrains = { video: { facingMode: front }, audio: false };
                        if (deviceId !== "") {
                            constrains.video.deviceId = { exact: deviceId };
                        }
                        navigator.mediaDevices.getUserMedia(constrains).then(stream => {
                            video.srcObject = stream;
                            video.play();
                            mediaStreamTrack = stream.getTracks()[0];
                            $el.data('bb_video_track', mediaStreamTrack);
                            obj.invokeMethodAsync("Start");
                        }).catch(err => {
                            console.log(err)
                            obj.invokeMethodAsync("GetError", err.message)
                        });
                    }
                    else if (data_method === 'stop') {
                        stop(video, mediaStreamTrack);
                        obj.invokeMethodAsync("Stop");
                    }
                    else if (data_method === 'capture') {
                        context.drawImage(video, 0, 0, 300, 200);
                        var url = canvas.toDataURL();
                        var maxLength = 30 * 1024;
                        while (url.length > maxLength) {
                            var data = url.substr(0, maxLength);
                            console.log(data);
                            await obj.invokeMethodAsync("Capture", data);
                            url = url.substr(data.length);
                        }

                        if (url.length > 0) {
                            await obj.invokeMethodAsync("Capture", url);
                        }
                        await obj.invokeMethodAsync("Capture", "__BB__%END%__BB__");
                    }
                });
            });
        }
    });
})(jQuery);
