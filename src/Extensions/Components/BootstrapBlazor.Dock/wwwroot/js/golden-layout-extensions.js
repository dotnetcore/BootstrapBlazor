import "./golden-layout.js"
import { getAllItemsByType } from "./golden-layout-utility.js"

goldenLayout.Tab.prototype.onCloseClick = function () {
    const component = document.getElementById(this.componentItem.id)
    const title = this.componentItem.title

    this.notifyClose();
    this._layoutManager.emit('tabClosed', component, title)
};

const originSplitterDragStop = goldenLayout.RowOrColumn.prototype.onSplitterDragStop;
goldenLayout.RowOrColumn.prototype.onSplitterDragStop = function (splitter) {
    originSplitterDragStop.call(this, splitter)
    this.layoutManager.emit('splitterDragStop')
};

const originprocessTabDropdownActiveChanged = goldenLayout.Header.prototype.processTabDropdownActiveChanged;
goldenLayout.Header.prototype.processTabDropdownActiveChanged = function () {
    originprocessTabDropdownActiveChanged.call(this)

    this._closeButton.onClick = function (ev) {
        // find own dock
        const dock = goldenLayout.bb_docks.find(i => i.layout === this._header.layoutManager);
        const eventsData = dock.eventsData

        const tabs = this._header.tabs.map(tab => {
            return { element: tab.componentItem.element, title: tab.componentItem.title }
        })
        if (!eventsData.has(this._header.parent)) {
            this._pushEvent(ev)

            const handler = setTimeout(() => {
                clearTimeout(handler)
                tabs.forEach(tab => {
                    this._header.layoutManager.emit('tabClosed', tab.element, tab.title)
                })
            }, 100)
        }
    }
};

const createDock = (el, option) => {
    const config = getConfig(option)

    if (option.lock) {
        getAllItemsByType('component', option).forEach(i => {
            i.componentState.lock = option.lock
        })
    }

    const layout = new goldenLayout.GoldenLayout(config, el)

    layout.registerComponentFactoryFunction("component", (container, state) => {
        const el = document.getElementById(state.id)
        if (el) {
            el.classList.remove('d-none')
            if (state.class) {
                container.element.classList.add(state.class)
            }
            container.element.append(el)
        }
    })
    layout.resizeWithContainerAutomatically = true
    return layout
}

const getConfig = option => {
    option = {
        enableLocalStorage: false,
        layoutConfig: null,
        name: 'default',
        ...option
    }

    let config = null
    let layoutConfig = option.layoutConfig;
    if (layoutConfig === null && option.enableLocalStorage) {
        layoutConfig = localStorage.getItem(option.localStorageKey);
    }
    if (layoutConfig) {
        // 当tab全部关闭时，没有root节点
        const configItem = JSON.parse(layoutConfig)
        if (configItem.root) {
            config = configItem
            resetComponentId(config, option)
        }
    }

    return {
        ...(config || { content: [] }),
        ...{
            dimensions: {
                borderWidth: 5,
                minItemHeight: 10,
                minItemWidth: 10,
                headerHeight: 25
            },
            labels: {
                close: 'close',
                maximise: 'maximise',
                minimise: 'minimise',
                popout: false
            }
        },
        ...option
    }
}

const resetComponentId = (config, option) => {
    // 本地配置
    const localComponents = getAllItemsByType('component', config.root)
    // 服务器端配置
    const serverItems = getAllItemsByType('component', option)
    localComponents.forEach(com => {
        const item = serverItems.find(i => i.componentState.key === com.componentState.key)
        if (item) {
            com.id = item.id
            com.title = item.title
            com.componentState = item.componentState
            com.componentState.lock = com.componentState.lock || item.componentState.lock
        }
        else {
            // 本地存储中有，配置中没有，需要显示这个组件，通过 key 来定位新 Component
            const newEl = document.querySelector(`[data-bb-key='${com.componentState.key}']`) || document.querySelector(`[data-bb-title='${com.componentState.key}']`)
            if (newEl) {
                com.id = newEl.getAttribute('id')
                com.title = newEl.getAttribute('data-bb-title')
                com.componentState.id = com.id

                option.invokeVisibleChangedCallback(com.title, true)
            }
            else {
                removeContent(config.root.content, com)

                // remove empty stack
                config.root.content.filter(v => v.content.length === 0).forEach(v => {
                    const index = config.root.content.indexOf(v)
                    if (index > -1) {
                        config.root.content.splice(index, 1)
                    }
                })
            }
        }
    })

    serverItems.forEach(item => {
        // 更新服务器端组件可见状态
        const com = localComponents.find(i => i.componentState.key === item.componentState.key)
        option.invokeVisibleChangedCallback(item.title, com !== void 0)

        // 本地存储中没有，配置中有
        if (com === void 0) {
            if (config.root.content.length > 0) {
                if (config.root.type === 'stack' && config.root.content.length === 1 && option.content.length === 1) {
                    config.root.type = option.content[0].type;
                    config.root.content = option.content[0].content
                }
                else {
                    config.root.content.push(parseItem(item))
                }
            }
            else {
                config.root.type = option.content[0].type;
                config.root.content = option.content[0].content;
            }
        }
    })

    // set stack headers
    // 本地配置
    const localStacks = getAllItemsByType('stack', config.root)
    // 服务器端配置
    const serverStacks = getAllItemsByType('stack', option)

    localStacks.forEach(s => {
        const stack = {
            hasHeaders: true,
            ...findStack(s, serverStacks),
        };
        s.header = {
            ...s.header,
            show: stack.hasHeaders
        }
    });
}

const parseItem = item => ({
    type: 'component',
    content: [],
    title: item.title,
    id: item.id,
    componentType: 'component',
    componentState: item.componentState
});

const findStack = (stack, stacks) => {
    let find = null;
    for (let com of stack.content) {
        find = stacks.find(s => s.content.find(c => c.componentState.key === com.componentState.key));
        if (find) {
            break;
        }
    }
    return find;
}

export { createDock };
