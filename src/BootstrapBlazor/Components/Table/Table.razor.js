export { getResponsive } from '../../modules/responsive.js?v=$version'
import { copy, drag, getDescribedElement, getOuterHeight, getWidth } from '../../modules/utility.js?v=$version'
import '../../modules/browser.js?v=$version'
import Data from '../../modules/data.js?v=$version'
import EventHandler from '../../modules/event-handler.js?v=$version'

const setBodyHeight = table => {
    const el = table.el
    const body = table.body || table.tables[0]
    const search = el.querySelector('.table-search')
    table.search = search
    let searchHeight = 0
    if (search) {
        searchHeight = getOuterHeight(search)
    }
    const pagination = el.querySelector('.nav-pages')
    let paginationHeight = 0
    if (pagination) {
        paginationHeight = getOuterHeight(pagination)
    }
    const toolbar = el.querySelector('.table-toolbar')
    let toolbarHeight = 0
    if (toolbar) {
        toolbarHeight = getOuterHeight(toolbar)
    }
    const bodyHeight = paginationHeight + toolbarHeight + searchHeight;
    if (bodyHeight > 0) {
        body.parentNode.style.height = `calc(100% - ${bodyHeight}px)`
    }
    let headerHeight = 0
    if (table.thead) {
        headerHeight = getOuterHeight(table.thead)
    }
    if (headerHeight > 0) {
        body.style.height = `calc(100% - ${headerHeight}px)`
    }
}

const fixHeader = table => {
    const el = table.el
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

    setBodyHeight(table)
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

const resetTableWidth = table => {
    table.tables.forEach(t => {
        const group = [...t.children].find(i => i.nodeName === 'COLGROUP')
        if (group) {
            let width = 0;
            [...group.children].forEach(col => {
                width += parseInt(col.style.width)
            })
            t.style.width = `${width}px`
        }
    })
}

const setResizeListener = table => {
    const eff = (col, toggle) => {
        const th = col.closest('th')
        if (toggle) th.classList.add('border-resize')
        else th.classList.remove('border-resize')

        const index = [].indexOf.call(th.parentNode.children, th);
        table.tables.forEach(t => {
            const body = [...t.children].find(i => i.nodeName === 'TBODY')
            if (body) {
                const rows = [...body.children].filter(i => i.nodeName === 'TR')
                rows.forEach(row => {
                    if (!row.classList.contains('is-detail')) {
                        const td = row.children.item(index)
                        if (toggle) td.classList.add('border-resize')
                        else {
                            td.classList.remove('border-resize')
                            if (td.classList.length === 0) {
                                td.removeAttribute('class')
                            }
                        }
                    }
                })
            }
        })
        return index
    }

    let colWidth = 0
    let tableWidth = 0
    let colIndex = 0
    let originalX = 0

    disposeColumnDrag(table.columns)
    table.columns = []
    const columns = [...table.tables[0].querySelectorAll('.col-resizer')]
    columns.forEach((col, index) => {
        if (table.thead && index === columns.length - 1) {
            col.classList.add('last')
            return
        }
        else {
            col.classList.remove('last')
        }
        table.columns.push(col)
        EventHandler.on(col, 'click', e => e.stopPropagation())
        drag(col,
            e => {
                colIndex = eff(col, true)
                const table = col.closest('table')
                const currentCol = table.querySelectorAll('colgroup col')[colIndex]
                const width = currentCol.style.width
                if (width) {
                    colWidth = parseInt(width)
                }
                else {
                    colWidth = getWidth(col.closest('th'))
                }
                tableWidth = getWidth(col.closest('table'))
                originalX = e.clientX
            },
            e => {
                const marginX = e.clientX - originalX
                table.tables.forEach(t => {
                    const group = [...t.children].find(i => i.nodeName === 'COLGROUP')
                    if (group) {
                        const curCol = group.children.item(colIndex)
                        curCol.style.width = `${colWidth + marginX}px`
                        const tableEl = curCol.closest('table')
                        let width = tableWidth + marginX
                        if (t.closest('.table-fixed-body')) {
                            width = width - 6
                        }
                        tableEl.setAttribute('style', `width: ${width}px;`)
                    }
                })
            },
            () => {
                eff(col, false)
                if (table.callbacks.resizeColumnCallback) {
                    const width = getWidth(col.parentNode);
                    table.invoke.invokeMethodAsync(table.callbacks.resizeColumnCallback, index, width)
                }
            }
        )
    })
}

const setCopyColumn = table => {
    const copyCellValue = td => {
        let ret;
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
        if (table.thead) {
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

const disposeColumnDrag = columns => {
    columns = columns || []
    columns.forEach(col => {
        EventHandler.off(col, 'click')
        EventHandler.off(col, 'mousedown')
        EventHandler.off(col, 'touchstart')
    })
}

const setDraggable = table => {
    let dragItem = null;
    let index = 0
    table.dragColumns = [...table.tables[0].querySelectorAll('thead > tr > th')].filter(i => i.draggable)
    table.dragColumns.forEach(col => {
        EventHandler.on(col, 'dragstart', e => {
            col.parentNode.classList.add('table-dragging')
            col.classList.add('table-drag')
            index = table.dragColumns.indexOf(col)
            dragItem = col
            e.dataTransfer.effectAllowed = 'move'
        })
        EventHandler.on(col, 'dragend', e => {
            col.parentNode.classList.remove('table-dragging')
            dragItem.classList.remove('table-drag')
            table.dragColumns.forEach(i => {
                i.classList.remove('table-drag-over')
            })
            dragItem = null
        })
        EventHandler.on(col, 'drop', e => {
            e.stopPropagation()
            e.preventDefault()
            if (table.callbacks.dragColumnCallback) {
                table.invoke.invokeMethodAsync(table.callbacks.dragColumnCallback, index, table.dragColumns.indexOf(col))
            }
            return false
        })
        EventHandler.on(col, 'dragenter', e => {
            e.preventDefault()
            if (dragItem !== col) {
                col.classList.add('table-drag-over')
            }
        })
        EventHandler.on(col, 'dragover', e => {
            e.preventDefault()
            if (dragItem !== col) {
                e.dataTransfer.dropEffect = 'move'
            }
            else {
                e.dataTransfer.dropEffect = 'none'
            }
            return false
        })
        EventHandler.on(col, 'dragleave', e => {
            e.preventDefault()
            col.classList.remove('table-drag-over')
        })
    })
}

const disposeDragColumns = columns => {
    columns = columns || []
    columns.forEach(col => {
        EventHandler.off(col, 'dragstart')
        EventHandler.off(col, 'dragend')
        EventHandler.off(col, 'drop')
        EventHandler.off(col, 'dragenter')
        EventHandler.off(col, 'dragover')
        EventHandler.off(col, 'dragleave')
    })
}

export function init(id, invoke, callbacks) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const table = {
        el,
        invoke,
        callbacks,
        columns: [],
        tables: [],
        dragColumns: []
    }
    Data.set(id, table)
    const shim = [...el.children].find(i => i.classList.contains('table-shim'))
    if (shim === undefined) {
        return
    }
    table.thead = [...shim.children].find(i => i.classList.contains('table-fixed-header'))
    table.isResizeColumn = shim.classList.contains('table-resize')
    if (table.thead) {
        table.isExcel = table.thead.firstChild.classList.contains('table-excel')
        table.body = [...shim.children].find(i => i.classList.contains('table-fixed-body'))
        table.isDraggable = table.thead.firstChild.classList.contains('table-draggable')
        table.tables.push(table.thead.firstChild)
        table.tables.push(table.body.firstChild)
        fixHeader(table)

        EventHandler.on(table.body, 'scroll', () => {
            const left = table.body.scrollLeft
            table.thead.scrollTo(left, 0)
        });
    }
    else {
        table.isExcel = shim.firstChild.classList.contains('table-excel')
        table.isDraggable = shim.firstChild.classList.contains('table-draggable')
        table.tables.push(shim.firstChild)
        setBodyHeight(table)
    }

    if (table.isExcel) {
        setExcelKeyboardListener(table)
    }

    if (table.isResizeColumn) {
        setResizeListener(table)
    }

    if (table.isDraggable) {
        setDraggable(table)
    }

    setCopyColumn(table)

    if(table.search) {
        const observerSearch = new ResizeObserver(() => {
            setBodyHeight(table)
        });
        observerSearch.observe(table.search)
        table.observerSearch = observerSearch
    }

    if (table.thead) {
        const observerBody = new ResizeObserver(() => {
            setBodyHeight(table)
        });
        observerBody.observe(table.el)
        table.observerBody = observerBody
    }
}

export function resetColumn(id) {
    const table = Data.get(id)
    setResizeListener(table)
    resetTableWidth(table)
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
        if (table.thead) {
            EventHandler.off(table.body, 'scroll')
        }

        if (table.isExcel) {
            EventHandler.off(table.element, 'keydown')
        }

        disposeColumnDrag(table.columns)
        disposeDragColumns(table.dragColumns)
        EventHandler.off(table.element, 'click', '.col-copy')

        if (table.observerSearch) {
            table.observerSearch.disconnect()
        }

        if (table.observerBody) {
            table.observerBody.disconnect()
        }
    }
}
