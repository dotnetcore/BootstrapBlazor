import zhCN from './locale/zh-CN.js';
import enUS from './locale/en-US.js';
import { ReportController } from './controllers/report.controller.js';
import { eventsListen } from "./eventsListener.js"
import { MainCustomExtension } from './canvasExtension/main-extension.js'

const { Plugin, UniverInstanceType, LocaleService, Inject, touchDependencies, Injector, setDependencies } = UniverCore
const REPORT_PLUGIN = 'ReportPlugin';

// 定义插件类
export class ReportPlugin extends Plugin {
    static type = UniverInstanceType.UNIVER_SHEET;
    static pluginName = REPORT_PLUGIN;

    constructor(
        _injector,
        _localeService,
    ) {
        super();
        this._injector = _injector;
        this._localeService = _localeService;
        this._localeService.load({
            zhCN,
            enUS
        });
    }

    onStarting() {
        this._injector.add([ReportController])
    }

    onReady() {
        this._injector.get(ReportController)
    }
    onRendered() {
        // touchDependencies(this._injector, [[ReportController]])

        // 注册自定义表格单元格渲染
        const unitId = univerAPI.getActiveWorkbook().id
        univerAPI.registerSheetMainExtension(unitId, new MainCustomExtension())

        // 绑定report插件的事件监听
        const eventDisposableList = eventsListen({ univer, univerAPI });
        // excel销毁时，解绑事件监听
        univer.onDispose(() => eventDisposableList.forEach(disposable => { disposable.dispose(); console.log("解绑report插件的事件监听"); }));
    }

    onInit(univer, univerAPI) {

    }
}

// 设置依赖
setDependencies(ReportPlugin, [Injector, LocaleService]);
