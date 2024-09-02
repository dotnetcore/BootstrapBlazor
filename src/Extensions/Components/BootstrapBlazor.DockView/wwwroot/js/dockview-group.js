import { getIcons, getIcon } from "./dockview-icon.js"
import { deletePanel, findContentFromPanels } from "./dockview-panel.js"
import { saveConfig } from "./dockview-config.js"
import { observeGroup } from "./dockview-utils.js"
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

    group.header.onDrop(() => {
        saveConfig(dockview)
    })
    group.model.contentContainer.dropTarget.onDrop(() => {
        saveConfig(dockview)
    })
    createGroupActions(group);
    dockview._inited && observeGroup(group)
}

const addGroupWithPanel = (dockview, panel, panels, index) => {
    if (panel.groupId) {
        addPanelWidthGroupId(dockview, panel, index)
    }
    else {
        addPanelWidthCreatGroup(dockview, panel, panels)
    }
    deletePanel(dockview, panel)
}

const addPanelWidthGroupId = (dockview, panel, index) => {
    let group = dockview.api.getGroup(panel.groupId)
    let { position = {}, currentPosition, packupHeight, isPackup, isMaximized } = panel.params || {}
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
        renderer: panel.renderer,
        component: panel.component,
        position: { referenceGroup: group, index: index || 0 },
        params: { ...panel.params, isPackup, packupHeight, isMaximized, position }
    })
    dockview._panelVisibleChanged?.fire({ title: panel.title, status: true });
}

const addPanelWidthCreatGroup = (dockview, panel, panels) => {
    let { position = {}, currentPosition, packupHeight, isPackup, isMaximized } = panel.params || {}
    let brothers = panels.filter(p => p.params.parentId == panel.params.parentId && p.id != panel.id)
    let group, direction
    if (brothers.length > 0 && brothers[0].params.parentType == 'group') {
        group = dockview.groups.find(g => findContentFromPanels(g.panels, brothers[0]))
    }
    else {
        let targetPanel
        for (let i = 0, len = panels.length; i < len; i++) {
            if (panels[i]?.id === panel.id) {
                if (i == len - 1) {
                    targetPanel = panels[i - 1]
                    group = dockview.groups.find(g => findContentFromPanels(g.panels, targetPanel))
                    direction = getOrientation(dockview.gridview.root, group) === 'VERTICAL' ? 'below' : 'right'
                    break
                }
                else {
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
        renderer: panel.renderer,
        component: panel.component,
        position: { referenceGroup: group },
        params: { ...panel.params, isPackup, packupHeight, isMaximized, position }
    }
    if (direction) option.position.direction = direction
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
}

const disposeGroup = group => {
    const { observer } = group.api.accessor.params;
    if (observer) {
        observer.unobserve(group.header.element);
        observer.unobserve(group.header.tabContainer);
    }
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
    if (showUp(group) && getUpState(group)) {
        actionContainer.classList.add('bb-up')
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
const showUp = (group) => {
    return group.model.location.type == 'floating'
}
const getUpState = (group) => {
    return group.panels.some(p => p.params.isPackup)
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
        : (type === 'floating' ? group.activePanel?.params.isMaximized : false)
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
            const targetTabEle = e.target.closest('.tab')
            group.api.accessor.moveGroupOrPanel({
                from: { groupId: group.id, panelId: group.panels.find(p => p.view.tab.element.parentElement == targetTabEle).id },
                to: {
                    group,
                    position: 'center',
                    index: 0,
                },
            });
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
    saveConfig(group.api.accessor)
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
    const gridGroups = dockview.groups.filter(g => g.panels.length > 0 && g.model.location.type === 'grid')
    if (gridGroups.length <= 1) return;

    const { position = {} } = group.getParams()
    const floatingGroupPosition = {
        x: position.left || (x < 35 ? 35 : x),
        y: position.top || (y < 35 ? 35 : y),
        width: position.width || 500,
        height: position.height || 460
    }

    const floatingGroup = dockview.createGroup({ id: getFloatingId(group.id) });

    observeFloatingGroupLocationChange(floatingGroup)

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
    saveConfig(dockview)
}
const observeFloatingGroupLocationChange = fg => {
    const dockview = fg.api.accessor
    fg.api.onDidLocationChange(e => {
        if (e.location.type == 'grid') {
            setTimeout(() => {
                let originalGroup = dockview.groups.find(g => g.id.split('_')[0] == fg.id.split('_')[0])
                if (originalGroup) {
                    dockview.isClearing = true
                    dockview.removeGroup(originalGroup)
                    dockview.isClearing = false
                    fg.header.rightActionsContainer.classList.remove('bb-float')
                    saveConfig(dockview)
                }
            }, 0)

        }
    })
}
const getFloatingId = id => {
    const arr = id.split('_')
    return arr.length == 1 ? id + '_floating' : arr[0]
}

const dock = group => {
    if (group.locked) return;
    const dockview = group.api.accessor
    const originGroup = dockview.groups.find(g => g.id.split('_')[0] == group.id.split('_')[0] && g.id != group.id)
    if (!originGroup) return
    dockview.setVisible(originGroup, true)

    let { isPackup, packupHeight, isMaximized, position } = group.getParams()
    if (!isMaximized) {
        position = {
            width: group.element.parentElement.offsetWidth,
            height: group.element.parentElement.offsetHeight,
            top: parseFloat(group.element.parentElement.style.top || 0),
            left: parseFloat(group.element.parentElement.style.left || 0)
        }
    }
    dockview.moveGroup({
        from: { group: group },
        to: { group: originGroup, position: 'center' }
    })

    originGroup.setParams({ position, isPackup, packupHeight, isMaximized })
    saveConfig(dockview)
}

const down = (group, actionContainer) => {
    const parentEle = group.element.parentElement
    const { isPackup, packupHeight } = group.getParams();
    if (isPackup) {
        group.setParams({ 'isPackup': false })
        parentEle.style.height = `${packupHeight}px`;
        actionContainer.classList.remove('bb-up')
    }
    else {
        group.setParams({ 'isPackup': true, 'packupHeight': parseFloat(parentEle.style.height) });
        parentEle.style.height = `${group.activePanel.view.tab.element.offsetHeight + 2}px`;
        actionContainer.classList.add('bb-up');
    }
    saveConfig(group.api.accessor)
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
            const inactiveTabs = header.querySelectorAll('.tabs-container>.inactive-tab')
            const lastTab = inactiveTabs[inactiveTabs.length - 1]
            const aEle = document.createElement('a')
            const liEle = document.createElement('li')
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

export { onAddGroup, addGroupWithPanel, toggleLock, disposeGroup, observeFloatingGroupLocationChange };
