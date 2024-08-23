import Data from "./data.js"

export function speak(id, invoke, option) {
    const synth = window.speechSynthesis;
    if (synth.speaking) {
        console.error("speechSynthesis.speaking");
        return;
    }
    const { text, lang } = option;
    if (text !== "") {
        const utter = new SpeechSynthesisUtterance(text);
        if (lang) {
            utter.lang = lang;
        }

        utter.onend = function (event) {
            console.log(event, "SpeechSynthesisUtterance.onend");
        };

        utter.onerror = function (event) {
            console.error("SpeechSynthesisUtterance.onerror");
        };
        synth.speak(utter);
    }
}

export function pause(id) {

}

export function resume(id) {

}
