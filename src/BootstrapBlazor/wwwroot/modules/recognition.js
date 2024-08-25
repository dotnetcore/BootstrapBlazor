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
        invoke.invokeMethodAsync("TriggerSpeechEndCallback");
    }
    recognition.onnomatch = e => {
        invoke.invokeMethodAsync("TriggerNoMatchCallback", {
            error: 'no-match',
            message: 'No match found.'
        });
    }
    recognition.onend = () => {
        invoke.invokeMethodAsync("TriggerEndCallback");
    }
    recognition.onerror = e => {
        invoke.invokeMethodAsync("TriggerErrorCallback", {
            error: e.error,
            message: e.message
        });
    }
    recognition.onresult = e => {
        let final_transcript = '';
        let interim_transcript = '';
        for (let i = e.resultIndex; i < e.results.length; i++) {
            if (e.results[i].isFinal) {
                final_transcript += e.results[i][0].transcript;
            } else {
                interim_transcript += e.results[i][0].transcript;
            }
        }
        invoke.invokeMethodAsync("TriggerResultCallback", {
            transcript: interim_transcript || final_transcript,
            isFinal: final_transcript !== ''
        });
    }
    const { lang, maxAlternatives, continuous, interimResults } = option;
    if (lang !== null) {
        recognition.lang = lang;
    }
    if (maxAlternatives !== null) {
        recognition.maxAlternatives = maxAlternatives;
    }
    if (interimResults !== null) {
        recognition.interimResults = interimResults;
    }
    if (continuous !== null) {
        recognition.continuous = continuous;
    }
    recognition.start();
}

export function stop(id) {
    const speechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
    const recognition = new speechRecognition();
    recognition.stop();
}

export function abort(id) {
    const speechRecognition = window.webkitSpeechRecognition || window.SpeechRecognition;
    const recognition = new speechRecognition();
    recognition.abort();
}
