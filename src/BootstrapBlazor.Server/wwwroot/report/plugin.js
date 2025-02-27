import zhCN from './locale/zh-CN.js';
import enUS from './locale/en-US.js';
import { ReportController } from './controllers/report.controller.js';
import { eventsListen } from "./eventsListener.js"
import { MainCustomExtension } from './canvasExtension/main-extension.js'
import { CustomerService } from './CustomerService.js'

const { Plugin, UniverInstanceType, LocaleService, Inject, touchDependencies, Injector, setDependencies, FUniver, IUniverInstanceService, createIdentifier } = UniverCore
const REPORT_PLUGIN = 'ReportPlugin';

// 定义插件类
export class ReportPlugin extends Plugin {
    static type = UniverInstanceType.UNIVER_SHEET;
    static pluginName = REPORT_PLUGIN;
    static DataServiceName = 'CustomerService';
    constructor(
        _injector,
        _localeService,
    ) {
        super();
        console.log('ReportPlugin constructor');
        this._injector = _injector;
        this._localeService = _localeService;
        this._localeService.load({
            zhCN,
            enUS
        });
    }

    onStarting() {
        console.log('onStarting');
        this._injector.add([ReportController])
        this._injector.add([ReportPlugin.DataServiceName, { useClass: CustomerService }])
    }

    onReady() {
        console.log('onReady');
        this._injector.get(ReportController)

        // const customerService = this._injector.get('zhangsan')
        // console.log(customerService, 'customerService');
        // const resource = customerService._getResource().resources.find(item => item.name === 'CustomerService').data || {}
        // const univerAPI = FUniver.newAPI(this._injector)
        // const unitId = univerAPI.getActiveWorkbook().getId()
        // univerAPI.createUniverSheet(JSON.parse(resource))
        // univerAPI.disposeUnit(unitId)

        const customerService = this._injector.get(ReportPlugin.DataServiceName)
        const resource = customerService._getResource().find(item => item.name === 'CustomerService').data || {}
        const univerAPI = FUniver.newAPI(this._injector)
        const sheet = univerAPI.getActiveWorkbook().getActiveSheet().getRange(2, 2, 2, 1)
        sheet.setValue(resource)
    }
    onRendered() {
        // touchDependencies(this._injector, [[ReportController]])

        // 动态添加link标签
        const link = document.createElement('link');
        link.type = 'text/css';
        link.rel = 'stylesheet';
        link.href = './report/report.css';
        document.head.appendChild(link);

        const univerAPI = FUniver.newAPI(this._injector)

        // 注册自定义表格单元格渲染
        const unitId = univerAPI.getActiveWorkbook().id
        univerAPI.registerSheetMainExtension(unitId, new MainCustomExtension())

        // 绑定report插件的事件监听
        const eventDisposableList = eventsListen({ univerAPI });
        // excel销毁时，解绑事件监听
        this._injector.onDispose(() => eventDisposableList.forEach(disposable => { disposable.dispose(); console.log("解绑report插件的事件监听"); }));
    }

    // onSteady() {
    // }
}

// 设置依赖
setDependencies(ReportPlugin, [Injector, LocaleService]);
