import Data from "./data.js"

export async function start(id, invoke, option) {
    const recognition = new webkitSpeechRecognition() || new SpeechRecognition();

    if (option.triggerStart || true) {
        recognition.onstart = () => {
            invoke.invokeMethodAsync("TriggerStartCallback");
        }
    }
    if (option.triggerSpeechStart || true) {
        recognition.onspeechstart = () => {
            invoke.invokeMethodAsync("TriggerSpeechStartCallback");
        }
    }
    if (option.triggerSpeechEnd || true) {
        recognition.onspeechend = () => {
            recognition.stop();
            invoke.invokeMethodAsync("TriggerSpeechEndCallback");
        }
    }
    recognition.onnomatch = e => {
        invoke.invokeMethodAsync("TriggerNoMatchCallback", {});
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
        const transcript = e.results[0][0];
        invoke.invokeMethodAsync("TriggerResultCallback", {
            transcript: transcript.transcript,
            confidence: transcript.confidence
        });
    }
    recognition.lang = 'zh-CN';
    recognition.maxAlternatives = 1;
    recognition.interimResults = false;
    recognition.continuous = false;
    recognition.start();
}

export function stop(id) {
    const synth = window.speechSynthesis;
    synth.pause();
}

export function abort(id) {
    const synth = window.speechSynthesis;
    synth.resume();
}
