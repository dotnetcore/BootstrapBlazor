// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples.Charts;

/// <summary>
/// 
/// </summary>
public sealed partial class Index
{
    [Inject]
    [NotNull]
    private VersionService? VersionManager { get; set; }

    private string Version { get; set; } = "fetching";

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        Version = await VersionManager.GetVersionAsync("bootstrapblazor.chart");
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnInitAsync",
                Description="组件数据初始化委托方法",
                Type ="Func<Task<ChartDataSource>>"
            },
            new EventItem()
            {
                Name = "OnAfterInitAsync",
                Description="客户端绘制图表完毕后回调此委托方法",
                Type ="Func<Task>"
            },
            new EventItem()
            {
                Name = "OnAfterUpdateAsync",
                Description="客户端更新图表完毕后回调此委托方法",
                Type ="Func<ChartAction, Task>"
            }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Width",
                Description = "组件宽度支持单位 如: 100px 75%",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ChartType",
                Description = "设置图表类型",
                Type = "ChartType",
                ValueList = "Line|Bar|Pie|Doughnut|Bubble",
                DefaultValue = "Line"
            }
    };
}
