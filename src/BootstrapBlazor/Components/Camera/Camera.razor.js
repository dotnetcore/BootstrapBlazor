import Data from "../../modules/data.js"

const openDevice = camera => {
    if (camera.video) {
        return
    }

    const deviceId = camera.el.getAttribute("data-device-id")
    if (deviceId) {
        const videoWidth = parseInt(camera.el.getAttribute("data-video-width"))
        const videoHeight = parseInt(camera.el.getAttribute("data-video-height"))
        play(camera, {
            video: {
                deviceId,
                width: { ideal: videoWidth },
                height: { ideal: videoHeight }
            }
        });
    }
}

const stopDevice = camera => {
    if (camera.video) {
        camera.video.element.pause()
        if (camera.video.track) {
            camera.video.track.stop()
        }
        delete camera.video
    }
}

const play = (camera, option = {}) => {
    const constrains = {
        ...{
            video: {
                facingMode: "environment"
            },
            audio: false
        },
        ...option
    }
    navigator.mediaDevices.getUserMedia(constrains).then(stream => {
        camera.video = { deviceId: option.video.deviceId };
        camera.video.element = camera.el.querySelector('video')
        camera.video.element.srcObject = stream
        camera.video.element.play()
        camera.video.track = stream.getVideoTracks()[0]
        camera.invoke.invokeMethodAsync("TriggerOpen")
    }).catch(err => {
        delete camera.video
        camera.invoke.invokeMethodAsync("TriggerError", err.name)
    })
}

export function init(id, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const camera = { el, invoke }
    Data.set(id, camera)

    navigator.mediaDevices.getUserMedia({ video: true, audio: false }).then(s => {
        navigator.mediaDevices.enumerateDevices().then(videoInputDevices => {
            const videoInputs = videoInputDevices.filter(device => {
                return device.kind === 'videoinput'
            })
            invoke.invokeMethodAsync("TriggerInit", videoInputs)
        })
    }).catch(err => {
        invoke.invokeMethodAsync("TriggerError", err.message)
    })
}

export function update(id) {
    const camera = Data.get(id)
    if (camera === null) {
        return
    }

    // handler switch device
    if (camera.video) {
        const deviceId = camera.el.getAttribute("data-device-id")
        if (camera.video.deviceId !== deviceId) {
            stopDevice(camera)
            openDevice(camera)
        }
    }
    else {
        const autoStart = camera.el.getAttribute("data-auto-start") || false
        if (autoStart) {
            openDevice(camera)
        }
    }
}

export function open(id) {
    const camera = Data.get(id)
    if (camera) {
        openDevice(camera)
    }
}

export function close(id) {
    const camera = Data.get(id)
    if (camera === null || camera.video === void 0) {
        return
    }

    if (camera.video) {
        stopDevice(camera)
        camera.invoke.invokeMethodAsync("TriggerClose")
    }
}

export function capture(id) {
    const camera = Data.get(id)
    if (camera === null || camera.video === void 0) {
        return null;
    }

    const url = drawImage(camera);
    return new Blob([url])
}

export function download(id, fileName) {
    const camera = Data.get(id)
    if (camera === null || camera.video === void 0) {
        return
    }

    const createEl = document.createElement('a');
    createEl.href = drawImage(camera);
    createEl.download = fileName || 'capture.png';
    createEl.click();
    createEl.remove();
}

export function resize(id, width, height) {
    const camera = Data.get(id)
    if (camera === null || camera.video === void 0) {
        return
    }

    const constrains = {
        video: {
            deviceId: camera.video.deviceId,
            width: { ideal: width },
            height: { ideal: height }
        }
    }

    stopDevice(camera)
    play(camera, constrains)
}

export function dispose(id) {
    const camera = Data.get(id)
    Data.remove(id)

    if (camera) {
        if (camera.video) {
            stopDevice(camera)
        }
    }
}

const drawImage = camera => {
    const quality = camera.el.getAttribute("data-capture-quality") || 0.9;
    const captureJpeg = camera.el.getAttribute("data-capture-jpeg") || false;
    const canvas = camera.el.querySelector('canvas');
    const { videoWidth, videoHeight } = camera.video.element;
    canvas.width = videoWidth * devicePixelRatio;
    canvas.height = videoHeight * devicePixelRatio;
    canvas.style.width = `${videoWidth}px`;
    canvas.style.height = `${videoHeight}px`;
    const context = canvas.getContext('2d')
    context.scale(devicePixelRatio, devicePixelRatio)
    context.drawImage(camera.video.element, 0, 0, videoWidth, videoHeight);
    let url = "";
    if (captureJpeg) {
        url = canvas.toDataURL("image/jpeg", quality);
    }
    else {
        url = canvas.toDataURL()
    }
    return url
}
