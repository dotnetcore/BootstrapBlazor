import { addLink, addScript } from '../../modules/utility.js?v=$version'

export async function init(op) {
    await addLink('./_content/BootstrapBlazor/lib/splitting/splitting-cells.css')
    await addScript('./_content/BootstrapBlazor/lib/splitting/splitting.min.js')
    await addScript('./_content/BootstrapBlazor/modules/gsap.min.js')

    const el = document.getElementById(op.id);
    const cell = el.querySelectorAll(".loader-flip");

    const results = Splitting({
        target: cell,
        by: 'cells',
        image: true,
        columns: op.columns,
        rows: 1
    });

    const cells = results[0].cells;
    const repeat = op.isrepeat ? -1 : 0;

    var tl = gsap.timeline({
        repeat: repeat,
        repeatDelay: 0.75
    });

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
        stagger: {
            amount: 0.75,
            from: "start"
        },
    }, "+=0.5");
}

export async function update(op) {
    const el = document.getElementById(op.id);
    const flip = el.querySelectorAll(".loader-flip");

    delete flip[0]['🍌'];

    const cells = el.querySelectorAll(".cell-grid");
    cells.forEach(x => x.remove());

    await init(op);
}
