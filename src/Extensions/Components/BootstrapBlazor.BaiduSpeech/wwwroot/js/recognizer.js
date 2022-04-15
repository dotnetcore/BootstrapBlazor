let rec;
let isStart;
let handler;

export function bb_baidu_speech_recognizeOnce(obj, beginRecognize, recognizeCallback) {
    var baidu_recognizer = function () {
        isStart = true;
        rec = new Recorder({ type: "wav", sampleRate: 16000, bitRate: 16 });
        rec.open(function () {
            rec.start();
            // 通知 UI 开始接收语音
            obj.invokeMethodAsync(beginRecognize, "Start");
            handler = setTimeout(function () {
                bb_baidu_speech_close(obj, "Finished", recognizeCallback);
            }, 5000);
        }, function (msg, isUserNotAllow) {
            console.log((isUserNotAllow ? "UserNotAllow，" : "") + "无法录音:" + msg);
        });
    }

    BootstrapBlazorModules.addScript('_content/BootstrapBlazor.BaiduSpeech/js/recorder.wav.min.js');
    BootstrapBlazorModules.load('Recorder', baidu_recognizer);
};

export function bb_baidu_speech_close(obj, recognizerStatus, recognizeCallback) {
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
            obj.invokeMethodAsync(recognizeCallback, "Error", null);
        });
    }
};
