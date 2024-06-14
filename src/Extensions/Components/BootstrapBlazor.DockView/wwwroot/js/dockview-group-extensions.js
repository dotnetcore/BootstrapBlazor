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

const addActionEvent = (group, actionContainer) => {
    EventHandler.on(actionContainer, 'click', '.bb-dockview-control-icon', e => {
        const ele = e.delegateTarget;

        if (ele.classList.contains('bb-dockview-control-icon-lock')) {
            toggleLock(group, actionContainer, false);
        }
        else if (ele.classList.contains('bb-dockview-control-icon-unlock')) {
            toggleLock(group, actionContainer, true);
        }
    });
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

const toggleFull = (group, isMaximized) => {
    const type = group.model.location.type;
    const actionContainer = group.header.element.querySelector('.right-actions-container');

    if (type === 'grid') {
        isMaximized ? group.api.exitMaximized() : group.api.maximize()
    }
    else if (type === 'floating') {
        isMaximized ? _floatingExitMaximized() : _floatingMaximize()
    }
    isMaximized ? actionContainer.classList.remove('bb-maximize') : actionContainer.classList.add('bb-maximize')
    group.panels.forEach(panel => panel.params.isMaximized = !isMaximized)
}

const down = group => {
    const dockview = group.api.accessor;
    const parentEle = group.element.parentElement
    const actionContainer = group.header.element.querySelector('.right-actions-container');
    const isPackup = getParams(group, 'isPackup');
    if (isPackup) {
        _setGroupParams(group, { 'isPackup': false })
        parentEle.style.height = `${getParams('height')}px`;
        actionContainer.classList.remove('bb-up')
    }
    else {
        _setGroupParams(group, { 'isPackup': true, 'height': parseFloat(parentEle.style.height) });
        parentEle.style.height = `${group.activePanel.view._tab._element.offsetHeight}px`;
        actionContainer.classList.add('bb-up');
    }
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
    dockview.addFloatingGroup(floatingGroup, floatingGroupPosition, { skipRemoveGroup: true })

    floatingGroup.panels.slice(0).forEach((panel, index) => {
        dockview.moveGroupOrPanel({
            from: { groupId: group.id, panelId: panel.id },
            to: { floatingGroup, position: 'center', index },
            skipRemoveGroup: true
        })
    })
    dockview.setVisible(group, false)
    floatingGroup.setParams({ isPackup, height, isMaximized })
    createGroupActions(floatingGroup);
}

const dock = group => {
    if (group.locked) return;

    const dockview = group.api.accessor
    const originGroup = dockview.groups.find(group => `${group.id}_floating` === group.id)
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

close = group => {
    if (!group.locked) {
        group.api.close()
    }
}

const getParams = (group, key) => {
    return key && group.activePanel.params[key]
}

const setParams = (group, data) => {
    Object.keys(data).forEach(key => {
        group.panels.forEach(panel => panel.params[key] = data[key])
    })
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

export { createGroupActions, removeGroupActions };
