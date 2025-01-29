// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
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

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<string>>? Localizer { get; set; }

    private string? DropUploadClassString => CssBuilder.Default(ClassString)
        .AddClass("is-drop")
        .AddClassFromAttributes(AdditionalAttributes)
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected override async Task OnFileChange(InputFileChangeEventArgs args)
    {
        var file = new UploadFile()
        {
            OriginFileName = args.File.Name,
            Size = args.File.Size,
            File = args.File,
            Uploaded = false,
            UpdateCallback = Update
        };
        UploadFiles.Add(file);
        if (OnChange != null)
        {
            await OnChange(file);
        }
        file.Uploaded = true;
    }
}
