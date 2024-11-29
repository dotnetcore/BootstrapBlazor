// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Components;

/// <summary>
/// 自定义行组件
/// </summary>
public partial class CustomTableRow
{
    /// <summary>
    /// 获得/设置 行上下文数据实例
    /// </summary>
    [Parameter]
    [NotNull]
    public TableRowContext<Foo>? Context { get; set; }

    /// <summary>
    /// 获得/设置 值改变回调方法
    /// </summary>
    [Parameter]
    public Func<TableRowContext<Foo>, Task>? OnValueChanged { get; set; }

    private async Task OnDateTimeChanged(DateTime? dt)
    {
        // 通知数据源数据已更新
        Context.Row.DateTime = dt;
        Context.Row.Count = Random.Shared.Next(1, 100);
        if (OnValueChanged != null)
        {
            await OnValueChanged(Context);
        }

        // 仅更新本组件不更新父 Table 组件
        StateHasChanged();
    }
}
