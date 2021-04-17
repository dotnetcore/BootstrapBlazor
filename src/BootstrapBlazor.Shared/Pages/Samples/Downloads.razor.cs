// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
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

        [Inject]
        [NotNull]
        private IOptions<WebsiteOptions>? SiteOptions { get; set; }

        private async Task DownloadFile()
        {
            await using var ms = new MemoryStream();
            TextWriter textWriter = new StreamWriter(ms);
            await textWriter.WriteAsync("自行生成并写入的文本，这里可以换成图片或其他内容");
            await textWriter.FlushAsync();
            ms.Position = 0;
            var option = new DownloadOption();
            option.FileName = "测试文件.txt";
            option.File = ms.ToArray();
            await downloadService.Download(option);
        }

        private async Task DownloadLargeFile()
        {
            await using var ms = new MemoryStream();
            TextWriter textWriter = new StreamWriter(ms);
            for (int i = 0; i < 1000000; i++)
            {
                await textWriter.WriteAsync("这里是一个大文件下载示例，共循环100万次\r\n");
            }
            await textWriter.FlushAsync();
            ms.Position = 0;
            var option = new DownloadOption();
            option.FileName = "测试大文件.txt";
            option.File = ms.ToArray();
            await downloadService.Download(option);
        }
    }
}
