import BlazorComponent from "./base/blazor-component.js"
import EventHandler from "./base/event-handler.js"
import { getResponsive } from "./responsive.js"
import { drag, getDescribedElement, getHeight, getWidth } from "./base/utility.js"

export class Table extends BlazorComponent {
    _init() {
        this._invoker = this._config.arguments[0]
        this._fixedHeader = this._element.querySelector('.table-fixed') != null
        this._isExcel = this._element.querySelector('.table-excel') != null
        this._isResizeColumn = this._element.querySelector('.col-resizer') != null
        this._columns = []

        if (this._fixedHeader) {
            this._thead = this._element.querySelector('.table-fixed-header')
            this._body = this._element.querySelector('.table-fixed-body')
            this._fixHeader()
        }

        this._setListeners()
    }

    _setListeners() {
        if (this._fixedHeader) {
            EventHandler.on(this._body, 'scroll', () => {
                const left = this._body.scrollLeft
                this._thead.scrollTo(left, 0)
            });
        }

        if (this._isExcel) {
            this._setExcelKeyboardListener()
        }

        if (this._isResizeColumn) {
            this._setResizeListener()
        }

        if (true) {
            this._setCopyColumn()
        }
    }

    _execute(args) {
        const category = args[1]

        if (category === 'load') {
            const method = args[2]
            const loader = this._element.querySelector('.table-loader')
            if (method === 'show') {
                loader.classList.add('show')
            } else {
                loader.classList.remove('show')
            }
        } else if (category === 'sort') {
            const span = this._element.querySelector('.sortable .table-text[aria-describedby]')
            if (span) {
                const tooltip = getDescribedElement(span)
                if (tooltip) {
                    tooltip.querySelector('.tooltip-inner').innerHTML = span.getAttribute('data-bs-original-title')
                }
            }
        }
    }

    _fixHeader() {
        const fs = this._element.querySelector('.fixed-scroll')
        if (fs) {
            let prev = fs.previousElementSibling
            while (prev) {
                if (prev.classList.contains('fixed-right') && !prev.classList.contains('modified')) {
                    let margin = prev.style.right
                    margin = margin.replace('px', '')
                    if ($.browser.versions.mobile) {
                        margin = (parseFloat(margin) - 6) + 'px'
                    }
                    prev.classList.add('modified')
                    prev.style.right = margin
                    prev = prev.previousElementSibling
                } else {
                    break
                }
            }
        }

        // 尝试自适应高度
        this._updateBodyHeight()
    }

    _updateBodyHeight() {
        const search = this._element.querySelector('.table-search')
        let searchHeight = 0
        if (search) {
            searchHeight = getHeight(search)
        }
        const pagination = this._element.querySelector('.nav-pages')
        let paginationHeight = 0
        if (pagination) {
            paginationHeight = getHeight(pagination)
        }
        const toolbar = this._element.querySelector('.table-toolbar')
        let toolbarHeight = 0
        if (toolbar) {
            toolbarHeight = getHeight(toolbar)
        }
        const bodyHeight = paginationHeight + toolbarHeight + searchHeight;
        if (bodyHeight > 0) {
            this._body.parentNode.style.height = `calc(100% - ${bodyHeight}px)`
        }

        const headerHeight = getHeight(this._thead)
        if (headerHeight > 0) {
            this._body.style.height = `calc(100% - ${headerHeight}px)`
        }
    }

    _setResizeListener() {
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
        const columns = [...this._element.querySelectorAll('.col-resizer')]
        if (this._fixedHeader) {
            const last = columns.pop()
            if (last) {
                last.remove();
            }
        }

        columns.forEach(col => {
            this._columns.push(col)
            drag(col,
                e => {
                    colIndex = eff(col, true)
                    const currentCol = this._element.querySelectorAll('table colgroup col')[colIndex]
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
                    this._element.querySelectorAll('table colgroup').forEach(group => {
                        const curCol = group.children.item(colIndex)
                        curCol.style.width = `${colWidth + marginX}px`
                        const table = curCol.closest('table')
                        const width = tableWidth + marginX
                        if (this._fixedHeader) {
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

    _setExcelKeyboardListener() {
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
            const handler = window.setTimeout(function () {
                window.clearTimeout(handler);
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

        EventHandler.on(this._element, 'keydown', e => {
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

    _setCopyColumn() {
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

        EventHandler.on(this._element, 'click', '.col-copy', e => {
            const index = e.delegateTarget.closest('th').cellIndex
            let rows
            if (this._fixedHeader) {
                rows = this._body.querySelectorAll('table > tbody > tr')
            }
            else if (this._element.querySelector('.table-fixed-column')) {
                rows = this._element.querySelectorAll('.table-scroll > .overflow-auto > table > tbody > tr')
            }
            else {
                rows = this._element.querySelectorAll('.table-scroll > table > tbody > tr')
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
            this._copy(content)
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

    _copy = (text = '') => {
        if (navigator.clipboard) {
            navigator.clipboard.writeText(text)
        } else {
            const input = document.createElement('textarea')
            input.setAttribute('value', text)
            input.setAttribute('hidden', 'true')
            document.body.appendChild(input)
            input.select()
            document.execCommand('copy')
            document.body.removeChild(input)
        }
    }

    _dispose() {
        if (this._fixedHeader) {
            EventHandler.off(this._body, 'scroll')
        }

        if (this._isExcel) {
            EventHandler.off(this._element, 'keydown')
        }

        this._columns.forEach(col => {
            EventHandler.off(col, 'mousedown')
            EventHandler.off(col, 'touchstart')
        })

        EventHandler.off(this._element, 'click', '.col-copy')
    }

    static getResponsive() {
        return getResponsive()
    }
}
