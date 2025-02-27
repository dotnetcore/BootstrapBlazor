import { ReportController } from './controllers/report.controller.js';
import { DataService } from './DataService.js'

const { Plugin, UniverInstanceType, Injector, setDependencies } = UniverCore
const REPORT_PLUGIN = 'ReportPlugin';

// 定义插件类
export class ReportPlugin extends Plugin {
    static type = UniverInstanceType.UNIVER_SHEET;
    static pluginName = REPORT_PLUGIN;
    static DataServiceName = 'DataService';
    constructor(
        _injector,
    ) {
        super();
        this._injector = _injector;
    }

    onStarting() {
        console.log('onStarting');
        this._injector.add([ReportController])
        this._injector.add([ReportPlugin.DataServiceName, { useClass: DataService }])
    }

    onReady() {
        this._injector.get(ReportController)
    }
    onRendered() {
        // 动态添加link标签
        const link = document.createElement('link');
        link.type = 'text/css';
        link.rel = 'stylesheet';
        link.href = './report/report.css';
        document.head.appendChild(link);
    }
}

// 设置依赖
setDependencies(ReportPlugin, [Injector]);
