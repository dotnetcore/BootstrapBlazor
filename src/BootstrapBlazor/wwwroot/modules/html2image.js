import '../lib/html2image/html-to-image.js'

export async function execute(selector, methodName, options) {
    let data = null;
    const el = document.querySelector(selector);
    if (el) {
        const fn = htmlToImage[methodName];
        data = await fn(el, {});
    }
    return data;
}
