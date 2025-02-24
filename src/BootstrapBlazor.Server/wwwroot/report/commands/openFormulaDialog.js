import { insertVariable } from '../api/reportApis.js'
import { INSERT_VARIABLE_DIALOG } from '../constant.js'
const { CommandType, ICommandService, IUniverInstanceService, IContextService} = UniverCore;
const { IDialogService } = UniverUi;

export const OpenFormulaDialogCommand = {
    id: 'report.open-formula-dialog',
    type: CommandType.OPERATION,
    handler: async (accessor, params = {}) => {
        const dialogService = accessor.get(IDialogService);
        const closeDialog = () => dialogService.close(INSERT_VARIABLE_DIALOG);
        closeDialog()
        console.log('OpenFormulaDialogCommand', params);
        const title = params.cellData?.custom?.variable ? 'Edit Variable' : 'Insert Variable';
        const dialog = dialogService.open({
            id: INSERT_VARIABLE_DIALOG,
            title: { title },
            children: { label: 'FormulaSelection', ...params, closeDialog },
            // footer: { title: 'Insert' },
            width: 650,
            draggable: true,
            resizable: true,
            destroyOnClose: true,
            preservePositionOnDestroy: true,
            onClose: closeDialog,
        });
    },
};
