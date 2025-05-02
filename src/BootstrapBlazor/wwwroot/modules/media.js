import { registerBootstrapBlazorModule } from "./utility.js"

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
            deviceId: options.deviceId ? { exact: options.deviceId } : null,
            facingMode: { ideal: options.facingMode || "environment" }
        },
        audio: false
    }

    const { videoSelector, width, height } = options;
    if (width) {
        constrains.video.width = { ideal: width };
    }
    if (height) {
        constrains.video.height = { ideal: height };
    }

    let ret = false;
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constrains);
        const media = registerBootstrapBlazorModule("MediaDevices");
        media.stream = stream;

        if (videoSelector) {
            const video = document.querySelector(videoSelector);
            if (video) {
                video.srcObject = stream;
            }
        }
        ret = true;
    }
    catch (err) {
        console.error("Error accessing media devices.", err);
    }
    return ret;
}

export async function close(videoSelector) {
    if (videoSelector) {
        const video = document.querySelector(videoSelector);
        if (video) {
            video.pause();
            const stream = video.srcObject;
            closeStream(stream);
            video.srcObject = null;
        }
    }
    const media = registerBootstrapBlazorModule("MediaDevices");
    const { stream } = media;
    if (stream && stream.active) {
        closeStream(stream);
    }
    media.stream = null;
}

export async function getPreviewUrl() {
    let url = null;
    const media = registerBootstrapBlazorModule("MediaDevices");
    const { stream } = media;
    if (stream && stream.active) {
        const tracks = stream.getVideoTracks();
        if (tracks) {
            const track = tracks[0];
            const capture = new ImageCapture(track);
            const blob = await capture.takePhoto();
            url = URL.createObjectURL(blob);
        }
    }
    return url;
}

const closeStream = stream => {
    if (stream) {
        const tracks = stream.getTracks();

        tracks.forEach(track => {
            track.stop();
        });
    }
}
