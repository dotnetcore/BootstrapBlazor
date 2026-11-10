export function scrollTo(id, direction = "bottom") {
    const el = document.getElementById(id);
    let top = el.scrollTop;
    let left = el.scrollLeft;
    switch (direction) {
        case "top":
            top = 0;
            break;
        case "bottom":
            top = el.scrollHeight;
            break;
        case "left":
            left = 0;
            break;
        case "right":
            left = el.scrollWidth;
            break;
        default:
            return;
    }
    el.scrollTo({
        top,
        left,
        behavior: "smooth"
    });
}
