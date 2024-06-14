import { getIcons, getIcon } from "./dockview-icon-extensions.js"
import EventHandler from '../../BootstrapBlazor/modules/event-handler.js'

const createGroupActions = group => {
    const actionContainer = group.header.element.querySelector('.right-actions-container');
    getIcons().forEach(item => {
        if (item.name !== 'bar') {
            const icon = getIcon(item.name);
            actionContainer.append(icon);
        }
    });
    resetActionStates(group, actionContainer);
    addActionEvent(group, actionContainer);
}

const removeGroupActions = group => {
    removeActionEvent(group);
}

const resetActionStates = (group, actionContainer) => {
    const dockview = group.api.accessor;
    if (showLock(dockview, group)) {
        actionContainer.classList.add('bb-show-lock');
        if (getLockState(dockview, group)) {
            actionContainer.classList.add('bb-lock');
            toggleLock(true)
        }
    }
    if (showMaximize(dockview, group)) {
        actionContainer.classList.add('bb-show-maximize');
        if (getMaximizeState(group)) {
            actionContainer.classList.add('bb-maximize');
            toggleFull(false)
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

    EventHandler.on(actionContainer, 'click', '.bb-dockview-control-icon', e => {
        const ele = e.delegateTarget;

        if (ele.classList.contains('bb-dockview-control-icon-lock')) {
            toggleLock(group, actionContainer, false);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-unlock')) {
            toggleLock(group, actionContainer, true);
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
        else if (ele.classList.contains('bb-dockview-control-icon-close')) {
            close(group, actionContainer, true);
        }
    });
}

const removeActionEvent = group => {
    const actionContainer = group.header.element.querySelector('.right-actions-container');

    EventHandler.off(actionContainer, 'click', '.bb-dockview-control-icon');
}

const toggleLock = (group, actionContainer, isLock) => {
    const dockview = group.api.accessor;

    group.panels.forEach(panel => panel.params.isLock = isLock);
    if (isLock) {
        actionContainer.classList.add('bb-lock')
    }
    else {
        actionContainer.classList.remove('bb-lock')
    }
    dockview._lockChanged.fire({ title: group.panels.map(panel => panel.title), isLock })
}

const toggleFull = (group, actionContainer, isMaximized) => {
    const type = group.model.location.type;

    if (type === 'grid') {
        isMaximized ? group.api.maximize() : group.api.exitMaximized();
    }
    else if (type === 'floating') {
        isMaximized ? floatingMaximize() : floatingExitMaximized();
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
    const floatingGroupPosition = isMaximized
        ? {
            x: 0, y: 0,
            width: dockview.width,
            height: dockview.height
        }
        : {
            x: position.left || (x < 35 ? 35 : x),
            y: position.top || (y < 35 ? 35 : y),
            width: position.width || 500,
            height: position.height || 460
        }

    const floatingGroup = dockview.createGroup({ id: `${group.id}_floating` });

    group.panels.forEach((panel, index) => {
        dockview.moveGroupOrPanel({
            from: { groupId: group.id, panelId: panel.id },
            to: { group: floatingGroup, position: 'center', index },
            skipRemoveGroup: true
        })
    })
    dockview.setVisible(group, false)
    floatingGroup.setParams({ isPackup, height, isMaximized })
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
    const isPackup = group.getParams('isPackup');
    if (isPackup) {
        group.setParams({ 'isPackup': false })
        parentEle.style.height = `${getPanelParams('height')}px`;
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
    this._setGroupParams({
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
    const position = getParams('position')
    Object.keys(position).forEach(key => parentEle.style[key] = position[key] + 'px')
    setParams({ isMaximized: false })
}

const getPanelParams = (group, key) => {
    return key && group.activePanel.params[key]
}

const setPanelParams = (group, data) => {
    Object.keys(data).forEach(key => {
        group.panels.forEach(panel => panel.params[key] = data[key])
    })
}

export { createGroupActions, removeGroupActions };
