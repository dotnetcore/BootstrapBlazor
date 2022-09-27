let codeReader = null;

export function bb_barcode(el, method, auto, obj) {
    codeReader = new ZXing.BrowserMultiFormatReader();

    if (el.getAttrbite('data-scan') === 'Camera') {
        codeReader.getVideoInputDevices().then((videoInputDevices) => {
            obj.invokeMethodAsync("InitDevices", videoInputDevices).then(() => {
                if (auto && videoInputDevices.length > 0) {
                    var button = $el.find('button[data-method="scan"]');
                    var data_method = $el.attr('data-scan');
                    if (data_method === 'Camera') button.trigger('click');
                }
            });
        });
    }

    $el.on('click', 'button[data-method]', function () {
        var data_method = $(this).attr('data-method');
        if (data_method === 'scan') {
            obj.invokeMethodAsync("Start");
            var deviceId = $el.find('.dropdown-item.active').attr('data-val');
            var video = $el.find('video').attr('id');
            codeReader.decodeFromVideoDevice(deviceId, video, (result, err) => {
                if (result) {
                    $.bb_vibrate();
                    console.log(result.text);
                    obj.invokeMethodAsync("GetResult", result.text);

                    var autostop = $el.attr('data-autostop') === 'true';
                    if (autostop) {
                        codeReader.reset();
                    }
                }
                if (err && !(err instanceof ZXing.NotFoundException)) {
                    console.error(err)
                    obj.invokeMethodAsync('GetError', err);
                }
            });
        }
        else if (data_method === 'scanImage') {
            codeReader = new ZXing.BrowserMultiFormatReader();
            $el.find(':file').remove();
            var $img = $('.scanner-image');
            var $file = $('<input type="file" hidden accept="image/*">');
            $el.append($file);

            $file.on('change', function () {
                if (this.files.length === 0) {
                    return;
                }
                var reader = new FileReader();
                reader.onloadend = function (e) {
                    $img.attr('src', e.target.result);
                    codeReader.decodeFromImage($img[0]).then((result) => {
                        if (result) {
                            $.bb_vibrate();
                            console.log(result.text);
                            obj.invokeMethodAsync('GetResult', result.text);
                        }
                    }).catch((err) => {
                        if (err) {
                            console.log(err)
                            obj.invokeMethodAsync('GetError', err.message);
                        }
                    })
                };
                reader.readAsDataURL(this.files[0]);
            })
            $file.trigger('click');
        }
        else if (data_method === 'close') {
            codeReader.reset();
            obj.invokeMethodAsync("Stop");
        }
    });
};

export function bb_barcode_dispose() {
    if (codeReader != null) {
        codeReader.reset();
    }
};
