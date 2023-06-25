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

    [Inject]
    [NotNull]
    private IZipArchiveService? ZipArchiveService { get; set; }

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
                Text="仪表盘 Dashboard",
                Url="dashboard"
            },
            new MenuItem()
            {
                Template = CreateDownloadButtonComponent("登陆和注册praclogin", pracloginFileList),
                Text="登陆和注册 Login & Register",
                Url="praclogin"
            },
            new MenuItem()
            {
                Template = CreateDownloadButtonComponent("瀑布流图片pintereso", pinteresoFileList),
                Text="瀑布流图片 Pintereso",
                Url="pintereso"
            }
        };
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
    /// 打包并下载源码
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fileList"></param>
    /// <returns></returns>
    private async Task DownloadZipArchive(string name, string[] fileList)
    {
        using var stream = await ZipArchiveService.ArchiveAsync(fileList, new ArchiveOptions()
        {
            ReadStreamAsync = async file =>
            {
                var code = await CodeSnippetService.GetFileContentAsync(file);
                return new MemoryStream(Encoding.UTF8.GetBytes(code));
            }
        });
        await DownloadService.DownloadFromStreamAsync($"BootstrapBlazor-{name}.zip", stream);
        stream.Close();
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
        "Practicals/LoginAndRegister/PracLogin.razor.css",
        "Practicals/LoginAndRegister/PracRegister.razor"
    };

    private readonly string[] pinteresoFileList = new[]
    {
        "Practicals/Pintereso/Pintereso.razor",
        "Practicals/Pintereso/Pintereso.razor.cs",
        "Practicals/Pintereso/Pintereso.razor.css"
    };
}
