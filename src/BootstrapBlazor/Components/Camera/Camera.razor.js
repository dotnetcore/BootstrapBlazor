﻿import Data from "../../modules/data.js?v=$version"

const play = camera => {
    const constrains = {
        video: {
            width: { ideal: camera.video.videoWidth },
            height: { ideal: camera.video.videoHeight },
            facingMode: "environment"
        },
        audio: false
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
        camera.invoke.invokeMethodAsync("TriggerError", err.message)
    })
}

const stop = camera => {
    camera.video.element.pause()
    camera.video.element.srcObject = null
    if (camera.video.track) {
        camera.video.track.stop()
    }
    delete camera.video
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

    const autoStart = camera.el.getAttribute("data-auto-start") || false
    if (autoStart) {
        open(id)
    }
}

export function open(id) {
    const camera = Data.get(id)
    if (camera === null || camera.video) {
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

export function close(id) {
    const camera = Data.get(id)
    if (camera === null || camera.video === void 0) {
        return
    }

    if (camera.video) {
        stop(camera)
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

export function dispose(id) {
    const camera = Data.get(id)
    Data.remove(id)

    if (camera) {
        if (camera.video) {
            stop(camera)
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
