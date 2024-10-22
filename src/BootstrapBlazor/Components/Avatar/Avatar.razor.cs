﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Avatar 头像框组件
/// </summary>
public partial class Avatar
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default("avatar")
        .AddClass("avatar-circle", IsCircle)
        .AddClass($"avatar-{Size.ToDescriptionString()}", Size != Size.None && Size != Size.Medium)
        .AddClass("border border-info", IsBorder)
        .AddClass("border-success", IsBorder && IsLoaded.HasValue && IsLoaded.Value && !IsIcon && !IsText)
        .AddClass("border-danger", IsBorder && IsLoaded.HasValue && !IsLoaded.Value)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 图片样式
    /// </summary>
    private string? ImgClassString => (IsLoaded.HasValue && IsLoaded.Value) ? null : "d-none";

    /// <summary>
    /// 获得/设置 是否为圆形
    /// </summary>
    [Parameter]
    public bool IsCircle { get; set; }

    /// <summary>
    /// 获得/设置 Image 头像路径地址
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 是否为图标
    /// </summary>
    [Parameter]
    public bool IsIcon { get; set; }

    /// <summary>
    /// 获得/设置 头像框显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 是否为显示为文字
    /// </summary>
    [Parameter]
    public bool IsText { get; set; }

    /// <summary>
    /// 获得/设置 头像框显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 头像框大小
    /// </summary>
    [Parameter]
    public Size Size { get; set; } = Size.Medium;

    /// <summary>
    /// 获得/设置 是否显示 Border 默认为 false
    /// </summary>
    [Parameter]
    public bool IsBorder { get; set; }

    /// <summary>
    /// 获得/设置 获取图片地址异步回调方法
    /// </summary>
    [Parameter]
    public Func<Task<string>>? GetUrlAsync { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// 获得/设置 是否显示图片
    /// </summary>
    private bool? IsLoaded { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (string.IsNullOrEmpty(Url) && GetUrlAsync != null)
        {
            Url = await GetUrlAsync();
        }
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.AvatarIcon);
    }

    /// <summary>
    /// 图片加载失败时回调此方法
    /// </summary>
    private void OnError()
    {
        IsIcon = true;
        IsLoaded = false;
    }

    /// <summary>
    /// 图片加载成功时回调此方法
    /// </summary>
    private void OnLoad()
    {
        IsLoaded = true;
    }
}
