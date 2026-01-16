// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">CollapseItem 组件</para>
///  <para lang="en">CollapseItem component</para>
/// </summary>
public class CollapseItem : BootstrapComponentBase, IDisposable
{
    /// <summary>
    ///  <para lang="zh">获得/设置 文本文字</para>
    ///  <para lang="en">Get/Set text</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 当前状态是否收缩 默认 true</para>
    ///  <para lang="en">Get/Set whether current status is collapsed, default is true</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 图标字符串 默认为 null</para>
    ///  <para lang="en">Get/Set icon string, default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 标题颜色 默认无颜色 Color.None</para>
    ///  <para lang="en">Get/Set title color, default is Color.None</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color TitleColor { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 CSS 样式名称 默认 null</para>
    ///  <para lang="en">Get/Set CSS style name, default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Class { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件内容</para>
    ///  <para lang="en">Get/Set component content</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Header CSS 样式名称 默认 null</para>
    ///  <para lang="en">Get/Set Header CSS style name, default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? HeaderClass { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件 Header 模板</para>
    ///  <para lang="en">Get/Set component Header template</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? HeaderTemplate { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 所属 Collapse 实例</para>
    ///  <para lang="en">Get/Set the Collapse instance it belongs to</para>
    /// </summary>
    [CascadingParameter]
    protected Collapse? Collapse { get; set; }

    /// <summary>
    ///  <para lang="zh">OnInitialized 方法</para>
    ///  <para lang="en">OnInitialized method</para>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Collapse?.AddItem(this);
    }

    /// <summary>
    ///  <para lang="zh">设置是否被选中方法</para>
    ///  <para lang="en">Set whether it is collapsed method</para>
    /// </summary>
    /// <param name="collapsed"></param>
    public virtual void SetCollapsed(bool collapsed) => IsCollapsed = collapsed;

    private bool disposedValue;

    /// <summary>
    ///  <para lang="zh">资源销毁</para>
    ///  <para lang="en">Resource disposal</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;

            if (disposing)
            {
                Collapse?.RemoveItem(this);
            }
        }
    }

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
