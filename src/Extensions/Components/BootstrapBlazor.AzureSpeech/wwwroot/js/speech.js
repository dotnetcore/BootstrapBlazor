var recognizer = undefined;
var synthesizer = undefined;
var player = undefined;

export function bb_azure_speech_recognizeOnce(obj, method, token, region, recognitionLanguage, targetLanguage, interval) {
    var azure_recognizer = function () {
        var speechConfig = SpeechSDK.SpeechTranslationConfig.fromAuthorizationToken(token, region);
        speechConfig.speechRecognitionLanguage = recognitionLanguage;
        speechConfig.addTargetLanguage(targetLanguage)

        var audioConfig = SpeechSDK.AudioConfig.fromDefaultMicrophoneInput();
        recognizer = new SpeechSDK.TranslationRecognizer(speechConfig, audioConfig);

        obj.invokeMethodAsync(method, "Start", '');
        recognizer.recognizeOnceAsync(function (successfulResult) {
            recognizer.close();
            recognizer = undefined;
            obj.invokeMethodAsync(method, "Finished", successfulResult.privText);
        }, function (err) {
            console.log(err);
            recognizer = undefined;
            obj.invokeMethodAsync(method, "Error", err);
        });

        if (interval) {
            var handler = window.setTimeout(function () {
                window.clearTimeout(handler);
                if (recognizer != undefined) {
                    recognizer.close();
                    recognizer = undefined;
                }
            }, interval);
        }
    };

    BootstrapBlazorModules.addScript('_content/BootstrapBlazor.AzureSpeech/js/microsoft.cognitiveservices.speech.sdk.bundle.js');
    BootstrapBlazorModules.load('SpeechSDK', azure_recognizer);
}

export function bb_azure_close_recognizer(obj, method) {
    if (recognizer != undefined) {
        recognizer.close();
    }
    obj.invokeMethodAsync(method, "Close", '');
}

export function bb_azure_speech_synthesizerOnce(obj, method, token, region, synthesizerLanguage, voiceName, inputText) {
    var azure_synthesizer = function () {
        player = new SpeechSDK.SpeakerAudioDestination();
        player.onAudioEnd = function () {
            player = undefined;
            obj.invokeMethodAsync(method, "Finished");
        };

        var speechConfig = SpeechSDK.SpeechTranslationConfig.fromAuthorizationToken(token, region);
        speechConfig.speechSynthesisLanguage = synthesizerLanguage;
        speechConfig.speechSynthesisVoiceName = voiceName;
        var audioConfig = SpeechSDK.AudioConfig.fromSpeakerOutput(player);
        synthesizer = new SpeechSDK.SpeechSynthesizer(speechConfig, audioConfig);

        synthesizer.speakTextAsync(
            inputText,
            function (result) {
                obj.invokeMethodAsync(method, "Synthesizer");
                synthesizer.close();
                synthesizer = undefined;
            },
            function (err) {
                console.log(err);

                if (synthesizer != undefined) {
                    synthesizer.close();
                }
                synthesizer = undefined;
                obj.invokeMethodAsync(method, "Error");
            });
    };

    BootstrapBlazorModules.addScript('_content/BootstrapBlazor.AzureSpeech/js/microsoft.cognitiveservices.speech.sdk.bundle.js');
    BootstrapBlazorModules.load('SpeechSDK', azure_synthesizer);
}

export function bb_azure_close_synthesizer(obj, method) {
    if (synthesizer != undefined) {
        synthesizer.close();
        synthesizer = undefined;
    }
    if (player != undefined) {
        player.pause();
        player = undefined;
        obj.invokeMethodAsync(method, "Cancel");
    }
}
