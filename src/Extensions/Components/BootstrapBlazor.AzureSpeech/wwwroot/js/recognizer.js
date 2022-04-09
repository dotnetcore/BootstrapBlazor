var recognizer = undefined;

export function bb_speech_recognizeOnce(obj, method, token, region, recognitionLanguage, targetLanguage) {
    var SpeechSDK = window.SpeechSDK;
    var speechConfig = SpeechSDK.SpeechTranslationConfig.fromAuthorizationToken(token, region);
    speechConfig.speechRecognitionLanguage = recognitionLanguage;
    speechConfig.addTargetLanguage(targetLanguage)

    var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new SpeechSDK.TranslationRecognizer(speechConfig, audioConfig);

    recognizer.recognizeOnceAsync(function (successfulResult) {
        recognizer.close();
        recognizer = undefined;
        obj.invokeMethodAsync(method, successfulResult.privText);
    }, function (err) {
        console.log(err);
    });
}

export function bb_close(obj, method) {
    if (recognizer != undefined) {
        recognizer.close();
        recognizer = undefined;
    }
    obj.invokeMethodAsync(method, '');
}
