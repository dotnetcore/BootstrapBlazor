// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 卡片式上传组件
/// </summary>
public partial class CardUpload<TValue>
{
    private string? BodyClassString => CssBuilder.Default("upload-body is-card")
        .AddClass("is-single", IsSingle)
        .Build();

    private string? GetDisabledString(UploadFile item) => (!IsDisabled && item.Uploaded && item.Code == 0) ? null : "disabled";

    private bool ShowPreviewList => GetUploadFiles().Count != 0;

    private List<string?> PreviewList => GetUploadFiles().Select(i => i.PrevUrl).ToList();

    private string? GetDeleteButtonDisabledString(UploadFile item) => (!IsDisabled && item.Uploaded) ? null : "disabled";

    private string? CardItemClass => CssBuilder.Default("upload-item")
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
    /// 获得/设置 删除图标
    /// </summary>
    [Parameter]
    public string? DeleteIcon { get; set; }

    /// <summary>
    /// 获得/设置 移除图标
    /// </summary>
    [Parameter]
    public string? RemoveIcon { get; set; }

    /// <summary>
    /// 获得/设置 下载图标
    /// </summary>
    [Parameter]
    public string? DownloadIcon { get; set; }

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
    public bool ShowDeletedButton { get; set; } = true;

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        AddIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadAddIcon);
        StatusIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadStatusIcon);
        DeleteIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadDeleteIcon);
        RemoveIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadRemoveIcon);
        DownloadIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadDownloadIcon);
        ZoomIcon ??= IconTheme.GetIconByKey(ComponentIcons.CardUploadZoomIcon);
    }

    private bool IsImage(UploadFile item)
    {
        bool ret;
        if (item.File != null)
        {
            ret = item.File.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase) || CheckExtensions(item.File.Name);
        }
        else if (CanPreviewCallback != null)
        {
            ret = CanPreviewCallback(item);
        }
        else
        {
            ret = IsBase64Format() || CheckExtensions(item.FileName ?? item.PrevUrl ?? "");
        }

        bool IsBase64Format() => !string.IsNullOrEmpty(item.PrevUrl) && item.PrevUrl.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase);

        bool CheckExtensions(string fileName) => Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" or ".webp" => true,
            _ => false
        };
        return ret;
    }

    /// <summary>
    /// 获得/设置 点击 Zoom 图标回调方法
    /// </summary>
    [Parameter]
    public Func<UploadFile, Task>? OnZoomAsync { get; set; }

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
}
