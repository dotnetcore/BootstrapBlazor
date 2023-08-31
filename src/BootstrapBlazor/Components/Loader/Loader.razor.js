import { addLink, removeLink, addScript, removeScript } from '../../modules/utility.js?v=$version'


export async function init(id) {
    await addLink('./_content/BootstrapBlazor/lib/splitting/splitting-cells.css')
    await addScript('./_content/BootstrapBlazor/lib/splitting/splitting.min.js')
    await addScript('./_content/BootstrapBlazor/lib/splitting/splitting.min.js')

    Splitting();

    const el = document.getElementById(id);
    const cells = el.querySelectorAll(".loader-flip .cell");

    var tl = gsap.timeline({ repeat: -1, repeatDelay: 0.75 });
    tl.from(cells, {
        scale: 0,
        transformOrigin: "center",
        x: "1.5rem",
        duration: 0.25,
        ease: "circ.out",
        stagger: {
            amount: 3,
            from: "start"
        }
    });
    tl.to(cells, {
        scale: 0,
        xPercent: -900,
        duration: 0.25,
        stagger: { amount: 0.75, from: "start" }
    }, "+=0.5");
}
