// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DropUpload 组件</para>
/// <para lang="en">DropUpload component</para>
/// </summary>
public partial class DropUpload
{
    /// <summary>
    /// <para lang="zh">获得/设置 Body 模板 默认 null <para>设置 BodyTemplate 后 <see cref="IconTemplate"/> <see cref="TextTemplate"/> 不生效</para>
    ///</para>
    /// <para lang="en">Gets or sets Body template Default is null <para>Sets BodyTemplate 后 <see cref="IconTemplate"/> <see cref="TextTemplate"/> 不生效</para>
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? BodyTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标模板 默认 null</para>
    /// <para lang="en">Gets or sets icontemplate Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? IconTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标 默认 null</para>
    /// <para lang="en">Gets or sets icon Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? UploadIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文字模板 默认 null</para>
    /// <para lang="en">Gets or sets 文字template Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? TextTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传文字 默认 null</para>
    /// <para lang="en">Gets or sets 上传文字 Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? UploadText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 Footer 默认 false 不显示</para>
    /// <para lang="en">Gets or sets whetherdisplay Footer Default is false 不display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowFooter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 字符串模板 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Footer 字符串template Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? FooterTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Footer 字符串信息 默认 null 未设置</para>
    /// <para lang="en">Gets or sets Footer 字符串信息 Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? FooterText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示上传列表 默认 true</para>
    /// <para lang="en">Gets or sets whetherdisplay上传列表 Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowUploadFileList { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 设置文件格式图标回调委托</para>
    /// <para lang="en">Gets or sets Sets文件格式icon回调delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string?, string>? OnGetFileFormat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 加载中图标</para>
    /// <para lang="en">Gets or sets 加载中icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? LoadingIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传失败状态图标</para>
    /// <para lang="en">Gets or sets 上传失败状态icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? InvalidStatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传成功状态图标</para>
    /// <para lang="en">Gets or sets 上传成功状态icon</para>
    /// <para><version>10.2.2</version></para>
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
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        UploadIcon ??= IconTheme.GetIconByKey(ComponentIcons.DropUploadIcon);
        UploadText ??= Localizer["DropUploadText"];
    }
}
