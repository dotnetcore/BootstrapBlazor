import { getResponsive } from "../../modules/responsive.js?v=$version"
import { copy, drag, getDescribedElement, getHeight, getWidth } from "../../modules/utility.js?v=$version"
import * as browser from "../../modules/browser.js?v=$version"
import Data from "../../modules/data.js?v=$version"
import EventHandler from "../../modules/event-handler.js?v=$version"

const fixHeader = table => {
    const el = table.el
    const body = table.body
    const thead = table.thead

    const fs = el.querySelector('.fixed-scroll')
    if (fs) {
        let prev = fs.previousElementSibling
        while (prev) {
            if (prev.classList.contains('fixed-right') && !prev.classList.contains('modified')) {
                let margin = prev.style.right
                margin = margin.replace('px', '')
                const b = window.browser()
                if (b.device !== 'PC') {
                    margin = (parseFloat(margin) - 6) + 'px'
                }
                prev.classList.add('modified')
                prev.style.right = margin
                prev = prev.previousElementSibling
            }
            else {
                break
            }
        }
    }

    const setBodyHeight = () => {
        const search = el.querySelector('.table-search')
        table.search = search
        let searchHeight = 0
        if (search) {
            searchHeight = getHeight(search)
        }
        const pagination = el.querySelector('.nav-pages')
        let paginationHeight = 0
        if (pagination) {
            paginationHeight = getHeight(pagination)
        }
        const toolbar = el.querySelector('.table-toolbar')
        let toolbarHeight = 0
        if (toolbar) {
            toolbarHeight = getHeight(toolbar)
        }
        const bodyHeight = paginationHeight + toolbarHeight + searchHeight;
        if (bodyHeight > 0) {
            body.parentNode.style.height = `calc(100% - ${bodyHeight}px)`
        }
        const headerHeight = getHeight(table.thead)
        if (headerHeight > 0) {
            body.style.height = `calc(100% - ${headerHeight}px)`
        }
    }

    setBodyHeight()

    if (table.search) {
        // handler collapse event
        // TODO: 搜索栏初始化是转圈的，要先计算高度，转圈结束后再次计算，展开收缩时需要再次计算
        EventHandler.on(table.search, 'shown.bs.collapse', () => setBodyHeight())
        EventHandler.on(table.search, 'hidden.bs.collapse', () => setBodyHeight())
    }
}

const setExcelKeyboardListener = table => {
    const KeyCodes = {
        TAB: 9,
        ENTER: 13,
        SHIFT: 16,
        CTRL: 17,
        ALT: 18,
        ESCAPE: 27,
        SPACE: 32,
        PAGE_UP: 33,
        PAGE_DOWN: 34,
        END: 35,
        HOME: 36,
        LEFT_ARROW: 37,
        UP_ARROW: 38,
        RIGHT_ARROW: 39,
        DOWN_ARROW: 40
    }

    const setFocus = target => {
        const handler = setTimeout(function () {
            clearTimeout(handler);
            target.focus();
            target.select();
        }, 10);
    }

    const activeCell = (cells, index) => {
        let ret = false;
        const td = cells[index];
        const target = td.querySelector('input.form-control:not([readonly])');
        if (target) {
            setFocus(target);
            ret = true;
        }
        return ret;
    }

    const moveCell = (input, keyCode) => {
        const td = input.closest('td');
        const tr = td.parentNode;
        let cells = [...tr.children];
        let index = cells.indexOf(td);
        if (keyCode === KeyCodes.LEFT_ARROW) {
            while (--index >= 0) {
                if (activeCell(cells, index)) {
                    break;
                }
            }
        } else if (keyCode === KeyCodes.RIGHT_ARROW) {
            while (++index < cells.length) {
                if (activeCell(cells, index)) {
                    break;
                }
            }
        } else if (keyCode === KeyCodes.UP_ARROW) {
            cells = tr.previousElementSibling.children;
            while (index < cells.length) {
                if (activeCell(cells, index)) {
                    break;
                }
            }
        } else if (keyCode === KeyCodes.DOWN_ARROW) {
            cells = tr.nextElementSibling.children;
            while (index < cells.length) {
                if (activeCell(cells, index)) {
                    break;
                }
            }
        }
    }

    const getCaretPosition = element => {
        let result = -1;
        const startPosition = element.selectionStart;
        const endPosition = element.selectionEnd;
        if (startPosition === endPosition) {
            if (startPosition === element.value.length)
                result = 1;
            else if (startPosition === 0) {
                result = 0;
            }
        }
        return result;
    }

    EventHandler.on(table.el, 'keydown', e => {
        switch (e.keyCode) {
            case KeyCodes.UP_ARROW:
            case KeyCodes.LEFT_ARROW:
                if (getCaretPosition(e.target) === 0) {
                    moveCell(e.target, e.keyCode)
                }
                break
            case KeyCodes.DOWN_ARROW:
            case KeyCodes.RIGHT_ARROW:
                if (getCaretPosition(e.target) === 1) {
                    moveCell(e.target, e.keyCode)
                }
                break
        }
    })
}

const setResizeListener = table => {
    const eff = (col, toggle) => {
        const th = col.closest('th')
        if (toggle) th.classList.add('border-resize')
        else th.classList.remove('border-resize')

        const index = [].indexOf.call(th.parentNode.children, th);
        th.closest('.table-resize').querySelectorAll('.table > tbody > tr').forEach(tr => {
            if (!tr.classList.contains('is-detail')) {
                const td = tr.children.item(index)
                if (toggle) td.classList.add('border-resize')
                else td.classList.remove('border-resize')
            }
        });
        return index
    }

    let colWidth = 0
    let tableWidth = 0
    let colIndex = 0
    let originalX = 0

    // 固定表头的最后一列禁止列宽调整
    const el = table.el
    const columns = [...el.querySelectorAll('.col-resizer')]
    if (table.fixedHeader) {
        const last = columns.pop()
        if (last) {
            last.remove();
        }
    }

    columns.forEach(col => {
        table.columns.push(col)
        drag(col,
            e => {
                colIndex = eff(col, true)
                const currentCol = el.querySelectorAll('table colgroup col')[colIndex]
                const width = currentCol.style.width
                if (width) {
                    colWidth = parseInt(width)
                } else {
                    colWidth = getWidth(col.closest('th'))
                }
                tableWidth = getWidth(col.closest('table'))
                originalX = e.clientX
            },
            e => {
                const marginX = e.clientX - originalX
                el.querySelectorAll('table colgroup').forEach(group => {
                    const curCol = group.children.item(colIndex)
                    curCol.style.width = `${colWidth + marginX}px`
                    const table = curCol.closest('table')
                    const width = tableWidth + marginX
                    if (table.fixedHeader) {
                        table.style.width = `${width}px;`
                    } else {
                        curCol.closest('table').style.width = (width - 6) + 'px'
                    }
                })
            },
            () => {
                eff(col, false)
            }
        )
    })
}

const setCopyColumn = table => {
    const copyCellValue = td => {
        let ret = null;
        let input = td.querySelector('.datetime-picker-input')
        if (input === null) {
            input = td.querySelector('.form-select')
        }
        if (input === null) {
            input = td.querySelector('.switch')
            if (input) {
                if (input.classList.contains('is-checked')) {
                    ret = 'True'
                }
                else {
                    ret = 'False'
                }
                return ret
            }
        }
        if (input) {
            ret = input.value
        }
        else {
            ret = td.textContent
        }
        return ret
    }

    const el = table.el
    EventHandler.on(el, 'click', '.col-copy', e => {
        const index = e.delegateTarget.closest('th').cellIndex
        let rows
        if (table.fixedHeader) {
            rows = table.body.querySelectorAll('table > tbody > tr')
        }
        else if (el.querySelector('.table-fixed-column')) {
            rows = el.querySelectorAll('.table-scroll > .overflow-auto > table > tbody > tr')
        }
        else {
            rows = el.querySelectorAll('.table-scroll > table > tbody > tr')
        }

        let content = ''
        rows.forEach(row => {
            if (!row.classList.contains('is-detail')) {
                const td = row.childNodes[index]
                const v = copyCellValue(td)
                if (v !== null) {
                    content += `${v}\n`
                }
            }
        })
        copy(content)
        const span = e.delegateTarget.parentNode
        const tooltip = getDescribedElement(span)
        const tip = bootstrap.Tooltip.getInstance(span)
        if (tooltip) {
            tooltip.querySelector('.tooltip-inner').innerHTML = span.getAttribute('data-bb-title')
            tip.update()
        }

        const handler = window.setTimeout(() => {
            window.clearTimeout(handler)
            if (tooltip) {
                tooltip.querySelector('.tooltip-inner').innerHTML = span.getAttribute('data-bs-original-title')
                tip.update()
            }
        }, 1000);
    })
}

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const table = {
        el,
        fixedHeader: el.querySelector('.table-fixed') != null,
        isExcel: el.querySelector('.table-excel') != null,
        isResizeColumn: el.querySelector('.col-resizer') != null,
        columns: []
    }
    Data.set(id, table)

    if (table.fixedHeader) {
        table.thead = el.querySelector('.table-fixed-header')
        table.body = el.querySelector('.table-fixed-body')
        fixHeader(table)

        EventHandler.on(table.body, 'scroll', () => {
            const left = table.body.scrollLeft
            table.thead.scrollTo(left, 0)
        });
    }

    if (table.isExcel) {
        setExcelKeyboardListener(table)
    }

    if (table.isResizeColumn) {
        setResizeListener(table)
    }

    setCopyColumn(table)
}

export function sort(id) {
    const table = Data.get(id)
    const el = table.el

    const span = el.querySelector('.sortable .table-text[aria-describedby]')
    if (span) {
        const tooltip = getDescribedElement(span)
        if (tooltip) {
            tooltip.querySelector('.tooltip-inner').innerHTML = span.getAttribute('data-bs-original-title')
        }
    }
}

export function load(id, method) {
    const table = Data.get(id)

    const loader = table.el.querySelector('.table-loader')
    if (method === 'show') {
        loader.classList.add('show')
    } else {
        loader.classList.remove('show')
    }
}

export function dispose(id) {
    const table = Data.get(id)
    Data.remove(id)

    if (table) {
        if (table.fixedHeader) {
            EventHandler.off(table.body, 'scroll')
        }

        if (table.isExcel) {
            EventHandler.off(table.element, 'keydown')
        }

        table.columns.forEach(col => {
            EventHandler.off(col, 'mousedown')
            EventHandler.off(col, 'touchstart')
        })

        EventHandler.off(table.element, 'click', '.col-copy')

        if (table.search) {
            EventHandler.off(table.search, 'shown.bs.collapse')
            EventHandler.off(table.search, 'hidden.bs.collapse')
        }
    }
}

export {
    getResponsive
}
