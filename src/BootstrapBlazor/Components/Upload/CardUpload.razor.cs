// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class CardUpload<TValue>
{
    private string? BodyClassString => CssBuilder.Default("upload-body is-card")
        .AddClass("is-single", IsSingle)
        .Build();

    private string? GetDiabledString(UploadFile item) => (!IsDisabled && item.Uploaded && item.Code == 0) ? null : "disabled";

    private string? GetDeleteButtonDiabledString(UploadFile item) => (!IsDisabled && item.Uploaded) ? null : "disabled";

    private string? CardItemClass => CssBuilder.Default("upload-item")
        .AddClass("disabled", IsDisabled)
        .Build();

    private static bool IsImage(UploadFile item)
    {
        bool ret;
        if (item.File != null)
        {
            ret = item.File.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase) || CheckExtensions(item.File.Name);
        }
        else
        {
            ret = IsBase64Format() || CheckExtensions(item.FileName ?? item.PrevUrl ?? "");
        }

        bool IsBase64Format() => !string.IsNullOrEmpty(item.PrevUrl) && item.PrevUrl.StartsWith("data:image/", StringComparison.OrdinalIgnoreCase);

        bool CheckExtensions(string fileName) => Path.GetExtension(fileName).ToLowerInvariant() switch
        {
            ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" => true,
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
