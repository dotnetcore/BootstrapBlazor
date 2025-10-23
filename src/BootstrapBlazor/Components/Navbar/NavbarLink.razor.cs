// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// NavbarLink 组件用于在导航栏中添加链接
/// </summary>
public partial class NavbarLink
{
    /// <summary>
    /// 获得/设置 Url 默认为 #
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 A 标签 target 参数 默认 null
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 显示图片地址 默认为 null
    /// </summary>
    [Parameter]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// The css class of img element default value null
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
