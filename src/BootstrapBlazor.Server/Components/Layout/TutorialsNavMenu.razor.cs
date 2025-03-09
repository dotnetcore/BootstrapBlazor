// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    private IStringLocalizer<BaseLayout>? AppLocalizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Tutorials>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    [Inject]
    [NotNull]
    private CodeSnippetService? CodeSnippetService { get; set; }

    [Inject]
    [NotNull]
    private IZipArchiveService? ZipArchiveService { get; set; }

    private readonly List<MenuItem> _menus = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _menus.AddRange(
        [
            new()
            {
                Template = CreateDownloadButtonComponent("dashboard", _dashboardFileList),
                Text = Localizer["DashboardSummary"],
                Url = "tutorials/dashboard"
            },
            new()
            {
                Text = Localizer["LoginSummary"],
                Url = "tutorials/login",
                Items =
                [
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template1", _template1),
                        Text = "Template 1",
                        Url = "tutorials/template1"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template2", _template2),
                        Text = "Template 2",
                        Url = "tutorials/template2"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template3", _template3),
                        Text = "Template 3",
                        Url = "tutorials/template3"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template4", _template4),
                        Text = "Template 4",
                        Url = "tutorials/template4"
                    }
                ]
            },
            new()
            {
                Template = CreateDownloadButtonComponent("waterfall", _waterfallFileList),
                Text = Localizer["WaterfallSummary"],
                Url = "tutorials/waterfall"
            },
            new()
            {
                Template = CreateDownloadButtonComponent("translate", _translateFileList),
                Text = Localizer["TranslateSummary"],
                Url = "tutorials/translate"
            },
            new()
            {
                Template = CreateDownloadButtonComponent("drawing", _drawingAppFileList),
                Text = Localizer["DrawingSummary"],
                Url = "tutorials/drawing"
            },
            new()
            {
                Text = Localizer["AdminSummary"],
                Url = "tutorials/admin",
            },
            new()
            {
                Text = Localizer["OnlineSheet"],
                Url = "tutorials/online-sheet",
            },
            new()
            {
                Text = Localizer["MemorialMode"],
                Url = "tutorials/memorial",
            }
        ]);
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
            await TitleService.SetTitle($"{item.Text} - {AppLocalizer["SiteTitle"]}");
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

    private static readonly string[] _layoutFileList =
    [
        "../Layout/TutorialsLayout.razor",
        "../Layout/TutorialsLayout.razor.css",
        "../Layout/TutorialsLoginLayout.razor",
        "../Layout/TutorialsLoginLayout.razor.css"
    ];

    private readonly string[] _dashboardFileList =
    [
        "Tutorials/Dashboard.razor",
        "Tutorials/Dashboard.razor.cs",
        "Tutorials/Dashboard.razor.css",
        "Tutorials/DashboardData.cs",
        "../../Services/DashboardService.cs"
    ];

    private readonly string[] _template1 =
    [
        "Tutorials/LoginAndRegister/Template1.razor",
        "Tutorials/LoginAndRegister/Template1.razor.css",
        .. _layoutFileList
    ];

    private readonly string[] _template2 =
    [
        "Tutorials/LoginAndRegister/Template2.razor",
        .. _layoutFileList
    ];

    private readonly string[] _template3 =
    [
        "Tutorials/LoginAndRegister/Template3.razor",
        "Tutorials/LoginAndRegister/Template3.razor.css",
        .. _layoutFileList
    ];

    private readonly string[] _template4 =
    [
        "Tutorials/LoginAndRegister/Template4.razor",
        "Tutorials/LoginAndRegister/Template4.razor.css",
        .. _layoutFileList
    ];

    private readonly string[] _waterfallFileList =
    [
        "Tutorials/Waterfall.razor",
        "Tutorials/Waterfall.razor.cs",
        "Tutorials/Waterfall.razor.css"
    ];

    private readonly string[] _translateFileList =
    [
        "Tutorials/Translation/Translator.razor",
        "Tutorials/Translation/Translator.razor.cs",
        "Tutorials/Translation/Translator.razor.css",
        "Tutorials/Translation/LanguageWriter.cs",
        "Tutorials/Translation/LanguageDataTable.cs",
        "Tutorials/Translation/AzureTranslatorServiceExtensions.cs"
    ];

    private readonly string[] _drawingAppFileList =
    [
        "Tutorials/DrawingApp.razor",
        "Tutorials/DrawingApp.razor.cs",
        "Tutorials/DrawingApp.razor.js",
        "Tutorials/DrawingApp.razor.css",
    ];
}
