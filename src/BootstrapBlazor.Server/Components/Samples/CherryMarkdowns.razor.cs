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

    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "EditorSettings",
            Description = "编辑器设置",
            Type = "EditorSettings",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ToolbarSettings",
            Description = "工具栏设置",
            Type = "ToolbarSettings",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Value",
            Description = "组件值",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Html",
            Description = "组件 Html 代码",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnFileUpload",
            Description = "文件上传回调方法",
            Type = "Func<CherryMarkdownUploadFile, Task<string>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsViewer",
            Description = "组件是否为浏览器模式",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    ];
}
