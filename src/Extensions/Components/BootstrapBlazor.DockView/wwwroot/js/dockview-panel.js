import { saveConfig } from "./dockview-config.js";
import { getIcon } from "./dockview-icon.js"

const onAddPanel = panel => {
    updateCloseButton(panel);
    updateTitle(panel);
    panel.api.onDidActiveChange(({ isActive }) => {
        if (isActive && !panel.group.api.isMaximized()) {
            saveConfig(panel.accessor)
            if (panel.group.panels.length < 2) return
            panel.group.panels.filter(p => p != panel.group.activePanel && p.renderer == 'onlyWhenVisible').forEach(p => {
                appendTemplatePanelEle(p)
            })
        }
    })
}

const onRemovePanel = event => {
    const dockview = event.accessor
    let panel = {
        id: event.id,
        title: event.title,
        component: event.view.contentComponent,
        renderer: event.renderer,
        groupId: event.group.id,
        params: {
            ...event.params,
            currentPosition: {
                width: event.group.element.parentElement.offsetWidth,
                height: event.group.element.parentElement.offsetHeight,
                top: parseFloat(event.group.element.parentElement.style.top || 0),
                left: parseFloat(event.group.element.parentElement.style.left || 0)
            },
            index: event.group.delPanelIndex
        }
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

const appendTemplatePanelEle = (panel) => {
    const dockview = panel.accessor
    if (panel.view.content.element) {
        if (panel.titleMenuEle) {
            panel.view.content.element.append(panel.titleMenuEle)
        }
        if (dockview.params.template) {
            dockview.params.template.append(panel.view.content.element)
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
    const tabEle = panel.view.tab.element;
    const contents = [...panel.view.content.element.children];
    const titleElement = contents.find(i => i.classList.contains('bb-dockview-item-title'));
    if (titleElement) {
        tabEle.replaceChild(titleElement, panel.view.tab._content);
    }
    else {
        const titleBarElement = contents.find(i => i.classList.contains('bb-dockview-item-title-icon'));
        if (titleBarElement) {
            titleBarElement.removeAttribute('title');
            tabEle.insertAdjacentElement("afterbegin", titleBarElement);
        }
    }
}

const getPanelsFromOptions = options => {
    return getPanels(options.content[0], options)
}

const getPanels = (contentItem, options, parent = {}, panels = []) => {
    if (contentItem.type === 'component') {
        panels.push({
            id: contentItem.id,
            groupId: contentItem.groupId,
            title: contentItem.title,
            renderer: contentItem.renderer || options.renderer,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
            params: { ...contentItem, parentType: parent.type, parentId: parent.id }
        });
    }
    else {
        contentItem.content?.forEach(item => getPanels(item, options, contentItem, panels))
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
        const timer = setTimeout(() => {
            clearTimeout(timer)
            saveConfig(dockview)
        }, 0)
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
        saveConfig(dockview)
    }
}

export { onAddPanel, onRemovePanel, getPanelsFromOptions, findContentFromPanels, deletePanel };
