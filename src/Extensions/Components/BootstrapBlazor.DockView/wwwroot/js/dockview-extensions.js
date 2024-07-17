import { DockviewComponent, DockviewGroupPanel, getGridLocation, getRelativeLocation, DockviewEmitter } from "./dockview-core.esm.js"
import { getConfigFromStorage, saveConfig } from "./dockview-config.js"
import { disposeGroup } from "./dockview-group.js"

DockviewComponent.prototype.on = function (eventType, callback) {
    this['_' + eventType] = new DockviewEmitter();
    this['_' + eventType].event(callback)
}

const dispose = DockviewComponent.prototype.dispose;
DockviewComponent.prototype.dispose = function () {
    this.params.observer?.disconnect();
    saveConfig(this);
    dispose.call(this);
}

const groupDispose = DockviewGroupPanel.prototype.dispose
DockviewGroupPanel.prototype.dispose = function () {
    disposeGroup(this)
    groupDispose.call(this)
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

        // let delPanelsStr = localStorage.getItem(this.params.options.localStorageKey + '-panels')
        // let delPanels = delPanelsStr ? JSON.parse(delPanelsStr) : delPanelsStr
        // delPanels = delPanels?.map(panel => {
        //     if (panel.groupId == group.id) {
        //         panel.groupInvisible = true
        //     }
        //     return panel
        // })
        // delPanels && localStorage.setItem(this.params.options.localStorageKey + '-panels', JSON.stringify(delPanels))
    }
    else if (type == 'floating') {
        return removeGroup.apply(this, argu)
    }
}

const removePanel = DockviewComponent.prototype.removePanel
DockviewComponent.prototype.removePanel = function (...args) {
    const panel = args[0]
    if (!panel.group.locked) {
        removePanel.apply(this, args)
        if (!this.isClearing) {
            this._panelVisibleChanged?.fire({ title: panel.title, status: false });
        }
    }
}
