// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格 Toolbar 按钮组件
/// </summary>
public class TableToolbarButton<TItem> : ButtonBase
{
    /// <summary>
    /// 获得/设置 按钮点击后回调委托
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }

    /// <summary>
    /// 获得/设置 选中一行时启用按钮 默认 false 均可用
    /// </summary>
    [Parameter]
    public bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 默认 true 显示
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// 获得/设置 Table Toolbar 实例
    /// </summary>
    [CascadingParameter]
    protected TableToolbar<TItem>? Toolbar { get; set; }

    /// <summary>
    /// 组件初始化方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Toolbar?.AddButton(this);
    }

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override async ValueTask DisposeAsyncCore(bool disposing)
    {
        Toolbar?.RemoveButton(this);
        await base.DisposeAsyncCore(disposing);
    }
}
