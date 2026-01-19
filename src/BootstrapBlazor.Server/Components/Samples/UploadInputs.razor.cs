// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// InputUpload sample code
/// </summary>
public partial class UploadInputs
{
    private Person Foo1 { get; set; } = new Person();

    private async Task OnFileChange(UploadFile file)
    {
        // 未真正保存文件
        // file.SaveToFile()
        await Task.Delay(200);
        await ToastService.Information(Localizer["UploadsSaveFile"], $"{file.File!.Name} {Localizer["UploadsSuccess"]}");
    }

    private static Task OnSubmit(EditContext context)
    {
        // 示例代码请根据业务情况自行更改
        // var fileName = Foo.Picture?.Name;
        return Task.CompletedTask;
    }

    class Person
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; } = "Blazor";

        [Required]
        [FileValidation(Extensions = [".png", ".jpg", ".jpeg"], FileSize = 5 * 1024 * 1024)]
        public IBrowserFile? Picture { get; set; }
    }
}
