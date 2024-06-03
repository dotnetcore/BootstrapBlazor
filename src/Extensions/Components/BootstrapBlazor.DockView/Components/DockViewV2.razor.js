import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import {
    cerateDockview,
    serialize,
    loadDockview,
    getTheme,
    setTheme,
    toggleComponent,
    lockDock,
    addHook,
    getJson
} from '../js/dockview-utils.js'
import Data from '../../BootstrapBlazor/modules/data.js'

export async function init(id, invoke, options) {
    await addLink("./_content/BootstrapBlazor.DockView/css/dockview-bb.css")

    options = {
        ...options,
        ...{
            gear: {
                show: true
            },
            rightControl: [
                {
                    name: 'lock',
                    icon: ['<i class="fas fa-unlock"></i>', '<i class="fas fa-lock"></i>']
                },
                {
                    name: 'packup/expand',
                    icon: ['<i class="fas fa-chevron-circle-up"></i>', '<i class="fas fa-chevron-circle-down"></i>']
                },
                {
                    name: 'float',
                    icon: ['<i class="far fa-window-restore"></i>']
                },
                {
                    name: 'maximize',
                    icon: ['<i class="fas fa-expand"></i>', '<i class="fas fa-compress"></i>']
                },
                {
                    name: 'close',
                    icon: ['<i class="fas fa-times"></i>']
                }
            ],
        }
    }
    console.log(options, 'options');
    const { templateId } = options
    const el = document.getElementById(id);
    const template = document.getElementById(templateId)
    el.classList.add(options.theme)

    // 1、序列化options数据为dockview可用数据(layoutConfig优先)
    let serverData = options.layoutConfig || serialize(options)

    // 2、创建dockview实例
    const dockview = cerateDockview(el, template, options)

    // 3、保存可用信息
    dockview.prefix = options.prefix
    dockview.locked = options.lock
    Data.set(id, dockview)

    // 4、以本地优先, 得到最终的dockviewData并修正
    let dockviewData = getJson(dockview, serverData)
    // 5、绑定钩子函数
    addHook(dockview, dockviewData, options, invoke, template)
    // 渲染dockview结构
    loadDockview(dockview, dockviewData)
}

export function update(id, option) {
    let dock = Data.get(id)
    let ele = dock.element.parentElement
    let theme = getTheme(ele)
    console.log(dock, 'update: dockview');
    // console.log(option);
    if (dock) {

        if (dock.locked !== option.lock) {
            // 处理 Lock 逻辑
            dock.locked = option.lock
            lockDock(dock)
        }
        else if (theme !== option.theme) {
            setTheme(ele, theme, option.theme)
        }
        else {
            // 处理 toggle 逻辑
            toggleComponent(dock, option, option.invoke)
        }
    }





}

export function dispose(id) {
    const dock = Data.get(id)
    Data.remove(id);

    if (dock) {
        const { dockview } = dock;
        if (dockview) {
            dockview.dispose();
        }
    }
}
