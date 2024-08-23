import Data from "./data.js"

export function speak(id, invoke, option) {
    const synth = window.speechSynthesis;
    if (synth.speaking) {
        console.error("speechSynthesis.speaking");
        invoke.invokeMethodAsync("OnSpeaking");
        return;
    }
    const { text, lang } = option;
    if (text !== "") {
        const utter = new SpeechSynthesisUtterance(text);
        if (lang) {
            utter.lang = lang;
        }

        utter.onend = () => {
            invoke.invokeMethodAsync("OnEnd");
        };

        utter.onerror = e => {
            console.error("SpeechSynthesisUtterance.onerror", e);
            invoke.invokeMethodAsync("OnError");
        };
        synth.speak(utter);
    }
}

export function pause(id) {

}

export function resume(id) {

}
