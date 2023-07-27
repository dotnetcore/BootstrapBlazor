// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Live2DDisplays
/// </summary>
public partial class Live2DDisplays
{
    private bool IsDraggable { get; set; } = true;

    private string BackgroundColor { get; set; } = "#000000";

    private bool BackgroundAlpha { get; set; }

    private bool AddHitAreaFrames { get; set; }

    private double _scale = 0.2;

    private double Scale { get => _scale * 100; set => _scale = value / 100; }

    private int _xOffset;

    private double XOffset { get => _xOffset; set => _xOffset = Convert.ToInt32(value); }

    private int _yOffset { get; set; }

    private double YOffset { get => _yOffset; set => _yOffset = Convert.ToInt32(value); }

    private LivePosition position { get; set; } = LivePosition.BottomLeft;

    private SelectedItem BindSrcItem { get; set; } = new SelectedItem();

    private IEnumerable<SelectedItem> SrcItems { get; } = new SelectedItem[]
    {
        new SelectedItem("./models/shizuku/shizuku.model.json", "shizuku"),
        new SelectedItem("./models/haru/haru_greeter_t03.model3.json", "haru"),
        new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/bilibili-22/index.json", "bilibili-22"),
        new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/bilibili-33/index.json", "bilibili-33"),
        new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/chiaki_kitty/chiaki_kitty.model.json", "chiaki_kitty"),
        new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/date_16/date_16.model.json", "date_16"),
        new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/hallo_16/hallo_16.model.json", "hallo_16"),
        new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/haruto/haruto.model.json", "haruto"),
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Source",
            Description = Localizer["Live2DDisplaysSource"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "empty"
        },
        new()
        {
            Name = "Scale",
            Description = Localizer["Live2DDisplaysScale"],
            Type = "double",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "XOffset",
            Description = Localizer["Live2DDisplaysXOffset"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "YOffset",
            Description = Localizer["Live2DDisplaysYOffset"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsDraggable(not yet implemented)",
            Description = Localizer["Live2DDisplaysIsDraggable"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "AddHitAreaFrames",
            Description = Localizer["Live2DDisplaysAddHitAreaFrames"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Position",
            Description = Localizer["Live2DDisplaysPosition"],
            Type = "enum",
            ValueList = "Default|BottomLeft|BottomRight|TopLeft|TopRight",
            DefaultValue = "Default"
        },
        new()
        {
            Name = "BackgroundColor",
            Description = Localizer["Live2DDisplaysBackgroundColor"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#000000"
        },
        new()
        {
            Name = "BackgroundAlpha",
            Description = Localizer["Live2DDisplaysBackgroundAlpha"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "false"
        }
    };
}
