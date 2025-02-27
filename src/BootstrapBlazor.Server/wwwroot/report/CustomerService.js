
const {IResourceManagerService, setDependencies, IResourceLoaderService, IUniverInstanceService, Injector, FUniver } = UniverCore
const { UniverType } = UniverProtocol

export class CustomerService{
    _model = {
        testResource: {
            name: 'testResource',
            value: 'testValue'
        }
    }
    constructor(
        _injector,
        _resourceManagerService,
        _univerInstanceService,
        _resourceLoaderService,
    ) {console.log('CustomerService constructor');

        this._injector = _injector;

        this._resourceManagerService = _resourceManagerService;
        this._univerInstanceService = _univerInstanceService;
        this._resourceLoaderService = _resourceLoaderService;

        this._resourceManagerService.registerPluginResource({
            toJson: (_unitId) => this._toJson(_unitId),
            parseJson: (json) => this._parseJson(json),
            pluginName: 'CustomerService',
            businesses: [UniverType.UNIVER_SHEET, UniverType.UNIVER_DOC, UniverType.UNIVER_SLIDE],
            onLoad: (_unitId, resource) => {
                this._model = resource;
            },
            onUnLoad: (_unitId) => {
                console.log(_unitId, 'onUnLoad');
                this._model = { };
            },
        });
    }

    _toJson(_unitId) {
        const model = this._model;
        console.log('_toJson');
        return JSON.stringify(model);
    }

    _parseJson(json) {
        return JSON.parse(json);
    }
    _getResource() {
        const workbook = this._univerInstanceService.getCurrentUnitForType(UniverType.UNIVER_SHEET)
        const snapshot = this._resourceLoaderService.saveUnit(workbook.getUnitId());
        return snapshot.resources
    }

    _setRangeValue(data = 'testValue001') {
        const univerAPI = FUniver.newAPI(this._injector)
        const range = univerAPI.getActiveWorkbook().getActiveSheet().getRange(5,3,2,1)
        range.setValue(data)
    }
}

setDependencies(CustomerService, [Injector, IResourceManagerService, IUniverInstanceService, IResourceLoaderService]);