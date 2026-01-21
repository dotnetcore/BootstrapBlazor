// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">按钮上传组件</para>
/// <para lang="en">Button Upload Component</para>
/// </summary>
public partial class ButtonUpload<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 浏览按钮加载中图标</para>
    /// <para lang="en">Gets or sets the loading icon for the browse button</para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传失败状态图标</para>
    /// <para lang="en">Gets or sets the upload failed status icon</para>
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传成功状态图标</para>
    /// <para lang="en">Gets or sets the upload success status icon</para>
    /// </summary>
    [Parameter]
    public string? ValidStatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 浏览按钮图标</para>
    /// <para lang="en">Gets or sets the browse button icon</para>
    /// </summary>
    [Parameter]
    public string? BrowserButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传按钮样式，默认 null 使用 Button 默认 Primary 颜色</para>
    /// <para lang="en">Gets or sets the upload button style. Default is null, uses Button Primary color.</para>
    /// </summary>
    [Parameter]
    public string? BrowserButtonClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示上传列表，默认 true</para>
    /// <para lang="en">Gets or sets whether to display the upload file list. Default is true.</para>
    /// </summary>
    [Parameter]
    public bool ShowUploadFileList { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 浏览按钮显示文字</para>
    /// <para lang="en">Gets or sets the browse button display text</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BrowserButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 浏览按钮颜色</para>
    /// <para lang="en">Gets or sets the browse button color</para>
    /// </summary>
    [Parameter]
    public Color BrowserButtonColor { get; set; } = Color.Primary;

    /// <summary>
    /// <para lang="zh">获得/设置 按钮大小</para>
    /// <para lang="en">Gets or sets the button size</para>
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件内容</para>
    /// <para lang="en">Gets or sets the child content</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 设置文件格式图标回调委托</para>
    /// <para lang="en">Gets or sets the callback delegate for setting file format icons</para>
    /// </summary>
    [Parameter]
    public Func<string?, string>? OnGetFileFormat { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<TValue>>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? BrowserButtonClassString => CssBuilder.Default("btn-browser upload-drop-body")
        .AddClass(BrowserButtonClass, !string.IsNullOrEmpty(BrowserButtonClass))
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];
        BrowserButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.ButtonUploadBrowserButtonIcon);
    }
}
