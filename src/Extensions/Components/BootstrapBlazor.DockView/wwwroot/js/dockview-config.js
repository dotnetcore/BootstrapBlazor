import { fixObject } from "./dockview-fix.js"
import { getPanelsFromOptions, findContentFromPanels } from "./dockview-panel.js"

const loadPanelsFromLocalstorage = dockview => {
    const { options } = dockview.params;
    if (options.enableLocalStorage) {
        const panelsStr = localStorage.getItem(`${options.localStorageKey}-panels`)
        dockview.params.panels = panelsStr ? JSON.parse(panelsStr) : [];
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

const getConfig = options => {
    const config = options.enableLocalStorage ? getConfigFromStorage(options) : null;
    return config ?? getConfigFromOptions(options);
}

const getConfigFromStorage = options => {
    const jsonString = localStorage.getItem(options.localStorageKey);
    return jsonString ? fixObject(renewConfigFromOptions(JSON.parse(jsonString), options)) : null;
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

const renewConfigFromOptions = (config, options) => {
    const optionPanels = getPanelsFromOptions(options)
    const localPanels = Object.values(config.panels)
    optionPanels.forEach(optionPanel => {// 在结构中添加
        const panel = localPanels.find(localPanel => localPanel.params.key == optionPanel.params.key)
        if(panel){//找到了就替换数据
            optionPanel.params = {
                ...panel.params,
                ...optionPanel.params
            }
            config.panels[panel.id] = optionPanel
        }
        else {// 本地没有Panel就需要添加
            const delPanels = JSON.parse(localStorage.getItem(options.localStorageKey + '-panels'))
            if(delPanels?.find(delPanel => delPanel.params.key == optionPanel.params.key)) return
            let index = optionPanels.findIndex(item => item.id == optionPanel.id)
            let brotherPanel, brotherType
            if(index == 0){
                brotherPanel = optionPanels[1]
                brotherType = 'after'
            }
            else {
                brotherPanel = optionPanels[index - 1]
                brotherType = 'front'
            }
            config.panels[optionPanel.id] = optionPanel
            const brotherId = Object.keys(config.panels).find(key => config.panels[key].params.key == brotherPanel.params.key)
            addPanel(config.grid.root, optionPanel, brotherPanel, brotherId, brotherType)

        }
    })
    localPanels.forEach(localPanel => {
        const panel = optionPanels.find(optionPanel => optionPanel.params.key == localPanel.params.key)
        if(panel){

        }
        else {// 在options里没有, 就需要在localPanels删除
            delete  config.panels[localPanel.id] && config.panels[localPanel.id]
            removePanel(config.grid.root, localPanel)
        }
    })
    return config
}

const addPanel = (branch, panel, brotherPanel, brotherId, brotherType) => {
    if(brotherPanel.params.parentType == 'group'){
        if(branch.type == 'leaf'){
            if(branch.data.views.includes(brotherId)){
                branch.data.views.push(panel.id)
            }
        }
        else if(branch.type == 'branch') {
            branch.data.forEach(item => {
                addPanel(item, panel, brotherPanel, brotherId)
            })
        }
    }
    else {
        if(branch.type == 'branch' && branch.data[0].type == 'leaf'){
            if(branch.data.find(leaf => leaf.data.views.includes(brotherId))){
                branch.data.push({
                    data: {
                        activeView: panel.id,
                        id: Date.now() + '',
                        views: [panel.id]
                    },
                    size: branch.data.reduce((pre, cur) => pre + cur.size, 0)/branch.data.length,
                    type: 'leaf'
                })
            }
        }
        else {
            branch.data.forEach(item => {
                addPanel(item, panel, brotherPanel, brotherId)
            })
        }
    }
}

const removePanel = (branch, panel, parent) => {
    if(branch.type == 'leaf'){
        branch.data.views = branch.data.views.filter(id => id != panel.id)
        parent && (parent.data = parent.data.filter(child => child.data.views.length > 0))
    }
    else if(branch.type == 'branch') {
        branch.data.forEach(item => {
            removePanel(item, panel, branch)
        })
    }
}

const getConfigFromContent = options => {
    const { width, height } = { width: 800, height: 600 };
    const getGroupId = getGroupIdFunc()
    const panels = {}, rootType = options.content[0].type
    const orientation = rootType === 'column' ? 'VERTICAL' : 'HORIZONTAL';
    const root = getTree(options.content[0], { width, height, orientation }, options, panels, getGroupId)
    return fixObject({
        activeGroup: '1',
        grid: { width, height, orientation, root },
        panels
    });
}

const getGroupIdFunc = () => {
    let currentId = 0;
    return () => `${currentId++}`;
}

const getTree = (contentItem, { width, height, orientation }, parent, panels, getGroupId) => {
    const length = parent.content.length;
    const boxSize = orientation === 'HORIZONTAL' ? width : height;
    let size;
    const hasSizeList = parent.content.filter(item => item.width || item.height)
    const hasSizeLen = hasSizeList.length
    if (hasSizeLen === 0) {
        size = (1 / length * boxSize).toFixed(2) * 1
    }
    else {
        size = hasSizeList.reduce((pre, cur) => pre + getSize(boxSize, cur.width || cur.height), 0)
        size = ((boxSize - size) / (length - hasSizeLen)).toFixed(2) * 1
    }
    orientation === 'HORIZONTAL' ? width = size : height = size
    orientation = orientation === 'HORIZONTAL' ? 'VERTICAL' : 'HORIZONTAL'

    let obj = {}
    if (contentItem.type === 'row' || contentItem.type === 'column') {
        obj.type = 'branch';
        obj.size = getSize(boxSize, contentItem.width || contentItem.height) || size
        obj.data = contentItem.content.map(item => getTree(item, { width, height, orientation }, contentItem, panels, getGroupId))
    }
    else if (contentItem.type === 'group') {
        obj = getGroupNode(contentItem, size, boxSize, parent, panels, getGroupId);
    }
    else if (contentItem.type === 'component') {
        obj = getLeafNode(contentItem, size, boxSize, parent, panels, getGroupId);
    }
    return obj
}

const getSize = (size, rate) => {
    return rate ? size * (rate / 100) : false
}

const getActualSize = (width, height, widthRate, heightRate, defaultSize) => (width ?? height) === null
    ? defaultSize
    : width ? width * widthRate / 100 : height * heightRate / 100;

const getGroupNode = (contentItem, size, boxSize, parent, panels, getGroupId) => {
    return {
        type: 'leaf',
        size: getSize(boxSize, contentItem.width || contentItem.height) || size,
        visible: contentItem.content.length > 0 || contentItem.content.some(item => item.visible !== false),
        data: {
            id: getGroupId(),
            activeView: contentItem.content[0]?.id || '',
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
}

const getLeafNode = (contentItem, size, boxSize, parent, panels, getGroupId) => {
    const visible = contentItem.visible !== false;
    const data = {
        type: 'leaf',
        visible,
        size: getSize(boxSize, contentItem.width || contentItem.height) || size,
        data: {
            id: getGroupId(),
            activeView: contentItem.id,
            hideHeader: contentItem.showHeader === false,
            views: visible ? [contentItem.id] : []
        }
    };
    if (visible) {
        panels[contentItem.id] = {
            id: contentItem.id,
            title: contentItem.title,
            tabComponent: contentItem.componentName,
            contentComponent: contentItem.componentName,
            params: { ...contentItem, parentId: parent.id }
        }
    }
    return data;
}

const saveConfig = dockview => {
    if (dockview.params.options.enableLocalStorage && dockview._inited === true) {
        const json = dockview.toJSON()
        localStorage.setItem(dockview.params.options.localStorageKey, JSON.stringify(json));
    }
}

export { getConfigFromStorage, getConfig, reloadFromConfig, saveConfig, loadPanelsFromLocalstorage };
