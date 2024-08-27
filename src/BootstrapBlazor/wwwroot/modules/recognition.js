import Data from "./data.js"

export async function start(id, invoke, trigger, option) {
    const speechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
    if (speechRecognition === null) {
        invoke.invokeMethodAsync("TriggerErrorCallback", {
            error: 'not-support',
            message: 'SpeechRecognition is not supported in this browser.'
        });
    }
    const recognition = new speechRecognition();
    if (trigger.triggerStart) {
        recognition.onstart = () => {
            invoke.invokeMethodAsync("TriggerStartCallback");
        }
    }
    if (trigger.triggerSpeechStart) {
        recognition.onspeechstart = () => {
            invoke.invokeMethodAsync("TriggerSpeechStartCallback");
        }
    }
    recognition.onspeechend = () => {
        recognition.stop();
        if (trigger.triggerSpeechEnd) {
            invoke.invokeMethodAsync("TriggerSpeechEndCallback");
        }
    }
    recognition.onnomatch = e => {
        Data.remove(id);
        if (trigger.triggerNoMatch) {
            invoke.invokeMethodAsync("TriggerNoMatchCallback", {
                error: 'no-match',
                message: 'No match found.'
            });
        }
    }
    recognition.onend = () => {
        Data.remove(id);
        if (trigger.triggerEnd) {
            invoke.invokeMethodAsync("TriggerEndCallback");
        }
    }
    recognition.onerror = e => {
        Data.remove(id);
        if (trigger.triggerError) {
            invoke.invokeMethodAsync("TriggerErrorCallback", {
                error: e.error,
                message: e.message
            });
        }
    }
    recognition.onresult = e => {
        let final_transcript = '';
        let interim_transcript = '';
        let isFinal = false;
        for (let i = e.resultIndex; i < e.results.length; i++) {
            if (e.results[i].isFinal) {
                final_transcript += e.results[i][0].transcript;
                isFinal = true;
            }
            else {
                interim_transcript += e.results[i][0].transcript;
            }
        }
        invoke.invokeMethodAsync("TriggerResultCallback", {
            transcript: interim_transcript || final_transcript,
            isFinal: isFinal
        });
    }
    const { lang, maxAlternatives, continuous, interimResults } = option;
    if (lang !== void 0) {
        recognition.lang = lang;
    }
    if (maxAlternatives !== void 0) {
        recognition.maxAlternatives = maxAlternatives;
    }
    if (interimResults !== void 0) {
        recognition.interimResults = interimResults;
    }
    if (continuous !== void 0) {
        recognition.continuous = continuous;
    }
    Data.set(id, recognition);
    recognition.start();
}

export function stop(id) {
    const recognition = Data.get(id);
    Data.remove(id);
    if (recognition) {
        recognition.stop();
    }
}

export function abort(id) {
    const recognition = Data.get(id);
    Data.remove(id);
    if (recognition) {
        recognition.abort();
    }
}
