// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// DropUpload 组件
/// </summary>
public partial class DropUpload
{
    /// <summary>
    /// 获得/设置 Body 模板 默认 null
    /// <para>设置 BodyTemplate 后 <see cref="IconTemplate"/> <see cref="TextTemplate"/> 不生效</para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    ///获得/设置 图标模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? IconTemplate { get; set; }

    /// <summary>
    /// 获得/设置 图标 默认 null
    /// </summary>
    [Parameter]
    public string? UploadIcon { get; set; }

    /// <summary>
    /// 获得/设置 文字模板 默认 null
    /// </summary>
    [Parameter]
    public RenderFragment? TextTemplate { get; set; }

    /// <summary>
    /// 获得/设置 上传文字 默认 null
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UploadText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Footer 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// 获得/设置 Footer 字符串模板 默认 null 未设置
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// 获得/设置 Footer 字符串信息 默认 null 未设置
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FooterText { get; set; }

    /// <summary>
    /// 获得/设置 是否显示上传列表 默认 true
    /// </summary>
    [Parameter]
    public bool ShowUploadFileList { get; set; } = true;

    /// <summary>
    /// 获得/设置 设置文件格式图标回调委托
    /// </summary>
    [Parameter]
    public Func<string?, string>? OnGetFileFormat { get; set; }

    /// <summary>
    /// 获得/设置 取消图标
    /// </summary>
    [Parameter]
    public string? CancelIcon { get; set; }

    /// <summary>
    /// 获得/设置 点击取消按钮回调此方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnCancel { get; set; }

    /// <summary>
    /// 获得/设置 是否显示下载按钮 默认 false
    /// </summary>
    [Parameter]
    public bool ShowDownloadButton { get; set; }

    /// <summary>
    /// 获得/设置 点击下载按钮回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnDownload { get; set; }

    /// <summary>
    /// 获得/设置 下载按钮图标
    /// </summary>
    [Parameter]
    public string? DownloadIcon { get; set; }

    /// <summary>
    /// 获得/设置 加载中图标
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// 获得/设置 上传失败状态图标
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 上传成功状态图标
    /// </summary>
    [Parameter]
    public string? ValidStatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 删除按钮图标
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconExcel { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconDocx { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconPPT { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconAudio { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconVideo { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconCode { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconPdf { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconZip { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconArchive { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconImage { get; set; }

    /// <summary>
    /// 获得/设置 Excel 类型文件图标
    /// </summary>
    [Parameter]
    public string? FileIconFile { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<string>>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("upload is-drop")
        .AddClass("disabled", CanUpload() == false)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? BodyClassString => CssBuilder.Default("upload-drop-body")
        .AddClass("btn-browser", CanUpload())
        .Build();

    private string? TextClassString => CssBuilder.Default("upload-drop-text")
        .AddClass("text-muted", CanUpload() == false)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UploadIcon ??= IconTheme.GetIconByKey(ComponentIcons.DropUploadIcon);
        UploadText ??= Localizer["DropUploadText"];
        FooterText ??= Localizer["DropFooterText"];
    }
}
