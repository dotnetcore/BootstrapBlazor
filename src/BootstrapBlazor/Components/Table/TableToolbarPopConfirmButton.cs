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
    /// <para lang="en">Gets or sets button点击后回调delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<IEnumerable<TItem>, Task>? OnConfirmCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 默认 true 显示</para>
    /// <para lang="en">Gets or sets whetherdisplay Default is true display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsShow { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 选中一行时启用按钮 默认 false 均可用</para>
    /// <para lang="en">Gets or sets 选中一行时启用button Default is false 均可用</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsEnableWhenSelectedOneRow { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮是否被禁用回调方法</para>
    /// <para lang="en">Gets or sets buttonwhether被禁用callback method</para>
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
    /// <para lang="zh">DisposeAsyncCore 方法</para>
    /// <para lang="en">DisposeAsyncCore 方法</para>
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected override ValueTask DisposeAsync(bool disposing)
    {
        Toolbar?.RemoveButton(this);
        return ValueTask.CompletedTask;
    }
}
