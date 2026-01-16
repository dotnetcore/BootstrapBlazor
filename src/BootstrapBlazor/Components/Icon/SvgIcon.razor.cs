// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">SvgIcon 组件</para>
///  <para lang="en">SvgIcon Component</para>
/// </summary>
public partial class SvgIcon
{
    /// <summary>
    ///  <para lang="zh">获得/设置 图标名称</para>
    ///  <para lang="en">Get/Set Icon Name</para>
    /// </summary>
    [Parameter, NotNull]
    [EditorRequired]
    public string? Name { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 图标地址</para>
    ///  <para lang="en">Get Icon URL</para>
    /// </summary>
    [Parameter, NotNull]
    public string? Href { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 样式字符串</para>
    ///  <para lang="en">Get Style String</para>
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-svg-icon")
        .AddClass($"bb-svg-icon-{Name}", !string.IsNullOrEmpty(Name))
        .Build();

    private string? _hrefString;

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Href ??= "./_content/BootstrapBlazor.IconPark/icon-park.svg";
        _hrefString = $"{Href}#{Name}";
    }
}
