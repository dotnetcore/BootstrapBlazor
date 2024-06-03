import { addLink } from '../../BootstrapBlazor/modules/utility.js'
import Data from '../../BootstrapBlazor/modules/data.js'

import '../js/dockview-override.js'
import { DockviewComponent } from "../js/dockview-core.esm.js"
import {
  serialize,
  loadDockview,
  DefaultPanel,
  myDefaultTab,
  getTheme,
  setTheme,
  toggleComponent,
  lockDock,
  addHook,
  getJson
} from '../js/dockview-utils.js'

  export async function init(id,invoke, options) {
    await addLink("./_content/BootstrapBlazor.DockView/css/dockview-bb.css")
    console.log(id, 'id', options, 'options');
    options = {
        ...options, ...{
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

    // invoke.invokeMethodAsync(options.initializedCallback)
    console.log(options, 'options');
    const { templateId } = options
    const el = document.getElementById(id);
    const template = document.getElementById(templateId)
    // dockview-theme-replit,dockview-theme-dracula,dockview-theme-vs,dockview-theme-light,dockview-theme-dark,dockview-theme-abyss
    el.classList.add(options.theme)

    // 1、序列化options数据为dockview可用数据(layoutConfig优先)
    let serverData = options.layoutConfig || serialize(options)

    // 2、创建dockview实例
    const dockview = new DockviewComponent({
      parentElement: el,
      createComponent: option => {
        return new DefaultPanel(option, {template})
      },
      createTabComponent: option => new myDefaultTab(option, options)
    });

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
        else if(theme !== option.theme){
            setTheme(ele, theme, option.theme)
        }
        else {
            // 处理 toggle 逻辑
            toggleComponent(dock, option, option.invoke)
        }
    }





}

export function dispose(id) {
    console.log(id);
}
