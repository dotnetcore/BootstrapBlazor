// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Title 网站标题示例代码
/// </summary>
public partial class Downloads
{
    [Inject]
    [NotNull]
    private DownloadService? DownloadService { get; set; }

    private static bool IsWasm => OperatingSystem.IsBrowser();

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
}
