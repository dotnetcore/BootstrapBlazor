// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// PdfReaders
/// </summary>
public partial class PdfReaders
{
    [DisplayName("the file in the wwwroot relative path or url")]
    private string FileName { get; set; } = "/samples/sample.pdf";

    [DisplayName("the file in the wwwroot relative path or url")]
    private string Filename { get; set; } = "/samples/sample.pdf";

    [NotNull]
    PdfReader? PdfReader { get; set; }

    private string FilenameStream { get; set; } = "https://blazor.app1.es/_content/DemoShared/samples/sample2.pdf";

    private async Task Apply()
    {
        await PdfReader!.Refresh();
    }

    [NotNull]
    PdfReader? AdvancedPdfReader { get; set; }

    [DisplayName("流模式")]
    private bool StreamMode { get; set; }

    [DisplayName("禁用复制/打印/下载")]
    private bool ReadOnly { get; set; }

    [DisplayName("水印内容")]
    private string Watermark { get; set; } = "www.blazor.zone";

    private EnumZoomMode Zoom { get; set; } = EnumZoomMode.PageHeight;

    private EnumPageMode PageMode { get; set; } = EnumPageMode.None;

    [DisplayName("搜索")]
    private string? Search { get; set; } = "Performance";

    private int Page { get; set; } = 3;

    private bool ForcePDFJS { get; set; } = true;

    private async Task ApplyZoom()
    {
        Zoom = Zoom switch
        {
            EnumZoomMode.Auto => EnumZoomMode.PageActual,
            EnumZoomMode.PageActual => EnumZoomMode.PageFit,
            EnumZoomMode.PageFit => EnumZoomMode.PageWidth,
            EnumZoomMode.PageWidth => EnumZoomMode.PageHeight,
            EnumZoomMode.PageHeight => EnumZoomMode.Zoom75,
            EnumZoomMode.Zoom75 => EnumZoomMode.Zoom50,
            EnumZoomMode.Zoom50 => EnumZoomMode.Zoom25,
            EnumZoomMode.Zoom25 => EnumZoomMode.Zoom200,
            _ => EnumZoomMode.Auto
        };
        await Refresh();
    }

    private async Task ApplyPageMode()
    {
        PageMode = PageMode switch
        {
            EnumPageMode.Thumbs => EnumPageMode.Outline,
            EnumPageMode.Outline => EnumPageMode.Attachments,
            EnumPageMode.Attachments => EnumPageMode.Layers,
            EnumPageMode.Layers => EnumPageMode.None,
            _ => EnumPageMode.Thumbs
        };
        await Refresh();
    }

    async Task Refresh()
    {
        if (PdfReader != null)
            await PdfReader.Refresh(Search, Page, PageMode, Zoom, ReadOnly, Watermark);
    }

    private async Task ApplyPage()
    {
        Search = null;
        await Refresh();
    }

    private async Task ApplyPagePrevious()
    {
        Page--;
        Search = null;
        await Refresh();
    }

    private async Task ApplyPageNext()
    {
        Page++;
        Search = null;
        await Refresh();
    }

    private Task ApplySearch() => Refresh();

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Filename",
            Description = Localizer["AttributesPdfReaderFilename"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "StreamMode",
            Description = Localizer["AttributesPdfReaderStreamMode"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Width",
            Description = Localizer["AttributesPdfReaderWidth"],
            Type = "string",
            ValueList = "-",
            DefaultValue = "100%"
        },
        new()
        {
            Name = "Height",
            Description = Localizer["AttributesPdfReaderHeight"],
            Type = "string",
            ValueList = "-",
            DefaultValue = "700px"
        },
        new()
        {
            Name = "StyleString",
            Description = Localizer["AttributesPdfReaderStyleString"],
            Type = "string",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Page",
            Description = Localizer["AttributesPdfReaderPage"],
            Type = "int",
            ValueList = "-",
            DefaultValue = "1"
        },
        new()
        {
            Name = "PageMode",
            Description = Localizer["AttributesPdfReaderPageMode"],
            Type = "EnumPageMode",
            ValueList = "-",
            DefaultValue = "Thumbs"
        },
        new()
        {
            Name = "Zoom",
            Description = Localizer["AttributesPdfReaderZoom"],
            Type = "EnumZoomMode",
            ValueList = "-",
            DefaultValue = "Auto"
        },
        new()
        {
            Name = "Search",
            Description = Localizer["AttributesPdfReaderSearch"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Refresh()",
            Description = Localizer["AttributesPdfReaderRefresh"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "NavigateToPage(int page)",
            Description = Localizer["AttributesPdfReaderNavigateToPage"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Refresh(int page)",
            Description = Localizer["AttributesPdfReaderRefreshPage"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Refresh(string? search, int? page, EnumPageMode? pageMode, EnumZoomMode? zoom)",
            Description = Localizer["AttributesPdfReaderRefreshComponent"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "Stream",
            Description = Localizer["AttributesPdfReaderStream"],
            Type = "Stream?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new()
        {
            Name = "ViewerBase",
            Description = Localizer["AttributesPdfReaderViewerBase"],
            Type = "string",
            ValueList = "-",
            DefaultValue = Localizer["AttributesPdfReaderViewerBaseDefaultValue"],
        },
        new()
        {
            Name = "NavPanels",
            Description = Localizer["AttributesPdfReaderNavPanels"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Toolbar",
            Description = Localizer["AttributesPdfReaderToolbar"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new()
        {
            Name = "StatusBar",
            Description = Localizer["AttributesPdfReaderStatusBar"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new()
        {
            Name = "Debug",
            Description = Localizer["AttributesPdfReaderDebug"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "false"
        }
    };
}
