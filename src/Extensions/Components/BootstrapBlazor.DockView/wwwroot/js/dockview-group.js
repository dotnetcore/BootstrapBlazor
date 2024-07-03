import { getIcons, getIcon } from "./dockview-icon.js"
import { deletePanel, findContentFromPanels } from "./dockview-panel.js"
import EventHandler from '../../BootstrapBlazor/modules/event-handler.js'

const onAddGroup = group => {
    Object.defineProperties(group, {
        type: {
            get() { return this.model.location.type }
        },
        params: {
            get() { return JSON.parse(JSON.stringify(group.activePanel?.params || {})) }
        }
    })

    const dockview = group.api.accessor;
    const { floatingGroups = [] } = dockview;
    let floatingGroup = floatingGroups.find(item => item.data.id === group.id)
    if (floatingGroup) {
        let { width, height, top, left } = floatingGroup.position
        setTimeout(() => {
            let style = group.element.parentElement.style
            style.width = width + 'px'
            style.height = height + 'px'
            style.top = top + 'px'
            style.left = left + 'px'
        }, 0)
    }

    createGroupActions(group);
}

const addGroupWithPanel = (dockview, panel, panels) => {console.log('add233');
    if (panel.groupId) {
        addPanelWidthGroupId(dockview, panel)
    }
    else {
        addPanelWidthCreatGroup(dockview, panel, panels)
    }
    deletePanel(dockview, panel)
}

const addPanelWidthGroupId = (dockview, panel) => {
    let group = dockview.api.getGroup(panel.groupId)
    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    if (!group) {
        group = dockview.createGroup({ id: panel.groupId })
        const floatingGroupPosition = isMaximized ? {
            x: 0, y: 0,
            width: dockview.width,
            height: dockview.height
        } : {
            x: currentPosition?.left || 0,
            y: currentPosition?.top || 0,
            width: currentPosition?.width,
            height: currentPosition?.height
        }
        dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })
        createGroupActions(group);
    }
    else {
        if (group.api.location.type === 'grid') {
            let isVisible = dockview.isVisible(group)
            if (isVisible === false) {
                dockview.setVisible(group, true)
                isMaximized && group.api.maximize();
            }
        }
    }
    dockview.addPanel({
        id: panel.id,
        title: panel.title,
        component: panel.component,
        position: { referenceGroup: group },
        params: { ...panel.params, isPackup, height, isMaximized, position }
    })
    dockview._panelVisibleChanged?.fire({ title: panel.title, status: true });
}

const addPanelWidthCreatGroup = (dockview, panel, panels) => {
    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    let brothers = panels.filter(p => p.params.parentId == panel.params.parentId && p.id != panel.id)
    let group, direction
    if (brothers.length > 0 && brothers[0].params.parentType == 'group') {
        group = dockview.groups.find(g => findContentFromPanels(g.panels, brothers[0]))
    }
    else {
        let targetPanel
        for (let i = 0, len = panels.length; i < len; i++) {
            if(panels[i]?.id == panel.id){
                if(i == len - 1){
                    targetPanel = panels[i - 1]
                    group = dockview.groups.find(g => findContentFromPanels(g.panels, targetPanel))
                    direction = getOrientation(dockview.gridview.root, group) === 'VERTICAL' ? 'below' : 'right'
                    break
                }
                else{
                    targetPanel = panels[i + 1]
                    group = dockview.groups.find(g => findContentFromPanels(g.panels, targetPanel))
                    direction = getOrientation(dockview.gridview.root, group) === 'VERTICAL' ? 'above' : 'left'
                    break
                }
            }
        }
    }
    let option = {
        id: panel.id,
        title: panel.title,
        component: panel.component,
        position: { referenceGroup: group },
        params: { ...panel.params, isPackup, height, isMaximized, position }
    }
    if(direction) option.position.direction = direction
    dockview.addPanel(option);
    dockview._panelVisibleChanged?.fire({ title: panel.title, status: true });
}

const getOrientation = function (child, group) {
    if (child.children) {
        let targetGroup = child.children.find(item => !item.children && item.element === group.element)
        if (targetGroup) {
            return child.orientation
        }
        else {
            for (const item of child.children) {
                let orientation = getOrientation(item, group)
                if (orientation) {
                    return orientation
                }
            }
        }
    }
    else {
        return false
    }
}

const createGroupActions = group => {
    const actionContainer = group.header.element.querySelector('.right-actions-container');
    getIcons().forEach(item => {
        if (item.name !== 'bar') {
            const icon = getIcon(item.name);
            actionContainer.append(icon);
        }
    });
    setTimeout(() => {
        resetActionStates(group, actionContainer);
    }, 0)
    addActionEvent(group, actionContainer);

    const dockview = group.api.accessor;
    if (dockview.params.observer === null) {
        dockview.params.observer = new ResizeObserver(setWidth);
    }
    dockview.params.observer.observe(group.header.element)
    dockview.params.observer.observe(group.header.tabContainer)

}

const disposeGroup = group => {
    group.api.accessor.params.observer.unobserve(group.header.element);
    group.api.accessor.params.observer.unobserve(group.header.tabContainer);
    removeActionEvent(group);
}

const resetActionStates = (group, actionContainer) => {
    const dockview = group.api.accessor;
    if (showLock(dockview, group)) {
        actionContainer.classList.add('bb-show-lock');
        if (getLockState(dockview, group)) {
            toggleLock(group, actionContainer, true)
        }
    }
    if (showMaximize(dockview, group)) {
        actionContainer.classList.add('bb-show-maximize');
        if (getMaximizeState(group)) {
            toggleFull(group, actionContainer, true)
        }
    }
    if (showFloat(dockview, group)) {
        actionContainer.classList.add('bb-show-float');
        if (getFloatState(group)) {
            actionContainer.classList.add('bb-float');
        }
    }
}

const showLock = (dockview, group) => {
    const { options } = dockview.params;
    return group.panels.every(panel => panel.params.showLock === null)
        ? options.showLock
        : group.panels.some(panel => panel.params.showLock === true)
}

const getLockState = (dockview, group) => {
    const { options } = dockview.params;
    return group.panels.every(p => p.params.isLock === null)
        ? options.isLock
        : group.panels.some(p => p.params.isLock === true);
}

const showMaximize = (dockview, group) => {
    const { options } = dockview.params;
    return group.panels.every(p => p.params.showMaximize === null)
        ? options.showMaximize
        : group.panels.some(p => p.params.showMaximize === true);
}

const getMaximizeState = (group) => {
    const type = group.model.location.type
    return type === 'grid'
        ? group.api.isMaximized()
        : (type === 'floating' ? group.activePanel.params.isMaximized : false)
}

const showFloat = (dockview, group) => {
    const { options } = dockview.params;
    return group.panels.every(panel => panel.params.showFloat === null)
        ? options.showFloat
        : group.panels.some(panel => panel.params.showFloat === true)
}

const getFloatState = group => group.model.location.type === 'floating';

const addActionEvent = group => {
    const actionContainer = group.header.element.querySelector('.right-actions-container');
    const tabsContainer = group.header.tabContainer

    EventHandler.on(actionContainer, 'click', '.bb-dockview-control-icon', e => {
        const ele = e.delegateTarget;
        if (ele.classList.contains('bb-dockview-control-icon-lock')) {
            toggleLock(group, actionContainer, false);
            group.api.accessor._lockChanged.fire({ title: group.panels.map(panel => panel.title), isLock: false });
        }
        else if (ele.classList.contains('bb-dockview-control-icon-unlock')) {
            toggleLock(group, actionContainer, true);
            group.api.accessor._lockChanged.fire({ title: group.panels.map(panel => panel.title), isLock: true });
        }
        else if (ele.classList.contains('bb-dockview-control-icon-restore')) {
            toggleFull(group, actionContainer, false);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-full')) {
            toggleFull(group, actionContainer, true);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-dock')) {
            dock(group);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-float')) {
            float(group);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-down')) {
            down(group, actionContainer, true);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-close') && ele.parentElement.classList.contains('right-actions-container')) {
            close(group, actionContainer, true);
        }
        else if (e.target.classList.contains('dv-default-tab-content')) {
            const liEle = e.target.closest('li');
            const tabEle = tabsContainer.children[0]
            liEle.tabWidth = tabEle.offsetWidth;

            liEle.children[0].appendChild(tabEle);
            tabsContainer.append(e.target.closest('.tab'));
        }
    });
}

const removeActionEvent = group => {
    const actionContainer = group.header.element.querySelector('.right-actions-container');

    EventHandler.off(actionContainer, 'click', '.bb-dockview-control-icon');
}

const toggleLock = (group, actionContainer, isLock) => {
    group.locked = isLock ? 'no-drop-target' : isLock
    group.panels.forEach(panel => panel.params.isLock = isLock);
    if (isLock) {
        actionContainer.classList.add('bb-lock')
    }
    else {
        actionContainer.classList.remove('bb-lock')
    }
}

const toggleFull = (group, actionContainer, isMaximized) => {
    const type = group.model.location.type;

    if (type === 'grid') {
        isMaximized ? group.api.maximize() : group.api.exitMaximized();
    }
    else if (type === 'floating') {
        isMaximized ? floatingMaximize(group) : floatingExitMaximized(group);
    }
    isMaximized ? actionContainer.classList.add('bb-maximize') : actionContainer.classList.remove('bb-maximize')
    group.panels.forEach(panel => panel.params.isMaximized = isMaximized)
}

const float = group => {
    if (group.locked) return;

    const dockview = group.api.accessor;
    const x = (dockview.width - 500) / 2
    const y = (dockview.height - 460) / 2
    const gridGroups = dockview.groups.filter(group => group.panels.length > 0 && group.type === 'grid')
    if (gridGroups.length <= 1) return;

    const { position = {}, isPackup, height, isMaximized } = group.getParams()
    const floatingGroupPosition = {
        x: position.left || (x < 35 ? 35 : x),
        y: position.top || (y < 35 ? 35 : y),
        width: position.width || 500,
        height: position.height || 460
    }

    const floatingGroup = dockview.createGroup({ id: `${group.id}_floating` });

    group.panels.slice(0).forEach((panel, index) => {
        dockview.moveGroupOrPanel({
            from: { groupId: group.id, panelId: panel.id },
            to: { group: floatingGroup, position: 'center', index },
            skipRemoveGroup: true
        })
    })
    dockview.setVisible(group, false)
    dockview.addFloatingGroup(floatingGroup, floatingGroupPosition, { skipRemoveGroup: true })
    createGroupActions(floatingGroup);
}

const dock = group => {
    if (group.locked) return;

    const dockview = group.api.accessor
    const originGroup = dockview.groups.find(item => `${item.id}_floating` === group.id)
    dockview.setVisible(originGroup, true)

    let { isPackup, height, isMaximized, position } = group.getParams()
    if (!isMaximized) {
        position = {
            width: group.width,
            height: group.height,
            top: parseFloat(group.element.parentElement.style.top || 0),
            left: parseFloat(group.element.parentElement.style.left || 0)
        }
    }
    dockview.moveGroup({
        from: { group: group },
        to: { group: originGroup, position: 'center' }
    })

    originGroup.setParams({ position, isPackup, height, isMaximized })
}

const down = (group, actionContainer) => {
    const parentEle = group.element.parentElement
    const { isPackup, height } = group.getParams();
    if (isPackup) {
        group.setParams({ 'isPackup': false })
        parentEle.style.height = `${height}px`;
        actionContainer.classList.remove('bb-up')
    }
    else {
        group.setParams({ 'isPackup': true, 'height': parseFloat(parentEle.style.height) });
        parentEle.style.height = `${group.activePanel.view._tab._element.offsetHeight}px`;
        actionContainer.classList.add('bb-up');
    }
}

close = group => {
    if (!group.locked) {
        group.api.close()
    }
}

const floatingMaximize = group => {
    const parentEle = group.element.parentElement
    const { width, height } = parentEle.style
    const { width: maxWidth, height: maxHeight } = group.api.accessor;
    const { top, left } = parentEle.style
    parentEle.style.left = 0;
    parentEle.style.top = 0;
    parentEle.style.width = `${maxWidth}px`;
    parentEle.style.height = `${maxHeight}px`;
    group.setParams({
        position: {
            top: parseFloat(top || 0),
            left: parseFloat(left || 0),
            width: parseFloat(width),
            height: parseFloat(height)
        },
        isMaximized: true
    })
}

const floatingExitMaximized = group => {
    const parentEle = group.element.parentElement
    const { position } = group.getParams()
    Object.keys(position).forEach(key => parentEle.style[key] = position[key] + 'px')
    group.setParams({ isMaximized: false })
}

const setWidth = (observerList) => {
    observerList.forEach(({ target }) => {
        let header, tabsContainer
        if (target.classList.contains('tabs-container')) {
            header = target.parentElement
            tabsContainer = target
        }
        else {
            header = target
            tabsContainer = header.querySelector('.tabs-container')
        }
        let voidWidth = header.querySelector('.void-container').offsetWidth
        let dropdown = header.querySelector('.right-actions-container>.dropdown')
        if (!dropdown) return
        let dropMenu = dropdown.querySelector('.dropdown-menu')
        if (voidWidth === 0) {
            if (tabsContainer.children.length <= 1) return
            let lastTab = header.querySelector('.tabs-container>.inactive-tab:not(:has(+ .inactive-tab))')
            let aEle = document.createElement('a')
            let liEle = document.createElement('li')
            aEle.className = 'dropdown-item'
            liEle.tabWidth = lastTab.offsetWidth;
            aEle.append(lastTab)
            liEle.append(aEle)
            dropMenu.insertAdjacentElement("afterbegin", liEle)
        }
        else {
            let firstLi = dropMenu.querySelector('li:has(.active-tab)') || dropMenu.children[0]
            if (firstLi) {
                let firstTab = firstLi.querySelector('.tab')
                if (voidWidth > firstLi.tabWidth || tabsContainer.children.length == 0) {
                    firstTab && tabsContainer.append(firstTab)
                    firstLi.remove()
                }
            }
        }
    })
}

export { onAddGroup, addGroupWithPanel, toggleLock, disposeGroup };
