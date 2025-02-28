import { ReportController } from './controller.js'
import { DataService } from '../_content/BootstrapBlazor.UniverSheet/univer.js'

const { Plugin, Injector, setDependencies } = UniverCore;
const REPORT_PLUGIN = 'ReportPlugin';

// 定义插件类
export class ReportPlugin extends Plugin {
    static pluginName = REPORT_PLUGIN;

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
