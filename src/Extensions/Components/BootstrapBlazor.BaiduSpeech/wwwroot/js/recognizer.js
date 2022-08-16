import "./recorder.wav.min.js";

let rec;
let isStart;
let handler;

export function bb_baidu_speech_recognizeOnce(obj, recognizeCallback, interval) {
    Recorder.TrafficImgUrl = "";
    rec = new Recorder({ type: "wav", sampleRate: 16000, bitRate: 16 });
    rec.open(function () {
        isStart = true;
        rec.start();
        // 通知 UI 开始接收语音
        obj.invokeMethodAsync(recognizeCallback, "Start", "");
        handler = setTimeout(function () {
            bb_baidu_speech_close(obj, recognizeCallback, interval);
        }, interval);
    }, function (msg, isUserNotAllow) {
        console.log((isUserNotAllow ? "UserNotAllow，" : "") + "无法录音:" + msg);
        obj.invokeMethodAsync(recognizeCallback, "Error", "UserNotAllow");
    });
};

export function bb_baidu_speech_close(obj, recognizeCallback, interval) {
    if (handler != 0) {
        clearTimeout(handler);
        handler = 0;
    }

    if (isStart) {
        isStart = false;
        rec.stop((blob, duration) => {
            var reader = blob.stream().getReader();
            reader.read().then(value => {
                obj.invokeMethodAsync(recognizeCallback, "Finished", value.value);
            });
        }, msg => {
            obj.invokeMethodAsync(recognizeCallback, "Error", msg);
        }, true);
    }
    else {
        obj.invokeMethodAsync(recognizeCallback, "Close", "");
    }
};
