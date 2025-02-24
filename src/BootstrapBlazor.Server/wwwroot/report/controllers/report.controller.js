
import { SaveRangeIcon } from '../components/menus-icon/SaveRangeIcon.js';
import { SaveAsTableTemplateIcon } from '../components/menus-icon/SaveAsTableTemplateIcon.js';
import { SaveAsCommonTemplateIcon } from '../components/menus-icon/SaveAsCommonTemplateIcon.js';
import {
    REPORT_OPERATION_SAVE_RANGE_ID,
    ReportSaveRangeFactory,
    ReportSaveAsTableTemplateFactory,
    ReportSaveAsCommonTemplateFactory
} from './menu/save-range.menu.js';
import { SaveAsTableTemplateOperation, SaveAsCommonTemplateOperation } from '../commands/operations/save-range.operation.js';

import { ReportInsertVariableFactory, ReportAddTableFactory, ReportAddPlotFactory, ReportPreviewFactory, ReportExitPreviewFactory, ReportSaveExcelFactory } from './menu/single-butten.menu.js';

import { InsertVariableOperation } from '../commands/operations/insert-variable.operation.js';
import { InsertVariableIcon } from '../components/menus-icon/InsertVariableIcon.js';

import { AddTableOperation } from '../commands/operations/add-table.operation.js';
import { AddTableIcon } from '../components/menus-icon/AddTableIcon.js';

import { AddPlotOperation } from '../commands/operations/add-plot.operation.js';
import { AddPlotIcon } from '../components/menus-icon/AddPlotIcon.js';

import { PreviewOperation } from '../commands/operations/preview.operation.js';
import { PreviewIcon } from '../components/menus-icon/PreviewIcon.js';

import { ExitPreviewOperation } from '../commands/operations/exit-preview.operation.js';
import { ExitPreviewIcon } from '../components/menus-icon/ExitPreviewIcon.js';

import { SaveExcelOperation } from '../commands/operations/save-excel.operation.js';
import { SaveExcelIcon } from '../components/menus-icon/SaveExcelIcon.js';

import { FormulaSelection } from '../components/formula-selection/FormulaSelection.js';
import { EditLayout } from '../components/variable-table-edit/EditLayout.js'

import { OpenFormulaDialogCommand } from '../commands/openFormulaDialog.js'
import { OpenEditDialogCommand } from '../commands/openEditDialog.js'



const { Disposable, setDependencies, Injector, ICommandService } = UniverCore;
const { ContextMenuGroup, ContextMenuPosition, RibbonStartGroup, ComponentManager, IMenuManagerService } = UniverUi;
const { RibbonDataGroup, RibbonFormulasGroup, RibbonInsertGroup, RibbonOthersGroup, RibbonPosition, RibbonViewGroup } = UniverUi

export class ReportController extends Disposable {
    constructor(_injector, _commandService, _menuManagerService, _componentManager) {
        super();
        this._injector = _injector;
        this._commandService = _commandService;
        this._menuManagerService = _menuManagerService ;
        this._componentManager = _componentManager;

        this._initCommands();
        this._registerComponents();
        this._initMenus();
    }
    _initCommands() {
        [
            InsertVariableOperation,
            AddTableOperation,
            AddPlotOperation,
            PreviewOperation,
            ExitPreviewOperation,
            SaveExcelOperation,
            
            SaveAsTableTemplateOperation,
            SaveAsCommonTemplateOperation,

            OpenFormulaDialogCommand,
            OpenEditDialogCommand,
            
        ].forEach((c) => {
            this.disposeWithMe(this._commandService.registerCommand(c));
        });
    }
    _registerComponents() {
        const componentMap = {
            InsertVariableIcon,
            AddTableIcon,
            AddPlotIcon,
            PreviewIcon,
            ExitPreviewIcon,
            SaveExcelIcon,

            SaveRangeIcon,
            SaveAsTableTemplateIcon,
            SaveAsCommonTemplateIcon,

            FormulaSelection,
            EditLayout,
        }
        Object.entries(componentMap).forEach((item) => {
            this.disposeWithMe(this._componentManager.register(...item));
        });

    }
    _initMenus() {
        console.log(this._menuManagerService, 'this._menuManagerService');
        this._menuManagerService.mergeMenu({
            // {
            //     "HISTORY": "ribbon.start.history",
            //     "FORMAT": "ribbon.start.format",
            //     "LAYOUT": "ribbon.start.layout",
            //     "FORMULAS_INSERT": "ribbon.start.insert",
            //     "FORMULAS_VIEW": "ribbon.start.view",
            //     "FILE": "ribbon.start.file",
            //     "OTHERS": "ribbon.start.others"
            // }
            [RibbonStartGroup.FILE]: {
                [InsertVariableOperation.id]: {
                    order: 2,
                    menuItemFactory: ReportInsertVariableFactory
                },
                [AddTableOperation.id]: {
                    order: 2,
                    menuItemFactory: ReportAddTableFactory
                },
                [AddPlotOperation.id]: {
                    order: 2,
                    menuItemFactory: ReportAddPlotFactory
                },
                [PreviewOperation.id]: {
                    order: 2,
                    menuItemFactory: ReportPreviewFactory
                },
                [ExitPreviewOperation.id]: {
                    order: 2,
                    menuItemFactory: ReportExitPreviewFactory
                },
                [SaveExcelOperation.id]: {
                    order: 2,
                    menuItemFactory: ReportSaveExcelFactory
                },
            },
            [RibbonStartGroup.OTHERS]: {
                // [REPORT_OPERATION_SAVE_RANGE_ID]: {
                //     order: -1,
                //     menuItemFactory: ReportSaveRangeFactory,
                //     [SaveAsTableTemplateOperation.id]: {
                //         order: 0,
                //         menuItemFactory: ReportSaveAsTableTemplateFactory
                //     },
                //     [SaveAsCommonTemplateOperation.id]: {
                //         order: 1,
                //         menuItemFactory: ReportSaveAsCommonTemplateFactory
                //     }
                // }
            },
            [ContextMenuPosition.MAIN_AREA]: {
                [ContextMenuGroup.DATA]: {
                    [InsertVariableOperation.id]: {
                        order: 0,
                        menuItemFactory: ReportInsertVariableFactory
                    },
                    [AddTableOperation.id]: {
                        order: 0,
                        menuItemFactory: ReportAddTableFactory
                    },
                    [AddPlotOperation.id]: {
                        order: 0,
                        menuItemFactory: ReportAddPlotFactory
                    },
                    [REPORT_OPERATION_SAVE_RANGE_ID]: {
                        order: 0,
                        menuItemFactory: ReportSaveRangeFactory,
                        [SaveAsTableTemplateOperation.id]: {
                            order: 0,
                            menuItemFactory: ReportSaveAsTableTemplateFactory
                        },
                        [SaveAsCommonTemplateOperation.id]: {
                            order: 1,
                            menuItemFactory: ReportSaveAsCommonTemplateFactory
                        }
                    }
                }
            }
        });
    }
}
setDependencies(ReportController, [Injector, ICommandService, IMenuManagerService, ComponentManager]);
