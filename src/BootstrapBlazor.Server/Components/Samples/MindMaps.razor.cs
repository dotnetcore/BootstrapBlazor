// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MindMaps
/// </summary>
public partial class MindMaps
{
    [Inject]
    [NotNull]
    private MessageService? MessageService { get; set; }

    private EnumMindMapLayout _layout = EnumMindMapLayout.LogicalStructure;

    private EnumMindMapTheme _theme = EnumMindMapTheme.DefaultTheme;

    private string? _result;

    [NotNull]
    private MindMap? MindMap { get; set; }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public string _data = SampleData1;

    async Task ExportImage()
    {
        await MindMap.Export();
        await ShowBottomMessage("下载Png");
    }

    async Task ExportJson()
    {
        await MindMap.Export("json", withConfig: false);
        await ShowBottomMessage("下载Json");
    }

    async Task ExportPng()
    {
        await MindMap.Export(download: false, withConfig: false);
        await ShowBottomMessage("已导出Png");
    }

    private Task ShowBottomMessage(string message) => MessageService.Show(new MessageOption()
    {
        Content = message,
        Icon = "fa-solid fa-circle-info",
    });

    async Task GetFullData()
    {
        _result = await MindMap.GetData(true);
    }

    async Task GetData()
    {
        _result = await MindMap.GetData(false);
    }

    async Task SetData()
    {
        if (!string.IsNullOrEmpty(_result))
        {
            await MindMap.SetData(_result);
        }
    }

    Task Reset() => MindMap.Reset();

    Task Fit() => MindMap.Fit();

    private float _scale = 1.0f;
    async Task Scale(float step)
    {
        _scale += step;
        await MindMap.Scale(_scale);
    }

    async Task Sample1()
    {
        _result = SampleData1;
        await SetData();
    }

    async Task Sample2()
    {
        _result = SampleData2;
        await SetData();
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Data",
            Description = Localizer["Data"],
            Type = "MindMapNode",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowUI",
            Description = Localizer["ShowUI"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = "StyleCss",
            Description = Localizer["StyleCss"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "OnReceive",
            Description = Localizer["OnReceive"],
            Type = "Func<string?, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnError",
            Description = Localizer["OnError"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Export",
            Description = Localizer["Export"],
            Type = "Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "GetData",
            Description = Localizer["GetData"],
            Type = "Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SetData",
            Description = Localizer["SetData"],
            Type = "Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Reset",
            Description = Localizer["Reset"],
            Type = "Task",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    /// <summary>
    /// NodeData
    /// </summary>
    /// <returns></returns>
    private List<AttributeItem> GetNodeDataAttributes() =>
    [
        new()
        {
            Name = "Text",
            Description = Localizer["NodeTextText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Image",
            Description = Localizer["NodeImageText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "ImageTitle",
            Description = Localizer["NodeImageTitleText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "ImageSize",
            Description = Localizer["NodeImageSizeText"],
            Type = "ImageSize",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["NodeIconText"],
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Tag",
            Description = Localizer["NodeTagText"],
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Hyperlink",
            Description = Localizer["NodeHyperlinkText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "HyperlinkTitle",
            Description = Localizer["NodeHyperlinkTitleText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Note",
            Description = Localizer["NodeNoteText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Generalization",
            Description = Localizer["NodeGeneralizationText"],
            Type = "Generalization",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Expand",
            Description = Localizer["NodeExpandText"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        }
    ];
}
