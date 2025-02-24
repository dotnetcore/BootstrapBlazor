import { insertVariable } from '../../api/reportApis.js'

const { CommandType, IConfigService } = UniverCore;
const { IMenuManagerService } = UniverUi;

export const AddPlotOperation = {
    id: 'report.operation.add-plot',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        const menuManagerService = accessor.get(IMenuManagerService);
        const configService = accessor.get(IConfigService)
        console.log(menuManagerService, 'menuManagerService');
        console.log(configService, 'configService');
        alert('AddPlotOperation');
    },
};
