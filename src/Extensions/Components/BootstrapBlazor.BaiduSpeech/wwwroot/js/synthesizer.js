let audio = undefined;

export function bb_baidu_speech_synthesizerOnce(obj, callback, data) {
    audio = document.createElement("audio");
    audio.controls = true;

    var blob = new Blob([data], { type: 'audio/mp3' });
    var url = (window.URL || webkitURL).createObjectURL(blob);
    audio.src = url;
    audio.addEventListener("ended", function () {
        audio = undefined;
        obj.invokeMethodAsync(callback, "Finished");
    });
    obj.invokeMethodAsync(callback, "Synthesizer");
    audio.play();
};

export function bb_baidu_close_synthesizer(obj, callback) {
    if (audio != undefined) {
        audio.pause();
        obj.invokeMethodAsync(callback, "Cancel");
    }
};
