// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Charts;

/// <summary>
/// 
/// </summary>
public sealed partial class Index
{
    /// <summary>
    /// 获得方法列表
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<MethodItem> GetMethodAttributes() => new MethodItem[]
    {
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.OnInitAsync),
            Description = "组件数据初始化委托方法",
            Parameters = "Func<Task<ChartDataSource>>",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.OnAfterInitAsync),
            Description = "客户端绘制图表完毕后回调此委托方法",
            Parameters = "Func<Task>",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.OnAfterUpdateAsync),
            Description = "客户端更新图表完毕后回调此委托方法",
            Parameters = "Func<ChartAction, Task>",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.Update),
            Description = "更新图表方法",
            Parameters ="Task",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = nameof(BootstrapBlazor.Components.Chart.Reload),
            Description = "重新加载,强制重新渲染图表",
            Parameters = "Task",
            ReturnValue = " — "
        }
    };
}
