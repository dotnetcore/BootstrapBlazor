// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SlideButton 项组件</para>
/// <para lang="en">SlideButtonItem component</para>
/// </summary>
public class SlideButtonItem : ComponentBase, IDisposable
{
    /// <summary>
    /// <para lang="zh">显示文本</para>
    /// <para lang="en">Display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">选项值</para>
    /// <para lang="en">Option value</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? Value { get; set; }

    [CascadingParameter]
    private List<SlideButtonItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnInitialized()
    {
        Items?.Add(this);
    }

    /// <summary>
    /// <para lang="zh">资源销毁</para>
    /// <para lang="en">Dispose usage</para>
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            Items?.Remove(this);
        }
    }

    /// <summary>
    /// <para lang="zh">资源销毁</para>
    /// <para lang="en">Dispose usage</para>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
