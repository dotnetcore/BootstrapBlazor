﻿import Data from "../../modules/data.js?v=$version"

const stop = camera => {
    camera.video.pause()
    camera.video.srcObject = null
    if (camera.video.mediaStreamTrack) {
        camera.video.mediaStreamTrack.stop()
    }
    delete camera.video
}

const play = camera => {
    const constrains = {
        video: {
            facingMode: 'environment',
            focusMode: "continuous",
            width: camera.video.videoWidth,
            height: camera.video.videoHeight
        },
        audio: false
    }
    if (camera.video.deviceId) {
        constrains.video.deviceId = { exact: camera.video.deviceId }
    }
    navigator.mediaDevices.getUserMedia(constrains).then(stream => {
        camera.video = camera.el.querySelector('video')
        camera.video.srcObject = stream
        camera.video.play()
        camera.video.mediaStreamTrack = stream.getTracks()[0]
        camera.invoke.invokeMethodAsync("Start")
    }).catch(err => {
        camera.invoke.invokeMethodAsync("GetError", err.message)
    })
}

export function init(id, invoke) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const camera = { el, invoke }
    Data.set(id, camera)

    navigator.mediaDevices.getUserMedia({
        video: { facingMode: 'environment', focusMode: "continuous" },
        audio: false
    }).then(s => {
        navigator.mediaDevices.enumerateDevices().then(videoInputDevices => {
            const videoInputs = videoInputDevices.filter(device => {
                return device.kind === 'videoinput'
            })
            invoke.invokeMethodAsync("InitDevices", videoInputs)
        })
    }).catch(err => {
        invoke.invokeMethodAsync("GetError", err.message)
    })
}

export function update(id) {
    const camera = Data.get(id)
    if(camera === null) {
        return
    }

    const autoStart = camera.el.getAttribute("data-auto-start") || false
    if(autoStart) {
        open(id)
    }
}

export function open(id) {
    const camera = Data.get(id)
    if(camera === null || camera.video === void 0) {
        return
    }

    const deviceId = camera.el.getAttribute("data-device-id")
    if(deviceId) {
        const videoWidth = camera.el.getAttribute("data-video-width")
        const videoHeight = camera.el.getAttribute("data-video-height")
        camera.video = {
            deviceId, videoWidth, videoHeight
        }
        play(camera)
    }
}

export function close(id) {
    const camera = Data.get(id)
    if(camera === null || camera.video === void 0) {
        return
    }

    if(camera.video) {
        stop(camera)
        camera.invoke.invokeMethodAsync("Stop")
    }
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
