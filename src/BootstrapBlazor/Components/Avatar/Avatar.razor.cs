// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

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
        .AddClass("is-circle", IsCircle)
        .AddClass("border border-info", IsBorder)
        .AddClass("border-success", IsBorder && IsLoaded.HasValue && IsLoaded.Value && !IsIcon && !IsText)
        .AddClass("border-danger", IsBorder && IsLoaded.HasValue && !IsLoaded.Value)
        .AddClass($"is-{Size.ToDescriptionString()}", Size != Size.None && Size != Size.Medium)
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
    public string Icon { get; set; } = "fa fa-user";

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
