// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

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
