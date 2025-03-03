import DataService from '../_content/BootstrapBlazor.UniverSheet/data-service.js'

const { Disposable, setDependencies, Injector, ICommandService, CommandType, UniverInstanceType } = UniverCore;
const { ContextMenuGroup, ContextMenuPosition, RibbonStartGroup, ComponentManager, IMenuManagerService, MenuItemType, getMenuHiddenObservable } = UniverUi;

const GetDataOperation = {
    id: 'report.operation.add-table',
    type: CommandType.OPERATION,
    handler: async (accessor) => {
        const dataService = accessor.get(DataService.name);
        const data = await dataService.getDataAsync({
            messageName: "getDataMessage",
            commandName: "getDataCommand"
        });
        if (data) {
            const univerAPI = dataService.getUniverSheet().univerAPI;
            const range = univerAPI.getActiveWorkbook().getActiveSheet().getRange(0, 0, 2, 3)
            const defaultData = [
                [{ v: data.data.key }, { v: null }, { v: null }],
                [{ v: data.data.value }, { v: null }, { v: null }]
            ]
            range.setValues(defaultData);
        }
    }
};

function GetDataIcon() {
    return React.createElement(
        'svg',
        { xmlns: "http://www.w3.org/2000/svg", width: "1em", height: "1em", viewBox: "0 0 24 24" },
        React.createElement(
            'path',
            { fill: "currentColor", d: "M12 2c5.523 0 10 4.477 10 10s-4.477 10-10 10S2 17.523 2 12S6.477 2 12 2m.16 14a6.981 6.981 0 0 0-5.147 2.256A7.966 7.966 0 0 0 12 20a7.97 7.97 0 0 0 5.167-1.892A6.979 6.979 0 0 0 12.16 16M12 4a8 8 0 0 0-6.384 12.821A8.975 8.975 0 0 1 12.16 14a8.972 8.972 0 0 1 6.362 2.634A8 8 0 0 0 12 4m0 1a4 4 0 1 1 0 8a4 4 0 0 1 0-8m0 2a2 2 0 1 0 0 4a2 2 0 0 0 0-4" }
        )
    );
}

function ReportGetDataFactory(accessor) {
    return {
        id: GetDataOperation.id,
        type: MenuItemType.BUTTON,
        icon: 'GetDataIcon',
        tooltip: '获取数据',
        title: '获取数据',
        hidden$: getMenuHiddenObservable(accessor, UniverInstanceType.UNIVER_SHEET)
    };
}

export class ReportController extends Disposable {
    constructor(_injector, _commandService, _menuManagerService, _componentManager) {
        super();

        this._injector = _injector;
        this._commandService = _commandService;
        this._menuManagerService = _menuManagerService;
        this._componentManager = _componentManager;

        this._initCommands();
        this._registerComponents();
        this._initMenus();
        this._registerReceiveDataCallback();
    }

    _registerReceiveDataCallback() {
        const dataService = this._injector.get(DataService.name);
        dataService.registerReceiveDataCallback(data => {
            this.receiveData(data, dataService.getUniverSheet());
        });
    }

    _initCommands() {
        [GetDataOperation].forEach((c) => {
            this.disposeWithMe(this._commandService.registerCommand(c));
        });
    }

    _registerComponents() {
        const componentMap = {
            GetDataIcon,
        }
        Object.entries(componentMap).forEach((item) => {
            this.disposeWithMe(this._componentManager.register(...item));
        });

    }

    _initMenus() {
        this._menuManagerService.mergeMenu({
            [RibbonStartGroup.HISTORY]: {
                [GetDataOperation.id]: {
                    order: -1,
                    menuItemFactory: ReportGetDataFactory
                },
            },
            [ContextMenuPosition.MAIN_AREA]: {
                [ContextMenuGroup.DATA]: {
                    [GetDataOperation.id]: {
                        order: 0,
                        menuItemFactory: ReportGetDataFactory
                    }
                }
            }
        });
    }

    receiveData(data, sheet) {
        const { univerAPI } = sheet;
        const range = univerAPI.getActiveWorkbook().getActiveSheet().getRange(0, 0, 2, 3)
        const defaultData = [
            [{ v: 'Id' }, { v: 'Name' }, { v: 'Value' }],
            [{ v: data.data.id }, { v: data.data.name }, { v: data.data.value }]
        ]
        range.setValues(defaultData);
    }
}

setDependencies(ReportController, [Injector, ICommandService, IMenuManagerService, ComponentManager]);
