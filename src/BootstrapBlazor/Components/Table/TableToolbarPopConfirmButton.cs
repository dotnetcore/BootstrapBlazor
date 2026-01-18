// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableToolbarPopConfirmButton 组件</para>
/// <para lang="en">TableToolbarPopConfirmButton component</para>
/// </summary>
[JSModuleNotInherited]
public class TableToolbarPopConfirmButton<TItem> : PopConfirmButtonBase, ITableToolbarButton<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 按钮点击后回调委托</para>
    /// <para lang="en">Gets or sets button click callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task>? OnConfirmCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示，默认为 true 显示</para>
    /// <para lang="en">Gets or sets whether to display. Default is true.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 选中一行时启用按钮，默认为 false</para>
    /// <para lang="en">Gets or sets whether to enable button when one row is selected. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮是否被禁用的回调方法</para>
    /// <para lang="en">Gets or sets the callback method for button disabled state</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, bool>? IsDisabledCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Table Toolbar 实例</para>
    /// <para lang="en">Gets or sets Table Toolbar instance</para>
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
    /// <inheritdoc/>
    /// </summary>
    /// <param name="disposing"></param>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Toolbar?.RemoveButton(this);
        return ValueTask.CompletedTask;
    }
}
