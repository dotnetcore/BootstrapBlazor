import Data from "./data.js"

export async function start(id, invoke, option) {
    const recognition = new webkitSpeechRecognition() || new SpeechRecognition();

    if (option.triggerStart || true) {
        recognition.onstart = () => {
            console.log('onstart');
            invoke.invokeMethodAsync("TriggerStartCallback");
        }
    }
    if (option.triggerSpeechStart || true) {
        recognition.onspeechstart = () => {
            console.log('onspeechstart');
            invoke.invokeMethodAsync("TriggerSpeechStartCallback");
        }
    }
    if (option.triggerSpeechEnd || true) {
        recognition.onspeechend = () => {
            console.log('onspeechend');
            invoke.invokeMethodAsync("TriggerSpeechEndCallback");
        }
    }
    if (option.triggerSoundStart || true) {
        recognition.onsoundstart = () => {
            console.log('onsoundstart');
            invoke.invokeMethodAsync("TriggerSoundStartCallback");
        }
    }
    if (option.triggerSoundEnd || true) {
        recognition.onsoundend = () => {
            console.log('onsoundend');
            invoke.invokeMethodAsync("TriggerSoundEndCallback");
        }
    }
    if (option.triggerAudioStart || true) {
        recognition.onaudiostart = () => {
            console.log('onaudiostart');
            invoke.invokeMethodAsync("TriggerAudioStartCallback");
        }
    }
    if (option.triggerAudioEnd || true) {
        recognition.onaudioend = () => {
            console.log('onaudioend');
            invoke.invokeMethodAsync("TriggerAudioEndCallback");
        }
    }
    recognition.onresult = e => {
        console.log(e);
        invoke.invokeMethodAsync("TriggerResultCallback", {});
    }
    recognition.onnomatch = e => {
        console.log(e);
        invoke.invokeMethodAsync("TriggerNoMatchCallback", {});
    }
    recognition.onend = () => {
        console.log('onend');
        invoke.invokeMethodAsync("TriggerEndCallback");
    }
    recognition.onerror = e => {
        invoke.invokeMethodAsync("TriggerErrorCallback", {
            error: e.error,
            message: e.message
        });
    }
    //const grammarList = new webkitSpeechGrammarList();
    //const grammar =
    //    "#JSGF V1.0; grammar colors; public <color> = aqua | azure | beige | bisque | black | blue | brown | chocolate | coral | crimson | cyan | fuchsia | ghostwhite | gold | goldenrod | gray | green | indigo | ivory | khaki | lavender | lime | linen | magenta | maroon | moccasin | navy | olive | orange | orchid | peru | pink | plum | purple | red | salmon | sienna | silver | snow | tan | teal | thistle | tomato | turquoise | violet | white | yellow ;";
    //grammarList.addFromString(grammar, 1);
    //recognition.grammars = grammarList;
    recognition.lang = 'zh-CN';
    recognition.maxAlternatives = 1;
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
