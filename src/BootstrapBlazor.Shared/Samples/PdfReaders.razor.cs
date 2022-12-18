// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class PdfReaders
{
    PdfReader? pdfReader;
    PdfReader? pdfReader2;

    private string Search { get; set; } = "Performance";
    private int Page { get; set; } = 3;
    private bool ForcePDFJS { get; set; } = true;

    private string Filename = "/_content/BootstrapBlazor.Shared/sample.pdf";
    private string UrlBaseStream = "https://blazor.app1.es/_content/DemoShared/";
    private string FilenameStream = "sample.pdf";
    

    private async Task Apply()
    {
        await pdfReader!.Refresh();
    } 
    private async Task Apply5()
    {
        await pdfReader2!.Refresh();
    } 

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Stream",
            Description = "用于渲染的文件流,为空则用URL参数读取文件",
            Type = "Stream?",
            ValueList = "-",
            DefaultValue = "-"
        },
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
            Name = "UrlBase",
            Description = "PDF文件基础路径, (使用流化模式才需要设置)",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "-"
        },
        new AttributeItem() {
            Name = "Height",
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
            Name = "Page",
            Description = "页码",
            Type = "int",
            ValueList = "-",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "Page",
            Description = "显示工具栏",
            Type = "int",
            ValueList = "-",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "Statusbar",
            Description = "显示状态栏",
            Type = "int",
            ValueList = "-",
            DefaultValue = "1"
        },
        new AttributeItem() {
            Name = "View",
            Description = "视图模式",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "FitV"
        },
        new AttributeItem() {
            Name = "Pagemode",
            Description = "页面模式",
            Type = "string?",
            ValueList = "-",
            DefaultValue = "thumbs"
        },
        new AttributeItem() {
            Name = "Search",
            Description = "查询字符串",
            Type = "string?",
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
    };    

}
