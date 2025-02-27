
const { IResourceManagerService, setDependencies, IResourceLoaderService, IUniverInstanceService, Injector, FUniver } = UniverCore
const { UniverType } = UniverProtocol

export class DataService {
    constructor(
        _injector
    ) {
        this._injector = _injector;
    }
    receiveData(data) {
        console.log(data);
        if (typeof (this.pushData) === 'function') {
            this.pushData({
                messageName: 'DataService',
                commandName: 'setRangeValue',
                data: {
                    value: 1
                }
            });
        }
    }
}

setDependencies(DataService, [Injector]);
