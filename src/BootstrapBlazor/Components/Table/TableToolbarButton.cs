// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 表格 Toolbar 按钮组件
/// </summary>
public class TableToolbarButton<TItem> : BootstrapComponentBase, IToolbarButton<TItem>, IDisposable
{
    /// <summary>
    /// 获得/设置 是否为异步按钮，默认为 false 如果为 true 表示是异步按钮，点击按钮后禁用自身并且等待异步完成，过程中显示 loading 动画
    /// </summary>
    [Parameter]
    public bool IsAsync { get; set; }

    /// <summary>
    /// 获得/设置 按钮颜色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 是否禁用
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// 获得/设置 按钮点击后回调委托
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }

    /// <summary>
    /// 获得/设置 按钮点击后回调委托
    /// </summary>
    [Parameter]
    public Func<Task>? OnClick { get; set; }

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
    /// Dispose 方法
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Toolbar?.RemoveButton(this);
        }
    }

    /// <summary>
    /// Dispose 方法
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
