// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Services;

using Microsoft.AspNetCore.Components.Web;

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
                Text="登陆和注册 Login & Register",
                Url="praclogin",
                Items = new List<MenuItem>()
                {
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆和注册模板1", praclogintemplate1),
                        Text = "模板 template 1",
                        Url = "praclogintemplate1"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆和注册模板2", praclogintemplate2),
                        Text = "模板 template 2",
                        Url = "praclogintemplate2",
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆和注册模板3", praclogintemplate3),
                        Text = "模板 template 3",
                        Url = "praclogintemplate3"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆和注册模板4", praclogintemplate4),
                        Text = "模板 template 4",
                        Url = "praclogintemplate4"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆和注册模板5", praclogintemplate5),
                        Text = "模板 template 5",
                        Url = "praclogintemplate5"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆模板6", praclogintemplate6),
                        Text = "模板 template 6",
                        Url = "praclogintemplate6"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆模板7", praclogintemplate7),
                        Text = "模板 template 7",
                        Url = "praclogintemplate7"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆模板8", praclogintemplate8),
                        Text = "模板 template 8",
                        Url = "praclogintemplate8"
                    },
                    new()
                    {
                        Template = CreateDownloadButtonComponent("登陆模板9", praclogintemplate9),
                        Text = "模板 template 9",
                        Url = "praclogintemplate9"
                    }
                }
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

    private readonly string[] praclogintemplate1 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate1.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate1.razor.css",
    };

    private readonly string[] praclogintemplate2 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate2.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate2.razor.css",
    };

    private readonly string[] praclogintemplate3 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate3.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate3.razor.css",
    };

    private readonly string[] praclogintemplate4 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate4.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate4.razor.css",
        "Practicals/LoginAndRegister/PracLoginTemplate4.razor.js",
    };

    private readonly string[] praclogintemplate5 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate5.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate5.razor.css",
        "Practicals/LoginAndRegister/PracLoginTemplate5.razor.js",
    };

    private readonly string[] praclogintemplate6 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate6.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate6.razor.css",
        "Shared/PracLayout.razor",
        "Shared/PracLayout.razor.cs",
        "Shared/PracLayout.razor.css",
        "Shared/PracLoginLayout.razor",
        "Shared/PracLoginLayout.razor.css"
    };

    private readonly string[] praclogintemplate7 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate7.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate7.razor.css",
        "Shared/PracLayout.razor",
        "Shared/PracLayout.razor.cs",
        "Shared/PracLayout.razor.css",
        "Shared/PracLoginLayout.razor",
        "Shared/PracLoginLayout.razor.css"
    };

    private readonly string[] praclogintemplate8 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate8.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate8.razor.css",
        "Shared/PracLayout.razor",
        "Shared/PracLayout.razor.cs",
        "Shared/PracLayout.razor.css",
        "Shared/PracLoginLayout.razor",
        "Shared/PracLoginLayout.razor.css"
    };

    private readonly string[] praclogintemplate9 = new[]
    {
        "Practicals/LoginAndRegister/PracLoginTemplate9.razor",
        "Practicals/LoginAndRegister/PracLoginTemplate9.razor.css",
        "Shared/PracLayout.razor",
        "Shared/PracLayout.razor.cs",
        "Shared/PracLayout.razor.css",
        "Shared/PracLoginLayout.razor",
        "Shared/PracLoginLayout.razor.css"
    };

    private readonly string[] pinteresoFileList = new[]
    {
        "Practicals/Pintereso/Pintereso.razor",
        "Practicals/Pintereso/Pintereso.razor.cs",
        "Practicals/Pintereso/Pintereso.razor.css"
    };
}
