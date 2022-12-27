// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class PdfReaders
{
    [NotNull]
    PdfReader? PdfReader { get; set; }

    [NotNull]
    PdfReader? PdfReader2 { get; set; }

    private EnumZoomMode Zoom { get; set; } = EnumZoomMode.Auto;

    private EnumPageMode Pagemode { get; set; } = EnumPageMode.Thumbs;

    [DisplayName("搜索")]
    private string? Search { get; set; } = "Performance";

    private int Page { get; set; } = 3;

    private bool ForcePDFJS { get; set; } = true;

    [DisplayName("文件相对路径或者URL")]
    private string Filename { get; set; } = "/samples/sample.pdf";

    private string FilenameStream { get; set; } = "https://blazor.app1.es/_content/DemoShared/samples/sample2.pdf";

    [DisplayName("流模式")]
    private bool StreamMode { get; set; }

    private async Task Apply()
    {
        await PdfReader!.Refresh();
    } 
    private async Task Apply5()
    {
        await PdfReader2!.Refresh();
    }
    private async Task ApplyZoom()
    {
        Zoom = Zoom switch
        {
            EnumZoomMode.Auto => EnumZoomMode.PageFit,
            EnumZoomMode.PageFit => EnumZoomMode.PageWidth,
            EnumZoomMode.PageWidth => EnumZoomMode.PageHeight,
            EnumZoomMode.PageHeight => EnumZoomMode.Zoom75,
            EnumZoomMode.Zoom75 => EnumZoomMode.Zoom50,
            EnumZoomMode.Zoom50 => EnumZoomMode.Zoom25,
            _ => EnumZoomMode.Auto
        };
        await PdfReader2.Refresh();
    }

    private async Task ApplyPagemode()
    {
        Pagemode = Pagemode switch
        {
            EnumPageMode.Thumbs => EnumPageMode.Outline,
            EnumPageMode.Outline => EnumPageMode.Attachments,
            EnumPageMode.Attachments => EnumPageMode.Layers,
            EnumPageMode.Layers => EnumPageMode.None,
            _ => EnumPageMode.Thumbs
        };
        await PdfReader2.Refresh(Search, Page, Pagemode, Zoom);
    }
    private async Task ApplyPage()
    {
        Search = null;
        await PdfReader2.Refresh(Search, Page, Pagemode, Zoom);
    }
    private async Task ApplyPagePrevious()
    {
        Page--;
        Search = null;
        await PdfReader2.Refresh(Search, Page, Pagemode, Zoom);
    }
    private async Task ApplyPageNext()
    {
        Page++;
        Search = null;
        await PdfReader2.Refresh(Search, Page, Pagemode, Zoom);
    }
    private async Task ApplySearch()
    {
        await PdfReader2.Refresh(Search, Page, Pagemode, Zoom);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Filename",
            Description = "PDF文件路径(Url或相对路径)",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "StreamMode",
            Description = "使用流化模式,可跨域读取文件",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Width",
            Description = "宽 单位(px/%)",
            Type = "string",
            ValueList = "-",
            DefaultValue = "100%"
        },
        new AttributeItem() {
            Name = "Height",
            Description = "高 单位(px/%)",
            Type = "string",
            ValueList = "-",
            DefaultValue = "700px"
        },
        new AttributeItem() {
            Name = "StyleString",
            Description = "组件外观 Css Style",
            Type = "string",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Page",
            Description = "页码",
            Type = "int",
            ValueList = "-",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "Pagemode",
            Description = "页面模式",
            Type = "EnumPageMode",
            ValueList = "-",
            DefaultValue = "Thumbs"
        },
        new AttributeItem() {
            Name = "Zoom",
            Description = "缩放模式",
            Type = "EnumZoomMode",
            ValueList = "-",
            DefaultValue = "Auto"
        },
        new AttributeItem() {
            Name = "Search",
            Description = "查询字符串",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Refresh()",
            Description = "刷新组件",
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "NavigateToPage(int page)",
            Description = "跳转页码",
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Refresh(int page)",
            Description = "跳转页码",
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Refresh(string? search, int? page, EnumPageMode? pagemode, EnumZoomMode? zoom)",
            Description = "刷新组件(查询关键字,页码,页面模式,缩放模式)",
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Stream",
            Description = "用于渲染的文件流,为空则用URL参数读取文件",
            Type = "Stream?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "ViewerBase",
            Description = "浏览器页面路径",
            Type = "string",
            ValueList = "-",
            DefaultValue = "内置"
        },
        new AttributeItem() {
            Name = "Navpanes",
            Description = "显示导航窗格",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Toolbar",
            Description = "显示工具栏",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Statusbar",
            Description = "显示状态栏",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Debug",
            Description = "显示调试信息",
            Type = "bool",
            ValueList = "-",
            DefaultValue = "false"
        },
    };    

}
