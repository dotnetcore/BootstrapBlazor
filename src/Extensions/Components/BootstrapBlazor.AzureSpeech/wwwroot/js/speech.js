var recognizer = undefined;
var synthesizer = undefined;
var player = undefined;

function bb_load_speech() {
    const sdk = '_content/BootstrapBlazor.AzureSpeech/js/microsoft.cognitiveservices.speech.sdk.bundle.js';
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

export function bb_azure_speech_recognizeOnce(obj, method, token, region, recognitionLanguage, targetLanguage) {
    var azure_recognizer = function () {
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

    };

    bb_load_speech();

    if (window.SpeechSDK === undefined) {
        var handler = window.setInterval(function () {
            if (window.SpeechSDK) {
                window.clearInterval(handler);

                azure_recognizer();
            }
        }, 100);
    }
    else {
        azure_recognizer();
    }
}

export function bb_azure_close_recognizer(obj, method) {
    if (recognizer != undefined) {
        recognizer.close();
    }
    obj.invokeMethodAsync(method, '');
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
                //if (result.reason === SpeechSDK.ResultReason.SynthesizingAudioCompleted) {
                //    console.log("synthesis finished for [" + inputText + "]");
                //} else if (result.reason === SpeechSDK.ResultReason.Canceled) {
                //    console.log("synthesis failed. Error detail: " + result.errorDetails);
                //}
                obj.invokeMethodAsync(method, "Synthesizer");
                synthesizer.close();
                synthesizer = undefined;
            },
            function (err) {
                console.log(err);

                synthesizer.close();
                synthesizer = undefined;
                obj.invokeMethodAsync(method, "Error");
            });
    };

    bb_load_speech();

    if (window.SpeechSDK === undefined) {
        var handler = window.setInterval(function () {
            if (window.SpeechSDK) {
                window.clearInterval(handler);

                azure_synthesizer();
            }
        }, 200);
    }
    else {
        azure_synthesizer();
    }
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
