import DataService from '../_content/BootstrapBlazor.UniverSheet/data-service.js'
import { ReportController } from './controller.js'

const { Plugin, Injector, setDependencies } = UniverCore;

// 定义插件类
export class ReportPlugin extends Plugin {
    static pluginName = 'ReportPlugin';

    constructor(_injector) {
        super();

        this._injector = _injector;
    }

    onStarting() {
        this._injector.add([ReportController]);
        this._injector.add([DataService.name, { useClass: DataService }])
    }

    onReady() {
        this._injector.get(ReportController)
    }

    onRendered() {

    }
}

// 设置依赖
setDependencies(ReportPlugin, [Injector]);
