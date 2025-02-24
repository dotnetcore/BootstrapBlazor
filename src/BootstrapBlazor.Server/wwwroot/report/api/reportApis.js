import { getCategoryList, getVariableList } from './getData.js';

const { IDialogService } = UniverUi;
const { useState } = React;

export function insertVariable({cellData} = {}) {
    const categoryList = getCategoryList();
    console.log('insertVariable', cellData);
    if(!cellData){
        const sheet = univerAPI.getActiveWorkbook().getActiveSheet();
        cellData = sheet.getSelection().getActiveRange().getCellData() || {};
    }
    univerAPI.executeCommand('report.open-formula-dialog', {categoryList, cellData})
}
export function editVariableAndTable({cellData}) {
    const variable = cellData.custom?.variable;
    const params = {
        tabs: [
            {key: 0, title: 'Variable'},
            {key: 1, title: 'Table Column'},
            {key: 2, title: 'Filter Peaks'},
            {key: 3, title: 'Header Rows'},
            {key: 4, title: 'Table Properties'}
        ],
        contentList: [
            {key: 0, component: 'VariableEdit'},
            {key: 1, component: 'TableColumnEdit'},
            {key: 2, component: 'TableFilterPeaks'},
            {key: 3, component: 'TableHeaderRowsEdit'},
            {key: 4, component: 'TablePropertiesEdit'}
        ]
    }
    
    univerAPI.executeCommand('report.open-edit-dialog', {...params, cellData})
}
export function preview() {
    const activeWorkbook = univerAPI.getActiveWorkbook();
    const activeSheet = activeWorkbook.getActiveSheet();
    const snapshot = activeWorkbook.save()
    const sheet1 = Object.values(snapshot.sheets).find((sheet) => {
        return sheet.name === 'sheet1'
    })
    const data = sheet1.cellData
    Object.keys(data).forEach(rowIndex => {
        Object.keys(data[rowIndex]).forEach(columnIndex => {
            if(data[rowIndex][columnIndex].custom?.variable){
                const range = activeSheet.getRange(rowIndex * 1, columnIndex * 1, 1, 1)
                range.setValue({
                    v: data[rowIndex][columnIndex].custom.variable.desc || '预览数据',
                    custom: data[rowIndex][columnIndex].custom,
                });
            }
        })
    })
}
// 保存数据
export function saveExcel() {
    const activeWorkbook = univerAPI.getActiveWorkbook();
    const activeSheet = activeWorkbook.getActiveSheet();
    const snapshot = activeWorkbook.save()
    return snapshot
}

// 保存选取数据
export function saveSelectionData(saveType = 'table') {
    const activeSheet = univerAPI.getActiveWorkbook().getActiveSheet();
    const selection = activeSheet.getSelection();
    const range = selection.getActiveRange();
    const data = range.getCellDataGrid();

    console.log('saveSelectedData', data);
    localStorage.setItem('table01', JSON.stringify(data));
}

// 添加表格
export function addTable() {
    const activeSheet = univerAPI.getActiveWorkbook().getActiveSheet();
    // const selection = activeSheet.getSelection();
    // const range = selection.getActiveRange();
    console.log('addTable', univerAPI.getActiveWorkbook().getSnapshot());

}
