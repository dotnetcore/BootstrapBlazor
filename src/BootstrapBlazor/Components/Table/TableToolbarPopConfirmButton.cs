// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// TableToolbarPopConfirmButton 组件
/// </summary>
[JSModuleNotInherited]
public class TableToolbarPopConfirmButton<TItem> : PopConfirmButtonBase, ITableToolbarButton<TItem>
{
    /// <summary>
    /// 获得/设置 按钮点击后回调委托
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task>? OnConfirmCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 默认 true 显示
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// 获得/设置 选中一行时启用按钮 默认 false 均可用
    /// </summary>
    [Parameter]
    public bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// 获得/设置 按钮是否被禁用回调方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }

    /// <summary>
    /// 获得/设置 Table Toolbar 实例
    /// </summary>
    [CascadingParameter]
    protected TableToolbar<TItem>? Toolbar { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Toolbar?.AddButton(this);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
        CloseButtonText ??= Localizer[nameof(CloseButtonText)];
        Content ??= Localizer[nameof(Content)];
    }

    /// <summary>
    /// DisposeAsyncCore 方法
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Toolbar?.RemoveButton(this);
        return ValueTask.CompletedTask;
    }
}
