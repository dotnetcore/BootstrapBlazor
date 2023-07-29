// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Title 网站标题示例代码
/// </summary>
public partial class Downloads
{
    private string TempUrl { get; set; } = "./favicon.png";

    private static bool IsWasm => OperatingSystem.IsBrowser();

    private async Task DownloadPhysicalFileAsync()
    {
        try
        {
            var filePath = Path.Combine(SiteOptions.CurrentValue.WebRootPath, "favicon.png");
            await using var stream = File.OpenRead(filePath);
            await DownloadService.DownloadFromStreamAsync("favicon.png", stream);
        }
        catch (FileNotFoundException msg)
        {
            await ToastService.Error("下载", msg.Message);
        }
    }

    private async Task DownloadFileAsync()
    {
        await using var stream = await GenerateFileAsync();
        await DownloadService.DownloadFromStreamAsync("测试文件.txt", stream);

        static async Task<Stream> GenerateFileAsync()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            await writer.WriteLineAsync("自行生成并写入的文本，这里可以换成图片或其他内容");
            await writer.FlushAsync();
            ms.Position = 0;
            return ms;
        }
    }

    private async Task DownloadFolderAsync()
    {
        try
        {
            await DownloadService.DownloadFolderAsync("test.zip", SiteOptions.CurrentValue.WebRootPath);
        }
        catch (FileNotFoundException msg)
        {

            await ToastService.Error("下载", msg.Message);
        }
    }

    private async Task DownloadLargeFileAsync()
    {
        await using var stream = await GenerateFileStreamAsync();
        await DownloadService.DownloadFromStreamAsync("测试大文件.txt", stream);

        static async Task<Stream> GenerateFileStreamAsync()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            for (var i = 0; i < 1000000; i++)
            {
                await writer.WriteLineAsync($"这里是一个大文件下载示例，共循环 100 万次");
            }
            await writer.FlushAsync();
            ms.Position = 0;
            return ms;
        }
    }
}
