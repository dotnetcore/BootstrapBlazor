var synthesizer = undefined;

export function bb_speech_synthesizerOnce(obj, method, token, region, synthesizerLanguage, voiceName, inputText) {
    var SpeechSDK = window.SpeechSDK;
    var speechConfig = SpeechSDK.SpeechTranslationConfig.fromAuthorizationToken(token, region);
    speechConfig.speechSynthesisLanguage = synthesizerLanguage;
    speechConfig.speechSynthesisVoiceName = voiceName;
    var audioConfig = SpeechSDK.AudioConfig.fromDefaultSpeakerOutput();
    synthesizer = new SpeechSDK.SpeechSynthesizer(speechConfig, audioConfig);
    synthesizer.speakTextAsync(
        inputText,
        function (result) {
            if (result.reason === SpeechSDK.ResultReason.SynthesizingAudioCompleted) {
                console.log("synthesis finished for [" + inputText + "]");
            } else if (result.reason === SpeechSDK.ResultReason.Canceled) {
                console.log("synthesis failed. Error detail: " + result.errorDetails);
            }
            console.log(result);
            synthesizer.close();
            synthesizer = undefined;
            obj.invokeMethodAsync(method, "Finished");
        },
        function (err) {
            console.log(err);

            synthesizer.close();
            synthesizer = undefined;
            obj.invokeMethodAsync(method, "Error");
        });
}

export function bb_close(obj, method) {
    if (synthesizer) {
        synthesizer.close();
        synthesizer = undefined;
        obj.invokeMethodAsync(method, "Close");
    }
}
