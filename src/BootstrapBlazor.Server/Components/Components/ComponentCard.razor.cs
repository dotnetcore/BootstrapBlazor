// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class ComponentCard
{
    [Inject, NotNull]
    private IOptions<WebsiteOptions>? WebsiteOption { get; set; }

    private string ImageUrl => $"{WebsiteOption.Value.AssetRootPath}images/{Image}";

    private string? ClassString => CssBuilder.Default("col-12 col-sm-6 col-md-4 col-lg-3")
        .AddClass("d-none", IsHide)
        .Build();

    /// <summary>
    /// 获得/设置 Header 文字
    /// </summary>
    [Parameter]
    public string Text { get; set; } = "未设置";

    /// <summary>
    /// 获得/设置 组件图片
    /// </summary>
    [Parameter]
    public string Image { get; set; } = "Divider.svg";

    /// <summary>
    /// 获得/设置 链接地址
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    [CascadingParameter]
    private List<string>? ComponentNames { get; set; }

    [CascadingParameter]
    private ComponentCategory? Parent { get; set; }

    [CascadingParameter]
    private string? SearchText { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ComponentNames?.Add(Text);
        Parent?.Add(this);
    }

    /// <summary>
    /// 
    /// </summary>
    internal bool IsHide => !string.IsNullOrEmpty(SearchText) && !Text.Contains(SearchText, StringComparison.OrdinalIgnoreCase);
}
