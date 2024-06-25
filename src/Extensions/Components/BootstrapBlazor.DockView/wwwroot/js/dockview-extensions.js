import { DockviewComponent, DockviewGroupPanel, getGridLocation, getRelativeLocation, DockviewEmitter } from "./dockview-core.esm.js"
import { getConfigFromStorage } from "./dockview-config.js"
DockviewComponent.prototype.on = function (eventType, callback) {
    this['_' + eventType] = new DockviewEmitter();
    this['_' + eventType].event(callback)
}

DockviewGroupPanel.prototype.getParams = function () {
    return this.activePanel?.params || {}
}

DockviewGroupPanel.prototype.setParams = function (data) {
    Object.keys(data).forEach(key => {
        this.panels.forEach(panel => panel.params[key] = data[key])
    })
}

DockviewGroupPanel.prototype.removePropsOfParams = function (keys) {
    return (keys instanceof Array)
        ? keys.map(key => this.panels.forEach(panel => delete panel.params[key]))
        : typeof keys == 'string' ? delete panel.params[keys] : false
}

const removeGroup = DockviewComponent.prototype.removeGroup
DockviewComponent.prototype.removeGroup = function (...argu) {
    if (this.isClearing) {
        return removeGroup.apply(this, argu)
    }

    const group = argu[0]
    const type = group.api.location.type;
    if (type == 'grid') {
        [...group.panels].forEach(panel => {
            panel.api.close()
        })
        this.setVisible(group, false)

        // 在本地存储的已删除的panel上保存Group是否可见, 因为toJson()不保存此信息, 会默认展示隐藏的Group
        let delPanels = getConfigFromStorage(this.params.options.localStorageKey + '-panels')
        delPanels = delPanels?.map(panel => {
            if (panel.groupId == group.id) {
                panel.groupInvisible = true
            }
            return panel
        })
        delPanels && localStorage.setItem(this.params.options.localStorageKey + '-panels', JSON.stringify(delPanels))
    }
    else if (type == 'floating') {
        return removeGroup.apply(this, argu)
    }
}

const removePanel = DockviewComponent.prototype.removePanel
DockviewComponent.prototype.removePanel = function (...argu) {
    const panel = argu[0]
    if (!panel.group.locked) {
        removePanel.apply(this, argu)
        if (!this.isClearing) {
            this._panelClosed?.fire(panel.title)
        }
    }
}

DockviewComponent.prototype.moveGroupOrPanel = function moveGroupOrPanel(options) {
    var _a;
    const destinationGroup = options.to.group;
    const sourceGroupId = options.from.groupId;
    const sourceItemId = options.from.panelId;
    const destinationTarget = options.to.position;
    const destinationIndex = options.to.index;
    const sourceGroup = sourceGroupId
        ? (_a = this._groups.get(sourceGroupId)) === null || _a === void 0 ? void 0 : _a.value
        : undefined;
    if (!sourceGroup) {
        throw new Error(`Failed to find group id ${sourceGroupId}`);
    }
    if (sourceItemId === undefined) {
        /**
         * Moving an entire group into another group
         */
        this.moveGroup({
            from: { group: sourceGroup },
            to: {
                group: destinationGroup,
                position: destinationTarget,
            },
        });
        return;
    }
    if (!destinationTarget || destinationTarget === 'center') {
        /**
         * Dropping a panel within another group
         */
        const removedPanel = this.movingLock(() => sourceGroup.model.removePanel(sourceItemId, {
            skipSetActive: false,
            skipSetActiveGroup: true,
        }));
        if (!removedPanel) {
            throw new Error(`No panel with id ${sourceItemId}`);
        }
        if (sourceGroup.model.size === 0 && !options.skipRemoveGroup) {
            // remove the group and do not set a new group as active
            this.doRemoveGroup(sourceGroup, { skipActive: true });
        }
        this.movingLock(() => destinationGroup.model.openPanel(removedPanel, {
            index: destinationIndex,
            skipSetGroupActive: true,
        }));
        this.doSetGroupAndPanelActive(destinationGroup);
        this._onDidMovePanel.fire({
            panel: removedPanel,
        });
    }
    else {
        /**
         * Dropping a panel to the extremities of a group which will place that panel
         * into an adjacent group
         */
        const referenceLocation = getGridLocation(destinationGroup.element);
        const targetLocation = getRelativeLocation(this.gridview.orientation, referenceLocation, destinationTarget);
        if (sourceGroup.size < 2) {
            /**
             * If we are moving from a group which only has one panel left we will consider
             * moving the group itself rather than moving the panel into a newly created group
             */
            const [targetParentLocation, to] = tail(targetLocation);
            if (sourceGroup.api.location.type === 'grid') {
                const sourceLocation = getGridLocation(sourceGroup.element);
                const [sourceParentLocation, from] = tail(sourceLocation);
                if (sequenceEquals(sourceParentLocation, targetParentLocation)) {
                    // special case when 'swapping' two views within same grid location
                    // if a group has one tab - we are essentially moving the 'group'
                    // which is equivalent to swapping two views in this case
                    this.gridview.moveView(sourceParentLocation, from, to);
                    return;
                }
            }
            // source group will become empty so delete the group
            const targetGroup = this.movingLock(() => this.doRemoveGroup(sourceGroup, {
                skipActive: true,
                skipDispose: true,
            }));
            // after deleting the group we need to re-evaulate the ref location
            const updatedReferenceLocation = getGridLocation(destinationGroup.element);
            const location = getRelativeLocation(this.gridview.orientation, updatedReferenceLocation, destinationTarget);
            this.movingLock(() => this.doAddGroup(targetGroup, location));
            this.doSetGroupAndPanelActive(targetGroup);
        }
        else {
            /**
             * The group we are removing from has many panels, we need to remove the panels we are moving,
             * create a new group, add the panels to that new group and add the new group in an appropiate position
             */
            const removedPanel = this.movingLock(() => sourceGroup.model.removePanel(sourceItemId, {
                skipSetActive: false,
                skipSetActiveGroup: true,
            }));
            if (!removedPanel) {
                throw new Error(`No panel with id ${sourceItemId}`);
            }
            const dropLocation = getRelativeLocation(this.gridview.orientation, referenceLocation, destinationTarget);
            const group = this.createGroupAtLocation(dropLocation);
            this.movingLock(() => group.model.openPanel(removedPanel, {
                skipSetGroupActive: true,
            }));
            this.doSetGroupAndPanelActive(group);
        }
    }
}

const tail = arr => {
    if (arr.length === 0) {
        throw new Error('Invalid tail call');
    }
    return [arr.slice(0, arr.length - 1), arr[arr.length - 1]];
}

const sequenceEquals = (arr1, arr2) => {
    if (arr1.length !== arr2.length) {
        return false;
    }
    for (let i = 0; i < arr1.length; i++) {
        if (arr1[i] !== arr2[i]) {
            return false;
        }
    }
    return true;
}
