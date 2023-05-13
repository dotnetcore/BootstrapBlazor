// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components.Web;
using System.IO.Compression;
using System.Text;

namespace BootstrapBlazor.Shared.Shared;

/// <summary>
/// 实战栏目侧边菜单
/// </summary>
public partial class PracNavMenu
{
    [Inject]
    [NotNull]
    private IStringLocalizer<App>? AppLocalizer { get; set; }

    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    [Inject]
    [NotNull]
    private CodeSnippetService? CodeSnippetService { get; set; }

    [NotNull]
    private List<MenuItem>? Menus { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Menus = new List<MenuItem>
        {
            new MenuItem()
            {
                Template = CreateDownloadButtonComponent("仪表盘dashboard", dashboardFileList),
                Text="仪表盘 Dashboard",Url="dashboard"
            },
            new MenuItem()
            {
                Template = CreateDownloadButtonComponent("登陆和注册praclogin", pracloginFileList),
                Text="登陆和注册 Login & Register",Url="praclogin"
            },
            new MenuItem()
            {
                Template = CreateDownloadButtonComponent("瀑布流图片pintereso", pinteresoFileList),
                Text="瀑布流图片 Pintereso",Url="pintereso"
            }
        };
    }

    /// <summary>
    /// 打包并下载源码
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileList"></param>
    /// <returns></returns>
    private async Task DownloadZipArchive(string name, string[] fileList)
    {
        var data = await PackageSourceFile(fileList);
        await DownloadService.DownloadFromByteArrayAsync($"BootstrapBlazor-{name}.zip", data);
    }

    /// <summary>
    /// OnClickMenu回调
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private async Task OnClickMenu(MenuItem item)
    {
        if (!item.Items.Any() && !string.IsNullOrEmpty(item.Text))
        {
            await TitleService.SetTitle($"{item.Text} - {AppLocalizer["Title"]}");
        }
    }

    /// <summary>
    /// 动态创建下载按钮
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileList"></param>
    /// <returns></returns>
    private RenderFragment CreateDownloadButtonComponent(string name, string[] fileList) => BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
    {
        [nameof(Button.Color)] = Color.Danger,
        [nameof(Button.Icon)] = "fas fa-download",
        [nameof(Button.Size)] = Size.ExtraSmall,
        [nameof(Button.OnClick)] = EventCallback.Factory.Create<MouseEventArgs>(this, () => DownloadZipArchive(name, fileList))
    }).Render();

    /// <summary>
    /// 打包源码文件
    /// </summary>
    /// <param name="filelist"></param>
    /// <returns></returns>
    private async Task<byte[]> PackageSourceFile(string[] filelist)
    {
        using var memory = new MemoryStream();
        using var archive = new ZipArchive(memory, ZipArchiveMode.Create, false);
        foreach (var item in filelist)
        {
            var fileInArchive = archive.CreateEntry(Path.GetFileName(item), CompressionLevel.Optimal);
            using var entryStream = fileInArchive.Open();

            var code = await CodeSnippetService.GetFileContentAsync(item);
            using var fileToCompressStream = new MemoryStream(Encoding.UTF8.GetBytes(code));
            fileToCompressStream.CopyTo(entryStream);
        }
        archive.Dispose();
        return memory.ToArray();
    }

    private readonly string[] dashboardFileList = new[]
    {
        "Practicals/Dashboard/Dashboard.razor",
        "Practicals/Dashboard/Dashboard.razor.cs",
        "Practicals/Dashboard/Dashboard.razor.css",
        "Services/DashboardService.cs"
    };

    private readonly string[] pracloginFileList = new[]
    {
        "Practicals/LoginAndRegister/PracLogin.razor",
        "Practicals/LoginAndRegister/PracRegister.razor"
    };

    private readonly string[] pinteresoFileList = new[]
    {
        "Practicals/Pintereso/Pintereso.razor",
        "Practicals/Pintereso/Pintereso.razor.cs",
        "Practicals/Pintereso/Pintereso.razor.css"
    };
}
