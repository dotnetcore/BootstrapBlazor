// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DropUpload 组件</para>
/// <para lang="en">Drop Upload Component</para>
/// </summary>
public partial class DropUpload
{
    /// <summary>
    /// <para lang="zh">获得/设置 Body 模板，默认 null。设置 BodyTemplate 后 IconTemplate 和 TextTemplate 不生效。</para>
    /// <para lang="en">Gets or sets the body template. Default is null. When BodyTemplate is set, IconTemplate and TextTemplate are not effective.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标模板，默认 null</para>
    /// <para lang="en">Gets or sets the icon template. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? IconTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标，默认 null</para>
    /// <para lang="en">Gets or sets the icon. Default is null.</para>
    /// </summary>
    [Parameter]
    public string? UploadIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文字模板，默认 null</para>
    /// <para lang="en">Gets or sets the text template. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? TextTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传文字，默认 null</para>
    /// <para lang="en">Gets or sets the upload text. Default is null.</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UploadText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Footer，默认 false</para>
    /// <para lang="en">Gets or sets whether to display the footer. Default is false.</para>
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 字符串模板，默认 null</para>
    /// <para lang="en">Gets or sets the footer template. Default is null.</para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 字符串信息，默认 null</para>
    /// <para lang="en">Gets or sets the footer text. Default is null.</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FooterText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示上传列表，默认 true</para>
    /// <para lang="en">Gets or sets whether to display the upload file list. Default is true.</para>
    /// </summary>
    [Parameter]
    public bool ShowUploadFileList { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 设置文件格式图标回调委托</para>
    /// <para lang="en">Gets or sets the file format icon callback delegate</para>
    /// </summary>
    [Parameter]
    public Func<string?, string>? OnGetFileFormat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载中图标</para>
    /// <para lang="en">Gets or sets the loading icon.</para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传失败状态图标</para>
    /// <para lang="en">Gets or sets the upload failed status icon.</para>
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传成功状态图标</para>
    /// <para lang="en">Gets or sets the upload success status icon.</para>
    /// </summary>
    [Parameter]
    public string? ValidStatusIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<string>>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("upload is-drop")
        .AddClass("disabled", CheckStatus())
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? BodyClassString => CssBuilder.Default("upload-drop-body")
        .AddClass("btn-browser", CheckStatus() == false)
        .Build();

    private string? TextClassString => CssBuilder.Default("upload-drop-text")
        .AddClass("text-muted", CheckStatus())
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UploadIcon ??= IconTheme.GetIconByKey(ComponentIcons.DropUploadIcon);
        UploadText ??= Localizer["DropUploadText"];
    }
}
