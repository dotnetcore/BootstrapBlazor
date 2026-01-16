// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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

    private SelectedItem BindSrcItem { get; set; } = new();

    private readonly List<SelectedItem> _srcItems = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _srcItems.AddRange(
        [
            new SelectedItem($"{WebsiteOption.Value.AssetRootPath}models/shizuku/shizuku.model.json", "shizuku"),
            new SelectedItem($"{WebsiteOption.Value.AssetRootPath}models/haru/haru_greeter_t03.model3.json", "haru"),
            new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/bilibili-22/index.json", "bilibili-22"),
            new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/bilibili-33/index.json", "bilibili-33"),
            new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/chiaki_kitty/chiaki_kitty.model.json", "chiaki_kitty"),
            new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/date_16/date_16.model.json", "date_16"),
            new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/hallo_16/hallo_16.model.json", "hallo_16"),
            new SelectedItem("https://raw.githubusercontent.com/iCharlesZ/vscode-live2d-models/master/model-library/haruto/haruto.model.json", "haruto"),
        ]);
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
}
