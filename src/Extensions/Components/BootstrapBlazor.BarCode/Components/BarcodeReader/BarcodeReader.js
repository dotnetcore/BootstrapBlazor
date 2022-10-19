import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js"
import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js";
import {vibrate} from "../../../_content/BootstrapBlazor/modules/base/utility.js";

export class BarcodeReader extends BlazorComponent {
    _init() {
        this._reader = new ZXing.BrowserMultiFormatReader()
        this._scanType = this._config.scan
        this._invoker = this._config.arguments[0]

        if (this._scanType === 'Camera') {
            this.scan = () => {
                this._invoker.invokeMethodAsync("Start")
                const deviceId = this._element.getAttribute('data-bb-deviceid')
                const video = this._element.querySelector('video').getAttribute('id');
                this._reader.decodeFromVideoDevice(deviceId, video, (result, err) => {
                    if (result) {
                        vibrate()
                        console.log(result.text)
                        this._invoker.invokeMethodAsync("GetResult", result.text)

                        const autoStop = this._element.getAttribute('data-bb-autostop') === 'true'
                        if (autoStop) {
                            this.stop()
                        }
                    }
                    if (err && !(err instanceof ZXing.NotFoundException)) {
                        console.error(err)
                        this._invoker.invokeMethodAsync('GetError', err)
                    }
                });
            };

            this.stop = () => {
                this._reader.reset();
                this._invoker.invokeMethodAsync("Stop");
            };

            this._reader.getVideoInputDevices().then(videoInputDevices => {
                this._invoker.invokeMethodAsync("InitDevices", videoInputDevices).then(() => {
                    const autoStart = this._element.getAttribute('data-bb-autostart') === 'true'
                    if (this._config.autoStart && videoInputDevices.length > 0) {
                        const button = this._element.querySelector('[data-bb-method="scan"]')
                        button.click()
                    }
                });
            });
        } else {
            const scanImageHandler = () => {
                const files = this._reader.file.files;
                if (files.length === 0) {
                    return;
                }
                const reader = new FileReader();
                reader.onloadend = e => {
                    this._reader.decodeFromImageUrl(e.target.result).then(result => {
                        if (result) {
                            vibrate();
                            console.log(result.text);
                            this._invoker.invokeMethodAsync('GetResult', result.text);
                        }
                    }).catch((err) => {
                        if (err) {
                            console.log(err)
                            this._invoker.invokeMethodAsync('GetError', err.message);
                        }
                    })
                };
                reader.readAsDataURL(files[0]);
            }

            const resetFile = () => {
                let file = this._element.querySelector('[type="file"]');
                if (file) {
                    EventHandler.off(file, 'change');
                    file.remove();
                }
                file = document.createElement('input');
                file.setAttribute('type', 'file');
                file.setAttribute('hidden', 'true');
                file.setAttribute('accept', 'image/*');
                this._element.append(file);
                EventHandler.on(file, 'change', scanImageHandler);
                this._reader.file = file;
                return file;
            };

            this.scanImage = () => {
                let file = resetFile();
                file.click();
            };
        }

        const eventHandler = e => {
            let button = e.delegateTarget;
            const method = button.getAttribute('data-bb-method');
            const fn = this[method];
            if (typeof fn === 'function') {
                fn();
            }
        }

        EventHandler.on(this._element, 'click', '[data-bb-method]', eventHandler);
    }

    _dispose() {
        EventHandler.off(this._element, 'click', '[data-bb-method]');
        this._reader.reset();
    }
}
