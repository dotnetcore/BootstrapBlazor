import { DockviewComponent, DefaultTab} from "../js/dockview-core.esm.js"
import '../js/dockview-extensions.js'

export class DefaultPanel {
    _element;
    get element() {
        return this._element;
    }
    constructor(option, { template }) {
        this._element = document.createElement('div');
        // this._element.style.color = "white";
        this._element.style.width = '100%'
        this._element.style.height = '100%'
        this.template = template
        this.option = option
    }

    init(parameter) {
        let { params, api } = parameter
        let { panel, group } = api
        let { tab, content } = panel.view
        let contentEle = this.template?.querySelector('#' + this.option.id) || '暂无数据...'
        if (typeof contentEle != 'string') {
            let titleMenuEle = contentEle.querySelector(`[data-bb-component-id=${this.option.id}]`)
            panel.titleMenuEle = titleMenuEle && contentEle.removeChild(titleMenuEle)
            contentEle.style.width = '100%'
            contentEle.style.height = '100%'
        }
        this.element.append(contentEle)
        const {titleClass, titleWidth, contentClass, class: groupClass} = params
        titleClass && tab._content.classList.add(titleClass)
        titleWidth && (tab._content.style.width = titleWidth + 'px')
        contentClass && content.element.classList.add(contentClass)
        groupClass && group.element.classList.add(groupClass)
    }
}

export class myDefaultTab extends DefaultTab {
    constructor(option, options) {
        super();
    }
}

class PanelControl {
    constructor(dockviewPanel) {
        const { view, api } = dockviewPanel
        // Panel的Header
        this.tabEle = view.tab.element
        // Panel的Body
        this.contentEle = view.content.element
        this.panel = dockviewPanel
        this.createGear(api)
    }
    // 添加小齿轮
    createGear(api) {
        const divEle = document.createElement('div')
        // divEle.style.display = api.isVisible ? 'block' : 'none'
        // divEle.innerHTML = '<i class="fa-solid fa-fw fa-cog">'
        divEle.append(this.panel.titleMenuEle)
        // this.panel.titleMenuEle = null
        // divEle.addEventListener('click', () => {
        //     alert(api.id)
        // })
        divEle.addEventListener('mousedown', e => {
            e.stopPropagation()
        })
        this.tabEle.insertAdjacentElement("afterbegin", divEle)
        // api.onDidVisibilityChange(({ isVisible }) => {
        //     divEle.style.display = isVisible ? 'block' : 'none'
        // })
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

    creatPrefixControl(option) {
        // 添加header前控制按钮
        // ...
    }
    creatTabsAfterControl(option) {
        // 添加tabs后的控制按钮
        // ...
    }
    // 添加右侧控制按钮
    creatRightControl(option) {
        let divEle = document.createElement('div')
        // showClose, showFloat, showLock, showMaximize
        console.log(this.group, 'this.group');
        let {panels, api} = this.group
        let filterOption = option.filter(item => {
            switch(item.name){
                case 'lock': return panels.some(panel => panel.params.showLock)
                case 'packup/expand': return true
                case 'float': return panels.every(panel => panel.params.showFloat)
                case 'maximize': return panels.some(panel => panel.params.showMaximize)
                case 'close': return panels.every(panel => panel.params.showClose)
            }
        })
        console.log(filterOption, 'filterOptionfilterOptionfilterOption');
        filterOption.forEach(item => {
            if (api.location.type == 'grid' && item.name == 'packup/expand') return
            let btn = this._createButton(item)
            divEle.append(btn)
        })
        divEle.style.cssText = `display: flex;align-items: center;padding: 0px 8px;height: 100%;`
        this.headerEle.querySelector('.right-actions-container').append(divEle)
    }

    _createButton(item) {
        const divEle = document.createElement('div')
        divEle.title = item.name
        divEle.className = item.name
        divEle.innerHTML = item.icon[0]
        divEle.style.cssText = 'width: 10px; height: 10px; padding: 2px; margin-left: 6px; cursor: pointer; line-height: .8;'

        if (item.name == 'lock') {
            this.group.locked = this.options.lock ? true : this._getGroupParams('isLock') ? true : false
            divEle.innerHTML = item.icon[this.group.locked ? 1 : 0]
            divEle.title = this.group.locked ? 'unlock' : 'lock'
        }
        else if (item.name == 'packup/expand') {
            let isPackup = this._getGroupParams('isPackup')
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
        this.toggleLock(divEle, item)
        this.dockview._lockChanged?.fire(this.group.locked !== false)
    }
    toggleLock(divEle, item) {
        divEle = divEle || this.group.header.rightActionsContainer.querySelector('.lock')
        item = item || this.options.rightControl.find(option => option.name == 'lock')
        if(!divEle) return
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
            group.groupControl = new GroupControl(group, this.options, this.dockview)
        }
        else if (type == 'floating') {
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
        let type = this.group.model.location.type
        let isMaximized = type == 'grid' ? this.api.isMaximized() : type == 'floating' ? this._getGroupParams('isMaximized') : false
        if (isMaximized) {
            type == 'grid' ? this.group.api.exitMaximized() : type == 'floating' ? this._floatingExitMaximized() : ''
            divEle.innerHTML = item.icon[0]
            divEle.title = 'maximize'
        } else {
            type == 'grid' ? this.group.api.maximize() : type == 'floating' ? this._floatingMaximize() : ''
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
        let parentEle = this.group.element.parentElement
        let position = this._getGroupParams('position')
        Object.keys(position).forEach(key => parentEle.style[key] = position[key] + 'px')
        this._setGroupParams({ isMaximized: false })
    }
}

export function cerateDockview(el, options) {
    console.log(options, 'options77777');
    const { templateId } = options
    const template = document.getElementById(templateId)
    const dockview =  new DockviewComponent({
        parentElement: el,
        createComponent: option => {
            return new DefaultPanel(option, { template })
        },
        createTabComponent: option => new myDefaultTab(option, options)
    });

    dockview.prefix = options.prefix
    dockview.locked = options.lock
    dockview.update = updateOptions => {
        console.log('update', updateOptions);
        if (dockview.locked !== updateOptions.lock) {
            // 处理 Lock 逻辑
            dockview.locked = updateOptions.lock
            lockDock(dockview)
        }
        else {
            // 处理 toggle 逻辑
            toggleComponent(dockview, updateOptions)
        }
    }
    dockview.reset = (resetOptions) => {
        console.log('reset', resetOptions);
        dockview.isResetIng = true
        dockview.clear()
        setTimeout(() => {
            dockview.isResetIng && (delete dockview.isResetIng)
        }, 0);
        loadDockview(dockview, getJson(dockview, serverData, true))
    }
    dockview.dispose = () => {
        console.log('dispose:', dockview);
    }

    // 序列化options数据为dockview可用数据(layoutConfig优先)
    let serverData = options.layoutConfig || serialize(options)
    console.log(serverData, 'serverData');
    // 以本地优先, 得到最终的dockviewData并修正
    let dockviewData = getJson(dockview, serverData)
    // 绑定钩子函数
    addHook(dockview, dockviewData, options, template)
    // 渲染dockview结构
    loadDockview(dockview, dockviewData, serverData)
    return dockview
}

export function toggleComponent(dock, option) {
    let panels = getPanels(option.content)
    let localPanels = dock.panels || {}
    panels.forEach(panel => {
        let pan = localPanels.find(item => item.id == panel.id)
        if (pan === void 0) {//需要添加
            addDelPanel(panel, [], option, dock)
        }
    })

    localPanels.forEach(item => {
        let pan = panels.find(panel => panel.id == item.id)
        if (pan === void 0) {//需要删除
            dock.removePanel(item)
        }
    })
}
export function lockDock(dock) {
    dock.groups.forEach(group => {
        group.locked = dock.locked
        group.groupControl.toggleLock()
    })
}
export function getTheme(element) {
    function toClassList(element) {
        const list = [];
        for (let i = 0; i < element.classList.length; i++) {
            list.push(element.classList.item(i));
        }
        return list;
    }
    let theme = undefined;
    let parent = element;
    while (parent !== null) {
        theme = toClassList(parent).find((cls) => cls.startsWith('dockview-theme-'));
        if (typeof theme === 'string') {
            break;
        }
        parent = parent.parentElement;
    }
    return theme;
}
export function setTheme(ele, theme, newTheme) {
    ele.classList.remove(theme)
    ele.classList.add(newTheme)
}

export function addHook(dockview, dockviewData, options, template) {
    // 钩子1： 删除panel触发
    dockview.onDidRemovePanel(event => {
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
        console.log(event, '90909090');
        let contentEle = event.view.content.element.children[0]
        if(event.titleMenuEle){
            contentEle.append(event.titleMenuEle)
        }
        console.log(event, '90909090222222');
        template.append(event.view.content.element.children[0])

        // 放在onDidLayoutChange里保存
        // saveConfig(dockview)
    })
    // 钩子2：添加Panel触发
    dockview.onDidAddPanel(event => {
        if (event.titleMenuEle) {
            new PanelControl(event)
        }
        if (!event.group.children) {
            event.group.children = {}
        }
        event.group.children[event.id] = event.id
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
        if(0){
            event.header.hidden = true
        }
        // 修正floating Group的位置
        let { floatingGroups = [], panels } = dockviewData
        let floatingGroup = floatingGroups.find(item => item.data.id == event.id)
        if (floatingGroup) {
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
                event.groupControl = new GroupControl(event, options, dockview)
            }, 0);
        }

        // console.log(event, event.id, 'onDidAddGroup');
        event.model.onGroupDragStart(function (e) {
            // console.log(e, 'onGroupDragStartonGroupDragStart');
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
    let abc = 0
    let eve = dockview.onDidLayoutChange(event => {
        setTimeout(() => {
            // 维护Group的children属性
            if (dockview.totalPanels >= (dockview.panelSize || 0)) {
                dockview.groups.forEach(group => {
                    group.children = {}
                    group.panels.forEach(panel => {
                        group.children[panel.id] = panel.id
                    })
                })
            }
            dockview.panelSize = dockview.totalPanels
            saveConfig(dockview)
        }, 50)
    })

    // 钩子9: layout加载完成触发
    dockview.onDidLayoutFromJSON(event => {
        setTimeout(() => {
            dockview._initialized?.fire()
        }, 0)
        dockview.groups.forEach(group => {
            if (group.panels.length == 0) {
                dockview.setVisible(group, false)
            }
        })
    })
}

let panels = {}
let groupId = 0
const getGroupId = () => {
    return groupId++
}
export function serialize(options) {
    groupId = 0
    const orientation = options.content[0].type == 'row' ? 'HORIZONTAL' : 'VERTICAL'
    const {width = 100, height = 80} = options
    return options.content ? {
        activeGroup: '1',
        grid: {
            width,
            height,
            orientation,
            root: {
                type: 'branch',
                data: [getTree(options.content[0], {width, height, orientation})]
            },
        },
        panels
    } : null
}
export function addDelPanel(panel, delPanels, options, dockview) {
    // 手动添加已删除的panel
    let group = panel.groupId ? dockview.api.getGroup(panel.groupId) : (
        dockview.groups.find(group => group.children?.[panel.id]) || dockview.groups[0]
    )

    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    if (!group) {
        group = dockview.createGroup({ id: panel.groupId })
        let floatingGroupPosition = isMaximized ? {
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

        if (true) {
            setTimeout(() => {
                // group.setParams({isPackup, height, isMaximized, position})
                group.groupControl = new GroupControl(group, options, dockview)
            }, 50);
        }

    } else {
        if (group.api.location.type == 'grid') {
            let isVisible = dockview.isVisible(group)
            if (isVisible === false) {
                dockview.setVisible(group, true)
                // 修正Group的宽高(待完善...)
                // let lastDelPanel = delPanels.findLast(delPanel => delPanel.groupId == panel.groupId)
                // let {width, height} = lastDelPanel.params.currentPosition
                // console.log(width, height, 'group: width, height');
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
export function loadDockview(dockview, dockviewData, serverData) {
    try {
        dockview.fromJSON(dockviewData)
    } catch (error) {
        setTimeout(() => {
            localStorage.removeItem('dock-view-panels');
            localStorage.removeItem(dockview.prefix);
            dockview.fromJSON(serverData)
        }, 0)
        throw new Error('load error message: ', error)

    } finally {

    }
}
export function getJson(dockview, data, isReset) {
    // 修正JSON
    if(isReset !== true){
        let localData = localStorage.getItem(dockview.prefix)
        localData = localData && JSON.parse(localData)
        data = data || localData || data
    }
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
const correctBranch = branch => {
    // 修改branch
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
export function getLocal(key) {
    return JSON.parse(localStorage.getItem(key))
}
const setSumLocal = (key, data) => {
    // 在指定Key累加数组数据
    // 确定是占位的panel就不用管
    // if(key == 'dock-view-panels' && data.id.includes('_占位')) return

    let localData = getLocal(key) || []
    if (localData.find(item => item.id == data.id && item.groupId == data.groupId)) return
    localData.push(data)
    localStorage.setItem(key, JSON.stringify(localData))
}
const setDecreaseLocal = (key, id) => {
    // 在指定Key删除对应id的数组元素
    let localData = getLocal(key) || []
    localData = localData.filter(item => item.id != id)
    if (localData.length == 0) {
        return localStorage.removeItem(key)
    }
    localStorage.setItem(key, JSON.stringify(localData))
}

const saveConfig = (dockview, config) => {
    // 操作(拖拽、删除...)dockview后需要调用saveConfig保存数据
    if (dockview.hasMaximizedGroup()) return
    let json = dockview.toJSON()
    localStorage.setItem(
        dockview.prefix,
        (config && JSON.stringify(config)) || JSON.stringify(json)
    )
}
const getTree = (contentItem, {width, height, orientation}, length = 1) => {
    let obj = {}, size = orientation == 'HORIZONTAL' ? width : height
    size = (1/length*size).toFixed(2) * 1
    orientation == 'HORIZONTAL' ? width = size : height = size
    orientation =  orientation == 'HORIZONTAL' ? 'VERTICAL' : 'HORIZONTAL'

    if (contentItem.type == 'row' || contentItem.type == 'column') {
        obj.type = 'branch'
        obj.size = size
        obj.data = contentItem.content.map(item => getTree(item, {width, height, orientation}, contentItem.content.length))
    }
    else if (contentItem.type == 'group') {
        obj.type = 'leaf'
        obj.size = size
        obj.visible = contentItem.content.some(item => item.visible !== false)
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.content[0].id,
            views: contentItem.content.filter(item => item.visible !== false).map(item => {
                panels[item.id] = {
                    id: item.id,
                    title: item.title,
                    tabComponent: item.componentName,
                    contentComponent: item.componentName,
                    params: item
                }
                return item.id
            })
        }
    }
    else if (contentItem.type = 'component') {
        obj.type = 'leaf'
        obj.visible = contentItem.visible !== false
        obj.size = size
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.id,
            views: obj.visible ? [contentItem.id] : []
        }
        if (obj.visible) {
            panels[contentItem.id] = {
                id: contentItem.id,
                title: contentItem.title,
                tabComponent: contentItem.componentName,
                contentComponent: contentItem.componentName,
                params: contentItem
            }
        }
    }
    return obj
}
const generateRandomId = length => {
    // 随机生成指定长度的字符串
    const possible = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let id = '';
    for (let i = 0; i < length; i++) {
        id += possible.charAt(Math.floor(Math.random() * possible.length));
    }
    return id;
}

const getPanels = content => {
    return getPanel(content[0])
}
const getPanel = (contentItem, panels = []) => {
    if (contentItem.type == 'component') {
        panels.push({
            id: contentItem.id,
            groupId: contentItem.groupId,
            title: contentItem.title,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
            params: contentItem
        })
    } else {
        contentItem.content?.forEach(item => getPanel(item, panels))
    }
    return panels
}


