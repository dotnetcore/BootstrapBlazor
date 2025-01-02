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

    async Task ExportImage()
    {
        await MindMap.Export();
        await ShowBottomMessage("下载 Png");
    }

    async Task ExportJson()
    {
        await MindMap.Export("json", withConfig: false);
        await ShowBottomMessage("下载 Json");
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
        _result = await MindMap.GetData();
    }

    async Task SetData()
    {
        if (!string.IsNullOrEmpty(_result))
        {
            await MindMap.SetData(_result);
        }
    }

    Task ClickCustom() => MindMap.Execute("clickCustom", "args1");

    Task Reset() => MindMap.Reset();

    Task Fit() => MindMap.Fit();

    private float _scale = 1.0f;
    async Task Scale(float step)
    {
        _scale += step;
        await MindMap.Scale(_scale);
    }

    async Task SetSample1()
    {
        _result = SampleData1;
        await SetData();
    }

    async Task SetSample2()
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
}
