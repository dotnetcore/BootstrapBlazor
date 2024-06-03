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

    options.layoutConfig233 = {
        "grid": {
            "root": {
                "type": "branch",
                "data": [
                    {
                        "type": "branch",
                        "data": [
                            {
                                "type": "branch",
                                "data": [
                                    {
                                        "type": "leaf",
                                        "data": {
                                            "views": [
                                                "panel_dv_BJ9g57Pf"
                                            ],
                                            "activeView": "panel_dv_BJ9g57Pf",
                                            "id": "1"
                                        },
                                        "size": 368
                                    },
                                    {
                                        "type": "leaf",
                                        "data": {
                                            "views": [
                                                "panel_dv_6z6Uo1tV",
                                                "panel_dv_RhU0K6ux"
                                            ],
                                            "activeView": "panel_dv_RhU0K6ux",
                                            "id": "3"
                                        },
                                        "size": 632
                                    }
                                ],
                                "size": 400
                            },
                            {
                                "type": "branch",
                                "data": [
                                    {
                                        "type": "leaf",
                                        "data": {
                                            "views": [
                                                "panel_dv_Da4dpuc4"
                                            ],
                                            "activeView": "panel_dv_Da4dpuc4",
                                            "id": "2"
                                        },
                                        "size": 500
                                    },
                                    {
                                        "type": "branch",
                                        "data": [
                                            {
                                                "type": "leaf",
                                                "data": {
                                                    "views": [
                                                        "panel_dv_OE12l95I",
                                                        "panel_dv_p6VoRsni"
                                                    ],
                                                    "activeView": "panel_dv_p6VoRsni",
                                                    "id": "5"
                                                },
                                                "size": 200
                                            },
                                            {
                                                "type": "branch",
                                                "data": [
                                                    {
                                                        "type": "leaf",
                                                        "data": {
                                                            "views": [
                                                                "panel_dv_uGAjrJYu"
                                                            ],
                                                            "activeView": "panel_dv_uGAjrJYu",
                                                            "id": "6"
                                                        },
                                                        "size": 250
                                                    },
                                                    {
                                                        "type": "leaf",
                                                        "data": {
                                                            "views": [
                                                                "panel_dv_RV4axdB9"
                                                            ],
                                                            "activeView": "panel_dv_RV4axdB9",
                                                            "id": "7"
                                                        },
                                                        "size": 250
                                                    }
                                                ],
                                                "size": 200
                                            }
                                        ],
                                        "size": 500
                                    }
                                ],
                                "size": 400
                            }
                        ],
                        "size": 1000
                    }
                ],
                "size": 800
            },
            "width": 1000,
            "height": 800,
            "orientation": "HORIZONTAL"
        },
        "panels": {
            "panel_dv_BJ9g57Pf": {
                "id": "panel_dv_BJ9g57Pf",
                "tabComponent": "d1",
                "contentComponent": "d1",
                "title": "Panel dv_BJ9g57Pf"
            },
            "panel_dv_6z6Uo1tV": {
                "id": "panel_dv_6z6Uo1tV",
                "tabComponent": "d2",
                "contentComponent": "d2",
                "title": "Panel dv_6z6Uo1tV"
            },
            "panel_dv_RhU0K6ux": {
                "id": "panel_dv_RhU0K6ux",
                "tabComponent": "d3",
                "contentComponent": "d3",
                "title": "Panel dv_RhU0K6ux"
            },
            "panel_dv_Da4dpuc4": {
                "id": "panel_dv_Da4dpuc4",
                "tabComponent": "d4",
                "contentComponent": "d4",
                "title": "Panel dv_Da4dpuc4"
            },
            "panel_dv_OE12l95I": {
                "id": "panel_dv_OE12l95I",
                "tabComponent": "d5",
                "contentComponent": "d5",
                "title": "Panel dv_OE12l95I"
            },
            "panel_dv_p6VoRsni": {
                "id": "panel_dv_p6VoRsni",
                "tabComponent": "d6",
                "contentComponent": "d6",
                "title": "Panel dv_p6VoRsni"
            },
            "panel_dv_uGAjrJYu": {
                "id": "panel_dv_uGAjrJYu",
                "tabComponent": "d7",
                "contentComponent": "d7",
                "title": "Panel dv_uGAjrJYu"
            },
            "panel_dv_RV4axdB9": {
                "id": "panel_dv_RV4axdB9",
                "tabComponent": "d8",
                "contentComponent": "d8",
                "title": "Panel dv_RV4axdB9"
            }
        },
        "activeGroup": "2"
    }
    options.TestData233 = {
        cType: 'column',
        children: [
            {
                cType: 'row',
                children: [
                    {
                        panels: [
                            {
                                id: 'panel_dv_BJ9g57Pf',
                                name: 'd1'
                            }
                        ]
                    },
                    {
                        panels: [
                            {
                                id: 'panel_dv_6z6Uo1tV',
                                name: 'd2'
                            },
                            {
                                id: 'panel_dv_RhU0K6ux","name',
                                name: 'd3'
                            }
                        ]
                    }
                ]
            },
            {
                cType: 'row',
                children: [
                    {
                        panels: [
                            {
                                id: 'panel_dv_Da4dpuc4',
                                name: 'd4'
                            }
                        ]
                    },
                    {
                        cType: 'column',
                        children: [
                            {
                                panels: [
                                    {
                                        id: 'panel_dv_OE12l95I',
                                        name: 'd5'
                                    },
                                    {
                                        id: 'panel_dv_p6VoRsni',
                                        name: 'd6'
                                    }
                                ]
                            },
                            {
                                cType: 'row',
                                children: [
                                    {
                                        panels: [
                                            {
                                                id: 'panel_dv_uGAjrJYu',
                                                name: 'd7'
                                            }
                                        ]
                                    },
                                    {
                                        panels: [
                                            {
                                                id: 'panel_dv_RV4axdB9',
                                                name: 'd8'
                                            }
                                        ]
                                    }
                                ]
                            }
                        ]
                    }
                ]
            },
        ]
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
    addHook(dockview, dockviewData, options, template)
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
            toggleComponent(dock, option)
        }
    }





}

export function dispose(id) {
    console.log(id);
}
