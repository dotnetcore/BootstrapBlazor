import { insertVariable } from '../../api/reportApis.js'

const { CommandType, ICommandService, IUniverInstanceService, IContextService} = UniverCore;
const { IDialogService } = UniverUi;

export const InsertVariableOperation = {
    id: 'report.operation.insert-variable',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        const commandService = accessor.get(ICommandService);
        const univerInstanceService = accessor.get(IUniverInstanceService);
        const wb = univerInstanceService.getFocusedUnit().getActiveSheet().getRange(0,0,1,1)
        const dialogService = accessor.get(IDialogService);
        console.log(dialogService.getDialogs$());
        console.log(dialogService.valueOf(), 'dialogService');
        insertVariable()
    },
};
