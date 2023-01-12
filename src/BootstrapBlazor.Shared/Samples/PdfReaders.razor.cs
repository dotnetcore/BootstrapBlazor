// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// PdfReaders
/// </summary>
public partial class PdfReaders
{
    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Filename",
            Description = Localizer["AttributesPdfReaderFilename"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "StreamMode",
            Description = Localizer["AttributesPdfReaderStreamMode"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Width",
            Description = Localizer["AttributesPdfReaderWidth"],
            Type = "string",
            ValueList = "-",
            DefaultValue = "100%"
        },
        new AttributeItem() {
            Name = "Height",
            Description = Localizer["AttributesPdfReaderHeight"],
            Type = "string",
            ValueList = "-",
            DefaultValue = "700px"
        },
        new AttributeItem() {
            Name = "StyleString",
            Description = Localizer["AttributesPdfReaderStyleString"],
            Type = "string",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Page",
            Description = Localizer["AttributesPdfReaderPage"],
            Type = "int",
            ValueList = "-",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "Pagemode",
            Description = Localizer["AttributesPdfReaderPagemode"],
            Type = "EnumPageMode",
            ValueList = "-",
            DefaultValue = "Thumbs"
        },
        new AttributeItem() {
            Name = "Zoom",
            Description = Localizer["AttributesPdfReaderZoom"],
            Type = "EnumZoomMode",
            ValueList = "-",
            DefaultValue = "Auto"
        },
        new AttributeItem() {
            Name = "Search",
            Description = Localizer["AttributesPdfReaderSearch"],
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Refresh()",
            Description = Localizer["AttributesPdfReaderRefresh"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "NavigateToPage(int page)",
            Description = Localizer["AttributesPdfReaderNavigateToPage"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Refresh(int page)",
            Description = Localizer["AttributesPdfReaderRefreshPage"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Refresh(string? search, int? page, EnumPageMode? pagemode, EnumZoomMode? zoom)",
            Description = Localizer["AttributesPdfReaderRefreshComponent"],
            Type = "Task",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Stream",
            Description = Localizer["AttributesPdfReaderStream"],
            Type = "Stream?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "ViewerBase",
            Description = Localizer["AttributesPdfReaderViewerBase"],
            Type = "string",
            ValueList = "-",
            DefaultValue = Localizer["AttributesPdfReaderViewerBaseDefaultValue"],
        },
        new AttributeItem() {
            Name = "Navpanes",
            Description = Localizer["AttributesPdfReaderNavpanes"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Toolbar",
            Description = Localizer["AttributesPdfReaderToolbar"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Statusbar",
            Description = Localizer["AttributesPdfReaderStatusbar"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Debug",
            Description = Localizer["AttributesPdfReaderDebug"],
            Type = "bool",
            ValueList = "-",
            DefaultValue = "false"
        },
    };

}
