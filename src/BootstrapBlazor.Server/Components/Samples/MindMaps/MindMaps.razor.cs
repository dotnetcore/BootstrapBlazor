// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using static BootstrapBlazor.Components.MindMapNode;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// MindMaps
/// </summary>
public partial class MindMaps
{
    /// <summary>
    /// 
    /// </summary>
    [Inject, NotNull]
    protected MessageService? MessageService { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    protected Message? Message { get; set; }

    [NotNull]
    MindMap? MindMap { get; set; }

    string? Result { get; set; } = "";

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
            },

        },
        Children = new List<MindMapNode>()
        {
            new MindMapNode
            {
                Data = new NodeData
                {
                    Text = "二级节点1",
                },
                    Children = new List<MindMapNode>()
                    {
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
                    }
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
        }
    };

    private Task OnReceive(string? message)
    {
        Result = message;
        return Task.CompletedTask;
    }

    private Task OnError(string message)
    {
        Result = message;
        return Task.CompletedTask;
    }

    async Task Export()
    {
        await MindMap.Export();
        await ShowBottomMessage("下载Png");
    }

    async Task ExportJson()
    {
        await MindMap.Export("json", WithConfig: false);
        await ShowBottomMessage("下载Json");
    }

    async Task ExportPng()
    {
        await MindMap.Export(IsDownload: false, WithConfig: false);
        await ShowBottomMessage("已导出Png");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    protected async Task ShowBottomMessage(string message)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = message,
            Icon = "fa-solid fa-circle-info",
        }, Message);
    }

    async Task GetFullData()
    {
        await MindMap.GetData();
    }

    async Task GetData()
    {
        await MindMap.GetData(false);
    }

    async Task SetData()
    {
        if (Result != null) await MindMap.SetData(Result);
    }

    async Task Reset()
    {
        await MindMap.Reset();
    }

    async Task Sample()
    {
        Result = SampleData;
        await SetData();
    }

    async Task Sample2()
    {
        Result = SampleData2;
        await SetData();
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    { 
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
    };

    /// <summary>
    /// NodeData
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetNodeDataAttributes() => new AttributeItem[]
    {
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
        },
    };
}
