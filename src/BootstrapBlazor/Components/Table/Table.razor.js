export { getResponsive } from '../../modules/responsive.js'
import { copy, drag, getDescribedElement, getOuterHeight, getWidth, isVisible } from '../../modules/utility.js'
import '../../modules/browser.js'
import Data from '../../modules/data.js'
import EventHandler from '../../modules/event-handler.js'
import Popover from "../../modules/base-popover.js"

export function init(id, invoke, options) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }
    const table = {
        el,
        invoke,
        options,
        handlers: {}
    }
    Data.set(id, table)

    reset(id)
}

export function reloadColumnWidth(tableName) {
    const key = `bb-table-column-width-${tableName}`
    return localStorage.getItem(key);
}

export function reloadColumnOrder(tableName) {
    const key = `bb-table-column-order-${tableName}`
    return JSON.parse(localStorage.getItem(key)) ?? [];
}

export function saveColumnOrder(options) {
    const key = `bb-table-column-order-${options.tableName}`
    localStorage.setItem(key, JSON.stringify(options.columns));
}

export function reset(id) {
    const table = Data.get(id)
    if (table === null) {
        return;
    }

    table.columns = [];
    table.tables = [];
    table.dragColumns = [];
    table.thead = null;
    table.toolbar = null;
    table.pages = null;

    const shim = [...table.el.children].find(i => i.classList.contains('table-shim'))
    if (shim !== void 0) {
        table.shim = shim;
        table.thead = [...shim.children].find(i => i.classList.contains('table-fixed-header'))
        table.isResizeColumn = shim.classList.contains('table-resize')
        if (table.thead) {
            table.isExcel = table.thead.firstChild.classList.contains('table-excel')
            table.body = [...shim.children].find(i => i.classList.contains('table-fixed-body'))
            table.isDraggable = table.thead.firstChild.classList.contains('table-draggable')
            table.tables.push(table.thead.firstChild)
            table.tables.push(table.body.firstChild)
            table.scrollWidth = parseFloat(table.body.style.getPropertyValue('--bb-scroll-width'));
            fixHeader(table)

            EventHandler.on(table.body, 'scroll', () => {
                const left = table.body.scrollLeft
                table.thead.scrollTo(left, 0)
            });

            setTableDefaultWidth(table);
        }
        else {
            table.isExcel = shim.firstChild.classList.contains('table-excel')
            table.isDraggable = shim.firstChild.classList.contains('table-draggable')
            table.tables.push(shim.firstChild)
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
    }

    // popover
    const toolbar = [...table.el.children].find(i => i.classList.contains('table-toolbar'))
    if (toolbar) {
        const right = toolbar.querySelector('.table-column-right')
        if (right) {
            setToolbarDropdown(table, right)
        }
        table.toolbar = toolbar;
    }

    table.pages = [...table.el.children].find(i => i.classList.contains('nav-pages'));


    setColumnToolboxListener(table);

    if (isVisible(table.el) === false) {
        table.loopCheckHeightHandler = requestAnimationFrame(() => check(table));
        return;
    }

    observeHeight(table)
}

const observeHeight = table => {
    setBodyHeight(table);

    const observer = new ResizeObserver(entries => {
        entries.forEach(entry => {
            if (entry.target === table.shim) {
                setTableDefaultWidth(table);
            }
            else if (entry.target === table.search || entry.target === table.toolbar || entry.target === table.pages) {
                setBodyHeight(table)
            }
        });
    });
    if (table.thead) {
        observer.observe(table.shim);
    }
    if (table.search) {
        observer.observe(table.search);
    }
    if (table.toolbar) {
        observer.observe(table.toolbar);
    }
    if (table.pages) {
        observer.observe(table.pages);
    }
    table.observer = observer;
}

export function resetColumn(id) {
    const table = Data.get(id)
    if (table) {
        setResizeListener(table)
        resetTableWidth(table)
    }
}

export function bindResizeColumn(id) {
    const table = Data.get(id)
    if (table) {
        if (table.isResizeColumn) {
            setResizeListener(table)
        }
    }
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
    if (table) {
        const loader = [...table.el.children].find(el => el.classList.contains('table-loader'));
        if (method === 'show') {
            loader.classList.add('show')
        }
        else {
            loader.classList.remove('show')
        }
    }
}

export function scroll(id, align, options = { behavior: 'smooth' }) {
    const element = document.getElementById(id);
    if (element) {
        const selectedRow = [...element.querySelectorAll('.form-check.is-checked')].pop();
        if (selectedRow) {
            const row = selectedRow.closest('tr');
            if (row) {
                options.block = align;
                row.scrollIntoView(options);
            }
        }
    }
}

export function scrollTo(id, x = 0, y = 0, options = { behavior: 'smooth' }) {
    const element = document.getElementById(id);
    if (element) {
        const scroll = element.querySelector('.scroll');
        if (scroll) {
            scroll.scrollTo(x, y, options);
        }
    }
}

export function toggleView(id) {
    const table = Data.get(id);
    destroyTable(table);

    reset(id);
}

export function dispose(id) {
    const table = Data.get(id)
    Data.remove(id);

    destroyTable(table);
}

const destroyTable = table => {
    if (table) {
        if (table.loopCheckHeightHandler) {
            cancelAnimationFrame(table.loopCheckHeightHandler);
        }
        if (table.thead) {
            EventHandler.off(table.body, 'scroll')
        }

        if (table.isExcel) {
            EventHandler.off(table.element, 'keydown')
        }

        disposeColumnDrag(table.columns)
        disposeDragColumns(table.dragColumns)

        if (table.element) {
            EventHandler.off(table.element, 'click', '.col-copy');
        }

        if (table.handlers.setResizeHandler) {
            EventHandler.off(document, 'click', table.handlers.setResizeHandler);
        }
        if (table.handlers.setColumnToolboxHandler) {
            EventHandler.off(document, 'click', table.handlers.setColumnToolboxHandler);
        }
        if (table.observer) {
            table.observer.disconnect();
            table.observer = null;
        }

        if (table.popovers) {
            table.popovers.forEach(p => {
                Popover.dispose(p)
            })
        }
    }
}

const setColumnToolboxListener = table => {
    const header = table.tables[0];
    if (header) {
        const toolbox = header.querySelector('.toolbox-icon')
        if (toolbox) {
            table.handlers.setColumnToolboxHandler = e => {
                const target = e.target;
                if (target.closest('.popover-table-column-toolbox')) {
                    return;
                }

                [...header.querySelectorAll('.toolbox-icon')].forEach(toolbox => {
                    const popover = bootstrap.Popover.getInstance(toolbox);
                    if (popover && popover._isShown()) {
                        popover.hide();
                    }
                });
            }
            EventHandler.on(document, 'click', table.handlers.setColumnToolboxHandler);
        }
    }
}

const check = table => {
    const el = table.el;
    if (isVisible(el) === false) {
        table.loopCheckHeightHandler = requestAnimationFrame(() => check(table));
    }
    else {
        if (table.loopCheckHeightHandler > 0) {
            cancelAnimationFrame(table.loopCheckHeightHandler);
            delete table.loopCheckHeightHandler;
        }
        observeHeight(table);
    }
};

const setBodyHeight = table => {
    const el = table.el
    const children = [...el.children]
    const search = children.find(i => i.classList.contains('table-search'))
    table.search = search;

    let searchHeight = 0
    if (search) {
        searchHeight = getOuterHeight(search)
    }

    const pagination = children.find(i => i.classList.contains('nav-pages'))
    let paginationHeight = 0
    if (pagination) {
        paginationHeight = getOuterHeight(pagination)
    }

    const toolbar = children.find(i => i.classList.contains('table-toolbar'))
    let toolbarHeight = 0
    if (toolbar) {
        toolbarHeight = getOuterHeight(toolbar)
    }

    const bodyHeight = paginationHeight + toolbarHeight + searchHeight;
    const card = children.find(i => i.classList.contains('table-card'))
    if (card) {
        card.style.height = `calc(100% - ${bodyHeight}px)`
    }
    else {
        const body = table.body ?? table.tables[0];
        if (bodyHeight > 0 && body && body.parentNode) {
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
                    margin = (parseFloat(margin) - table.scrollWidth) + 'px'
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
            if (target.focus) {
                target.focus();
            }
            if (target.select) {
                target.select();
            }
        }, 10);
    }

    const activeCell = (cells, index) => {
        let ret = false;
        const td = cells[index];
        const target = td.querySelector('.form-control:not([readonly])');
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
        }
        else if (keyCode === KeyCodes.RIGHT_ARROW) {
            while (++index < cells.length) {
                if (activeCell(cells, index)) {
                    break;
                }
            }
        }
        else if (keyCode === KeyCodes.UP_ARROW) {
            let nextRow = tr.previousElementSibling;
            while (nextRow) {
                cells = nextRow.children;
                if (cells) {
                    if (activeCell(cells, index)) {
                        break;
                    }
                    else {
                        nextRow = nextRow.previousElementSibling;
                    }
                }
            }
        }
        else if (keyCode === KeyCodes.DOWN_ARROW) {
            let nextRow = tr.nextElementSibling;
            while (nextRow) {
                cells = nextRow.children;
                if (cells) {
                    if (activeCell(cells, index)) {
                        break;
                    }
                    else {
                        nextRow = nextRow.nextElementSibling;
                    }
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
    if (table.options.showColumnWidthTooltip) {
        table.handlers.setResizeHandler = e => {
            const element = e.target;
            const tips = element.closest('.table-resizer-tips');
            if (tips) {
                return;
            }

            closeAllTips(table.columns, null);
        }
        EventHandler.on(document, 'click', table.handlers.setResizeHandler);
    }

    disposeColumnDrag(table.columns)
    table.columns = []

    if (table.tables.length === 0) {
        return;
    }

    const eff = (col, toggle) => {
        const th = col.closest('th')
        if (th.parentNode === null) {
            EventHandler.off(col, 'click')
            EventHandler.off(col, 'mousedown')
            EventHandler.off(col, 'touchstart')

            table.tables.forEach(t => {
                const cells = t.querySelectorAll('.border-resize');
                cells.forEach(c => c.classList.remove('border-resize'))
            })

            return
        }

        if (toggle) th.classList.add('border-resize')
        else th.classList.remove('border-resize')

        const index = [].indexOf.call(th.parentNode.children, th);
        table.tables.forEach(t => {
            const body = [...t.children].find(i => i.nodeName === 'TBODY')
            if (body) {
                const rows = [...body.children].filter(i => i.nodeName === 'TR')
                rows.forEach(row => {
                    if (!row.classList.contains('is-detail')) {
                        const td = row.children.item(index);
                        if (td) {
                            if (toggle) td.classList.add('border-resize')
                            else {
                                td.classList.remove('border-resize')
                                if (td.classList.length === 0) {
                                    td.removeAttribute('class')
                                }
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

    const columns = [...table.tables[0].querySelectorAll('.col-resizer')]
    columns.forEach(col => {
        table.columns.push(col)
        EventHandler.on(col, 'click', e => e.stopPropagation());
        EventHandler.on(col, 'dblclick', async e => {
            e.preventDefault();
            e.stopPropagation();
            await autoFitColumnWidth(table, col);
        });

        setColumnResizingListen(table, col);
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
                originalX = e.clientX ?? e.touches[0].clientX
            },
            e => {
                const eventX = e.clientX ?? e.changedTouches[0].clientX
                const marginX = eventX - originalX
                table.tables.forEach(t => {
                    const group = [...t.children].find(i => i.nodeName === 'COLGROUP')
                    const calcColWidth = colWidth + marginX;
                    if (group) {
                        const curCol = group.children.item(colIndex)
                        curCol.style.setProperty('width', `${calcColWidth}px`);
                        const tableEl = curCol.closest('table')
                        let width = tableWidth + marginX
                        if (t.closest('.table-fixed-body')) {
                            width = width - table.scrollWidth;
                        }
                        tableEl.setAttribute('style', `width: ${width}px;`)

                        if (table.options.showColumnWidthTooltip) {
                            const tip = bootstrap.Tooltip.getInstance(col);
                            if (tip && tip._isShown()) {
                                const inner = tip.tip.querySelector('.tooltip-inner');
                                const tipText = getColumnTooltipTitle(table.options, colWidth + marginX);
                                inner.innerHTML = tipText;
                                tip._config.title = tipText;
                                tip.update();
                            }
                        }

                        const header = col.parentElement;
                        if (header.classList.contains('fixed')) {
                            resizeNextFixedColumnWidth(header, calcColWidth);
                        }
                    }

                    const tbody = [...t.children].find(i => i.nodeName === 'TBODY');
                    if (tbody) {
                        const rows = [...tbody.children].filter(i => i.nodeName === 'TR');
                        rows.forEach(row => {
                            const header = row.children.item(colIndex);
                            if (header.classList.contains('fixed')) {
                                resizeNextFixedColumnWidth(header, calcColWidth);
                            }
                        });
                    }
                })
            },
            () => {
                eff(col, false)
                if (table.options.resizeColumnCallback) {
                    const th = col.closest('th')
                    const width = getWidth(th);
                    const currentIndex = [...table.tables[0].querySelectorAll('thead > tr > th > .col-resizer')].indexOf(col)
                    table.invoke.invokeMethodAsync(table.options.resizeColumnCallback, currentIndex, width)
                }

                saveColumnWidth(table)
            }
        )
    })
}

const resizeNextFixedColumnWidth = (col, width) => {
    if (col.classList.contains('fixed-right')) {
        const nextColumn = col.previousElementSibling;
        if (nextColumn.classList.contains('fixed')) {
            const right = parseFloat(col.style.getPropertyValue('right'));
            nextColumn.style.setProperty('right', `${right + width}px`);
            resizeNextFixedColumnWidth(nextColumn, nextColumn.offsetWidth);
        }
    }
    else if (col.classList.contains('fixed')) {
        const nextColumn = col.nextElementSibling;
        if (nextColumn.classList.contains('fixed')) {
            const left = parseFloat(col.style.getPropertyValue('left'));
            nextColumn.style.setProperty('left', `${left + width}px`);
            resizeNextFixedColumnWidth(nextColumn, nextColumn.offsetWidth);
        }
    }
}

const setColumnResizingListen = (table, col) => {
    if (table.options.showColumnWidthTooltip) {
        EventHandler.on(col, 'mouseenter', e => {
            closeAllTips(table.columns, e.target);
            const th = col.closest('th');
            const tip = bootstrap.Tooltip.getOrCreateInstance(e.target, {
                title: getColumnTooltipTitle(table.options, th.offsetWidth),
                trigger: 'manual',
                placement: 'top',
                customClass: 'table-resizer-tips'
            });
            if (!tip._isShown()) {
                tip.show();
            }
        });
    }
}

const getColumnTooltipTitle = (options, width) => {
    return `${options.columnWidthTooltipPrefix}${width}px`;
}

const indexOfCol = col => {
    const th = col.closest('th');
    const row = th.parentElement;
    return [...row.children].indexOf(th);
}

const autoFitColumnWidth = async (table, col) => {
    const field = col.getAttribute('data-bb-field');
    const widthValue = await table.invoke.invokeMethodAsync(table.options.autoFitContentCallback, field);

    const index = indexOfCol(col);
    let rows = null;
    if (table.thead) {
        rows = table.body.querySelectorAll('table > tbody > tr');
    }
    else {
        rows = table.tables[0].querySelectorAll('table > tbody > tr');
    }

    let maxWidth = 0;
    [...rows].forEach(row => {
        const cell = row.cells[index];
        maxWidth = Math.max(maxWidth, calcCellWidth(cell));
    });

    if (maxWidth > 0) {
        table.tables.forEach(table => {
            const colEl = table.querySelectorAll('colgroup col')[index];
            if (colEl) {
                colEl.style.setProperty('width', `${maxWidth}px`);
            }

            const th = table.querySelectorAll('thead > tr > th')[index];
            if (th) {
                const span = th.querySelector('.table-text');
                span.style.removeProperty('width');
            }
        });

        setTableDefaultWidth(table);
    }
}

const calcCellWidth = cell => {
    const div = document.createElement('div');
    [...cell.children].forEach(c => {
        div.appendChild(c.cloneNode(true));
    })
    div.style.setProperty('visibility', 'hidden');
    div.style.setProperty('white-space', 'nowrap');
    div.style.setProperty('display', 'inline-block');
    div.style.setProperty('position', 'absolute');
    div.style.setProperty('top', '0');
    document.body.appendChild(div);

    const cellStyle = getComputedStyle(cell);
    const width = div.offsetWidth + parseFloat(cellStyle.getPropertyValue('padding-left')) + parseFloat(cellStyle.getPropertyValue('padding-right'));
    div.remove();
    return width;
}

const closeAllTips = (columns, self) => {
    columns.forEach(col => {
        const tip = bootstrap.Tooltip.getInstance(col);
        if (tip && col !== self) {
            if (tip._isShown()) {
                tip.hide();
            }
        }
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
        e.stopPropagation();
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
    (columns ?? []).forEach(col => {
        EventHandler.off(col, 'click');
        EventHandler.off(col, 'dblclick');
        EventHandler.off(col, 'mousedown');
        EventHandler.off(col, 'touchstart');
        EventHandler.off(col, 'mouseenter');

        const tip = bootstrap.Tooltip.getInstance(col);
        if (tip) {
            tip.dispose();
        }
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
            table.dragColumns = [...table.tables[0].querySelectorAll('thead > tr > th')].filter(i => i.draggable)
            index = table.dragColumns.indexOf(col)
            dragItem = col
            e.dataTransfer.effectAllowed = 'move'
        })
        EventHandler.on(col, 'dragend', () => {
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
            if (table.options.dragColumnCallback) {
                table.invoke.invokeMethodAsync(table.options.dragColumnCallback, index, table.dragColumns.indexOf(col))
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
    (columns ?? []).forEach(col => {
        EventHandler.off(col, 'dragstart')
        EventHandler.off(col, 'dragend')
        EventHandler.off(col, 'drop')
        EventHandler.off(col, 'dragenter')
        EventHandler.off(col, 'dragover')
        EventHandler.off(col, 'dragleave')
    })
}

const setToolbarDropdown = (table, toolbar) => {
    table.popovers = [];
    [...toolbar.querySelectorAll('.dropdown-column, .dropdown-export')].forEach(dropdown => {
        const button = dropdown.querySelector('.dropdown-toggle')
        if (button.getAttribute('data-bs-toggle') === 'bb.dropdown') {
            table.popovers.push(Popover.init(dropdown, {
                isDisabled: () => false
            }))
        }
    })
}

const saveColumnWidth = table => {
    const cols = table.columns
    const tableWidth = table.tables[0].offsetWidth
    const tableName = table.tables[0].getAttribute('data-bb-name')
    const key = `bb-table-column-width-${tableName}`
    localStorage.setItem(key, JSON.stringify({
        "cols": cols.map(col => {
            return { "width": col.closest('th').offsetWidth, "name": col.getAttribute('data-bb-field') }
        }),
        "table": tableWidth
    }));
}

const setTableDefaultWidth = table => {
    if (table.tables.length > 0 && isVisible(table.tables[0])) {
        const { scrollWidth, columnMinWidth } = table.options;
        const tableWidth = [...table.tables[0].querySelectorAll('col')]
            .map(i => {
                const colWidth = parseFloat(i.style.width);
                return isNaN(colWidth) ? columnMinWidth : colWidth;
            })
            .reduce((accumulator, val) => accumulator + val, 0);

        if (tableWidth > table.el.offsetWidth) {
            table.tables[0].style.setProperty('width', `${tableWidth}px`);
            if (table.thead) {
                table.tables[1].style.setProperty('width', `${tableWidth - scrollWidth}px`);
            }
        }
        else {
            table.tables[0].style.removeProperty('width');
            if (table.thead) {
                table.tables[1].style.setProperty('width', `${table.tables[0].offsetWidth - scrollWidth}px`);
            }
        }
    }
}
