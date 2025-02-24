import { InsertVariableOperation } from '../../commands/operations/insert-variable.operation.js';
import { AddTableOperation } from '../../commands/operations/add-table.operation.js';
import { AddPlotOperation } from '../../commands/operations/add-plot.operation.js';
import { PreviewOperation } from '../../commands/operations/preview.operation.js';
import { ExitPreviewOperation } from '../../commands/operations/exit-preview.operation.js';
import { SaveExcelOperation } from '../../commands/operations/save-excel.operation.js';

const { UniverInstanceType } = UniverCore;
const { MenuItemType, getMenuHiddenObservable } = UniverUi;

export function ReportInsertVariableFactory(accessor) {
    return {
        id: InsertVariableOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'InsertVariableIcon',
        tooltip: 'report.insertVariable',
        title: 'report.insertVariable',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };
}

export function ReportAddTableFactory(accessor) {
    return {
        id: AddTableOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'AddTableIcon',
        tooltip: 'report.addTable',
        title: 'report.addTable',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };
}

export function ReportAddPlotFactory(accessor) {
    return {
        id: AddPlotOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'AddPlotIcon',
        tooltip: 'report.addPlot',
        title: 'report.addPlot',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };
}

export function ReportPreviewFactory(accessor) {
    return {
        id: PreviewOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'PreviewIcon',
        tooltip: 'report.preview',
        title: 'report.preview',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };  
}

export function ReportExitPreviewFactory(accessor) {
    return {
        id: ExitPreviewOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'ExitPreviewIcon',
        tooltip: 'report.exitPreview',
        title: 'report.exitPreview',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };  
}

export function ReportSaveExcelFactory(accessor) {
    return {
        id: SaveExcelOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'SaveExcelIcon',
        tooltip: 'report.saveExcel',
        title: 'report.saveExcel',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };  
}

