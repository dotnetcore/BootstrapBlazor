import Data from "../../modules/data.js"

const openDevice = camera => {
    if(camera.video) {
        return
    }

    const deviceId = camera.el.getAttribute("data-device-id")
    if (deviceId) {
        const videoWidth = parseInt(camera.el.getAttribute("data-video-width"))
        const videoHeight = parseInt(camera.el.getAttribute("data-video-height"))
        camera.video = {
            deviceId, videoWidth, videoHeight
        }
        play(camera)
    }
}

const stopDevice = camera => {
    if (camera.video) {
        camera.video.element.pause()
        camera.video.element.srcObject = null
        if (camera.video.track) {
            camera.video.track.stop()
        }
        delete camera.video
    }
}

const play = (camera, option = {}) => {
    camera.video = {
        ...camera.video,
        ...{
            videoWidth: 320,
            videoHeight: 240
        }
    }
    const constrains = {
        ...{
            video: {
                width: { ideal: camera.video.videoWidth },
                height: { ideal: camera.video.videoHeight },
                facingMode: "environment"
            },
            audio: false
        },
        ...option
    }
    if (camera.video.deviceId) {
        constrains.video.deviceId = { exact: camera.video.deviceId }
    }
    navigator.mediaDevices.getUserMedia(constrains).then(stream => {
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
        if (camera.video.deviceId !== deviceId){
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
        return
    }

    const url = drawImage(camera)
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
            width: { exact: width },
            height: { exact: height }
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
    const { videoWidth, videoHeight } = camera.video
    const canvas = camera.el.querySelector('canvas')
    canvas.width = videoWidth
    canvas.height = videoHeight
    const context = canvas.getContext('2d')
    context.drawImage(camera.video.element, 0, 0, videoWidth, videoHeight)
    let url = "";
    if (captureJpeg) {
        url = canvas.toDataURL("image/jpeg", quality);
    }
    else {
        url = canvas.toDataURL()
    }
    return url
}
