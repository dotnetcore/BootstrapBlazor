import { drawImage } from "./utility.js"

window.BootstrapBlazor = window.BootstrapBlazor || {};
window.BootstrapBlazor[name] = window.BootstrapBlazor[name] || {


export async function enumerateDevices() {
    let ret = null;
    if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia || !navigator.mediaDevices.enumerateDevices) {
        console.log("enumerateDevices() not supported.");
    }
    else {
        await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
        const devices = await navigator.mediaDevices.enumerateDevices();
        ret = devices;
    }
    return ret;
}

export async function open(options) {
    const constrains = {
        video: {
            facingMode: options.facingMode || "environment",
            deviceId: options.deviceId ? { exact: options.deviceId } : null,
        },
        audio: false
    }
    const video = document.querySelector(options.videoSelector);
    if (video) {
        const stream = await navigator.mediaDevices.getUserMedia(constrains);
        video.srcObject = stream;
    }
}

export async function close(videoSelector) {
    const video = document.querySelector(videoSelector);
    if (video) {
        video.pause();
        const stream = video.srcObject;
        if (stream) {
            const tracks = stream.getTracks();

            tracks.forEach(track => {
                track.stop();
            });

            video.srcObject = null;
        }
    }
}

export async function capture(videoSelector) {
    const video = document.querySelector(videoSelector);
    if (video) {
        const stream = video.srcObject;
        if (stream) {
            const tracks = stream.getVideoTracks();
            if (tracks) {
                const track = tracks[0];
                const capture = new ImageCapture(track);
                const blob = await capture.takePhoto();
                const image = await createImageBitmap(blob);
                const { offsetWidth, offsetHeight } = video;
                drawImage(document.querySelector(".b-video-image"), image, offsetWidth, offsetHeight);
            }
        }
    }
}
