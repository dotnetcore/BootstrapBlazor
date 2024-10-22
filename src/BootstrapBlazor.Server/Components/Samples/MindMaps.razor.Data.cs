// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MindMaps
/// </summary>
public partial class MindMaps
{
    static readonly string SampleData = """
       {
           "root":{
               "data":{
                   "text":"根节点"
               },
               "children":[
                   {
                       "data":{
                           "text":"二级节点1",
                           "expand":true
                       },
                       "children":[
                           {
                               "data":{
                                   "text":"分支主题"
                               },
                               "children":[
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       },
                                       "children":[
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               },
                                               "children":[
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       },
                                                       "children":[
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               },
                                                               "children":[
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   },
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       },
                                                                       "children":[
                                                                           {
                                                                               "data":{
                                                                                   "text":"分支主题"
                                                                               }
                                                                           },
                                                                           {
                                                                               "data":{
                                                                                   "text":"分支主题"
                                                                               }
                                                                           },
                                                                           {
                                                                               "data":{
                                                                                   "text":"分支主题"
                                                                               }
                                                                           },
                                                                           {
                                                                               "data":{
                                                                                   "text":"分支主题"
                                                                               }
                                                                           }
                                                                       ]
                                                                   },
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   },
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   }
                                                               ]
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           }
                                                       ]
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   }
                                               ]
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           }
                                       ]
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   }
                               ]
                           }
                       ]
                   },
                   {
                       "data":{
                           "text":"二级节点2",
                           "expand":false
                       },
                       "children":[
                           {
                               "data":{
                                   "text":"分支主题"
                               }
                           },
                           {
                               "data":{
                                   "text":"分支主题"
                               },
                               "children":[
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   }
                               ]
                           },
                           {
                               "data":{
                                   "text":"分支主题"
                               }
                           },
                           {
                               "data":{
                                   "text":"分支主题"
                               }
                           }
                       ]
                   },
                   {
                       "data":{
                           "text":"二级节点3",
                           "expand":true
                       },
                       "children":[
                           {
                               "data":{
                                   "text":"分支主题"
                               },
                               "children":[
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       },
                                       "children":[
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           }
                                       ]
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   }
                               ]
                           }
                       ]
                   },
                   {
                       "data":{
                           "text":"二级节点4",
                           "expand":false
                       },
                       "children":[
                           {
                               "data":{
                                   "text":"分支主题"
                               },
                               "children":[
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       },
                                       "children":[
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               },
                                               "children":[
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   }
                                               ]
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           }
                                       ]
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       }
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       },
                                       "children":[
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               },
                                               "children":[
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       },
                                                       "children":[
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           }
                                                       ]
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       },
                                                       "children":[
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               },
                                                               "children":[
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   },
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   },
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   },
                                                                   {
                                                                       "data":{
                                                                           "text":"分支主题"
                                                                       }
                                                                   }
                                                               ]
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           }
                                                       ]
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   }
                                               ]
                                           }
                                       ]
                                   },
                                   {
                                       "data":{
                                           "text":"分支主题"
                                       },
                                       "children":[
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               }
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               },
                                               "children":[
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       },
                                                       "children":[
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           },
                                                           {
                                                               "data":{
                                                                   "text":"分支主题"
                                                               }
                                                           }
                                                       ]
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   }
                                               ]
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               },
                                               "children":[
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   }
                                               ]
                                           },
                                           {
                                               "data":{
                                                   "text":"分支主题"
                                               },
                                               "children":[
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   },
                                                   {
                                                       "data":{
                                                           "text":"分支主题"
                                                       }
                                                   }
                                               ]
                                           }
                                       ]
                                   }
                               ]
                           }
                       ]
                   }
               ]
           }
       }
       """;

    static readonly string SampleData2 = """
       {
           "root": {
               "data": {
                   "text": "一周安排"
               },
               "children": [
                   {
                       "data": {
                           "text": "生活"
                       },
                       "children": [
                           {
                               "data": {
                                   "text": "锻炼"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "晨跑"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "7:00-8:00"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "夜跑"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "20:00-21:00"
                                               },
                                               "children": []
                                           }
                                       ]
                                   }
                               ]
                           },
                           {
                               "data": {
                                   "text": "饮食"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "早餐"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "8:30"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "午餐"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "11:30"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "晚餐"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "19:00"
                                               },
                                               "children": []
                                           }
                                       ]
                                   }
                               ]
                           },
                           {
                               "data": {
                                   "text": "休息"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "午休"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "12:30-13:00"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "晚休"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "23:00-6:30"
                                               },
                                               "children": []
                                           }
                                       ]
                                   }
                               ]
                           }
                       ]
                   },
                   {
                       "data": {
                           "text": "工作日\n周一至周五"
                       },
                       "children": [
                           {
                               "data": {
                                   "text": "日常工作"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "9:00-18:00"
                                       },
                                       "children": []
                                   }
                               ]
                           },
                           {
                               "data": {
                                   "text": "工作总结"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "21:00-22:00"
                                       },
                                       "children": []
                                   }
                               ]
                           }
                       ]
                   },
                   {
                       "data": {
                           "text": "学习"
                       },
                       "children": [
                           {
                               "data": {
                                   "text": "工作日"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "早间新闻"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "8:00-8:30"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "阅读"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "21:00-23:00"
                                               },
                                               "children": []
                                           }
                                       ]
                                   }
                               ]
                           },
                           {
                               "data": {
                                   "text": "休息日"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "财务管理"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "9:00-10:30"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "职场技能"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "14:00-15:30"
                                               },
                                               "children": []
                                           }
                                       ]
                                   },
                                   {
                                       "data": {
                                           "text": "其他书籍"
                                       },
                                       "children": [
                                           {
                                               "data": {
                                                   "text": "16:00-18:00"
                                               },
                                               "children": []
                                           }
                                       ]
                                   }
                               ]
                           }
                       ]
                   },
                   {
                       "data": {
                           "text": "休闲娱乐"
                       },
                       "children": [
                           {
                               "data": {
                                   "text": "看电影"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "1~2部"
                                       },
                                       "children": []
                                   }
                               ]
                           },
                           {
                               "data": {
                                   "text": "逛街"
                               },
                               "children": [
                                   {
                                       "data": {
                                           "text": "1~2次"
                                       },
                                       "children": []
                                   }
                               ]
                           }
                       ]
                   }
               ]
           }
       }
       """;
}
