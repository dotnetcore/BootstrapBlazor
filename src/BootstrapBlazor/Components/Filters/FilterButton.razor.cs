// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// FilterButton 组件
/// </summary>
public partial class FilterButton<TValue> : Dropdown<TValue>
{
    /// <summary>
    /// 获得/设置 清除过滤条件时的回调方法
    /// </summary>
    [Parameter]
    public Func<Task>? OnClearFilter { get; set; }

    /// <summary>
    /// 获得/设置 过滤按钮图标
    /// </summary>
    [Parameter]
    public string? FilterIcon { get; set; }

    /// <summary>
    /// 获得/设置 重置按钮图标
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        FilterIcon ??= IconTheme.GetIconByKey(ComponentIcons.FilterButtonFilterIcon);
        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.FilterButtonClearIcon);
    }

    private async Task ClearFilter()
    {
        if (OnClearFilter != null)
        {
            await OnClearFilter();
        }
    }
}
