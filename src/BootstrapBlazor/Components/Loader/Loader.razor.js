import { addLink, removeLink, addScript, removeScript } from '../../modules/utility.js?v=$version'

const spcss = "./splitting/splitting.css";
const spcellscss = "./splitting/splitting-cells.css";
const spjs = "./splitting/splitting.min.js";
const gsapjs = "./gsap/gsap.min.js";

export async function init(isRepeat, repeatDelay, duration) {
    await addLink(spcss)
    await addLink(spcellscss)
    await addScript(spjs)
    await addScript(gsapjs)

    Splitting();

    const repeat = isRepeat ? -1 : 0;

    var tl = gsap.timeline({ repeat: repeat, repeatDelay: repeatDelay });
    tl
        .from(".loader-flip .cell", {
            scale: 0,
            transformOrigin: "center",
            x: "1.5rem",
            duration: duration,
            ease: "circ.out",
            stagger: {
                amount: 3,
                from: "start"
            }
        })
        .to(".loader-flip .cell",
            {
                scale: 0,
                xPercent: -900,
                duration: duration,
                stagger: { amount: repeatDelay, from: "start" }
            }, "+=0.5"
        );
}

export async function dispose() {
    await removeLink(spcss)
    await removeLink(spcellscss)
    await removeScript(spjs)
    await removeScript(gsapjs)
}
