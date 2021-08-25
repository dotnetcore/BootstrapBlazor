// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.IO;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CardUpload<TValue>
    {
        private string? GetDiabledString(UploadFile item) => (!IsDisabled && item.Uploaded && item.Code == 0) ? null : "disabled";

        private string? GetDeleteButtonDiabledString(UploadFile item) => (!IsDisabled && item.Uploaded) ? null : "disabled";

        private string? CardItemClass => CssBuilder.Default("upload-item")
            .AddClass("disabled", IsDisabled)
            .Build();

        private static bool IsImage(UploadFile item)
        {
            return item.File?.ContentType.Contains("image", StringComparison.OrdinalIgnoreCase)
                ?? Path.GetExtension(item.OriginFileName ?? item.FileName)?.ToLowerInvariant() switch
                {
                    ".jpg" or ".jpeg" or ".png" or ".bmp" or ".gif" => true,
                    _ => false
                };
        }

        private async Task OnCardFileDelete(UploadFile item)
        {
            await OnFileDelete(item);
            StateHasChanged();
        }
    }
}
