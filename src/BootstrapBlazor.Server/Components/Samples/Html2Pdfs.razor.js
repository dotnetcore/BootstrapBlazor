export function getHtml(id) {
    let html = ''; 
    const el = document.getElementById(id);
    if (el) {
        html = el.outerHTML;
    }
    return html;
}
