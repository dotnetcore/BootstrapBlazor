const layout_column = (el, target, rowType, colSpan, itemsPerRow) => {
    let isLabel = false
    let groupCell = null
    const div = document.createElement('div')
    div.classList.add("row")
    div.classList.add("g-3")
    if (rowType === "inline") {
        div.classList.add('form-inline')
    }

    [...el.children].forEach(ele => {
        const isRow = ele.getAttribute('data-bb-toggle') === 'row'
        const colSpan = getColSpan(ele)
        if (isRow) {
            const newRow = document.createElement('div')
            calc(colSpan, itemsPerRow).split(' ').forEach(v => {
                newRow.classList.add(v)
            })
            newRow.appendChild(ele)
            div.appendChild(newRow)
        }
        else {
            isLabel = ele.tagName === 'LABEL'

            // 如果有 Label 表示在表单内
            if (isLabel) {
                if (groupCell === null) {
                    groupCell = document.createElement('div')
                    calc(colSpan, itemsPerRow).split(' ').forEach(v => {
                        groupCell.classList.add(v)
                    })
                }
                groupCell.appendChild(ele)
            }
            else {
                isLabel = false
                if (groupCell == null) {
                    groupCell = document.createElement('div')
                    calc(colSpan, itemsPerRow).split(' ').forEach(v => {
                        groupCell.classList.add(v)
                    })
                }
                groupCell.appendChild(ele)
                if (target === null) {
                    div.appendChild(groupCell)
                }
                else {
                    target.appendChild(groupCell)
                }
                groupCell = null
            }
        }
    })

    if (target == null) {
        el.appendChild(div)
    }
}

const layout_parent_row = el => {
    const uid = el.getAttribute('data-bb-target')
    const target = document.querySelector(`[data-uid='${uid}']`)
    const row = document.createElement('div')
    row.classList.add('row')
    target.appendChild(row)
    layout_column(row)
}

const calc = (colSpan, itemsPerRow) => {
    if (colSpan > 0) itemsPerRow = itemsPerRow * colSpan
    let ret = "col-12"
    if (itemsPerRow !== 12) {
        ret = `col-12 col-sm-${itemsPerRow}`
    }
    return ret
}

const getColSpan = el => {
    let colSpan = parseInt(el.getAttribute('data-bb-colspan'))
    if (isNaN(colSpan)) colSpan = 0
    return colSpan
}

export function init(id) {
    const el = document.getElementById(id)
    if (el === null) {
        return
    }

    const colSpan = getColSpan(el)
    const rowType = el.getAttribute('data-bb-type')
    let itemsPerRow = parseInt(el.getAttribute('data-bb-items'))
    if (isNaN(itemsPerRow)) {
        itemsPerRow = 12
    }

    layout_column(el, null, rowType, colSpan, itemsPerRow)
    el.classList.remove('d-none')
}
