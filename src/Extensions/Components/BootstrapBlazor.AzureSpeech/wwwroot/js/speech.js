var recognizer = null;

export function bb_speech_recognizeOnce(obj, method, token, region, recognitionLanguage, targetLanguage) {
    var SpeechSDK = window.SpeechSDK;
    var speechConfig = SpeechSDK.SpeechTranslationConfig.fromAuthorizationToken(token, region);
    speechConfig.speechRecognitionLanguage = recognitionLanguage;
    speechConfig.addTargetLanguage(targetLanguage)

    var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
    recognizer = new SpeechSDK.TranslationRecognizer(speechConfig, audioConfig);

    recognizer.recognizeOnceAsync(function (successfulResult) {
        recognizer.close();
        recognizer = null;
        console.log(successfulResult);
        obj.invokeMethodAsync(method, successfulResult.privText);
    }, function (err) {

    });
}

export function bb_close(obj, method) {
    if (recognizer != null) {
        recognizer.close();
    }
    obj.invokeMethodAsync(method, '');
}
