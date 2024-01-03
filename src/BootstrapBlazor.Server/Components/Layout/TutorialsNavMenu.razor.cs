// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Server.Components.Pages;
using Microsoft.AspNetCore.Components.Web;
using System.Text;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// 实战栏目侧边菜单
/// </summary>
public partial class TutorialsNavMenu
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

        Menus =
        [
            new()
            {
                Template = CreateDownloadButtonComponent("dashboard", dashboardFileList),
                Text = "仪表盘 Dashboard",
                Url = "tutorials/dashboard"
            },
            new()
            {
                Text = "登陆和注册 Login & Register",
                Url = "tutorials/login",
                Items =
                [
                    //new()
                    //{
                    //    Template = CreateDownloadButtonComponent("template1", Template1),
                    //    Text = "模板 Template 1",
                    //    Url = "template1"
                    //},
                    //new()
                    //{
                    //    Template = CreateDownloadButtonComponent("template2", Template2),
                    //    Text = "模板 Template 2",
                    //    Url = "template2",
                    //},
                    //new()
                    //{
                    //    Template = CreateDownloadButtonComponent("template3", Template3),
                    //    Text = "模板 Template 3",
                    //    Url = "template3"
                    //},
                    //new()
                    //{
                    //    Template = CreateDownloadButtonComponent("template4", Template4),
                    //    Text = "模板 Template 4",
                    //    Url = "template4"
                    //},
                    //new()
                    //{
                    //    Template = CreateDownloadButtonComponent("template5", Template5),
                    //    Text = "模板 Template 5",
                    //    Url = "template5"
                    //},
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template6", Template6),
                        Text = "模板 Template 6",
                        Url = "tutorials/template6"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template7", Template7),
                        Text = "模板 Template 7",
                        Url = "tutorials/template7"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template8", Template8),
                        Text = "模板 Template 8",
                        Url = "tutorials/template8"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template9", Template9),
                        Text = "模板 Template 9",
                        Url = "tutorials/template9"
                    }
                ]
            },
            new()
            {
                Template = CreateDownloadButtonComponent("waterfall", waterfallFileList),
                Text = "瀑布流图片 Waterfall",
                Url = "tutorials/waterfall"
            },
            new()
            {
                Text = "翻译工具 Translate",
                Url = "tutorials/translate"
            }
        ];
    }

    /// <summary>
    /// OnClickMenu 回调
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
        await using var stream = await ZipArchiveService.ArchiveAsync(fileList, new ArchiveOptions()
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

    private readonly string[] dashboardFileList =
    [
        "Tutorials/Dashboard.razor",
        "Tutorials/Dashboard.razor.cs",
        "Tutorials/Dashboard.razor.css",
        "Tutorials/DashboardData.cs",
        "/../../Services/DashboardService.cs"
    ];

    private readonly string[] Template1 =
    [
        "Tutorials/LoginAndRegister/Template1.razor",
        "Tutorials/LoginAndRegister/Template1.razor.css",
    ];

    private readonly string[] Template2 =
    [
        "Tutorials/LoginAndRegister/Template2.razor",
        "Tutorials/LoginAndRegister/Template2.razor.css",
    ];

    private readonly string[] Template3 =
    [
        "Tutorials/LoginAndRegister/Template3.razor",
        "Tutorials/LoginAndRegister/Template3.razor.css",
    ];

    private readonly string[] Template4 =
    [
        "Tutorials/LoginAndRegister/Template4.razor",
        "Tutorials/LoginAndRegister/Template4.razor.css",
        "Tutorials/LoginAndRegister/Template4.razor.js",
    ];

    private readonly string[] Template5 =
    [
        "Tutorials/LoginAndRegister/Template5.razor",
        "Tutorials/LoginAndRegister/Template5.razor.css",
        "Tutorials/LoginAndRegister/Template5.razor.js",
    ];

    private readonly string[] Template6 =
    [
        "Tutorials/LoginAndRegister/Template6.razor",
        "Tutorials/LoginAndRegister/Template6.razor.css",
        "Shared/TutorialsLayout.razor",
        "Shared/TutorialsLayout.razor.cs",
        "Shared/TutorialsLayout.razor.css",
        "Shared/TutorialsLoginLayout.razor",
        "Shared/TutorialsLoginLayout.razor.css"
    ];

    private readonly string[] Template7 =
    [
        "Tutorials/LoginAndRegister/Template7.razor",
        "Tutorials/LoginAndRegister/Template7.razor.css",
        "Shared/TutorialsLayout.razor",
        "Shared/TutorialsLayout.razor.cs",
        "Shared/TutorialsLayout.razor.css",
        "Shared/TutorialsLoginLayout.razor",
        "Shared/TutorialsLoginLayout.razor.css"
    ];

    private readonly string[] Template8 =
    [
        "Tutorials/LoginAndRegister/Template8.razor",
        "Tutorials/LoginAndRegister/Template8.razor.css",
        "Shared/TutorialsLayout.razor",
        "Shared/TutorialsLayout.razor.cs",
        "Shared/TutorialsLayout.razor.css",
        "Shared/TutorialsLoginLayout.razor",
        "Shared/TutorialsLoginLayout.razor.css"
    ];

    private readonly string[] Template9 =
    [
        "Tutorials/LoginAndRegister/Template9.razor",
        "Tutorials/LoginAndRegister/Template9.razor.css",
        "Shared/TutorialsLayout.razor",
        "Shared/TutorialsLayout.razor.cs",
        "Shared/TutorialsLayout.razor.css",
        "Shared/TutorialsLoginLayout.razor",
        "Shared/TutorialsLoginLayout.razor.css"
    ];

    private readonly string[] waterfallFileList =
    [
        "Tutorials/Waterfall.razor",
        "Tutorials/Waterfall.razor.cs",
        "Tutorials/Waterfall.razor.css"
    ];
}
