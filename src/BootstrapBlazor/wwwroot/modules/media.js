import { registerBootstrapBlazorModule } from "./utility.js"

export async function enumerateDevices() {
    let ret = null;
    if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia || !navigator.mediaDevices.enumerateDevices) {
        console.log("enumerateDevices() not supported.");
    }
    else {
        try {
            await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
            ret = await navigator.mediaDevices.enumerateDevices();
        }
        catch (e) {
            console.warn(e);
        }
    }
    return ret;
}

export async function open(type, options) {
    let ret = false;
    if (type === "video") {
        ret = await openVideoDevice(options);
    }
    else if (type === "audio") {
        ret = await record(options);
    }
    return ret;
}

export async function close(selector) {
    const media = registerBootstrapBlazorModule("MediaDevices");
    let ret;
    if (media.stream) {
        ret = await closeVideoDevice(selector);
    }
    else {
        ret = stop(selector);
    }
    return ret;
}

const openVideoDevice = async options => {
    const constrains = {
        video: {
            deviceId: options.deviceId ? { exact: options.deviceId } : null,
            facingMode: { ideal: options.facingMode || "environment" }
        }
    }

    const { selector, width, height } = options;
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

        if (selector) {
            const video = document.querySelector(selector);
            if (video) {
                video.srcObject = stream;
            }
        }
        ret = true;
    }
    catch (err) {
        console.error("Error accessing video devices.", err);
    }
    return ret;
}

const closeVideoDevice = async selector => {
    let ret = false;

    try {
        if (selector) {
            const video = document.querySelector(selector);
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
        ret = true;
    }
    catch (err) {
        console.error("Error closing video devices.", err);
    }
    return ret;
}

export async function apply(options) {
    let ret = false;
    try {
        const media = registerBootstrapBlazorModule("MediaDevices");
        const { stream } = media;
        if (stream && stream.active) {
            const tracks = stream.getVideoTracks();
            if (tracks) {
                const track = tracks[0];
                const settings = track.getSettings();
                const { aspectRatio } = settings;
                if (options.width) {
                    settings.width = {
                        exact: options.width,
                    };
                    settings.height = {
                        exact: Math.floor(options.width / aspectRatio)
                    };
                }
                if (options.facingMode) {
                    settings.facingMode = {
                        ideal: options.facingMode,
                    }
                }
                await track.applyConstraints(settings);
            }
        }
    }
    catch (err) {
        console.error("Error apply constraints media devices.", err);
    }
    return ret;
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
            media.previewBlob = blob;
        }
    }
    return url;
}

export function getPreviewData() {
    const media = registerBootstrapBlazorModule("MediaDevices");
    return media.previewBlob;
}

const closeStream = stream => {
    if (stream) {
        const tracks = stream.getTracks();

        tracks.forEach(track => {
            track.stop();
        });
    }
}

export async function record(options) {
    const constrains = {
        audio: {
            deviceId: options.deviceId ? { exact: options.deviceId } : null
        }
    }

    let ret = false;
    try {
        const stream = await navigator.mediaDevices.getUserMedia(constrains);
        const media = registerBootstrapBlazorModule("MediaDevices");
        const mediaRecorder = new MediaRecorder(stream);

        stop();
        media.recorder = mediaRecorder;
        media.audioSelector = options.selector;
        media.chunks = [];

        mediaRecorder.start();
        mediaRecorder.ondataavailable = function (e) {
            media.chunks.push(e.data);
        };
        mediaRecorder.onstop = function () {
            if (media.audioSelector) {
                const audio = document.querySelector(media.audioSelector);
                if (audio) {
                    if (media.chunks && media.chunks.length > 0) {
                        const blob = new Blob(media.chunks, { type: media.recorder.mimeType });
                        media.chunks = [];
                        audio.src = window.URL.createObjectURL(blob);
                        audio.classList.remove("d-none");
                        audio.classList.remove("hidden");
                        audio.removeAttribute("hidden");
                        media.audioBlob = blob;
                    }
                }
                delete media.audioSelector;
                delete media.recorder;
            }
        };
        ret = true;
    }
    catch (err) {
        console.error("Error accessing audio devices.", err);
    }
    return ret;
}

export function stop(selector) {
    let ret = false;
    const media = registerBootstrapBlazorModule("MediaDevices");
    if (selector) {
        media.audioSelector = selector;
    }
    if (media.recorder) {
        if (media.recorder.state === "recording") {
            media.recorder.stop();
        }
        else {
            delete media.recorder;
        }
        ret = true;
    }
    return ret;
}

export function getAudioData() {
    const media = registerBootstrapBlazorModule("MediaDevices");
    return media.audioBlob
}
