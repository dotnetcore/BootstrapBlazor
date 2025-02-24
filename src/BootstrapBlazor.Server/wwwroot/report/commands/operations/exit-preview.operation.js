import { preview } from '../../api/reportApis.js'

const { CommandType } = UniverCore;
export const ExitPreviewOperation = {
    id: 'report.operation.exit-preview',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        alert('Exit Preview')
        return true;
    },
};
