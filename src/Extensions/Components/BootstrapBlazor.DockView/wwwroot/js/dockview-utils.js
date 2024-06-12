import { DockviewComponent, DefaultTab } from "../js/dockview-core.esm.js"
import '../js/dockview-extensions.js'

class DefaultPanel {
    constructor(option) {
        this.option = option
    }

    init(parameter) {
        const { params, api: { panel, accessor: { template } } } = parameter;
        const { titleClass, titleWidth, class: panelClass, key, title } = params;
        const { tab, content } = panel.view

        if (template) {
            this._element = key
                ? template.querySelector(`[data-bb-key=${key}]`)
                : (template.querySelector(`#${this.option.id}`) ?? template.querySelector(`[data-bb-title=${title}]`))
        }

        if (titleClass) {
            tab._content.classList.add(titleClass);
        }
        if (titleWidth) {
            tab._content.style.width = `${titleWidth}px`;
        }
        if (panelClass) {
            content.element.classList.add(panelClass);
        }
    }

    get element() {
        return this._element;
    }
}

class PanelControl {
    constructor(panel) {
        const { view } = panel
        this.panel = panel
        this.tabEle = view.tab.element
        this.contentEle = view.content.element

        this.updateCloseButton()
        this.updateTitle()
    }

    updateTitle() {
        const titleElement = this.contentEle.querySelector('.bb-dockview-item-title');
        if (titleElement) {
            this.panel.view.tab.element.replaceChild(titleElement, this.panel.view.tab._content);
        }
        else {
            const titleBarElement = this.contentEle.querySelector('.bb-dockview-item-title-icon')
            if (titleBarElement) {
                titleBarElement.removeAttribute('title');
                this.tabEle.insertAdjacentElement("afterbegin", titleBarElement);
            }
        }
    }

    updateCloseButton() {
        const showClose = this.panel.params.showClose ?? this.panel.accessor.params.options.showClose;
        if (showClose) {
            const closeButton = this.panel.view.tab._content.nextElementSibling;
            if (closeButton) {
                const closeIcon = getActionIcon(this.panel.accessor, 'close', false);
                if (closeIcon) {
                    closeButton.replaceChild(closeIcon, closeButton.children[0]);
                }
            }
        }
        else {
            this.tabEle.classList.add('dv-tab-on')
        }
    }
}

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
        if (type == 'grid') {
            isMaximized ? this.group.api.exitMaximized() : this.group.api.maximize()
        }
        else if (type == 'floating') {
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

    _down(icon) {
        const { accessor: dockview } = this.group.api
        let isPackup = this._getGroupParams('isPackup')
        let parentEle = this.group.element.parentElement
        if (isPackup) {
            this._setGroupParams({ 'isPackup': false })
            parentEle.style.height = this._getGroupParams('height') + 'px'
        } else {
            this._setGroupParams({ 'isPackup': true, 'height': parseFloat(parentEle.style.height) })
            parentEle.style.height = '35px'
        }
        isPackup ? this.actionContainer.classList.remove('bb-up') : this.actionContainer.classList.add('bb-up')
        saveConfig(dockview)
    }

    _full() {
        return this.toggleFull(false)
    }

    _restore() {
        return this.toggleFull(true)
    }

    _float() {
        if (this.group.locked) return
        const dockview = this.group.api.accessor;
        const x = (dockview.width - 500) / 2
        const y = (dockview.height - 460) / 2

        let gridGroups = dockview.groups.filter(group => group.panels.length > 0 && group.type == 'grid')
        if (gridGroups.length <= 1) return
        let { position = {}, isPackup, height, isMaximized } = this.group.getParams()
        let floatingGroupPosition = isMaximized ? {
            x: 0, y: 0,
            width: dockview.width,
            height: dockview.height
        } : {
            x: position.left || (x < 35 ? 35 : x),
            y: position.top || (y < 35 ? 35 : y),
            width: position.width || 500,
            height: position.height || 460
        }

        let group = dockview.createGroup({ id: this.group.id + '_floating' })
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
        if (this.group.locked) return
        const { accessor: dockview } = this.group.api
        let originGroup = dockview.groups.find(group => group.id + '_floating' == this.group.id)
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

        // 把floating移回到grid, floating会被删除,在这之前需要保存悬浮框的position, 此处保存在了原Group的panel的params上
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

    _removeGroupParams(keys) {
        return (keys instanceof Array) ? keys.map(key => this.group.panels.forEach(panel => delete panel.params[key])) : false
    }

    _floatingMaximize() {
        let parentEle = this.group.element.parentElement
        let { width, height } = parentEle.style
        let { width: maxWidth, height: maxHeight } = this.group.api.accessor;
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

const getActionIcon = (dockview, name, hasTitle = true) => {
    let icon = null;
    var control = dockview.groupControls.find(i => i.name == name);
    if (control) {
        icon = control.icon.cloneNode(true);
    }
    if (!hasTitle) {
        icon.removeAttribute('title');
    }
    return icon;
}

const initActionIcon = () => {
    return ['bar', 'dropdown', 'lock', 'unlock', 'down', 'full', 'restore', 'float', 'dock', 'close'].map(v => {
        return {
            name: v,
            icon: document.querySelector(`template > .bb-dockview-control-icon-${v}`)
        };
    });
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
    return type == 'grid'
        ? group.api.isMaximized()
        : (type == 'floating' ? group.activePanel?.params['isMaximized'] : false)
}

const showFloat = (dockview, group) => {
    const { options } = dockview.params;
    return group.panels.every(panel => panel.params.showFloat === null)
        ? options.showFloat
        : group.panels.some(panel => panel.params.showFloat === true)
}

const getGroupFloatState = group => group.model.location.type == 'floating'

export function cerateDockview(el, options) {
    const dockview = new DockviewComponent({
        parentElement: el,
        createComponent: option => {
            return new DefaultPanel(option)
        },
        createTabComponent: () => new DefaultTab()
    });
    dockview.template = el.querySelector('template');
    dockview.groupControls = initActionIcon();
    dockview.prefix = options.localStorageKey
    dockview.locked = options.lock
    // dockview.showClose = options.showClose
    // dockview.showLock = options.showLock
    dockview.params = { options };
    dockview.saveLayout = () => {
        return dockview.toJSON()
    }
    dockview.update = updateOptions => {
        if (updateOptions.layoutConfig) {
            reloadDockview(updateOptions, dockview, el)
        }
        else if (dockview.locked !== updateOptions.lock) {
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
        reloadDockview(resetOptions, dockview, el)
    }
    dockview.dispose = () => {
    }

    // 序列化options数据为dockview可用数据(layoutConfig优先)
    let localConfig = options.enableLocalStorage ? getLocal(options.prefix) : null
    let layoutConfig = options.layoutConfig && getLayoutConfig(options)
    let serializeData = serialize(options, { width: el.clientWidth, height: el.clientHeight })

    // 以本地优先, 得到最终的dockviewData并修正
    let dockviewData = getJson(dockview, localConfig || layoutConfig || serializeData)
    // 绑定钩子函数
    addHook(dockview, dockviewData, options)
    // 渲染dockview结构
    loadDockview(dockview, dockviewData, serializeData)
    return dockview
}
const reloadDockview = (options, dockview, el) => {
    dockview.isClearIng = true
    dockview.clear()
    setTimeout(() => {
        dockview.isClearIng && (delete dockview.isClearIng)
        localStorage.removeItem(dockview.prefix + '-panels');
    }, 0);
    let resetConfig = options.layoutConfig && getLayoutConfig(options)
    loadDockview(dockview, getJson(dockview, resetConfig || serialize(options, { width: el.clientWidth, height: el.clientHeight })))

}

export function toggleComponent(dock, option) {
    let panels = getPanels(option.content)
    let localPanels = dock.panels || {}
    panels.forEach(panel => {
        let pan = localPanels.find(item => item.title == panel.title)
        if (pan === void 0) {//需要添加
            // 添加时先在localStorage找
            let storagePanels = getLocal(dock.prefix + '-panels') || []
            let storagePanel = storagePanels.find(item => (panel.params.key && panel.params.key == item.params.key) || item.id == panel.id || item.title == panel.title)
            addDelPanel(storagePanel || panel, [], dock)
        }
    })

    localPanels.forEach(item => {
        let pan = panels.find(panel => panel.title == item.title)
        if (pan === void 0) {//需要删除
            dock.removePanel(item)
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
        setSumLocal(dockview.prefix + '-panels', obj)

        // 在group上存储已删除的panel标识
        !event.group.children && (event.group.children = [])
        event.group.children = event.group.children.filter(panel => {
            return !((event.params.key && event.params.key == panel.params.key) || panel.id == event.id || panel.title == event.title)
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
    dockview.onDidAddPanel(event => {
        new PanelControl(event)

        // if (!event.group.children) {
        //     event.group.children = {}
        // }
        // event.group.children[event.id] = event.id
    })
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
            if (group.panels.length == 0) {
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
        if (voidWidth == 0) {
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

let panels = {}
let groupId = 0
const getGroupId = () => {
    return groupId++
}
export function serialize(options, { width = 800, height = 600 }) {
    groupId = 0
    const orientation = options.content[0].type == 'row' ? 'VERTICAL' : 'HORIZONTAL';
    return options.content ? {
        activeGroup: '1',
        grid: {
            width,
            height,
            orientation,
            root: {
                type: 'branch',
                data: [getTree(options.content[0], { width, height, orientation }, options)]
            },
        },
        panels
    } : null
}
function getLayoutConfig({ layoutConfig, content }) {
    layoutConfig = JSON.parse(layoutConfig)
    let panels = getPanels(content)
    Object.values(layoutConfig.panels).forEach(value => {
        let contentPanel = panels.find(panel => (panel.params.key && panel.params.key == value.params.key) || panel.id == value.id || panel.title == value.title)
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
    // 手动添加已删除的panel
    // let group = panel.groupId ? dockview.api.getGroup(panel.groupId) : (
    //     dockview.groups.find(group => group.children?.[panel.id]) || dockview.groups[0]
    // )
    let group
    let { position = {}, currentPosition, height, isPackup, isMaximized } = panel.params || {}
    if (panel.groupId) { //有groupId就按groupId找group,找到就显示, 找不到就按groupId创建浮动窗口
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
            if (group.api.location.type == 'grid') {
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
    else {// 没有groupId就通过group.children来查找,找到就显示, 找不到就按panel.params.parentId来找到group所在的同级结构进行创建
        group = dockview.groups.find(group => group.children?.[panel.id])
        if (!group) {
            let curentPanel = dockview.panels.findLast(item => item.params.parentId == panel.params.parentId)
            let direction = getOrientation(dockview.gridview.root, curentPanel.group) == 'VERTICAL' ? 'below' : 'right'
            let newPanel = dockview.addPanel({
                id: panel.id,
                title: panel.title,
                component: panel.component,
                position: { referenceGroup: curentPanel.group, direction },//direction: "bottom"
                params: { ...panel.params, isPackup, height, isMaximized, position }
            });
        }
        else {
            if (group.api.location.type == 'grid') {
                let isVisible = dockview.isVisible(group)
                if (isVisible === false) {
                    dockview.setVisible(group, true)
                    isMaximized && group.api.maximize()
                    // 修正Group的宽高(待完善...)
                    // ...
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

    // if (!group) {
    //     group = dockview.createGroup({ id: panel.groupId })
    //     let floatingGroupPosition = isMaximized ? {
    //         x: 0, y: 0,
    //         width: dockview.width,
    //         height: dockview.height
    //     } : {
    //         x: currentPosition?.left || 0,
    //         y: currentPosition?.top || 0,
    //         width: currentPosition?.width,
    //         height: currentPosition?.height
    //     }
    //     dockview.addFloatingGroup(group, floatingGroupPosition, { skipRemoveGroup: true })

    //     if (true) {
    //         setTimeout(() => {
    //             // group.setParams({isPackup, height, isMaximized, position})
    //             group.groupControl = new GroupControl(group, dockview)
    //         }, 50);
    //     }

    // } else {
    //     if (group.api.location.type == 'grid') {
    //         let isVisible = dockview.isVisible(group)
    //         if (isVisible === false) {
    //             dockview.setVisible(group, true)
    //             isMaximized && group.api.maximize()
    //             // 修正Group的宽高(待完善...)
    //             // ...
    //         }
    //     }
    // }
    // let panelObj = dockview.addPanel({
    //     id: panel.id,
    //     title: panel.title,
    //     component: panel.component,
    //     position: { referenceGroup: group },
    //     params: { ...panel.params, isPackup, height, isMaximized, position }
    // });
    // dockview._visibleChanged?.fire({ panel: panelObj, isVisible: true })
    setDecreaseLocal(dockview.prefix + '-panels', panel)
}
export function loadDockview(dockview, dockviewData, serializeData) {
    try {
        dockview.fromJSON(dockviewData)
    } catch (error) {
        setTimeout(() => {
            localStorage.removeItem(dockview.prefix + '-panels');
            localStorage.removeItem(dockview.prefix);
            dockview.fromJSON(serializeData)
        }, 0)
        throw new Error('load error message: ', error)

    } finally {

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
const setSumLocal = (key, panel) => {
    // 在指定Key累加数组数据
    // 确定是占位的panel就不用管
    // if(key == 'dock-view-panels' && data.id.includes('_占位')) return

    let localData = getLocal(key) || []
    // if (localData.find(item => item.id == data.id && item.groupId == data.groupId)) return
    localData = localData.filter(item => item.title != panel.title)
    localData.push(panel)
    localStorage.setItem(key, JSON.stringify(localData))
}
const setDecreaseLocal = (key, panel) => {
    // 在指定Key删除对应id的数组元素
    let localData = getLocal(key) || []
    localData = localData.filter(item => item.title != panel.title)
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
const getTree = (contentItem, { width, height, orientation }, parent) => {
    let length = parent.content.length || 1
    let obj = {}, boxSize = orientation == 'HORIZONTAL' ? width : height, size
    let hasSizeList = parent.content.filter(item => item.width || item.height)
    let hasSizeLen = hasSizeList.length
    if (hasSizeLen == 0) {
        size = (1 / length * boxSize).toFixed(2) * 1
    } else {
        size = hasSizeList.reduce((pre, cur) => pre + (cur.width || cur.height), 0)
        size = ((boxSize - size) / (length - hasSizeLen)).toFixed(2) * 1
    }
    orientation == 'HORIZONTAL' ? width = size : height = size
    orientation = orientation == 'HORIZONTAL' ? 'VERTICAL' : 'HORIZONTAL'

    if (contentItem.type == 'row' || contentItem.type == 'column') {
        obj.type = 'branch'
        obj.size = contentItem.width || contentItem.height || size
        obj.data = contentItem.content.map(item => getTree(item, { width, height, orientation }, contentItem))
    }
    else if (contentItem.type == 'group') {
        obj.type = 'leaf'
        obj.size = contentItem.width || contentItem.height || size
        obj.visible = contentItem.content.some(item => item.visible !== false)
        obj.data = {
            id: getGroupId() + '',
            activeView: contentItem.content[0].id,
            hideHeader: contentItem.content.length == 1 && contentItem.content[0].showHeader === false,
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
    else if (contentItem.type = 'component') {
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
    if (contentItem.type == 'component') {
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


