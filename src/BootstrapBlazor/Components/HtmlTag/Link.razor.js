export function init(options) {
    const { href, rel } = options;
    const nhref = new URL(href, document.baseURI);
    const links = document.head.getElementsByTagName("link");
    // 遍历所有link元素
    for (let i = 0; i < links.length; i++) {
        const hlink = links[i];
        if (hlink.href == nhref && hlink.rel == rel) {
            return;
        }
    }
    const link = document.createElement('link');
    link.rel = rel;
    link.href = href;
    document.head.appendChild(link);
}
