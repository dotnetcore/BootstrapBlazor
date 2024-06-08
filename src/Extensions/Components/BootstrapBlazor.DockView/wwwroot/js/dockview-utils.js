import { DockviewComponent, DefaultTab } from "../js/dockview-core.esm.js"
import '../js/dockview-extensions.js'

export class DefaultPanel {
    _element;
    get element() {
        return this._element;
    }
    constructor(option) {
        this.option = option
    }

    init(parameter) {
        let { params, api, containerApi: { component: { template } } } = parameter
        let { panel, group } = api
        let { tab, content } = panel.view
        if (template) {
            let contentEle = template.querySelector('#' + this.option.id)
            if (!contentEle) {
                contentEle = panel.params.key ? template.querySelector(`[data-bb-key=${panel.params.key}]`) : template.querySelector(`[data-bb-title=${parameter.title}]`)
            }
            this._element = contentEle || document.createElement('div')
        }
        const { titleClass, titleWidth, class: contentClass } = params
        titleClass && tab._content.classList.add(titleClass)
        titleWidth && (tab._content.style.width = titleWidth + 'px')
        contentClass && content.element.classList.add(contentClass)
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

        dockviewPanel.titleMenuEle = this.contentEle.querySelector(`.bb-dockview-item-title`) || this.contentEle.querySelector(`.bb-dockview-item-title-icon`)
        this.panel = dockviewPanel

        dockviewPanel.titleMenuEle && this.createGear(api)
        this.creatCloseBtn()
    }
    // 添加小齿轮
    createGear(api) {
        // const divEle = document.createElement('div')
        // divEle.append(this.panel.titleMenuEle)
        // divEle.addEventListener('mousedown', e => {
        //     e.stopPropagation()
        // })
        if (this.panel.titleMenuEle.className.includes('bb-dockview-item-title-icon')) {
            this.tabEle.insertAdjacentElement("afterbegin", this.panel.titleMenuEle)
        }
        else if (this.panel.titleMenuEle.className.includes('bb-dockview-item-title')) {
            // this.panel.view.tab._content.innerHTML = ''
            this.panel.view.tab.element.replaceChild(this.panel.titleMenuEle, this.panel.view.tab._content)
        }

        // api.onDidVisibilityChange(({ isVisible }) => {
        //     divEle.style.display = isVisible ? 'block' : 'none'
        // })
    }
    creatCloseBtn() {
        let showClose = this.panel.params?.showClose
        showClose = showClose === null ? this.panel.accessor.showClose !== false : showClose
        if (showClose === false) {
            this.tabEle.classList.add('dv-tab-on')
        } else {
            let closeBtn = this.tabEle.children[this.tabEle.children.length - 1]
            let closeControl = this.panel.accessor.groupControls?.find(control => control.name == 'close')
            closeBtn.innerHTML = closeControl?.icon[0]
        }
    }
}
class GroupControl {
    constructor(dockviewGroupPanel, dockview, isOpenFloat) {
        const { element, header, api } = dockviewGroupPanel
        // Group
        this.ele = element
        // Group的Header
        this.api = api
        this.headerEle = header.element
        this.group = dockviewGroupPanel
        this.parentEle = dockviewGroupPanel.element.parentElement
        this.dockview = dockview
        this.isOpenFloat = isOpenFloat
        !dockviewGroupPanel.header.hidden && this.creatRightControl()
    }

    creatPrefixControl() {
        // 添加header前控制按钮
        // ...
    }
    creatTabsAfterControl() {
        // 添加tabs后的控制按钮
        // ...
    }
    // 添加右侧控制按钮
    creatRightControl() {
        // showClose, showFloat, showLock, showMaximize
        // let divEle = this.document.createElement('div')
        let divEle = this.headerEle.querySelector('.right-actions-container')
        let { panels, api } = this.group
        let showLock = panels.every(panel => panel.params.showLock === null) ? this.dockview.showLock !== false : panels.some(panel => panel.params.showLock !== false)
        let filterControls = this.dockview.groupControls.filter(item => {
            switch (item.name) {
                case 'dropdown': return true
                case 'lock': return showLock
                case 'packup/expand': return true
                case 'float': return panels.every(panel => panel.params.showFloat !== false)
                case 'maximize': return panels.every(panel => panel.params.showMaximize !== false)
                case 'close': return true
            }
        })
        filterControls.forEach(item => {
            if (api.location.type == 'grid' && item.name == 'packup/expand') return
            // item.name == 'packup/expand' && (item.icon = ['<i class="fas fa-chevron-circle-up"></i>', '<i class="fas fa-chevron-circle-down"></i>'])
            let btn = this._createButton(item)
            divEle.append(btn)
        })
        // divEle.style.cssText = `display: flex;align-items: center;padding: 0px 8px;height: 100%;`
        // this.headerEle.querySelector('.right-actions-container').append(divEle)
    }

    _createButton(item) {
        const divEle = document.createElement('div')
        divEle.className = 'bb-dockview-control-' + item.name
        if (item.name == 'dropdown') {
            divEle.innerHTML = `${item.icon[0]}<ul class="dropdown-menu"></ul>`
            divEle.children[0].setAttribute('data-bs-toggle', "dropdown")
            divEle.children[0].setAttribute('aria-expanded', "false")
        } else {
            divEle.title = item.name
            divEle.innerHTML = item.icon[0]
            if (item.name == 'lock') {
                this.lockEle = divEle
                let panelLock = this.group.panels.some(panel => panel.params.isLock === true)
                this.group.locked = this.isOpenFloat ? false : panelLock ? true : this.dockview.locked ? true : false
                divEle.innerHTML = item.icon[this.group.locked ? 1 : 0]
                divEle.title = this.group.locked ? 'unlock' : 'lock'
                if (this.group.locked) {
                    this.group.header.element.classList.add('lock')
                }
            }
            else if (item.name == 'packup/expand') {
                divEle.className = 'bb-dockview-control-up'
                let isPackup = this._getGroupParams('isPackup')
                if (isPackup) {
                    // divEle.innerHTML = item.icon[1]
                    // divEle.style.transform = 'rotateZ(180deg)'
                    divEle.classList.add('bb-dockview-control-down')
                }
            }
            else if (item.name == 'float') {
                let type = this.group.model.location.type
                if (type == 'floating') {
                    divEle.title = 'restore'
                    divEle.innerHTML = item.icon[1]
                }
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
        }

        return divEle
    }
    _lock(divEle, item) {
        this.group.locked = this.group.locked ? false : 'no-drop-target'
        this.group.panels.forEach(panel => {
            panel.params && (panel.params.isLock = this.group.locked !== false)
        })
        this.toggleLock(divEle, item)
        this.dockview._lockChanged?.fire({ title: this.group.panels.map(panel => panel.title), isLock: this.group.locked !== false })
    }
    toggleLock(divEle, item) {
        divEle = divEle || this.lockEle
        item = item || this.dockview.groupControls.find(option => option.name == 'lock')
        if (!divEle) return
        divEle.innerHTML = item.icon[this.group.locked ? 1 : 0]
        divEle.title = this.group.locked ? 'unlock' : 'lock'
        this.group.locked ? this.group.header.element.classList.add('lock') : this.group.header.element.classList.remove('lock')
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
        // divEle.innerHTML = item.icon[isPackup ? 0 : 1]
        divEle.style.transform = isPackup ? divEle.classList.remove('bb-dockview-control-down') : divEle.classList.add('bb-dockview-control-down')
        saveConfig(this.dockview)
    }
    _float(divEle, item) {
        if (this.group.locked) return
        let type = this.group.model.location.type
        let x = (this.dockview.width - 500) / 2
        let y = (this.dockview.height - 460) / 2
        if (type == 'grid') {
            let gridGroups = this.dockview.groups.filter(group => group.panels.length > 0 && group.type == 'grid')
            if (gridGroups.length <= 1) return
            let { position = {}, isPackup, height, isMaximized } = this.group.getParams()
            let floatingGroupPosition = isMaximized ? {
                x: 0, y: 0,
                width: this.dockview.width,
                height: this.dockview.height
            } : {
                x: position.left || (x < 35 ? 35 : x),
                y: position.top || (y < 35 ? 35 : y),
                width: position.width || 500,
                height: position.height || 460
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
            group.groupControl = new GroupControl(group, this.dockview, true)
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
            this.group.panels.forEach(panel => panel.params.isMaximized = false)
        } else {
            type == 'grid' ? this.group.api.maximize() : type == 'floating' ? this._floatingMaximize() : ''
            divEle.innerHTML = item.icon[1]
            divEle.title = 'exitMaximize'
            this.group.panels.forEach(panel => panel.params.isMaximized = true)
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
    const { templateId } = options
    const template = document.getElementById(templateId)
    const dockview = new DockviewComponent({
        parentElement: el,
        createComponent: option => {
            return new DefaultPanel(option)
        },
        createTabComponent: option => new myDefaultTab(option, options)
    });
    dockview.template = template
    dockview.groupControls = [
        { name: 'dropdown', icon: ['dropdown'] },
        { name: 'lock', icon: ['unlock', 'lock'] },
        { name: 'packup/expand', icon: ['down'] },
        { name: 'maximize', icon: ['full', 'restore'] },
        { name: 'float', icon: ['float', 'dock'] },
        { name: 'close', icon: ['close'] }
    ].map(({ name, icon }) => ({
        name,
        icon: icon.map(item => template.querySelector(`.bb-dockview-control-icon-${item}`)?.outerHTML || '')
    }))
    dockview.prefix = options.localStorageKey
    dockview.locked = options.lock
    dockview.showClose = options.showClose
    dockview.showLock = options.showLock
    dockview.saveLayout = () => {
        return dockview.toJSON()
    }
    dockview.update = updateOptions => {
        if (updateOptions.layoutConfig) {
            reloadDockview(updateOptions, dockview)
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
        reloadDockview(resetOptions, dockview)
    }
    dockview.dispose = () => {
    }

    // 序列化options数据为dockview可用数据(layoutConfig优先)
    let localConfig = options.enableLocalStorage ? getLocal(options.prefix) : null
    let layoutConfig = options.layoutConfig && JSON.parse(options.layoutConfig)
    let serializeData = serialize(options, { width: el.clientWidth, height: el.clientHeight })

    // 以本地优先, 得到最终的dockviewData并修正
    let dockviewData = getJson(dockview, localConfig || layoutConfig || serializeData)
    // 绑定钩子函数
    addHook(dockview, dockviewData, options)
    // 渲染dockview结构
    loadDockview(dockview, dockviewData, serializeData)
    return dockview
}
const reloadDockview = (options, dockview) => {
    dockview.isClearIng = true
    dockview.clear()
    setTimeout(() => {
        dockview.isClearIng && (delete dockview.isClearIng)
    }, 0);
    let resetConfig = options.layoutConfig && JSON.parse(options.layoutConfig)
    loadDockview(dockview, getJson(dockview, resetConfig || serialize(options)))
}

export function toggleComponent(dock, option) {
    let panels = getPanels(option.content)
    let localPanels = dock.panels || {}
    panels.forEach(panel => {
        let pan = localPanels.find(item => item.title == panel.title)
        if (pan === void 0) {//需要添加
            // 添加时先在localStorage找
            let storagePanels = getLocal(dock.prefix + '-panels') || []
            let storagePanel = storagePanels.find(item => item.title == panel.title)
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
    group.groupControl.toggleLock()
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

export function addHook(dockview, dockviewData) {
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

        if (event.view.content.element) {
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

        if (!event.group.children) {
            event.group.children = {}
        }
        event.group.children[event.id] = event.id
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

        if (true) {
            setTimeout(() => {
                event.groupControl = new GroupControl(event, dockview)
            }, 0);
        }
        // 监听groupHeader的宽度变化
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
    dockview.onDidRemove(() => {
    })
}
const setWidth = (observerList) => {
    observerList.forEach(observer => {
        let header = observer.target
        let tabsContainer = header.querySelector('.tabs-container')
        let voidWidth = header.querySelector('.void-container').offsetWidth
        let dropdown = header.querySelector('.right-actions-container>.bb-dockview-control-dropdown')
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
                dropMenu.children[0].setAttribute('tabWidth', tabsContainer.children[0].offsetWidth)
                dropMenu.children[0].children[0].append(tabsContainer.children[0])
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
                data: [getTree(options.content[0], { width, height, orientation }, options.content)]
            },
        },
        panels
    } : null
}
export function addDelPanel(panel, delPanels, dockview) {
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
                group.groupControl = new GroupControl(group, dockview)
            }, 50);
        }

    } else {
        if (group.api.location.type == 'grid') {
            let isVisible = dockview.isVisible(group)
            if (isVisible === false) {
                dockview.setVisible(group, true)
                isMaximized && group.api.maximize()
                // 修正Group的宽高(待完善...)
                // let lastDelPanel = delPanels.findLast(delPanel => delPanel.groupId == panel.groupId)
                // let {width, height} = lastDelPanel.params.currentPosition
                // console.log(width, height, 'group: width, height');
                // group.layout(width, height)
            }
        }
    }
    let panelObj = dockview.addPanel({
        id: panel.id,
        title: panel.title,
        component: panel.component,
        position: { referenceGroup: group },
        params: { ...panel.params, isPackup, height, isMaximized, position }
    });
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
    let length = parent.length || 1
    let obj = {}, boxSize = orientation == 'HORIZONTAL' ? width : height, size
    let hasSizeList = parent.filter(item => item.width || item.height)
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
        obj.data = contentItem.content.map(item => getTree(item, { width, height, orientation }, contentItem.content))
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
                    params: item
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


