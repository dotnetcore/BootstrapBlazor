import { fixObject } from "./dockview-fix.js"
import { getPanelsFromOptions, findContentFromPanels } from "./dockview-panel.js"

const loadPanelsFromLocalstorage = dockview => {
    const { options } = dockview.params;
    if (options.enableLocalStorage) {
        dockview.params.panels = localStorage.getItem(`${options.localStorageKey}-panels`) || [];
    }
}

const reloadFromConfig = (dockview, options) => {
    dockview.isClearing = true
    dockview.clear()
    dockview.isClearing = false
    dockview.params.panels = [];

    const jsonData = getConfigFromOptions(options);
    dockview.fromJSON(jsonData);
}

const getConfig = options => options.enableLocalStorage ? getConfigFromStorage(options.localStorageKey) : getConfigFromOptions(options);

const getConfigFromStorage = key => {
    return fixObject(JSON.parse(localStorage.getItem(key)));
}

const getConfigFromOptions = options => options.layoutConfig ? getConfigFromLayoutString(options) : getConfigFromContent(options);

const getConfigFromLayoutString = options => {
    let config = JSON.parse(options.layoutConfig);
    const panels = getPanelsFromOptions(options);
    Object.values(config.panels).forEach(value => {
        const contentPanel = findContentFromPanels(panels, value);
        if (contentPanel) {
            value.params = {
                ...value.params,
                ...contentPanel.params
            }
        }
    });
    return fixObject(config);
}

const getConfigFromContent = options => {
    const { width, height } = { width: 800, height: 600 };
    const getGroupId = getGroupIdFunc()
    const panels = {}
    const orientation = options.content[0].type === 'row' ? 'VERTICAL' : 'HORIZONTAL';
    return fixObject({
        activeGroup: '1',
        grid: {
            width,
            height,
            orientation,
            root: {
                type: 'branch',
                data: [getTree(options.content[0], { width, height, orientation }, options, panels, getGroupId)]
            },
        },
        panels
    });
}

const getGroupIdFunc = () => {
    let currentId = 0;
    return () => `${currentId++}`;
}

const getTree = (contentItem, { width, height, orientation }, parent, panels, getGroupId) => {
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
        obj.data = contentItem.content.map(item => getTree(item, { width, height, orientation }, contentItem, panels, getGroupId))
    }
    else if (contentItem.type === 'group') {
        obj.type = 'leaf'
        obj.size = contentItem.width || contentItem.height || size
        obj.visible = contentItem.content.some(item => item.visible !== false)
        obj.data = {
            id: getGroupId(),
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
            id: getGroupId(),
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
                params: { ...contentItem, parentId: parent.id }
            }
        }
    }
    return obj
}

const saveConfig = dockview => {
    if (dockview.params.options.enableLocalStorage) {
        const json = dockview.toJSON()
        localStorage.setItem(dockview.params.options.localStorageKey, JSON.stringify(json));
    }
}

export { getConfigFromStorage, getConfig, reloadFromConfig, saveConfig, loadPanelsFromLocalstorage };
