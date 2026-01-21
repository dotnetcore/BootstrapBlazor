// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SegmentedItem 组件</para>
/// <para lang="en">SegmentedItem Component</para>
/// </summary>
public class SegmentedItem<TValue> : ComponentBase, IDisposable
{
    /// <summary>
    /// <para lang="zh">组件内容</para>
    /// <para lang="en">Child Content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否禁用 默认 false</para>
    /// <para lang="en">Gets or sets Whether disabled. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中 默认 false</para>
    /// <para lang="en">Gets or sets Whether active. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标 默认 null</para>
    /// <para lang="en">Gets or sets Icon. Default null</para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文字 默认 null</para>
    /// <para lang="en">Gets or sets Text. Default null</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件值 默认 null</para>
    /// <para lang="en">Gets or sets Value. Default null</para>
    /// </summary>
    [Parameter]
    public TValue? Value { get; set; }

    [CascadingParameter]
    private List<SegmentedOption<TValue>>? Items { get; set; }

    private SegmentedOption<TValue>? _option;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _option = new SegmentedOption<TValue>()
        {
            Text = Text,
            Value = Value,
            Active = IsActive,
            IsDisabled = IsDisabled,
            Icon = Icon,
            ChildContent = ChildContent
        };
        Items?.Add(_option);
    }

    /// <summary>
    /// <para lang="zh">资源销毁方法</para>
    /// <para lang="en">Dispose Method</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (_option != null && disposing)
        {
            Items?.Remove(_option);
        }
    }

    /// <summary>
    /// <para lang="zh">资源销毁方法</para>
    /// <para lang="en">Dispose Method</para>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
