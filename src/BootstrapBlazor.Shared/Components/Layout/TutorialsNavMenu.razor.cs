// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using System.Text;

namespace BootstrapBlazor.Shared.Components.Layout;

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
    private IStringLocalizer<TutorialsNavMenu>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    [Inject]
    [NotNull]
    private ISampleDocumentationService? CodeSnippetService { get; set; }

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
                Template = CreateDownloadButtonComponent("dashboard", TutorialSourceFiles.Dashboard),
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
                        Template = CreateDownloadButtonComponent("template1", TutorialSourceFiles.Template1),
                        Text = "Template 1",
                        Url = "tutorials/template1"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template2", TutorialSourceFiles.Template2),
                        Text = "Template 2",
                        Url = "tutorials/template2"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template3", TutorialSourceFiles.Template3),
                        Text = "Template 3",
                        Url = "tutorials/template3"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template4", TutorialSourceFiles.Template4),
                        Text = "Template 4",
                        Url = "tutorials/template4"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("template5", TutorialSourceFiles.Template5),
                        Text = "Template 5",
                        Url = "/tutorials/template5"
                    }
                ]
            },
            new()
            {
                Template = CreateDownloadButtonComponent("waterfall", TutorialSourceFiles.Waterfall),
                Text = Localizer["WaterfallSummary"],
                Url = "tutorials/waterfall"
            },
            new()
            {
                Template = CreateDownloadButtonComponent("translate", TutorialSourceFiles.Translation),
                Text = Localizer["TranslateSummary"],
                Url = "tutorials/translate"
            },
            new()
            {
                Template = CreateDownloadButtonComponent("drawing", TutorialSourceFiles.Drawing),
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
            },
            new()
            {
                Text = Localizer["MFA"],
                Url = "tutorials/mfa",
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
    private RenderFragment CreateDownloadButtonComponent(string name, IReadOnlyList<string> fileList) => BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>
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
    private async Task DownloadZipArchive(string name, IReadOnlyList<string> fileList)
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

}
