export function scrollToBottom(id) {
    const el = document.getElementById(id);
    const top = el.scrollHeight;
    el.scrollTo({
        top,
        left: 0,
        behavior: "smooth"
    });
}
