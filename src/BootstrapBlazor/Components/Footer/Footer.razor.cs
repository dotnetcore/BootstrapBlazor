// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Footer 组件</para>
/// <para lang="en">Footer Component</para>
/// </summary>
public partial class Footer
{
    /// <summary>
    /// <para lang="zh">获得 按钮样式集合</para>
    /// <para lang="en">Get Button Style Collection</para>
    /// </summary>
    /// <returns></returns>
    protected string? ClassName => CssBuilder.Default("footer")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 显示文字</para>
    /// <para lang="en">Gets or sets Footer Text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 组件中返回顶端按钮控制的滚动条所在组件 设置 <see cref="ShowGoto"/> 为 true 时生效</para>
    /// <para lang="en">Gets or sets The component where the scrollbar controlled by the back-to-top button in the Footer component is located. Effective when <see cref="ShowGoto"/> is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 内容</para>
    /// <para lang="en">Gets or sets Content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Goto 小组件 默认 true 显示</para>
    /// <para lang="en">Gets or sets Whether to Show Goto Widget Default true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowGoto { get; set; } = true;
}
