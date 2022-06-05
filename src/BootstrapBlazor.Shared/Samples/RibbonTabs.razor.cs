// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class RibbonTabs
{
    [NotNull]
    private IEnumerable<RibbonTabItem>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        Items = new List<RibbonTabItem>()
        {
            new() 
            {
                Text = "文件",
                Items = new List<RibbonTabItem>()
                {
                    new() { Text = "常规操作", Icon = "fa fa-fa", GroupName = "操作组一" },
                    new() { Text = "常规操作", Icon = "fa fa-fa", GroupName = "操作组一" },
                    new() { Text = "常规操作", Icon = "fa fa-fa", GroupName = "操作组一" },
                    new() { Text = "打开", Icon = "fa fa-fa", GroupName = "操作组二" },
                    new() { Text = "保存", Icon = "fa fa-fa", GroupName = "操作组二" },
                    new() { Text = "另存为", Icon = "fa fa-fa", GroupName = "操作组二" }
                }
            },
            new()
            {
                Text = "编辑",
                Items = new List<RibbonTabItem>()
                {
                    new() { Text = "打开", Icon = "fa fa-fa", GroupName = "操作组三" },
                    new() { Text = "保存", Icon = "fa fa-fa", GroupName = "操作组三" },
                    new() { Text = "另存为", Icon = "fa fa-fa", GroupName = "操作组三" },
                    new() { Text = "常规操作", Icon = "fa fa-fa", GroupName = "操作组四" },
                    new() { Text = "常规操作", Icon = "fa fa-fa", GroupName = "操作组四" },
                    new() { Text = "常规操作", Icon = "fa fa-fa", GroupName = "操作组四" }
                }
            }
        };
    }
}
