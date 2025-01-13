// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MindMaps
/// </summary>
public partial class MindMaps
{
    const string SampleData1 = "{\"layout\":\"logicalStructure\",\"root\":{\"data\":{\"text\":\"<p><span>根节点</span></p>\",\"expand\":true,\"richText\":true,\"isActive\":false,\"uid\":\"c3e83e33-fdbd-4ed7-be0a-1f2ad8f1c97d\"},\"children\":[{\"data\":{\"text\":\"<p><span>二级节点</span></p>\",\"generalization\":{\"text\":\"<p><span style=\\\"color: rgb(255, 255, 255); font-family: 微软雅黑, &quot;Microsoft YaHei&quot;; font-size: 14px; font-weight: normal; font-style: normal; text-decoration: none;\\\">概要</span></p>\",\"richText\":true,\"expand\":true,\"isActive\":false},\"richText\":true,\"expand\":true,\"isActive\":false,\"uid\":\"5f7a54d5-1122-4107-8200-568865b9da96\"},\"children\":[{\"data\":{\"text\":\"<p><span>分支主题</span></p>\",\"richText\":true,\"expand\":true,\"isActive\":false,\"uid\":\"ee9834bc-3d28-42b0-98b3-d1430a2a9fcf\"},\"children\":[]},{\"data\":{\"text\":\"<p><span>分支主题</span></p>\",\"richText\":true,\"expand\":true,\"isActive\":false,\"uid\":\"2045232b-cf13-4e71-96b7-97c952e33ead\"},\"children\":[]}]}]},\"theme\":{\"template\":\"defaultTheme\",\"config\":{}},\"view\":{\"transform\":{\"scaleX\":1,\"scaleY\":1,\"shear\":0,\"rotate\":0,\"translateX\":-267.0000000000002,\"translateY\":17.5,\"originX\":0,\"originY\":0,\"a\":1,\"b\":0,\"c\":0,\"d\":1,\"e\":-267.0000000000002,\"f\":17.5},\"state\":{\"scale\":1,\"x\":-267.0000000000002,\"y\":17.5,\"sx\":-267.0000000000002,\"sy\":0.9999999999999432}}}";

    const string SampleData2 =
    """
        {
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
    """;
}
