import Data from "./data.js"

export function speak(id, invoke, option) {
    const synth = window.speechSynthesis;
    if (synth.speaking) {
        invoke.invokeMethodAsync("OnSpeaking");
        return;
    }

    const { text, lang, pitch, rate, voice, volume } = option;
    if (text !== "") {
        const utter = new SpeechSynthesisUtterance(text);
        if (lang) {
            utter.lang = lang;
        }
        if (pitch) {
            utter.pitch = pitch;
        }
        if (rate) {
            utter.rate = rate;
        }
        if (voice) {
            const voices = synth.getVoices();
            utter.voice = voices.find(v => v.name === voice);
        }
        if (volume) {
            utter.volume = volume;
        }

        utter.onend = () => {
            invoke.invokeMethodAsync("OnEnd");
        };

        utter.onerror = e => {
            invoke.invokeMethodAsync("OnError");
        };
        synth.speak(utter);
    }
}

export function pause(id) {

}

export function resume(id) {

}

export function cancel(id) {

}

export function getVoices() {
    const synth = window.speechSynthesis;
    const voices = synth.getVoices();
    console.log(voices);
    return voices;
}
