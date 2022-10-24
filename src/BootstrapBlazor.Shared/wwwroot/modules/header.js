import EventHandler from "../../../_content/BootstrapBlazor/modules/base/event-handler.js";

export class Header {
    static init() {
        const scrollTop = () => (document.documentElement && document.documentElement.scrollTop) || document.body.scrollTop
        let prevScrollTop = 0;
        EventHandler.on(document, 'scroll',  () => {
            const items = document.querySelectorAll('header, .coms-search')
            const currentScrollTop = scrollTop()
            if (currentScrollTop > prevScrollTop) {
                items.forEach(item => item.classList.add('hide'))
            } else {
                items.forEach(item => item.classList.remove('hide'))
            }
            prevScrollTop = currentScrollTop
        })
    }

    static dispose() {
        EventHandler.off(document, 'scroll')
    }
}
