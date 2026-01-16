// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">AnchorLink 组件</para>
///  <para lang="en">AnchorLink component</para>
/// </summary>
public partial class AnchorLink
{
    /// <summary>
    ///  <para lang="zh">获得/设置 组件 Text 显示文字</para>
    ///  <para lang="en">Gets or sets the component text display</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件 拷贝成功后 显示文字</para>
    ///  <para lang="en">Gets or sets the display text after successful copy</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 锚点图标 默认 fa-solid fa-link</para>
    ///  <para lang="en">Gets or sets the anchor icon. Default is fa-solid fa-link</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? IconString => CssBuilder.Default("anchor-link-icon")
        .AddClass(Icon)
        .Build();

    private string? ClassString => CssBuilder.Default("anchor-link")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AnchorLinkIcon);
    }
}
