let rec;
let isStart;
let handler;

function bb_load_speech() {
    const sdk = '_content/BootstrapBlazor.BaiduSpeech/js/recorder.wav.min.js';
    const links = [...document.getElementsByTagName('script')];
    var link = links.filter(function (link) {
        return link.src.indexOf(sdk) > -1;
    });
    if (link.length === 0) {
        link = document.createElement('script');
        link.setAttribute('src', sdk);
        document.body.appendChild(link);
    }
}

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

    bb_load_speech();

    if (window.Recorder === undefined) {
        var handler = window.setInterval(function () {
            if (window.Recorder) {
                window.clearInterval(handler);

                baidu_recognizer();
            }
        }, 100);
    }
    else {
        baidu_recognizer();
    }
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
