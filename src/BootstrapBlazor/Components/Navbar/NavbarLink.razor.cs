// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">NavbarLink 组件用于在导航栏中添加链接</para>
///  <para lang="en">NavbarLink component用于在导航栏中添加链接</para>
/// </summary>
public partial class NavbarLink
{
    /// <summary>
    ///  <para lang="zh">获得/设置 Url 默认为 #</para>
    ///  <para lang="en">Gets or sets Url Default is为 #</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 A 标签 target 参数 默认 null</para>
    ///  <para lang="en">Gets or sets A 标签 target 参数 Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 显示图片地址 默认为 null</para>
    ///  <para lang="en">Gets or sets display图片地址 Default is为 null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    ///  <para lang="zh">css class of img element default value null</para>
    ///  <para lang="en">The css class of img element default value null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ImageCss { get; set; }

    private string? ClassString => CssBuilder.Default("nav-link")
        .AddClass("disabled", IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private async Task OnClickButton()
    {
        if (IsAsync)
        {
            IsAsyncLoading = true;
            IsDisabled = true;
        }

        await HandlerClick();

        // 恢复按钮
        if (IsAsync)
        {
            IsDisabled = IsKeepDisabled;
            IsAsyncLoading = false;
        }
    }
}
