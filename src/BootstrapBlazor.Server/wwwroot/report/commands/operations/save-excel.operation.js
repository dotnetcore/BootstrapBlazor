import { saveExcel } from '../../api/reportApis.js'

const { CommandType } = UniverCore;
export const SaveExcelOperation = {
    id: 'report.operation.save-excel',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        console.log(saveExcel());
        return true;
    },
};
