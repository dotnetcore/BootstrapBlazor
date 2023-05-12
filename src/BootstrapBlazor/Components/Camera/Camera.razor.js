import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const stop = (video, track) => {
    video.pause()
    video.srcObject = null
    if(track) {
        track.stop()
    }
}

export function init(id, invoke, auto, videoWidth, videoHeight) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const camera = { el, invoke }
    Data.set(id, camera)

    camera.playButton = el.querySelector('button[data-method="play"]')

    navigator.mediaDevices.enumerateDevices().then(videoInputDevices => {
        const videoInputs = videoInputDevices.filter(device => {
            return device.kind === 'videoinput'
        })
        invoke.invokeMethodAsync("InitDevices", videoInputs).then(() => {
            if (auto && videoInputs.length > 0) {
                camera.playButton.click()
            }
        })

        // handler button click event
        camera.video = el.querySelector('video')
        const canvas = el.querySelector('canvas')
        camera.canvas = canvas
        canvas.width = videoWidth
        canvas.height = videoHeight
        const context = canvas.getContext('2d')

        EventHandler.on(el, 'click', 'button[data-method]', async e => {
            const button = e.delegateTarget
            const data_method = button.getAttribute('data-method')
            if (data_method === 'play') {
                const deviceId = el.getAttribute('data-device-id')
                const constrains = { video: { facingMode: 'environment', width: videoWidth, height: videoHeight }, audio: false }
                if (deviceId) {
                    constrains.video.deviceId = { exact: deviceId }
                }
                navigator.mediaDevices.getUserMedia(constrains).then(stream => {
                    camera.video.srcObject = stream
                    camera.video.play()
                    camera.mediaStreamTrack = stream.getTracks()[0]
                    invoke.invokeMethodAsync("Start")
                }).catch(err => {
                    invoke.invokeMethodAsync("GetError", err.message)
                })
            }
            else if (data_method === 'stop') {
                stop(camera.video, camera.mediaStreamTrack)
                invoke.invokeMethodAsync("Stop")
            }
            else if (data_method === 'capture') {
                context.drawImage(camera.video, 0, 0, videoWidth, videoHeight)
                let url = canvas.toDataURL()
                const maxLength = 30 * 1024
                while (url.length > maxLength) {
                    const data = url.substring(0, maxLength)
                    await invoke.invokeMethodAsync("Capture", data)
                    url = url.substring(data.length)
                }

                if (url.length > 0) {
                    await invoke.invokeMethodAsync("Capture", url)
                }
                await invoke.invokeMethodAsync("Capture", "__BB__%END%__BB__")
            }
        })
    })
}

export function dispose(id) {
    const camera = Data.get(id)
    Data.remove(id)

    if (camera) {
        EventHandler.off(camera.el, 'click')

        if (camera.track) {
            stop(camera.video, camera.track)
        }
    }
}
