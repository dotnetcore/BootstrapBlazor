const lockDock = dock => {
    const stacks = dock.layout.getAllStacks()
    stacks.forEach(stack => {
        if (dock.lock) {
            lockStack(stack, dock)
        }
        else {
            unlockStack(stack, dock)
        }
    })
    dock.layout.emit('lockChanged')
}

const resetDockLock = dock => {
    const unlocks = dock.layout.getAllContentItems().filter(com => com.isComponent && !com.container.getState().lock)
    const lock = unlocks.length === 0
    if (dock.lock !== lock) {
        dock.lock = lock
        dock.invokeLockAsync(lock)
    }
}

const lockStack = (stack, dock) => {
    const eventsData = dock.eventsData

    if (!eventsData.has(stack)) {
        eventsData.set(stack, stack)

        stack.lockElement.classList.add('lock')
        stack.header.tabs.forEach(tab => {
            lockTab(tab, eventsData)
        })
    }
}

const unlockStack = (stack, dock) => {
    const eventsData = dock.eventsData

    if (eventsData.has(stack)) {
        eventsData.delete(stack)

        stack.lockElement.classList.remove('lock')
        stack.header.tabs.forEach(tab => {
            unlockTab(tab, eventsData)
        })
    }
}

const lockTab = (tab, eventsData) => {
    if (!eventsData.has(tab)) {
        tab.disableReorder()
        tab.onCloseClick = () => { }
        eventsData.set(tab, tab.onCloseClick)
        tab.componentItem.container.getState().lock = true
    }
}

const unlockTab = (tab, eventsData) => {
    if (eventsData.has(tab)) {
        tab.enableReorder()
        tab.onCloseClick = eventsData.get(tab)
        eventsData.delete(tab)
        tab.componentItem.container.getState().lock = false
    }
}

const toggleComponent = (dock, option) => {
    const items = getAllItemsByType('component', option);
    const comps = dock.layout.getAllContentItems().filter(s => s.isComponent);
    const stacks = dock.layout.getAllContentItems().filter(s => s.isStack);

    // gt 没有 items 有时添加
    items.forEach(v => {
        const c = comps.find(i => i.id === v.id)
        if (c === void 0) {
            if (dock.layout.root.contentItems.length === 0) {
                const componentItem = dock.layout.createAndInitContentItem({ type: option.content[0].type, content: [] }, dock.layout.root)
                dock.layout.root.addChild(componentItem)
            }
            if (dock.layout.root.contentItems[0].isStack) {
                const typeConfig = goldenLayout.ResolvedItemConfig.createDefault(option.content[0].type)
                const rowOrColumn = dock.layout.root.layoutManager.createContentItem(typeConfig, dock.layout.root)
                const stack = dock.layout.root.contentItems[0]
                dock.layout.root.replaceChild(stack, rowOrColumn)
                rowOrColumn.addChild(stack)
                rowOrColumn.addItem(v)
                rowOrColumn.updateSize()
            }
            else {
                const stack = stacks.find(s => s.id == v.parent.id);
                if (stack) {
                    stack.addItem(v);
                }
                else if (v.parent.type === 'stack' && stacks.length > 0) {
                    stacks.pop().addItem(v);
                }
                else {
                    dock.layout.root.contentItems[0].addItem(v);
                }
            }

            if (v.componentState.lock) {
                const component = dock.layout.getAllContentItems().find(i => i.isComponent && i.id === v.id)
                lockStack(component.parentItem, dock)
            }
        }
    })

    // gt 有 items 没有时移除
    comps.forEach(v => {
        const c = items.find(i => i.id === v.id)
        if (c === void 0) {
            closeItem(dock, v)
        }
        else if (v.title !== c.title) {
            // 更新 Title
            v.setTitle(c.title)
        }
    })
}

const closeItem = (dock, component) => {
    const { template } = dock;
    const item = document.getElementById(component.id)
    if (item) {
        template.append(item)
    }
    const parent = component.parent
    parent.removeChild(component)

}

const removeContent = (content, item) => {
    content.forEach(v => {
        if (Array.isArray(v.content)) {
            const index = v.content.indexOf(item)
            if (index > -1) {
                v.content.splice(index, 1)
            }
            else {
                removeContent(v.content, item)
            }
        }
    })
}

const getAllItemsByType = (type, parent) => {
    const items = []

    parent.content.forEach(v => {
        if (v.type === type) {
            v.parent = parent;
            items.push(v)
        }

        if (v.content != null) {
            items.push.apply(items, getAllItemsByType(type, v))
        }
    })
    return items
}

export { lockDock, resetDockLock, lockStack, unlockStack, lockTab, toggleComponent, getAllItemsByType }
