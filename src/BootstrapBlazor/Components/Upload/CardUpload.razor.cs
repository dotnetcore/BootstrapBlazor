// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">CardUpload component</para>
/// <para lang="en">CardUpload component</para>
/// </summary>
public partial class CardUpload<TValue>
{
    private string? ClassString => CssBuilder.Default("upload")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetItemClassString(UploadFile item) => CssBuilder.Default(ItemClassString)
        .AddClass("is-valid", item is { Uploaded: true, Code: 0 })
        .AddClass("is-invalid", item.Code != 0)
        .Build();

    private string? ItemClassString => CssBuilder.Default("upload-item")
        .AddClass("disabled", CanUpload() == false)
        .Build();

    private string? BodyClassString => CssBuilder.Default("upload-body is-card")
        .AddClass("is-single", IsMultiple == false)
        .Build();

    private string? GetDisabledString(UploadFile item) => (!IsDisabled && item is { Uploaded: true, Code: 0 }) ? null : "disabled";

    private bool ShowPreviewList => Files.Count != 0;

    private List<string?> PreviewList => [.. Files.Select(i => i.PrevUrl)];

    private string? GetDeleteButtonDisabledString(UploadFile item) => (!IsDisabled && item.Uploaded) ? null : "disabled";

    private string? CardItemClass => CssBuilder.Default("upload-item upload-item-plus btn-browser upload-drop-body")
        .AddClass("disabled", IsDisabled)
        .Build();

    private string? StatusIconString => CssBuilder.Default("valid-icon valid")
        .AddClass(StatusIcon)
        .Build();

    private string? DeleteIconString => CssBuilder.Default("valid-icon invalid")
        .AddClass(DeleteIcon)
        .Build();

    private string PreviewerId => $"prev_{Id}";

    /// <summary>
    /// <para lang="zh">获得/设置 是否允许预览回调方法 默认 null</para>
    /// <para lang="en">Gets or sets whether允许预览callback method Default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<UploadFile, bool>? CanPreviewCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标模板</para>
    /// <para lang="en">Gets or sets icontemplate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? IconTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 操作按钮模板</para>
    /// <para lang="en">Gets or sets 操作buttontemplate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? BeforeActionButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 操作按钮模板</para>
    /// <para lang="en">Gets or sets 操作buttontemplate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? ActionButtonTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示文件尺寸，默认为 true 显示</para>
    /// <para lang="en">Gets or sets whetherdisplay文件尺寸，Default is为 true display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowFileSize { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 新建图标</para>
    /// <para lang="en">Gets or sets 新建icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? AddIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 状态图标</para>
    /// <para lang="en">Gets or sets 状态icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? StatusIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 移除图标</para>
    /// <para lang="en">Gets or sets 移除icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 放大图标</para>
    /// <para lang="en">Gets or sets 放大icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ZoomIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示放大按钮 默认 true</para>
    /// <para lang="en">Gets or sets whetherdisplay放大button Default is true</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowZoomButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示删除按钮 默认 true 显示</para>
    /// <para lang="en">Gets or sets whetherdisplay删除button Default is true display</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowDeleteButton 参数。Deprecated, please use the ShowDeleteButton parameter")]
    [ExcludeFromCodeCoverage]
    public bool ShowDeletedButton { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 继续上传按钮是否在列表前 默认 false</para>
    /// <para lang="en">Gets or sets 继续上传buttonwhether在列表前 Default is false</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsUploadButtonAtFirst { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 点击 Zoom 图标回调方法</para>
    /// <para lang="en">Gets or sets 点击 Zoom iconcallback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnZoomAsync { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标文件扩展名集合 ".png"</para>
    /// <para lang="en">Gets or sets icon文件扩展名collection ".png"</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public List<string>? AllowExtensions { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除前是否显示确认对话框，依赖 <see cref="FileListUploadBase{TValue}.ShowDeleteButton"/> 属性为 true 时有效</para>
    /// <para lang="en">Gets or sets 删除前whetherdisplay确认对话框，依赖 <see cref="FileListUploadBase{TValue}.ShowDeleteButton"/> property为 true 时有效</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool ShowDeleteConfirmButton { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除确认弹窗中确认按钮颜色 默认 <see cref="Color.Danger"/></para>
    /// <para lang="en">Gets or sets 删除确认弹窗中确认buttoncolor Default is <see cref="Color.Danger"/></para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Color DeleteConfirmButtonColor { get; set; } = Color.Danger;

    /// <summary>
    /// <para lang="zh">获得/设置 删除确认弹窗中确认按钮图标 默认 null 未设置</para>
    /// <para lang="en">Gets or sets 删除确认弹窗中确认buttonicon Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DeleteConfirmButtonIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除确认弹窗中确认文本内容 默认 null 未设置 使用资源文件中内置文字</para>
    /// <para lang="en">Gets or sets 删除确认弹窗中确认文本content Default is null 未Sets 使用资源文件中内置文字</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DeleteConfirmContent { get; set; }
    /// <summary>
    /// <para lang="zh">获得/设置 删除确认弹窗中确认按钮显示文字 默认 null 未设置</para>
    /// <para lang="en">Gets or sets 删除确认弹窗中确认buttondisplay文字 Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DeleteConfirmButtonText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 删除确认弹窗中取消按钮显示文字 默认 null 未设置</para>
    /// <para lang="en">Gets or sets 删除确认弹窗中取消buttondisplay文字 Default is null 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DeleteCloseButtonText { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<CardUpload<TValue>>? Localizer { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AddIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadAddIcon);
        StatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadStatusIcon);
        ZoomIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadZoomIcon);
        RemoveIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadRemoveIcon);

        DeleteConfirmContent ??= Localizer["DeleteConfirmContent"];
    }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    protected override async Task TriggerOnChanged(UploadFile file)
    {
        // 从客户端获得预览地址不使用 base64 编码
        if (file.IsImage(AllowExtensions, CanPreviewCallback))
        {
            file.PrevUrl = await InvokeAsync<string?>("getPreviewUrl", Id, file.OriginFileName);
        }
        await base.TriggerOnChanged(file);
    }

    private IReadOnlyCollection<ValidationResult> _results = [];

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="results"></param>
    public override async Task ToggleMessage(IReadOnlyCollection<ValidationResult> results)
    {
        _results = results;
        IsValid = results.Count == 0;

        ValidateModule ??= await LoadValidateModule();

        var items = IsInValidOnAddItem
            ? [AddId]
            : Files.Select(i => i.ValidateId).ToList();
        var invalidItems = _results.GetInvalidItems(IsInValidOnAddItem, AddId);
        var addId = IsInValidOnAddItem ? null : AddId;
        await ValidateModule.InvokeVoidAsync("executeUpload", items, invalidItems, addId);
    }

    private bool IsInValidOnAddItem => Files.Count == 0 && _results.Count > 0;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override ValueTask ShowValidResult() => ValueTask.CompletedTask;

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <param name="validateId"></param>
    /// <returns></returns>
    protected override async ValueTask RemoveValidResult(string? validateId = null)
    {
        if (!string.IsNullOrEmpty(validateId))
        {
            await base.RemoveValidResult(validateId);
        }
    }

    private string? AddId => $"{Id}_new";

    private async Task OnCardFileDelete(UploadFile item)
    {
        await OnFileDelete(item);
        StateHasChanged();
    }

    private async Task OnClickZoom(UploadFile item)
    {
        if (OnZoomAsync != null)
        {
            await OnZoomAsync(item);
        }
    }

    private async Task OnClickDownload(UploadFile item)
    {
        if (OnDownload != null)
        {
            await OnDownload(item);
        }
    }

    private async Task OnClickCancel(UploadFile item)
    {
        if (OnCancel != null)
        {
            await OnCancel(item);
        }
    }
}
