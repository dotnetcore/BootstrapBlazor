import { EDIT_DIALOG, INSERT_VARIABLE_DIALOG } from '../constant.js'

const { CommandType, ICommandService, IUniverInstanceService, IContextService} = UniverCore;
const { IDialogService } = UniverUi;

export const OpenEditDialogCommand = {
    id: 'report.open-edit-dialog',
    type: CommandType.OPERATION,
    handler: async (accessor, params = {}) => {
        const dialogService = accessor.get(IDialogService);
        const closeDialog = () => dialogService.close(EDIT_DIALOG);
        closeDialog();
        const dialog = dialogService.open({
            id: EDIT_DIALOG,
            title: { title: 'Edit Variable and Table' },
            children: { label: 'EditLayout', ...params, closeDialog },
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