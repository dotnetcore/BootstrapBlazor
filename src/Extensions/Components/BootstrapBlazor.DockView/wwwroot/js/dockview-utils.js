import { DockviewComponent } from "../js/dockview-core.esm.js"
import { DockviewPanelContent } from "../js/dockview-content.js"
import { updateDockviewPanel } from "../js/dockview-panel-extensions.js"
import '../js/dockview-extensions.js'

class GroupControl {
    constructor(group) {
        this.group = group
        this.actionContainer = group.header.element.querySelector('.right-actions-container')
        if (group.header.hidden === false) {
            this._creatRightActions();
        }
    }

    _creatRightActions() {
        const actionContainer = this.actionContainer;
        const dockview = this.group.api.accessor;
        const group = this.group;
        dockview.groupControls.forEach(item => {
            if (item.name !== 'bar') {
                const icon = getActionIcon(dockview, item.name);
                icon.addEventListener('click', () => {
                    const handleName = '_' + item.name
                    this[handleName] && this[handleName](icon)
                })
                actionContainer.append(icon);
            }
        });
        if (showLock(dockview, group)) {
            actionContainer.classList.add('bb-show-lock');
            if (getGroupLockState(dockview, group)) {
                actionContainer.classList.add('bb-lock');
                this.toggleLock(true)
            }
        }
        if (showMaximize(dockview, group)) {
            actionContainer.classList.add('bb-show-maximize');
            if (getGroupMaximizeState(this.group, this._getGroupParams.bind(this))) {
                actionContainer.classList.add('bb-maximize');
                this.toggleFull(false)
            }
        }
        if (showFloat(dockview, group)) {
            actionContainer.classList.add('bb-show-float');
            if (getGroupFloatState(group)) {
                actionContainer.classList.add('bb-float');
            }
        }
    }

    toggleLock(isLock) {
        const dockview = this.group.api.accessor;
        this.group.locked = isLock ? 'no-drop-target' : isLock
        this.group.panels.forEach(panel => panel.params.isLock = isLock);
        isLock ? this.actionContainer.classList.add('bb-lock') : this.actionContainer.classList.remove('bb-lock')
        dockview._lockChanged.fire({ title: this.group.panels.map(panel => panel.title), isLock })
        saveConfig(dockview)
    }

    toggleFull(isMaximized) {
        const type = this.group.model.location.type
        if (type === 'grid') {
            isMaximized ? this.group.api.exitMaximized() : this.group.api.maximize()
        }
        else if (type === 'floating') {
            isMaximized ? this._floatingExitMaximized() : this._floatingMaximize()
        }
        isMaximized ? this.actionContainer.classList.remove('bb-maximize') : this.actionContainer.classList.add('bb-maximize')
        this.group.panels.forEach(panel => panel.params.isMaximized = !isMaximized)
    }

    _lock() {
        this.toggleLock(false)
    }

    _unlock() {
        this.toggleLock(true)
    }

    _down() {
        const dockview = this.group.api.accessor;
        const isPackup = this._getGroupParams('isPackup')
        const parentEle = this.group.element.parentElement
        if (isPackup) {
            this._setGroupParams({ 'isPackup': false })
            parentEle.style.height = `${this._getGroupParams('height')}px`;
            this.actionContainer.classList.remove('bb-up')
        }
        else {
            this._setGroupParams({ 'isPackup': true, 'height': parseFloat(parentEle.style.height) });
            parentEle.style.height = `${this.group.activePanel.view._tab._element.offsetHeight}px`;
            this.actionContainer.classList.add('bb-up');
        }
        saveConfig(dockview)
    }

    _full() {
        return this.toggleFull(false)
    }

    _restore() {
        return this.toggleFull(true)
    }

    _float() {
        if (this.group.locked) return;

        const dockview = this.group.api.accessor;
        const x = (dockview.width - 500) / 2
        const y = (dockview.height - 460) / 2
        const gridGroups = dockview.groups.filter(group => group.panels.length > 0 && group.type === 'grid')
        if (gridGroups.length <= 1) return;

        const { position = {}, isPackup, height, isMaximized } = this.group.getParams()
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

        const group = dockview.createGroup({ id: `${this.group.id}_floating` });
        dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })

        this.group.panels.slice(0).forEach((panel, index) => {
            dockview.moveGroupOrPanel({
                from: { groupId: this.group.id, panelId: panel.id },
                to: { group, position: 'center', index },
                skipRemoveGroup: true
            })
        })
        dockview.setVisible(this.group, false)
        group.setParams({ isPackup, height, isMaximized })
        group.groupControl = new GroupControl(group)
        saveConfig(dockview)
    }

    _dock() {
        if (this.group.locked) return;

        const dockview = this.group.api.accessor
        const originGroup = dockview.groups.find(group => `${group.id}_floating` === this.group.id)
        dockview.setVisible(originGroup, true)

        let { isPackup, height, isMaximized, position } = this.group.getParams()
        if (!isMaximized) {
            position = {
                width: this.group.width,
                height: this.group.height,
                top: parseFloat(this.group.element.parentElement.style.top || 0),
                left: parseFloat(this.group.element.parentElement.style.left || 0)
            }
        }
        dockview.moveGroup({
            from: { group: this.group },
            to: { group: originGroup, position: 'center' }
        })

        originGroup.setParams({ position, isPackup, height, isMaximized })
        saveConfig(dockview)
    }

    _close() {
        if (!this.group.locked) {
            this.group.api.close()
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

    _floatingMaximize() {
        const parentEle = this.group.element.parentElement
        const { width, height } = parentEle.style
        const { width: maxWidth, height: maxHeight } = this.group.api.accessor;
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

    _floatingExitMaximized() {
        const parentEle = this.group.element.parentElement
        const position = this._getGroupParams('position')
        Object.keys(position).forEach(key => parentEle.style[key] = position[key] + 'px')
        this._setGroupParams({ isMaximized: false })
    }
}

const showLock = (dockview, group) => {
    const { options } = dockview.params;
    return group.panels.every(panel => panel.params.showLock === null)
        ? options.showLock
        : group.panels.some(panel => panel.params.showLock === true)
}

const getGroupLockState = (dockview, group) => {
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

const getGroupMaximizeState = (group) => {
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

const getGroupFloatState = group => group.model.location.type === 'floating'

export function cerateDockview(el, options) {
    const dockview = new DockviewComponent({
        parentElement: el,
        createComponent: option => new DockviewPanelContent(option)
    });
    dockview.template = el.querySelector('template');
    dockview.params = { options };
    dockview.saveLayout = () => {
        return dockview.toJSON()
    }
    dockview.update = updateOptions => {
        if (updateOptions.layoutConfig) {
            reloadDockview(updateOptions, dockview, el)
        }
        else if (dockview.locked !== updateOptions.lock) {
            lockDock(dockview)
        }
        else {
            toggleComponent(dockview, updateOptions)
        }
    }
    dockview.reset = (resetOptions) => {
        reloadDockview(resetOptions, dockview, el)
    }
    dockview.dispose = () => {
    }

    const localConfig = options.enableLocalStorage ? getLocal(options.localStorageKey) : null
    const layoutConfig = options.layoutConfig && getLayoutConfig(options)
    let wh = el.clientWidth != 0 ? { width: el.clientWidth, height: el.clientHeight } : {}
    const serializeData = serialize(options, wh)
    const dockviewData = getJson(dockview, localConfig || layoutConfig || serializeData)

    addHook(dockview, dockviewData, options)
    loadDockview(dockview, dockviewData, serializeData)
    return dockview
}

const reloadDockview = (options, dockview, el) => {
    dockview.isClearIng = true
    dockview.clear()
    setTimeout(() => {
        dockview.isClearIng && (delete dockview.isClearIng)
        localStorage.removeItem(dockview.params.options.localStorageKey + '-panels');
    }, 0);
    let resetConfig = options.layoutConfig && getLayoutConfig(options)
    loadDockview(dockview, getJson(dockview, resetConfig || serialize(options, { width: el.clientWidth, height: el.clientHeight })))

}

export function toggleComponent(dockview, options) {
    const panels = getPanels(options.content)
    const localPanels = dockview.panels || {}
    panels.forEach(panel => {
        let pan = localPanels.find(item => item.title === panel.title)
        if (pan === void 0) {
            let storagePanels = getLocal(`${dockview.params.options.localStorageKey}-panels`) || []
            let storagePanel = storagePanels.find(item => (panel.params.key && panel.params.key === item.params.key) || item.id === panel.id || item.title === panel.title)
            addDelPanel(storagePanel || panel, [], dockview)
        }
    })

    localPanels.forEach(item => {
        let pan = panels.find(panel => panel.title === item.title)
        if (pan === void 0) {
            dockview.removePanel(item)
        }
    })
}
const lockGroup = (group, isLock) => {
    group.locked = isLock ? 'no-drop-target' : false
    group.groupControl.toggleLock(isLock)
    group.panels.forEach(panel => {
        panel.params && (panel.params.isLock = isLock)
    })
}

export function lockDock(dock) {
    dock.groups.forEach(group => lockGroup(group, dock.locked))
    // dock._lockChanged?.fire(dock.locked)
}

export function addHook(dockview, dockviewData, options) {
    // 钩子1： 删除panel触发
    dockview.onDidRemovePanel(event => {
        // console.log('onDidRemovePanel');
        // 在删除的panel上存储信息并把panel存储到storage
        let obj = {
            id: event.id,
            title: event.title,
            component: event.view.contentComponent,
            groupId: event.group.id,
            params: {
                ...event.params,
                currentPosition: {
                    width: event.group.element.parentElement.offsetWidth,
                    height: event.group.element.parentElement.offsetHeight,
                    top: parseFloat(event.group.element.parentElement.style.top || 0),
                    left: parseFloat(event.group.element.parentElement.style.left || 0)
                }
            }
        }
        if (event.params.groupInvisible) {
            obj.groupInvisible = event.params.groupInvisible
        }
        setSumLocal(`${dockview.params.options.localStorageKey}-panels`, obj)

        // 在group上存储已删除的panel标识
        !event.group.children && (event.group.children = [])
        event.group.children = event.group.children.filter(panel => {
            return !((event.params.key && event.params.key === panel.params.key) || panel.id === event.id || panel.title === event.title)
        })
        event.group.children.push({
            id: event.id,
            title: event.title,
            params: event.params
        })

        if (event.view.content.element) {//删除时保存标题和内容
            if (event.titleMenuEle) {
                event.view.content.element.append(event.titleMenuEle)
            }
            if (dockview.template) {
                dockview.template.append(event.view.content.element)
            }
        }

        // 放在onDidLayoutChange里保存
        // saveConfig(dockview)
    })

    // 钩子2：添加Panel触发
    dockview.onDidAddPanel(updateDockviewPanel);

    // 钩子3：添加Group触发
    dockview.onDidAddGroup(event => {
        // 给每个Group实例添加type和params属性
        Object.defineProperties(event, {
            type: {
                get() { return this.model.location.type }
            },
            params: {
                get() { return JSON.parse(JSON.stringify(event.activePanel?.params || {})) }
            }
        })
        if (0) {
            event.header.hidden = true
        }
        // 修正floating Group的位置
        let { floatingGroups = [], panels } = dockviewData
        let floatingGroup = floatingGroups.find(item => item.data.id === event.id)
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

        setTimeout(() => {
            event.groupControl = new GroupControl(event)
        }, 0);
        observer.observe(event.header.element)
    })
    dockview.onDidRemoveGroup(event => { })

    // 钩子4：拖拽panel标题前触发
    dockview.onWillDragPanel(event => {
        if (event.panel.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    // 钩子5：拖拽panel之后触发
    dockview._onDidMovePanel.event(event => { })

    // 狗子6：拖拽Group之前触发
    dockview.onWillDragGroup(event => {
        if (event.group.locked) {
            event.nativeEvent.preventDefault()
        }
    })

    // 钩子7：拖拽分割线后触发
    dockview.gridview.onDidChange(event => { })

    // 钩子8：所有造成layout变化的操作都会触发
    let eve = dockview.onDidLayoutChange(event => {
        setTimeout(() => {
            // 维护Group的children属性
            // if (dockview.totalPanels >= (dockview.panelSize || 0)) {
            //     dockview.groups.forEach(group => {
            //         group.children = {}
            //         group.panels.forEach(panel => {
            //             group.children[panel.id] = panel.id
            //         })
            //     })
            // }
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
            if (group.panels.length === 0) {
                dockview.setVisible(group, false)
            }
        })
    })
    dockview.onDidRemove(() => {
    })
}

const setWidth = (observerList) => {
    observerList.forEach(observer => {
        let header = observer.target
        let tabsContainer = header.querySelector('.tabs-container')
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
            liEle.setAttribute('tabWidth', lastTab.offsetWidth)
            liEle.addEventListener('click', () => {
                liEle.setAttribute('tabWidth', tabsContainer.children[0].offsetWidth)
                liEle.children[0].append(tabsContainer.children[0])
                tabsContainer.append(liEle.children[0].children[0])
            })
            aEle.append(lastTab)
            liEle.append(aEle)
            dropMenu.insertAdjacentElement("afterbegin", liEle)
        } else {
            let firstLi = dropMenu.children[0]
            if (firstLi) {
                let firstTab = firstLi.querySelector('.tab')
                if (voidWidth > firstLi.getAttribute('tabWidth')) {
                    firstTab && tabsContainer.append(firstTab)
                    firstLi.remove()
                }
            }
        }
    })
}
const observer = new ResizeObserver(setWidth)

let groupId = 0
const getGroupId = () => {
    return groupId++
}
export function serialize(options, { width = 800, height = 600 }) {
    groupId = 0
    const panels = {}
    const orientation = options.content[0].type === 'row' ? 'VERTICAL' : 'HORIZONTAL';
    return options.content ? {
        activeGroup: '1',
        grid: {
            width,
            height,
            orientation,
            root: {
                type: 'branch',
                data: [getTree(options.content[0], { width, height, orientation }, options, panels)]
            },
        },
        panels
    } : null
}
function getLayoutConfig({ layoutConfig, content }) {
    layoutConfig = JSON.parse(layoutConfig)
    let panels = getPanels(content)
    Object.values(layoutConfig.panels).forEach(value => {
        let contentPanel = panels.find(panel => (panel.params.key && panel.params.key === value.params.key) || panel.id === value.id || panel.title === value.title)
        value.params = {
            ...value.params,
            class: contentPanel.params.class,
            height: contentPanel.params.height,
            parentId: contentPanel.params.parentId,
            showClose: contentPanel.params.showClose,
            showHeader: contentPanel.params.showHeader,
            showLock: contentPanel.params.showLock,
            titleClass: contentPanel.params.titleClass,
            titleWidth: contentPanel.params.titleWidth,
            type: contentPanel.params.type,
            width: contentPanel.params.width,
            // key: contentPanel.key,
            // id: "bb_13852650",
            // title: contentPanel.title,
        }
    })

    return layoutConfig
}

export function addDelPanel(panel, delPanels, dockview) {
    let group
    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    if (panel.groupId) {
        group = dockview.api.getGroup(panel.groupId)
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

            setTimeout(() => {
                group.groupControl = new GroupControl(group)
            }, 0);
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
    }
    else {
        group = dockview.groups.find(group => group.children?.[panel.id])
        if (!group) {
            let curentPanel = dockview.panels.findLast(item => item.params.parentId === panel.params.parentId)
            let direction = getOrientation(dockview.gridview.root, curentPanel.group) === 'VERTICAL' ? 'below' : 'right'
            let newPanel = dockview.addPanel({
                id: panel.id,
                title: panel.title,
                component: panel.component,
                position: { referenceGroup: curentPanel.group, direction },//direction: "bottom"
                params: { ...panel.params, isPackup, height, isMaximized, position }
            });
        }
        else {
            if (group.api.location.type === 'grid') {
                let isVisible = dockview.isVisible(group)
                if (isVisible === false) {
                    dockview.setVisible(group, true)
                    isMaximized && group.api.maximize()
                }
            }
            dockview.addPanel({
                id: panel.id,
                title: panel.title,
                component: panel.component,
                position: { referenceGroup: group },
                params: { ...panel.params, isPackup, height, isMaximized, position }
            })
        }
    }
    setDecreaseLocal(`${dockview.params.options.localStorageKey}-panels`, panel)
}
export function loadDockview(dockview, dockviewData, serializeData) {
    try {
        dockview.fromJSON(dockviewData)
    } catch (error) {
        setTimeout(() => {
            localStorage.removeItem(`${dockview.params.options.localStorageKey}-panels`);
            localStorage.removeItem(dockview.params.options.localStorageKey);
            dockview.fromJSON(serializeData)
        }, 0)
        throw new Error('load error message: ', error)

    }
    finally {

    }
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
                if (orientation) return orientation
            }
        }
    } else {
        return false
    }
}
export function getJson(dockview, data) {
    // 修正JSON
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
    if (branch.type === 'leaf') {
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

const setSumLocal = (key, panel) => {
    const localData = getLocal(key)?.filter(item => item.title !== panel.title) || [];
    localData.push(panel)
    localStorage.setItem(key, JSON.stringify(localData))
}

const setDecreaseLocal = (key, panel) => {
    const localData = getLocal(key)?.filter(item => item.title !== panel.title)
    if (!localData || localData.length === 0) {
        return localStorage.removeItem(key)
    }
    localStorage.setItem(key, JSON.stringify(localData))
}

const saveConfig = dockview => {
    if (dockview.hasMaximizedGroup()) return;

    if (dockview.params.options.enableLocalStorage) {
        const json = dockview.toJSON()
        localStorage.setItem(dockview.params.options.localStorageKey, JSON.stringify(json));
    }
}

const getTree = (contentItem, { width, height, orientation }, parent, panels) => {
    let length = parent.content.length || 1
    let obj = {}, boxSize = orientation === 'HORIZONTAL' ? width : height, size
    let hasSizeList = parent.content.filter(item => item.width || item.height)
    let hasSizeLen = hasSizeList.length
    if (hasSizeLen === 0) {
        size = (1 / length * boxSize).toFixed(2) * 1
    } else {
        size = hasSizeList.reduce((pre, cur) => pre + (cur.width || cur.height), 0)
        size = ((boxSize - size) / (length - hasSizeLen)).toFixed(2) * 1
    }
    orientation === 'HORIZONTAL' ? width = size : height = size
    orientation = orientation === 'HORIZONTAL' ? 'VERTICAL' : 'HORIZONTAL'

    if (contentItem.type === 'row' || contentItem.type === 'column') {
        obj.type = 'branch'
        obj.size = contentItem.width || contentItem.height || size
        obj.data = contentItem.content.map(item => getTree(item, { width, height, orientation }, contentItem, panels))
    }
    else if (contentItem.type === 'group') {
        obj.type = 'leaf'
        obj.size = contentItem.width || contentItem.height || size
        obj.visible = contentItem.content.some(item => item.visible !== false)
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.content[0].id,
            hideHeader: contentItem.content.length === 1 && contentItem.content[0].showHeader === false,
            views: contentItem.content.filter(item => item.visible !== false).map(item => {
                panels[item.id] = {
                    id: item.id,
                    title: item.title,
                    tabComponent: item.componentName,
                    contentComponent: item.componentName,
                    params: { ...item, parentId: parent.id }
                }
                return item.id
            })
        }
    }
    else if (contentItem.type === 'component') {
        obj.type = 'leaf'
        obj.visible = contentItem.visible !== false
        obj.size = contentItem.width || contentItem.height || size
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.id,
            hideHeader: contentItem.showHeader === false,
            views: obj.visible ? [contentItem.id] : []
        }
        if (obj.visible) {
            panels[contentItem.id] = {
                id: contentItem.id,
                title: contentItem.title,
                tabComponent: contentItem.componentName,
                contentComponent: contentItem.componentName,
                // params: {...contentItem, showClose: true, showLock: true, showFloat: true, showMaximize: true}
                params: { ...contentItem, parentId: parent.id }
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
const getPanel = (contentItem, parent = {}, panels = []) => {
    if (contentItem.type === 'component') {
        panels.push({
            id: contentItem.id,
            groupId: contentItem.groupId,
            title: contentItem.title,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
            params: { ...contentItem, parentId: parent.id }
        })
    } else {
        contentItem.content?.forEach(item => getPanel(item, contentItem, panels))
    }
    return panels
}
