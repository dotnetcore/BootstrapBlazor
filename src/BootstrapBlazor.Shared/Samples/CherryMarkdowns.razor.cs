// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Samples;

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

    [Inject]
    [NotNull]
    private IOptionsMonitor<WebsiteOptions>? SiteOptions { get; set; }

    private async Task<string> OnFileUpload(CherryMarkdownUploadFile arg)
    {
        var url = Path.Combine("images", "uploader",
            $"{Path.GetFileNameWithoutExtension(arg.FileName)}-{DateTimeOffset.Now:yyyyMMddHHmmss}{Path.GetExtension(arg.FileName)}");
        var fileName = Path.Combine(SiteOptions.CurrentValue.WebRootPath, url);
        var ret = await arg.SaveToFile(fileName);
        return ret ? url : "";
    }

    [Inject]
    [NotNull]
    private IStringLocalizer<CherryMarkdowns>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem(){
            Name = "EditorSettings",
            Description = "编辑器设置",
            Type = "EditorSettings",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "ToolbarSettings",
            Description = "工具栏设置",
            Type = "ToolbarSettings",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Value",
            Description = "组件值",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "Html",
            Description = "组件 Html 代码",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "OnFileUpload",
            Description = "文件上传回调方法",
            Type = "Func<CherryMarkdownUploadFile, Task<string>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "IsViewer",
            Description = "组件是否为浏览器模式",
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    };
}
