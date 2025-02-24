import { saveSelectionData } from '../../api/reportApis.js';

const { CommandType } = UniverCore;
export const SaveAsTableTemplateOperation = {
    id: 'report.operation.save-as-table-template',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        saveSelectionData()
        return true;
    },
};
export const SaveAsCommonTemplateOperation = {
    id: 'report.operation.save-as-common-template',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        saveSelectionData()
        return true;
    },
};
