import { getIcons, getIcon } from "./dockview-icon-extensions.js"

const createGroupActions = group => {
    const actionContainer = group.header.element.querySelector('.right-actions-container');
    getIcons().forEach(item => {
        if (item.name !== 'bar') {
            const icon = getIcon(item.name);
            icon.addEventListener('click', () => {
                const handleName = '_' + item.name
                this[handleName] && this[handleName](icon)
            })
            actionContainer.append(icon);
        }
    });

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
        if (getMaximizeState(group, _getParams.bind(this))) {
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

const getFloatState = group => group.model.location.type === 'floating'

const toggleLock = isLock => {
    const dockview = this.group.api.accessor;
    this.group.locked = isLock ? 'no-drop-target' : isLock
    this.group.panels.forEach(panel => panel.params.isLock = isLock);
    isLock ? this.actionContainer.classList.add('bb-lock') : this.actionContainer.classList.remove('bb-lock')
    dockview._lockChanged.fire({ title: this.group.panels.map(panel => panel.title), isLock })
    saveConfig(dockview)
}

//const toggleFull(isMaximized) {
//    const type = this.group.model.location.type
//    if (type === 'grid') {
//        isMaximized ? this.group.api.exitMaximized() : this.group.api.maximize()
//    }
//    else if (type === 'floating') {
//        isMaximized ? this._floatingExitMaximized() : this._floatingMaximize()
//    }
//    isMaximized ? this.actionContainer.classList.remove('bb-maximize') : this.actionContainer.classList.add('bb-maximize')
//    this.group.panels.forEach(panel => panel.params.isMaximized = !isMaximized)
//}

//_lock() {
//    this.toggleLock(false)
//}

//_unlock() {
//    this.toggleLock(true)
//}

//_down() {
//    const dockview = this.group.api.accessor;
//    const isPackup = this._getParams('isPackup')
//    const parentEle = this.group.element.parentElement
//    if (isPackup) {
//        this._setGroupParams({ 'isPackup': false })
//        parentEle.style.height = `${this._getParams('height')}px`;
//        this.actionContainer.classList.remove('bb-up')
//    }
//    else {
//        this._setGroupParams({ 'isPackup': true, 'height': parseFloat(parentEle.style.height) });
//        parentEle.style.height = `${this.group.activePanel.view._tab._element.offsetHeight}px`;
//        this.actionContainer.classList.add('bb-up');
//    }
//    saveConfig(dockview)
//}

//_full() {
//    return this.toggleFull(false)
//}

//_restore() {
//    return this.toggleFull(true)
//}

//_float() {
//    if (this.group.locked) return;

//    const dockview = this.group.api.accessor;
//    const x = (dockview.width - 500) / 2
//    const y = (dockview.height - 460) / 2
//    const gridGroups = dockview.groups.filter(group => group.panels.length > 0 && group.type === 'grid')
//    if (gridGroups.length <= 1) return;

//    const { position = {}, isPackup, height, isMaximized } = this.group.getParams()
//    const floatingGroupPosition = isMaximized
//        ? {
//            x: 0, y: 0,
//            width: dockview.width,
//            height: dockview.height
//        }
//        : {
//            x: position.left || (x < 35 ? 35 : x),
//            y: position.top || (y < 35 ? 35 : y),
//            width: position.width || 500,
//            height: position.height || 460
//        }

//    const group = dockview.createGroup({ id: `${this.group.id}_floating` });
//    dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })

//    this.group.panels.slice(0).forEach((panel, index) => {
//        dockview.moveGroupOrPanel({
//            from: { groupId: this.group.id, panelId: panel.id },
//            to: { group, position: 'center', index },
//            skipRemoveGroup: true
//        })
//    })
//    dockview.setVisible(this.group, false)
//    group.setParams({ isPackup, height, isMaximized })
//    group.groupControl = new GroupControl(group)
//    saveConfig(dockview)
//}

//_dock() {
//    if (this.group.locked) return;

//    const dockview = this.group.api.accessor
//    const originGroup = dockview.groups.find(group => `${group.id}_floating` === this.group.id)
//    dockview.setVisible(originGroup, true)

//    let { isPackup, height, isMaximized, position } = this.group.getParams()
//    if (!isMaximized) {
//        position = {
//            width: this.group.width,
//            height: this.group.height,
//            top: parseFloat(this.group.element.parentElement.style.top || 0),
//            left: parseFloat(this.group.element.parentElement.style.left || 0)
//        }
//    }
//    dockview.moveGroup({
//        from: { group: this.group },
//        to: { group: originGroup, position: 'center' }
//    })

//    originGroup.setParams({ position, isPackup, height, isMaximized })
//    saveConfig(dockview)
//}

//_close() {
//    if (!this.group.locked) {
//        this.group.api.close()
//    }
//}

const _getParams = (group, key) => {
    return key && group.activePanel.params[key]
}

//_setGroupParams(data) {
//    Object.keys(data).forEach(key => {
//        this.group.panels.forEach(panel => panel.params[key] = data[key])
//    })
//}

//_floatingMaximize() {
//    const parentEle = this.group.element.parentElement
//    const { width, height } = parentEle.style
//    const { width: maxWidth, height: maxHeight } = this.group.api.accessor;
//    const { top, left } = parentEle.style
//    parentEle.style.left = 0;
//    parentEle.style.top = 0;
//    parentEle.style.width = `${maxWidth}px`;
//    parentEle.style.height = `${maxHeight}px`;
//    this._setGroupParams({
//        position: {
//            top: parseFloat(top || 0),
//            left: parseFloat(left || 0),
//            width: parseFloat(width),
//            height: parseFloat(height)
//        },
//        isMaximized: true
//    })
//}

//_floatingExitMaximized() {
//    const parentEle = this.group.element.parentElement
//    const position = this._getParams('position')
//    Object.keys(position).forEach(key => parentEle.style[key] = position[key] + 'px')
//    this._setGroupParams({ isMaximized: false })
//}

export { createGroupActions };
