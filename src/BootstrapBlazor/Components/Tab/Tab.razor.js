import Data from "../../modules/data.js"
import EventHandler from "../../modules/event-handler.js"

const getPosition = el => {
    const rect = el.getBoundingClientRect()
    return rect;
}

const fixSize = function (el) {
    const height = el.offsetHeight
    const width = el.offsetWidth
    el.style.height = `${height}px`
    el.style.width = `${width}px`
}

const resize = tab => {
    const el = tab.el
    const tabNav = tab.tabNav
    const wrap = tab.wrap
    if (wrap.classList.contains('extend')) {
        return
    }
    const scroll = tab.scroll

    tab.vertical = el.classList.contains('tabs-left') || el.classList.contains('tabs-right')
    tab.horizontal = el.classList.contains('tabs-top') || el.classList.contains('tabs-bottom')

    const lastItem = [...tabNav.querySelectorAll('.tabs-item')].pop()
    if (lastItem) {
        if (tab.vertical) {
            wrap.style.height = `${el.offsetHeight}px`
            const tabHeight = tabNav.offsetHeight
            const itemHeight = getPosition(lastItem).top + lastItem.offsetHeight
        }
        else {
            // Item 总宽度大于 Nav 宽度
            const tabWidth = tabNav.offsetWidth
            let itemWidth = 0
            tabNav.querySelectorAll('.tabs-item').forEach(v => {
                itemWidth += v.offsetWidth
            })
            if (itemWidth > tabWidth) {
                wrap.classList.add('of')
            }
        }
    }
}

const active = tab => {
    resize(tab)

    const activeTab = tab.tabNav.querySelector('.tabs-item.active')
    if (activeTab) {
        // mark sure display total active tabitem
        const right = getPosition(activeTab).left - getPosition(activeTab.parentNode).left + activeTab.offsetWidth
        const navWidth = tab.scroll.offsetWidth
        const marginX = navWidth - right + 2
        if (marginX < 0) {
            tab.tabNav.style.transform = `translateX(${marginX}px)`
        }
        else {
            tab.tabNav.style.transform = null
        }
    }

    const bar = tab.tabNav.querySelector('.tabs-active-bar')
    if (bar === null) {
        return
    }
    if (tab.vertical) {
        const top = getPosition(activeTab).top - getPosition(activeTab.parentNode).top
        bar.style.width = '2px'
        bar.style.transform = `translateY(${top}px)`
    }
    else {
        const left = getPosition(activeTab).left - getPosition(activeTab.parentNode).left
        const width = activeTab.offsetWidth
        bar.style.width = `${width}px`
        bar.style.transform = `translateX(${left}px)`
    }
}

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const tab = { el }
    Data.set(id, tab)

    tab.header = el.firstChild
    tab.wrap = tab.header.firstChild
    tab.scroll = tab.wrap.querySelector('.tabs-nav-scroll')
    tab.tabNav = tab.scroll.firstChild

    tab.resizeHandler = () => {
        resize(tab)
    }

    EventHandler.on(window, 'resize', tab.resizeHandler)
    active(tab)
}

export function update(id) {
    const tab = Data.get(id)
    if (tab) {
        active(tab)
    }
}

export function dispose(id) {
    const tab = Data.get(id)
    Data.remove(id)

    if (tab) {
        EventHandler.off(window, 'resize', tab.resizeHandler)
    }
}
