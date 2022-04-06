var recognizer = null;

export function bb_speech_recognizeOnce(obj, method) {
    var SpeechSDK = window.SpeechSDK;
    var speechConfig = SpeechSDK.SpeechTranslationConfig.fromSubscription("b35c184e650c44d29734b634bda00789", "eastasia");

    speechConfig.speechRecognitionLanguage = "zh-CN";
    let language = "zh-CN"
    speechConfig.addTargetLanguage(language)

    var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new SpeechSDK.TranslationRecognizer(speechConfig, audioConfig);

    recognizer.recognizeOnceAsync(function (successfulResult) {
        recognizer.close();
        recognizer = null;
        console.log(successfulResult);
        obj.invokeMethodAsync(method, successfulResult.privText);
    });
}

export function bb_close(obj, method) {
    if (recognizer != null) {
        recognizer.close();
    }
    obj.invokeMethodAsync(method, '');
}
