import { addTable } from '../../api/reportApis.js'

const { CommandType } = UniverCore;

export const AddTableOperation = {
    id: 'report.operation.add-table',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        addTable()
    },
};
