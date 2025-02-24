import { editVariableAndTable, insertVariable, preview } from './api/reportApis.js'
import { getCategoryList, getVariableList } from './api/getData.js';


const {UniverSheetsUIPlugin} = UniverSheetsUi
    export const eventsListen = ({univer, univerAPI}) => [
        // 监听选区变化事件
        univerAPI.addEvent(univerAPI.Event.SelectionChanged, (params) => {
            const editEle = document.querySelector('.bb-edit-container');
            const formulaEle = document.querySelector('.bb-formula-selection');
            if(editEle){
                const { worksheet, workbook, selections:  [{startRow, startColumn}]} = params;
                const cellData = worksheet.getRange(startRow, startColumn, 1, 1).getCellData();
                // if( cellData.custom?.variable || cellData.custom?.table ) {
                    editVariableAndTable({cellData})
                // }
            }
            if (formulaEle) {
                insertVariable()
            }
        }),
        // 监听单元格编辑开始事件
        univerAPI.addEvent(univerAPI.Event.BeforeSheetEditStart, (params) => {
            const { worksheet, workbook, row, column, eventType, keycode, isZenEditor } = params;
            const cellData = worksheet.getRange(row, column, 1, 1).getCellData();
            if( cellData.custom?.variable || cellData.custom?.table ) {
                params.cancel = true;
                editVariableAndTable({cellData})
            }
        }),
        
        // univerAPI.addEvent(univerAPI.Event.CellClicked, (params) => {
        //     const { worksheet, workbook, row, column } = params;
        //     console.log('点击的单元格:', row, column);
        // }),


        univerAPI.onBeforeCommandExecute((command) => {
            const { id, type, params } = command;
            if(id === 'zen-editor.command.open-zen-editor'){
                console.log(id);
            }
        })
    ]
