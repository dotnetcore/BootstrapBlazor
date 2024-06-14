import { getIcon } from "./dockview-icon.js"

const updateDockviewPanel = panel => {
    updateCloseButton(panel);
    updateTitle(panel);
}

const updateCloseButton = panel => {
    const showClose = panel.params.showClose ?? panel.accessor.params.options.showClose;
    if (showClose) {
        const closeButton = panel.view.tab._content.nextElementSibling;
        if (closeButton) {
            const closeIcon = getIcon('close', false);
            if (closeIcon) {
                closeButton.replaceChild(closeIcon, closeButton.children[0]);
            }
        }
    }
    else {
        this.tabEle.classList.add('dv-tab-on')
    }
}

const updateTitle = panel => {
    const contentEle = panel.view.content.element;
    const tabEle = panel.view.tab.element;
    const titleElement = contentEle.querySelector('.bb-dockview-item-title');
    if (titleElement) {
        tabEle.replaceChild(titleElement, panel.view.tab._content);
    }
    else {
        const titleBarElement = contentEle.querySelector('.bb-dockview-item-title-icon')
        if (titleBarElement) {
            titleBarElement.removeAttribute('title');
            tabEle.insertAdjacentElement("afterbegin", titleBarElement);
        }
    }
}

export { updateDockviewPanel };
