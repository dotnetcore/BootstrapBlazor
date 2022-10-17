// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;

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
            Description="Component data initialization delegate method",
            Type ="Func<Task<ChartDataSource>>"
        },
        new EventItem()
        {
            Name = "OnAfterInitAsync",
            Description="This delegate method is called back after the client draws the chart",
            Type ="Func<Task>"
        },
        new EventItem()
        {
            Name = "OnAfterUpdateAsync",
            Description="This delegate method is called back after the client has finished updating the chart",
            Type ="Func<ChartAction, Task>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Width",
            Description = "Component width supports units such as: 100px 75%",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ChartType",
            Description = "Set chart type",
            Type = "ChartType",
            ValueList = "Line|Bar|Pie|Doughnut|Bubble",
            DefaultValue = "Line"
        }
    };
}
