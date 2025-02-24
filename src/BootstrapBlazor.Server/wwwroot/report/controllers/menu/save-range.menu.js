import { SaveAsTableTemplateOperation, SaveAsCommonTemplateOperation } from '../../commands/operations/save-range.operation.js';
export const REPORT_OPERATION_SAVE_RANGE_ID = 'report.operation.save-range';
const { MenuItemType } = UniverUi;
export function ReportSaveRangeFactory() {
    return {
        id: REPORT_OPERATION_SAVE_RANGE_ID,
        type: MenuItemType.SUBITEMS,
        icon: 'SaveRangeIcon',
        tooltip: 'report.saveRange',
        title: 'report.saveRange',
    };
}
export function ReportSaveAsTableTemplateFactory() {
    return {
        id: SaveAsTableTemplateOperation.id,
        type: MenuItemType.BUTTON,
        title: 'report.saveAsTableTemplate',
        icon: 'SaveAsTableTemplateIcon',
        positions: [REPORT_OPERATION_SAVE_RANGE_ID],
    };
}
export function ReportSaveAsCommonTemplateFactory() {
    return {
        id: SaveAsCommonTemplateOperation.id,
        type: MenuItemType.BUTTON,
        title: 'report.saveAsCommonTemplate',
        icon: 'SaveAsCommonTemplateIcon',
        positions: [REPORT_OPERATION_SAVE_RANGE_ID],
    };
}
