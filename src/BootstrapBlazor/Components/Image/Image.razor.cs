// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using ErrorEventArgs = Microsoft.AspNetCore.Components.Web.ErrorEventArgs;

namespace BootstrapBlazor.Components;

/// <summary>
/// Image 组件
/// </summary>
public partial class Image
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb_img")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ImageClassString => CssBuilder.Default()
        .AddClass($"obj-fit-{FitMode.ToDescriptionString()}")
        .Build();

    /// <summary>
    /// 获得/设置 图片 Url 默认 null 必填
    /// </summary>
    [Parameter]
    [NotNull]
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 原生 alt 属性 默认 null 未设置
    /// </summary>
    [Parameter]
    public string? Alt { get; set; }

    /// <summary>
    /// 获得/设置 图片加载时占位符 默认 null
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 是否显示占位符 适用于大图片加载 占位符内容请查看 <see cref="PlaceHolder"/> 默认 false
    /// </summary>
    [Parameter]
    public bool ShowPlaceHolder { get; set; }

    /// <summary>
    /// 获得/设置 占位模板 未设置 <see cref="Url"/> 或者 正在加载时显示 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment? PlaceHolderTemplate { get; set; }

    /// <summary>
    /// 获得/设置 错误模板 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment? ErrorTemplate { get; set; }

    /// <summary>
    /// 获得/设置 原生 object-fit 属性 默认 fill 未设置
    /// </summary>
    [Parameter]
    public ObjectFitMode FitMode { get; set; }

    private bool ShowImage => !string.IsNullOrEmpty(Url);

    private bool Loaded { get; set; }

    private RenderFragment RenderImage() => builder =>
    {
        builder.OpenElement(0, "img");
        builder.AddAttribute(1, "class", ImageClassString);
        if (!string.IsNullOrEmpty(Url))
        {
            builder.AddAttribute(2, "src", Url);
        }
        if (!string.IsNullOrEmpty(Alt))
        {
            builder.AddAttribute(3, "alt", Alt);
        }
        if (ShowPlaceHolder)
        {
            builder.AddAttribute(4, "onload", EventCallback.Factory.Create<ProgressEventArgs>(this, args =>
            {
                Loaded = true;
            }));
        }
        builder.CloseElement();
    };

    private void OnError(ErrorEventArgs e)
    {

    }

    private void OnLoad(ProgressEventArgs e)
    {
        Loaded = true;
    }
}
