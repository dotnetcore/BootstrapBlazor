// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Spinner 组件基类</para>
///  <para lang="en">Spinner Component</para>
/// </summary>
public partial class Spinner
{
    /// <summary>
    ///  <para lang="zh">获取Spinner样式</para>
    ///  <para lang="en">Get Spinner Class Name</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("spinner")
        .AddClass($"spinner-{SpinnerType.ToDescriptionString()}")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"spinner-border-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh">获得/设置 Spinner 颜色 默认 None 无设置</para>
    ///  <para lang="en">Get/Set Spinner Color. Default None</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 / 设置 Spinner 大小 默认 None 无设置</para>
    ///  <para lang="en">Get/Set Spinner Size. Default None</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 Spinner 类型 默认为 Border</para>
    ///  <para lang="en">Get/Set Spinner Type. Default Border</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public SpinnerType SpinnerType { get; set; }
}
