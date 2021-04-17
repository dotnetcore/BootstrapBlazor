// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// Title 网站标题示例代码
    /// </summary>
    public partial class Downloads
    {
        [Inject]
        [NotNull]
        private DownloadService? downloadService { get; set; }

        private async Task DownloadFileAsync()
        {
            var content = await GenerateFileAsync();
            await downloadService.DownloadAsync("测试文件.txt", content);

            static async Task<byte[]> GenerateFileAsync()
            {
                using var ms = new MemoryStream();
                using var writer = new StreamWriter(ms);
                await writer.WriteLineAsync("自行生成并写入的文本，这里可以换成图片或其他内容");
                await writer.FlushAsync();
                ms.Position = 0;
                return ms.ToArray();
            }
        }

        private Task DownloadLargeFileAsync() => Task.Run(async () =>
        {
            using var stream = await GenerateFileStreamAsync();
            await downloadService.DownloadAsync("测试大文件.txt", stream);

            static async Task<Stream> GenerateFileStreamAsync()
            {
                var ms = new MemoryStream();
                var writer = new StreamWriter(ms);
                for (var i = 0; i < 1000; i++)
                {
                    await writer.WriteLineAsync($"这里是一个大文件下载示例，共循环100万次");
                }
                await writer.FlushAsync();
                ms.Position = 0;
                return ms;
            }
        });
    }
}
