// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CherryMarkdowns
/// </summary>
public partial class CherryMarkdowns
{
    [NotNull]
    private CherryMarkdown? MarkdownElement { get; set; }

    private async Task InsertCheckList()
    {
        await MarkdownElement.DoMethodAsync("toolbar.toolbarHandlers.insert", "checklist");
    }

    private async Task InsertPicture()
    {
        await MarkdownElement.DoMethodAsync("insert", "![一张图片](https://i.niupic.com/images/2022/04/01/9Y6T.jpg)", false, false, true);
    }

    private string? HtmlString { get; set; }

    private string? MarkdownString { get; set; } = "# test";

    private EditorSettings EditorSettings { get; set; } = new EditorSettings()
    {
        DefaultModel = "editOnly"
    };

    private ToolbarSettings ToolbarSettings { get; set; } = new ToolbarSettings()
    {
        Toolbar = new List<object>
        {
            "italic", new{insert = new List<string>(){"image" } }
        },
        Bubble = new List<string>()
        {
            "bold"
        },
        Float = new List<string>()
        {
            "h1"
        }
    };

    private async Task<string> OnFileUpload(CherryMarkdownUploadFile arg)
    {
        var url = Path.Combine("images", "uploader",
            $"{Path.GetFileNameWithoutExtension(arg.FileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(arg.FileName)}");
        var fileName = Path.Combine(WebsiteOption.Value.WebRootPath, url);
        var ret = await arg.SaveToFile(fileName);
        return ret ? url : "";
    }

    [Inject]
    [NotNull]
    private IStringLocalizer<CherryMarkdowns>? Localizer { get; set; }
}
