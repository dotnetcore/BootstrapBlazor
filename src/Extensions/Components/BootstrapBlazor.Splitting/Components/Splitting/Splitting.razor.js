import { addLink, addScript } from '../../modules/utility.js?v=$version'

const reset = el => {
    const flip = el.querySelector(".splitting-flip");
    if (flip) {
        delete flip['🍌'];
        flip.innerHTML = ''
    }
}

const loader = el => {
    const flip = el.querySelector(".splitting-flip");
    const columns = el.getAttribute('data-bb-columns');
    const repeat = el.getAttribute('data-bb-repeat');

    const splitting = Splitting({
        target: flip,
        by: 'cells',
        image: true,
        columns: columns,
        rows: 1
    });

    const cells = splitting[0].cells;

    const tl = gsap.timeline({
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

export async function init(id) {
    await addLink('./_content/BootstrapBlazor.Splitting/lib/splitting/splitting-cells.css')
    await addScript('./_content/BootstrapBlazor.Splitting/lib/splitting/splitting.min.js')
    await addScript('./_content/BootstrapBlazor.Splitting/modules/gsap.min.js')

    const el = document.getElementById(id);
    loader(el);
}

export function update(id) {
    const el = document.getElementById(id);

    reset(el);
    loader(el);
}
