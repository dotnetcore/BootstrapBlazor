import { preview } from '../../api/reportApis.js'

const { CommandType } = UniverCore;
export const PreviewOperation = {
    id: 'report.operation.preview',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        preview()
        return true;
    },
};
