// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">InputUpload 组件</para>
/// <para lang="en">Input Upload Component</para>
/// </summary>
public partial class InputUpload<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 浏览按钮图标</para>
    /// <para lang="en">Gets or sets the browse button icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? BrowserButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上传按钮样式，默认 btn-primary</para>
    /// <para lang="en">Gets or sets the upload button style. Default is btn-primary.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string BrowserButtonClass { get; set; } = "btn-primary";

    /// <summary>
    /// <para lang="zh">获得/设置 浏览按钮显示文字</para>
    /// <para lang="en">Gets or sets the browse button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? BrowserButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除按钮样式，默认 btn-danger</para>
    /// <para lang="en">Gets or sets the delete button style. Default is btn-danger.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string DeleteButtonClass { get; set; } = "btn-danger";

    /// <summary>
    /// <para lang="zh">获得/设置 删除按钮图标</para>
    /// <para lang="en">Gets or sets the delete button icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DeleteButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除按钮显示文字</para>
    /// <para lang="en">Gets or sets the delete button display text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DeleteButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示删除按钮，默认为 false</para>
    /// <para lang="en">Gets or sets whether to display the delete button. Default is false.</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDeleteButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 PlaceHolder 占位符文本</para>
    /// <para lang="en">Gets or sets the placeholder text</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<UploadBase<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    private string? InputValueClassString => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    private string? RemoveButtonClassString => CssBuilder.Default()
        .AddClass(DeleteButtonClass)
        .Build();

    private bool IsDeleteButtonDisabled => IsDisabled || UploadFiles.Count == 0;

    private string? BrowserButtonClassString => CssBuilder.Default("btn-browser")
        .AddClass(BrowserButtonClass)
        .Build();

    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DeleteButtonText ??= Localizer[nameof(DeleteButtonText)];
        BrowserButtonText ??= Localizer[nameof(BrowserButtonText)];

        BrowserButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputUploadBrowserButtonIcon);
        DeleteButtonIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputUploadDeleteButtonIcon);
    }

    private async Task TriggerDeleteFile()
    {
        for (var index = Files.Count; index > 0; index--)
        {
            var item = Files[index - 1];
            await OnFileDelete(item);
        }
        CurrentValue = default;
    }
}
