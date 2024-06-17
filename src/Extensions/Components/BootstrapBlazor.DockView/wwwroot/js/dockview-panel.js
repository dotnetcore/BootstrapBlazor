import { getIcon } from "./dockview-icon.js"

const onAddPanel = panel => {
    updateCloseButton(panel);
    updateTitle(panel);
}

const onRemovePanel = event => {
    const dockview = event.accessor
    let panel = {
        id: event.id,
        title: event.title,
        component: event.view.contentComponent,
        groupId: event.group.id,
        params: {
            ...event.params,
            currentPosition: {
                width: event.group.element.parentElement.offsetWidth,
                height: event.group.element.parentElement.offsetHeight,
                top: parseFloat(event.group.element.parentElement.style.top || 0),
                left: parseFloat(event.group.element.parentElement.style.left || 0)
            }
        }
    }
    if (event.params.groupInvisible) {
        panel.groupInvisible = event.params.groupInvisible
    }
    savePanel(dockview, panel)

    if (event.group.children) {
        event.group.children = event.group.children.filter(p => findPanel(p, event) !== null);
        event.group.children.push({
            id: event.id,
            title: event.title,
            params: event.params
        })
    }

    if (event.view.content.element) {
        if (event.titleMenuEle) {
            event.view.content.element.append(event.titleMenuEle)
        }
        if (dockview.params.template) {
            dockview.params.template.append(event.view.content.element)
        }
    }
}

const updateCloseButton = panel => {
    const showClose = panel.params.showClose ?? panel.accessor.params.options.showClose;
    const tabEle = panel.view.tab.element
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
        tabEle.classList.add('dv-tab-on')
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

const getPanelsFromOptions = options => {
    return getPanel(options.content[0])
}

const getPanel = (contentItem, parent = {}, panels = []) => {
    if (contentItem.type === 'component') {
        panels.push({
            id: contentItem.id,
            groupId: contentItem.groupId,
            title: contentItem.title,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
            params: { ...contentItem, parentId: parent.id }
        })
    } else {
        contentItem.content?.forEach(item => getPanel(item, contentItem, panels))
    }
    return panels
}

const findContentFromPanels = (panels, content) => {
    return panels.find((p => p.params.key && p.params.key === content.params.key) || p.id === content.id || p.title === content.title);
}

const savePanel = (dockview, panel) => {
    const { panels, options } = dockview.params;
    panels.push(panel)
    if (options.enableLocalStorage) {
        localStorage.setItem(`${options.localStorageKey}-panels`, JSON.stringify(panels))
    }
}

const deletePanel = (dockview, panel) => {
    const { panels, options } = dockview.params;
    let index = panels.indexOf(panel);
    if (index > -1) {
        panels.splice(index, 1);
    }
    if (options.enableLocalStorage) {
        localStorage.setItem(`${options.localStorageKey}-panels`, JSON.stringify(panels))
    }
}

export { onAddPanel, onRemovePanel, getPanelsFromOptions, findContentFromPanels, deletePanel };
