// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components.MindMaps;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MindMaps
/// </summary>
public partial class MindMaps
{
    [Inject]
    [NotNull]
    private MessageService? MessageService { get; set; }

    private MindMap _mindMap = default!;

    private readonly MindMapOption _options = new();

    private string _result = "";

    /// <summary>
    /// 初始化数据
    /// </summary>
    public MindMapNode Data { get; set; } = new MindMapNode
    {
        Data = new NodeData
        {
            Text = "根节点",
            Generalization = new Generalization
            {
                Text = "概要的内容"
            }
        },
        Children =
        [
            new MindMapNode
            {
                Data = new NodeData
                {
                    Text = "二级节点1",
                },
                Children =
                [
                    new MindMapNode
                    {
                        Data = new NodeData
                        {
                            Text = "分支主题1",
                        },
                    },
                    new MindMapNode
                    {
                        Data = new NodeData
                        {
                            Text = "分支主题2",
                        },
                    },
                    new MindMapNode
                    {
                        Data = new NodeData
                        {
                            Text = "分支主题3",
                        },
                    }
                ]
            },
            new MindMapNode
            {
                Data = new NodeData
                {
                    Text = "二级节点2",
                },
            },
            new MindMapNode
            {
                Data = new NodeData
                {
                    Text = "二级节点3",
                },
            }
        ]
    };

    private Task OnReceive(string message)
    {
        _result = message;
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        _result = message;
        return Task.CompletedTask;
    }

    async Task Export()
    {
        await _mindMap.Export();
        await ShowBottomMessage("下载Png");
    }

    async Task ExportJson()
    {
        await _mindMap.Export("json", WithConfig: false);
        await ShowBottomMessage("下载Json");
    }

    async Task ExportPng()
    {
        await _mindMap.Export(IsDownload: false, WithConfig: false);
        await ShowBottomMessage("已导出Png");
    }

    private Task ShowBottomMessage(string message) => MessageService.Show(new MessageOption()
    {
        Content = message,
        Icon = "fa-solid fa-circle-info",
    });

    Task GetFullData() => _mindMap.GetData();

    Task GetData() => _mindMap.GetData(false);

    async Task SetData()
    {
        if (_result != null) await _mindMap.SetData(_result);
    }

    Task Reset() => _mindMap.Reset();

    async Task Sample()
    {
        _result = SampleData;
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
        },
        new()
        {
            Name = nameof(MindMap.SetTheme),
            Description = Localizer[nameof(MindMap.SetTheme)],
            Type = "Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(MindMap.SetLayout),
            Description = Localizer[nameof(MindMap.SetTheme)],
            Type = "Task",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(MindMap.Options),
            Description = Localizer[nameof(MindMap.Options)],
            Type = "MindMapOption",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    /// <summary>
    /// Options
    /// </summary>
    /// <returns></returns>
    private List<AttributeItem> GetOptionsAttributes() =>
    [
        new()
        {
            Name = nameof(MindMapOption.Layout),
            Description = Localizer[nameof(MindMapOption.Layout)],
            Type = "Enum",
            ValueList = "逻辑结构图 / 思维导图 / 组织结构图 / 目录组织图 / 时间轴 / 时间轴2 / 鱼骨图 / 竖向时间轴",
            DefaultValue = "逻辑结构图"
        },
        new()
        {
            Name = nameof(MindMapOption.Theme),
            Description = Localizer[nameof(MindMapOption.Theme)],
            Type = "Enum",
            ValueList = "默认 / 经典 / 黑色 / 天蓝 / ... ",
            DefaultValue = "默认"
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
