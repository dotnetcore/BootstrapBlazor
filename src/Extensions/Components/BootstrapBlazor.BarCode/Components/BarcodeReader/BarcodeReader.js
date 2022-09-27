const barcode = {};

export function bb_barcode(id, method, obj) {
    const el = document.querySelector(id);
    barcode[id] = {
        codeReader: new ZXing.BrowserMultiFormatReader(),
        element: el,
        scanType: el.getAttribute('data-bb-scan'),
    };

    let instance = barcode[id];
    const codeReader = instance.codeReader;

    if (instance.scanType === 'Camera') {
        const scan = () => {
            obj.invokeMethodAsync("Start");
            const deviceId = el.getAttribute('data-bb-deviceId');
            const video = el.querySelector('video').getAttribute('id');
            codeReader.decodeFromVideoDevice(deviceId, video, (result, err) => {
                if (result) {
                    //$.bb_vibrate();
                    console.log(result.text);
                    obj.invokeMethodAsync("GetResult", result.text);

                    if (instance.autoStop) {
                        codeReader.reset();
                    }
                }
                if (err && !(err instanceof ZXing.NotFoundException)) {
                    console.error(err)
                    obj.invokeMethodAsync('GetError', err);
                }
            });
        };

        const stop = () => {
            codeReader.reset();
            obj.invokeMethodAsync("Stop");
        };

        barcode[id] = {
            ...barcode[id],
            ...{
                autoStart: el.getAttribute('data-bb-autostart') === 'true',
                autoStop: el.getAttribute('data-bb-autostop') === 'true',
                scanButton: el.querySelector('[data-bb-method="scan"]'),
                scan: scan,
                stop: stop,
            }
        };
        instance = barcode[id];

        codeReader.getVideoInputDevices().then((videoInputDevices) => {
            obj.invokeMethodAsync("InitDevices", videoInputDevices).then(() => {
                if (instance.autoStart && videoInputDevices.length > 0) {
                    instance.scanButton.click();
                }
            });
        });
    } else {
        const scanImageHandler = () => {
            const files = instance.file.files;
            if (files.length === 0) {
                return;
            }
            const reader = new FileReader();
            reader.onloadend = function (e) {
                codeReader.decodeFromImageUrl(e.target.result).then((result) => {
                    if (result) {
                        //$.bb_vibrate();
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
            reader.readAsDataURL(files[0]);
        }

        const resetFile = () => {
            let file = el.querySelector('[type="file"]');
            if (file) {
                bootstrap.EventHandler.off(file, 'change', scanImageHandler);
                file.remove();
            }
            file = document.createElement('input');
            file.setAttribute('type', 'file');
            file.setAttribute('hidden', 'true');
            file.setAttribute('accept', 'image/*');
            el.append(file);
            bootstrap.EventHandler.on(file, 'change', scanImageHandler);
            instance.file = file;
            return file;
        };

        const scanImage = () => {
            let file = resetFile();
            file.click();
        };

        barcode[id] = {
            ...barcode[id],
            ...{
                scanImage: scanImage
            }
        }
        instance = barcode[id];
    }

    codeReader.eventHandler = () => {
        let button = event.target;
        if (!button.hasAttribute('[data-bb-method]')) {
            button = button.closest('[data-bb-method]');
        }
        if (button) {
            const method = button.getAttribute('data-bb-method');
            const fn = instance[method];
            if (typeof fn === 'function') {
                fn();
            }
        }
    }

    bootstrap.EventHandler.on(el, 'click', '[data-bb-method]', codeReader.eventHandler);
}

export function bb_barcode_dispose(id) {
    const instance = barcode[id];
    if (instance) {
        const codeReader = instance.codeReader;
        if (codeReader != null) {
            bootstrap.EventHandler.off(codeReader.el, 'click', '[data-bb-method]', codeReader.eventHandler);
            codeReader.reset();
        }
    }
}
