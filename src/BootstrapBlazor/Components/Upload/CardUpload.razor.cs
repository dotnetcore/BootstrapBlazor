// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// CardUpload component
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
        .AddClass(ValidCss, Files.Count == 0)
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
    /// 获得/设置 是否允许预览回调方法 默认 null
    /// </summary>
    [Parameter]
    public Func<UploadFile, bool>? CanPreviewCallback { get; set; }

    /// <summary>
    /// 获得/设置 图标模板
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? IconTemplate { get; set; }

    /// <summary>
    /// 获得/设置 操作按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? BeforeActionButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 操作按钮模板
    /// </summary>
    [Parameter]
    public RenderFragment<UploadFile>? ActionButtonTemplate { get; set; }

    /// <summary>
    /// 获得/设置 是否显示文件尺寸，默认为 true 显示
    /// </summary>
    [Parameter]
    public bool ShowFileSize { get; set; } = true;

    /// <summary>
    /// 获得/设置 新建图标
    /// </summary>
    [Parameter]
    public string? AddIcon { get; set; }

    /// <summary>
    /// 获得/设置 状态图标
    /// </summary>
    [Parameter]
    public string? StatusIcon { get; set; }

    /// <summary>
    /// 获得/设置 移除图标
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// 获得/设置 放大图标
    /// </summary>
    [Parameter]
    public string? ZoomIcon { get; set; }

    /// <summary>
    /// 获得/设置 是否显示放大按钮 默认 true
    /// </summary>
    [Parameter]
    public bool ShowZoomButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否显示删除按钮 默认 true 显示
    /// </summary>
    [Parameter]
    [Obsolete("已弃用，请使用 ShowDeleteButton 参数。Deprecated, please use the ShowDeleteButton parameter")]
    [ExcludeFromCodeCoverage]
    public bool ShowDeletedButton { get; set; } = true;

    /// <summary>
    /// 获得/设置 继续上传按钮是否在列表前 默认 false
    /// </summary>
    [Parameter]
    public bool IsUploadButtonAtFirst { get; set; }

    /// <summary>
    /// 获得/设置 点击 Zoom 图标回调方法
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnZoomAsync { get; set; }

    /// <summary>
    /// 获得/设置 图标文件扩展名集合 ".png"
    /// </summary>
    [Parameter]
    public List<string>? AllowExtensions { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AddIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadAddIcon);
        StatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadStatusIcon);
        ZoomIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadZoomIcon);
        RemoveIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadRemoveIcon);
    }

    /// <summary>
    /// <inheritdoc/>
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
