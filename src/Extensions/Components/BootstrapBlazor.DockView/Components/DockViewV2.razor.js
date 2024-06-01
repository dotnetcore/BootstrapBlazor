import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import Data from '../../BootstrapBlazor/modules/data.js'
import {
    DockviewComponent,
    DefaultTab,
    DockviewGroupPanel,
    getGridLocation,
    // indexInParent,
    getRelativeLocation,
    // getLocationOrientation,
    // getDirectionOrientation
} from "../js/dockview-core.esm.js"

// 给Group添加获取panel.params属性的方法
DockviewGroupPanel.prototype.getParams = function () {
    return this.activePanel?.params || {}
}
// 给Group添加设置panel.params的方法
DockviewGroupPanel.prototype.setParams = function (data) {
    Object.keys(data).forEach(key => {
        this.panels.forEach(panel => panel.params[key] = data[key])
    })
}
// 给Group添加删除panel.params属性的方法
DockviewGroupPanel.prototype.removePropsOfParams = function (keys) {
    return (keys instanceof Array)
        ? keys.map(key => this.panels.forEach(panel => delete panel.params[key]))
        : typeof keys == 'string' ? delete panel.params[keys] : false
}

// 修改removeGroup
const removeGroup = DockviewComponent.prototype.removeGroup
DockviewComponent.prototype.removeGroup = function (...argu) {
    const group = argu[0]
    console.log(group, 'removeGroup: group999999999999');
    const type = group.api.location.type;
    if (type == 'grid') {
        [...group.panels].forEach(panel => {
            panel.api.close()
        })
        this.setVisible(group, false)

        // 在本地存储的已删除的panel上保存Group是否可见, 因为toJson()不保存此信息, 会默认展示隐藏的Group
        let delPanels = getLocal('dock-view-panels')
        delPanels = delPanels?.map(panel => {
            if (panel.groupId == group.id) {
                panel.groupInvisible = true
            }
            return panel
        })
        delPanels && localStorage.setItem('dock-view-panels', JSON.stringify(delPanels))
    }
    else if (type == 'floating') {
        removeGroup.apply(this, argu)
    }
}
// 修改removePanel
const removePanel = DockviewComponent.prototype.removePanel
DockviewComponent.prototype.removePanel = function (...argu) {
    const panel = argu[0]
    console.log(panel, 'panel');
    if (!panel.group.locked) {
        removePanel.apply(this, argu)
    }
}
// 修改moveGroupOrPanel
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
// moveGroupOrPanel源码提供1
function tail(arr) {
    if (arr.length === 0) {
        throw new Error('Invalid tail call');
    }
    return [arr.slice(0, arr.length - 1), arr[arr.length - 1]];
}
// moveGroupOrPanel源码提供2
function sequenceEquals(arr1, arr2) {
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

class DefaultPanel {

    _element;

    get element() {
        return this._element;
    }

    constructor(option, { template }) {
        this._element = document.createElement('div');
        this._element.style.color = "white";
        this.template = template
        this.option = option
    }

    init(params) {
        // console.log(this.option, this.template, 'option, template');
        this.element.append(this.template.children[0] || '999')
    }
}

class myDefaultTab extends DefaultTab {
    constructor(option, options) {
        super();
    }
}

class PanelControl {
    constructor(dockviewPanel, options) {
        const { view, api } = dockviewPanel
        // Panel的Header
        this.tabEle = view.tab.element
        // Panel的Body
        this.contentEle = view.content.element

        options.gear && this.createGear(api)
    }
    // 添加小齿轮
    createGear(api) {
        const divEle = document.createElement('div')
        divEle.style.display = api.isVisible ? 'block' : 'none'
        divEle.innerHTML = '<i class="fa-solid fa-fw fa-cog">'
        divEle.addEventListener('click', () => {
            alert(api.id)
        })
        this.tabEle.insertAdjacentElement("afterbegin", divEle)
        api.onDidVisibilityChange(({ isVisible }) => {
            divEle.style.display = isVisible ? 'block' : 'none'
        })
    }
}
class GroupControl {
    constructor(dockviewGroupPanel, options, dockview) {
        const { element, header, api } = dockviewGroupPanel
        const { prefixControl, tabsAfterControl, rightControl } = options
        // Group
        this.ele = element
        // Group的Header
        this.headerEle = header.element
        this.api = api
        this.group = dockviewGroupPanel
        this.parentEle = dockviewGroupPanel.element.parentElement
        this.dockview = dockview
        this.options = options

        prefixControl && this.creatPrefixControl(prefixControl)
        tabsAfterControl && this.tabsAfterControl(tabsAfterControl)
        rightControl && this.creatRightControl(rightControl)
    }

    // 添加header前控制按钮
    creatPrefixControl(option) {
        // ...
    }
    // 添加tabs后的控制按钮
    creatTabsAfterControl(option) {
        // ...
    }
    // 添加右侧控制按钮
    creatRightControl(option) {
        let divEle = document.createElement('div')

        option.forEach(item => {
            if (this.group.api.location.type == 'grid' && item.name == 'packup/expand') return
            let btn = this._createButton(item)
            divEle.append(btn)
        })
        divEle.style.cssText = `display: flex;align-items: center;padding: 0px 8px;height: 100%;`
        this.headerEle.querySelector('.right-actions-container').append(divEle)
    }

    _createButton(item) {
        const divEle = document.createElement('div')
        divEle.title = item.name
        divEle.innerHTML = item.icon[0]
        divEle.style.cssText = 'width: 10px; height: 10px; padding: 2px; margin-left: 6px; color: white; cursor: pointer; line-height: .8;'

        if (item.name == 'lock') {
            divEle.innerHTML = item.icon[this.group.locked ? 1 : 0]
            divEle.title = this.group.locked ? 'unlock' : 'lock'
        }
        else if (item.name == 'packup/expand') {
            let isPackup = this._getGroupParams('isPackup')
            console.log(isPackup, 'isPackup');
            if (isPackup) {
                divEle.innerHTML = item.icon[1]
            }
        }
        else if (item.name == 'float') {
            // ...
        }
        else if (item.name == 'maximize') {
            let isMaximized = this._getGroupParams('isMaximized')
            if (isMaximized) {
                divEle.innerHTML = item.icon[1]
            }
        }

        divEle.addEventListener('click', () => {
            this['_' + item.name] && this['_' + item.name](divEle, item)
        })
        return divEle
    }
    _lock(divEle, item) {
        this.group.locked = this.group.locked ? false : 'no-drop-target'
        divEle.innerHTML = item.icon[this.group.locked ? 1 : 0]
        divEle.title = this.group.locked ? 'unlock' : 'lock'
        saveConfig(this.dockview)
    }
    '_packup/expand'(divEle, item) {
        let isPackup = this._getGroupParams('isPackup')
        let parentEle = this.group.element.parentElement
        if (isPackup) {
            this._setGroupParams({ 'isPackup': false })
            parentEle.style.height = this._getGroupParams('height') + 'px'
        } else {
            this._setGroupParams({ 'isPackup': true, 'height': parseFloat(parentEle.style.height) })
            parentEle.style.height = '35px'
        }
        divEle.innerHTML = item.icon[isPackup ? 0 : 1]
        saveConfig(this.dockview)
    }
    _float(divEle, item) {
        if (this.group.locked) return
        let type = this.group.model.location.type
        if (type == 'grid') {
            let { position = {}, isPackup, height, isMaximized } = this.group.getParams()
            let floatingGroupPosition = isMaximized ? {
                x: 0, y: 0,
                width: this.dockview.width,
                height: this.dockview.height
            } : {
                x: position.left || 0,
                y: position.top || 0,
                width: position.width || this.group.width,
                height: position.height || this.group.height
            }

            let group = this.dockview.createGroup({ id: this.group.id + '_floating' })
            this.dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })

            this.group.panels.slice(0).forEach((panel, index) => {
                this.dockview.moveGroupOrPanel({
                    from: { groupId: this.group.id, panelId: panel.id },
                    to: { group, position: 'center', index },
                    skipRemoveGroup: true
                })
            })
            this.dockview.setVisible(this.group, false)
            group.setParams({ isPackup, height, isMaximized })
            new GroupControl(group, this.options, this.dockview)
        }
        else if (type == 'floating') {
            console.log(this.group);
            let originGroup = this.dockview.groups.find(group => group.id + '_floating' == this.group.id)
            this.dockview.setVisible(originGroup, true)

            let { isPackup, height, isMaximized, position } = this.group.getParams()
            if (!isMaximized) {
                position = {
                    width: this.group.width,
                    height: this.group.height,
                    top: parseFloat(this.group.element.parentElement.style.top || 0),
                    left: parseFloat(this.group.element.parentElement.style.left || 0)
                }
            }
            console.log(position, 'position');
            this.dockview.moveGroup({
                from: { group: this.group },
                to: { group: originGroup, position: 'center' }
            })

            // 把floating移回到grid, floating会被删除,在这之前需要保存悬浮框的position, 此处保存在了原Group的panel的params上
            originGroup.setParams({ position, isPackup, height, isMaximized })
        }
        saveConfig(this.dockview)
    }
    _maximize(divEle, item) {
        console.log(this, 'this');
        let type = this.group.model.location.type
        let isMaximized = type == 'grid' ? this.api.isMaximized() : type == 'floating' ? this._getGroupParams('isMaximized') : false
        console.log(isMaximized, 'isMaximized');
        if (isMaximized) {
            type == 'grid' ? this.api.exitMaximized() : type == 'floating' ? this._floatingExitMaximized() : ''
            divEle.innerHTML = item.icon[0]
            divEle.title = 'maximize'
        } else {
            type == 'grid' ? this.api.maximize() : type == 'floating' ? this._floatingMaximize() : ''
            divEle.innerHTML = item.icon[1]
            divEle.title = 'exitMaximize'
        }
        // saveConfig(this.dockview)
    }
    _close() {
        if (!this.group.locked) {
            this.api.close()
        }
    }

    _getGroupParams(key) {
        return key && this.group.activePanel?.params[key]
    }
    _setGroupParams(data) {
        Object.keys(data).forEach(key => {
            this.group.panels.forEach(panel => panel.params[key] = data[key])
        })
    }
    _removeGroupParams(keys) {
        return (keys instanceof Array) ? keys.map(key => this.group.panels.forEach(panel => delete panel.params[key])) : false
    }
    _floatingMaximize() {
        console.log('floating: _maximize');
        let parentEle = this.group.element.parentElement
        let { width, height } = parentEle.style
        let { width: maxWidth, height: maxHeight } = this.dockview
        let { top, left } = parentEle.style
        parentEle.style.left = 0
        parentEle.style.top = 0
        parentEle.style.width = maxWidth + 'px'
        parentEle.style.height = maxHeight + 'px'
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
    _floatingExitMaximized() {
        console.log('floating: _exitMaximized');
        let parentEle = this.group.element.parentElement
        let position = this._getGroupParams('position')
        Object.keys(position).forEach(key => parentEle.style[key] = position[key] + 'px')
        this._setGroupParams({ isMaximized: false })
    }
}

let panels = {}
let groupId = 0
function getGroupId() {
    return groupId++
}
function serialize(options) {
    return {
        activeGroup: '1',
        grid: {
            root: {
                type: 'branch',
                data: [getTree(options.content[0])]
            },
            orientation: options.content[0].type == 'column' ? 'HORIZONTAL' : 'VERTICAL',
            width: 1000,
            height: 800
        },
        panels
    }
}
function getTree(contentItem) {
    let obj = {}
    if (contentItem.type == 'row' || contentItem.type == 'column') {
        obj.type = 'branch'
        obj.data = contentItem.content.map(item => getTree(item))
    }
    else if (contentItem.type == 'group') {
        obj.type = 'leaf'
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.content[0].id,
            views: contentItem.content.map(item => {
                panels[item.id] = {
                    id: item.id,
                    title: item.title,
                    tabComponent: item.componentName,
                    contentComponent: item.componentName,
                }
                return item.id
            })
        }
    }
    else if (contentItem.type = 'component') {
        obj.type = 'leaf'
        obj.size = 300
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.id,
            views: [contentItem.id]
        }
        panels[contentItem.id] = {
            id: contentItem.id,
            title: contentItem.title,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
        }
    }
    return obj
}

let resetHandle;
export async function init(id, invoker, options) {
    await addLink("./_content/BootstrapBlazor.DockView/css/dockview.css")
    console.log(id, 'id', options, 'options');
    options = {
        ...options, ...{
            gear: {
                show: true
            },
            rightControl: [
                {
                    name: 'lock',
                    icon: ['<i class="fas fa-unlock"></i>', '<i class="fas fa-lock"></i>']
                },
                {
                    name: 'packup/expand',
                    icon: ['<i class="fas fa-chevron-circle-up"></i>', '<i class="fas fa-chevron-circle-down"></i>']
                },
                {
                    name: 'float',
                    icon: ['<i class="far fa-window-restore"></i>']
                },
                {
                    name: 'maximize',
                    icon: ['<i class="fas fa-expand"></i>', '<i class="fas fa-compress"></i>']
                },
                {
                    name: 'close',
                    icon: ['<i class="fas fa-times"></i>']
                }
            ],
        }
    }

    options.data = {
        "grid": {
            "root": {
                "type": "branch",
                "data": [
                    {
                        "type": "branch",
                        "data": [
                            {
                                "type": "branch",
                                "data": [
                                    {
                                        "type": "leaf",
                                        "data": {
                                            "views": [
                                                "panel_dv_BJ9g57Pf"
                                            ],
                                            "activeView": "panel_dv_BJ9g57Pf",
                                            "id": "1"
                                        },
                                        "size": 368
                                    },
                                    {
                                        "type": "leaf",
                                        "data": {
                                            "views": [
                                                "panel_dv_6z6Uo1tV",
                                                "panel_dv_RhU0K6ux"
                                            ],
                                            "activeView": "panel_dv_RhU0K6ux",
                                            "id": "3"
                                        },
                                        "size": 632
                                    }
                                ],
                                "size": 400
                            },
                            {
                                "type": "branch",
                                "data": [
                                    {
                                        "type": "leaf",
                                        "data": {
                                            "views": [
                                                "panel_dv_Da4dpuc4"
                                            ],
                                            "activeView": "panel_dv_Da4dpuc4",
                                            "id": "2"
                                        },
                                        "size": 500
                                    },
                                    {
                                        "type": "branch",
                                        "data": [
                                            {
                                                "type": "leaf",
                                                "data": {
                                                    "views": [
                                                        "panel_dv_OE12l95I",
                                                        "panel_dv_p6VoRsni"
                                                    ],
                                                    "activeView": "panel_dv_p6VoRsni",
                                                    "id": "5"
                                                },
                                                "size": 200
                                            },
                                            {
                                                "type": "branch",
                                                "data": [
                                                    {
                                                        "type": "leaf",
                                                        "data": {
                                                            "views": [
                                                                "panel_dv_uGAjrJYu"
                                                            ],
                                                            "activeView": "panel_dv_uGAjrJYu",
                                                            "id": "6"
                                                        },
                                                        "size": 250
                                                    },
                                                    {
                                                        "type": "leaf",
                                                        "data": {
                                                            "views": [
                                                                "panel_dv_RV4axdB9"
                                                            ],
                                                            "activeView": "panel_dv_RV4axdB9",
                                                            "id": "7"
                                                        },
                                                        "size": 250
                                                    }
                                                ],
                                                "size": 200
                                            }
                                        ],
                                        "size": 500
                                    }
                                ],
                                "size": 400
                            }
                        ],
                        "size": 1000
                    }
                ],
                "size": 800
            },
            "width": 1000,
            "height": 800,
            "orientation": "HORIZONTAL"
        },
        "panels": {
            "panel_dv_BJ9g57Pf": {
                "id": "panel_dv_BJ9g57Pf",
                "tabComponent": "d1",
                "contentComponent": "d1",
                "title": "Panel dv_BJ9g57Pf"
            },
            "panel_dv_6z6Uo1tV": {
                "id": "panel_dv_6z6Uo1tV",
                "tabComponent": "d2",
                "contentComponent": "d2",
                "title": "Panel dv_6z6Uo1tV"
            },
            "panel_dv_RhU0K6ux": {
                "id": "panel_dv_RhU0K6ux",
                "tabComponent": "d3",
                "contentComponent": "d3",
                "title": "Panel dv_RhU0K6ux"
            },
            "panel_dv_Da4dpuc4": {
                "id": "panel_dv_Da4dpuc4",
                "tabComponent": "d4",
                "contentComponent": "d4",
                "title": "Panel dv_Da4dpuc4"
            },
            "panel_dv_OE12l95I": {
                "id": "panel_dv_OE12l95I",
                "tabComponent": "d5",
                "contentComponent": "d5",
                "title": "Panel dv_OE12l95I"
            },
            "panel_dv_p6VoRsni": {
                "id": "panel_dv_p6VoRsni",
                "tabComponent": "d6",
                "contentComponent": "d6",
                "title": "Panel dv_p6VoRsni"
            },
            "panel_dv_uGAjrJYu": {
                "id": "panel_dv_uGAjrJYu",
                "tabComponent": "d7",
                "contentComponent": "d7",
                "title": "Panel dv_uGAjrJYu"
            },
            "panel_dv_RV4axdB9": {
                "id": "panel_dv_RV4axdB9",
                "tabComponent": "d8",
                "contentComponent": "d8",
                "title": "Panel dv_RV4axdB9"
            }
        },
        "activeGroup": "2"
    }
    options.TestData = {
        cType: 'column',
        children: [
            {
                cType: 'row',
                children: [
                    {
                        panels: [
                            {
                                id: 'panel_dv_BJ9g57Pf',
                                name: 'd1'
                            }
                        ]
                    },
                    {
                        panels: [
                            {
                                id: 'panel_dv_6z6Uo1tV',
                                name: 'd2'
                            },
                            {
                                id: 'panel_dv_RhU0K6ux","name',
                                name: 'd3'
                            }
                        ]
                    }
                ]
            },
            {
                cType: 'row',
                children: [
                    {
                        panels: [
                            {
                                id: 'panel_dv_Da4dpuc4',
                                name: 'd4'
                            }
                        ]
                    },
                    {
                        cType: 'column',
                        children: [
                            {
                                panels: [
                                    {
                                        id: 'panel_dv_OE12l95I',
                                        name: 'd5'
                                    },
                                    {
                                        id: 'panel_dv_p6VoRsni',
                                        name: 'd6'
                                    }
                                ]
                            },
                            {
                                cType: 'row',
                                children: [
                                    {
                                        panels: [
                                            {
                                                id: 'panel_dv_uGAjrJYu',
                                                name: 'd7'
                                            }
                                        ]
                                    },
                                    {
                                        panels: [
                                            {
                                                id: 'panel_dv_RV4axdB9',
                                                name: 'd8'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ]
            },
        ]
    }
    console.log(options.data, 'options.data');
    console.log(options.TestData, 'options.TestData');
    const { templateId } = options
    const el = document.getElementById(id);
    const template = document.getElementById(templateId)
    el.className = "dockview-theme-abyss test"
    el.style.width = '100%'
    el.style.height = '100%'
    if (el.parentElement.className == 'dock-toggle-demo') {
        el.parentElement.style.width = '100%'
        el.parentElement.style.height = '100%'
    }
    // 1、序列化options数据为dockview可用数据
    let serializedData = serialize(options)
    console.log(serializedData, 'serializedData');
    // 2、以本地优先,对dockview数据进行优化加工,
    let dockviewData = serializedData || getJson(options.data)

    const dockview = new DockviewComponent({
        parentElement: el,
        createComponent: option => {
            console.log(option, '000000000000000000');
            return new DefaultPanel(option, { template })
        },
        createTabComponent: option => new myDefaultTab(option, options)
    });

    //invoke.invokeMethodAsync(option.initializedCallback)

    // 钩子1： 删除panel触发
    dockview.onDidRemovePanel(event => {
        console.log(event, 'onDidRemovePanel');
        // 在panel上存储信息
        let obj = {
            id: event.id,
            title: event.title,
            component: event.view.contentComponent,
            groupId: event.group.id,
            params: {
                ...event.params,
                currentPosition: {
                    width: event.group.width,
                    height: event.group.height,
                    top: parseFloat(event.group.element.parentElement.style.top || 0),
                    left: parseFloat(event.group.element.parentElement.style.left || 0)
                }
            }
        }
        if (event.params.groupInvisible) {
            obj.groupInvisible = event.params.groupInvisible
        }
        setSumLocal('dock-view-panels', obj)
        template.append(event.view.content.element.children[0])

        // 放在onDidLayoutChange里保存
        // saveConfig(dockview)
    })
    // 钩子2：添加Panel触发
    dockview.onDidAddPanel(event => {
        // console.log('onDidAddPanel', event);
        if (true) {
            new PanelControl(event, options)
        }
        // saveConfig(dockview)
    })
    // 钩子3：添加Group触发
    dockview.onDidAddGroup(event => {
        // console.log('onDidAddGroup', event);
        // 给每个Group实例添加type和params属性
        Object.defineProperties(event, {
            type: {
                get() { return this.model.location.type }
            },
            params: {
                get() { return JSON.parse(JSON.stringify(event.activePanel?.params || {})) }
            }
        })
        // 修正floating Group的位置
        // console.log(options, 'options');
        let { floatingGroups = [], panels } = dockviewData
        let floatingGroup = floatingGroups.find(item => item.data.id == event.id)
        if (floatingGroup) {
            console.log(floatingGroup.position, 'floatingGroup.position');
            let { width, height, top, left } = floatingGroup.position
            setTimeout(() => {
                let style = event.element.parentElement.style
                style.width = width + 'px'
                style.height = height + 'px'
                style.top = top + 'px'
                style.left = left + 'px'
            }, 0)
        }

        if (true) {
            setTimeout(() => {
                new GroupControl(event, options, dockview)
            }, 0);
        }

        // console.log(event, event.id, 'onDidAddGroup');
        event.model.onGroupDragStart(function (e) {
            console.log(e, 'onGroupDragStartonGroupDragStart');
        })
    })
    dockview.onDidRemoveGroup(event => { })

    // 钩子4：拖拽panel标题前触发
    dockview.onWillDragPanel(event => {
        // console.log(event, 'onWillDragPanel');
        if (event.panel.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    // 钩子5：拖拽panel之后触发
    dockview._onDidMovePanel.event(event => {
        console.log('onDidMovePanel');
    })

    // 狗子6：拖拽Group之前触发
    dockview.onWillDragGroup(event => {
        // console.log(event, 'onWillDragGroup');
        if (event.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    // 钩子7：拖拽分割线后触发
    dockview.gridview.onDidChange(event => {
        console.log(event, 'onDidChange');
    })

    // 钩子8：所有造成layout变化的操作都会触发
    let eve = dockview.onDidLayoutChange(event => {
        console.log(event, 'onDidLayoutChange');
        setTimeout(() => {
            saveConfig(dockview)
        }, 50)
    })

    // 钩子9: layout加载完成触发
    dockview.onDidLayoutFromJSON(event => {
        dockview.groups.forEach(group => {
            if (group.panels.length == 0) {
                dockview.setVisible(group, false)
            }
        })
    })

    //#region
    //let ele = document.getElementById(id);
    //const dock = new dockview.DockviewComponent({
    //    components: {
    //        default: DefaultPanel,
    //    },
    //    parentElement: ele,
    //});


    //const panel1 = dock.addPanel({
    //    id: 'panel_1',
    //    title: 'Panel 1',
    //    component: 'default',
    //});

    //const panel2 = dock.addPanel({
    //    id: 'panel_2',
    //    title: 'Panel 2',
    //    component: 'default',
    //    position: {
    //        referencePanel: panel1,
    //        direction: 'right',
    //    },
    //});

    //const panel3 = dock.addPanel({
    //    id: 'panel_3',
    //    title: 'Panel 3',
    //    component: 'default',
    //    position: {
    //        referenceGroup: panel2.group,
    //    },
    //});

    //const pane4 = dock.addPanel({
    //    id: 'panel_4',
    //    title: 'Panel 4',
    //    component: 'default',
    //    position: {
    //        direction: 'below',
    //    },
    //});
    //#endregion

    loadDockview(dockviewData, options.data, dockview)
    resetHandle = function () {
        let delPanels = getLocal('dock-view-panels')
        if (!delPanels) return
        delPanels.forEach(panel => addDelPanel(panel, delPanels, options, dockview))
    }
}
//   document.querySelector('.groupbox>button:last-of-type').onclick = function(e){
//     reset()
//     e.stopPropagation()
//   }

//   export function reset(){
//     resetHandle()
//   }

// 手动添加已删除的panel
function addDelPanel(panel, delPanels, options, dockview) {
    let group = dockview.api.getGroup(panel.groupId)
    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    let floatingGroupPosition = isMaximized ? {
        x: 0, y: 0,
        width: dockview.width,
        height: dockview.height
    } : {
        x: currentPosition.left,
        y: currentPosition.top,
        width: currentPosition.width,
        height: currentPosition.height
    }
    if (!group) {
        group = dockview.createGroup({ id: panel.groupId })
        dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })

        if (true) {
            console.log(group);
            setTimeout(() => {
                // group.setParams({isPackup, height, isMaximized, position})
                new GroupControl(group, options, dockview)
            }, 0);
        }

    } else {
        if (group.api.location.type == 'grid') {
            let isVisible = dockview.isVisible(group)
            if (isVisible === false) {
                dockview.setVisible(group, true)
                // 修正Group的宽高(待完善...)
                let lastDelPanel = delPanels.findLast(delPanel => delPanel.groupId == panel.groupId)
                let { width, height } = lastDelPanel.params.currentPosition
                console.log(width, height, 'group: width, height');
                // group.layout(width, height)
            }
        }
    }
    dockview.addPanel({
        id: panel.id,
        title: panel.title,
        component: panel.component,
        position: { referenceGroup: group },
        params: { isPackup, height, isMaximized, position }
    });
    setDecreaseLocal('dock-view-panels', panel.id)
}
// 加载JSON
function loadDockview(data, serverData, dockview) {
    return dockview.fromJSON(data)
    try {
        console.log(data, 'data');
        dockview.fromJSON(data)
    } catch (error) {
        setTimeout(() => {
            localStorage.removeItem('dock-view-panels');
            localStorage.removeItem('dock-view-layout');
            dockview.fromJSON(serverData)
        }, 0)
        throw new Error('load error message: ', error)

    } finally {

    }
}
// 修正JSON
function getJson(data) {
    let localData = localStorage.getItem('dock-view-layout')
    localData = localData && JSON.parse(localData)
    console.log(JSON.parse(JSON.stringify(localData)));
    data = localData || data
    // 修改浮动框的宽高
    data.floatingGroups?.forEach(item => {
        let { width, height } = item.position
        item.position.width = width - 2
        item.position.height = height - 2
    })
    // 修改隐藏Group的父级size可能为0
    let { grid: { root }, orientation } = data
    correctBranch(root)
    return data
}
// 修改branch
function correctBranch(branch) {
    // if(branch.type == 'branch' && branch.size == 0){
    //   console.log(branch, 'branch');
    //   branch.size = 445
    // }
    if (branch.type == 'leaf') {
        if (branch.visible === false) {
            delete branch.visible
        }
        return
    }
    branch.data.forEach(item => {
        correctBranch(item)
    })
}

// 获取localStorage
function getLocal(key) {
    return JSON.parse(localStorage.getItem(key))
}
// 在指定Key累加数组数据
function setSumLocal(key, data) {
    // 确定是占位的panel就不用管
    // if(key == 'dock-view-panels' && data.id.includes('_占位')) return

    let localData = getLocal(key) || []
    if (localData.find(item => item.id == data.id && item.groupId == data.groupId)) return
    localData.push(data)
    localStorage.setItem(key, JSON.stringify(localData))
}
// 在指定Key删除对应id的数组元素
function setDecreaseLocal(key, id) {
    let localData = getLocal(key) || []
    localData = localData.filter(item => item.id != id)
    if (localData.length == 0) {
        return localStorage.removeItem(key)
    }
    localStorage.setItem(key, JSON.stringify(localData))
}

// 操作(拖拽、删除...)dockview后需要调用saveConfig保存数据
function saveConfig(dockview, config) {
    let json = dockview.toJSON()
    console.log(json, 'json');
    localStorage.setItem(
        'dock-view-layout',
        (config && JSON.stringify(config)) || JSON.stringify(json)
    )
}

// 生成随机长度的字符串
function generateRandomId(length) {
    const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let id = '';
    for (let i = 0; i < length; i++) {
        id += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return id;
}

export function update(id, options) {
    console.log(id, options);
}

export function dispose(id) {
    console.log(id);
}
