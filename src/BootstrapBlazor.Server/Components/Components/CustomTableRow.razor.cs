// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

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
