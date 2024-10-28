﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 单元格内按钮组件
/// </summary>
[JSModuleNotInherited]
public class TableCellButton : ButtonBase, ITableCellButton
{
    /// <summary>
    /// 获得/设置 Table 扩展按钮集合实例
    /// </summary>
    [CascadingParameter]
    protected TableExtensionButton? Buttons { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool AutoSelectedRowWhenClick { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public bool AutoRenderTableWhenClick { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 默认 true 显示
    /// </summary>
    /// <remarks>一般是通过 context 进行业务判断是否需要显示功能按钮</remarks>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Buttons?.AddButton(this);

        if (Size == Size.None)
        {
            Size = Size.ExtraSmall;
        }
    }

    /// <summary>
    /// DisposeAsyncCore
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Buttons?.RemoveButton(this);
        return base.DisposeAsync(disposing);
    }
}
