// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Vditors 组件示例代码
/// </summary>
public partial class Vditors
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Vditors>? Localizer { get; set; }

    private VditorOptions _vditorOptions = new()
    {
        Height = "500px"
    };

    private Vditor _vditor = default!;
    private string? _htmlString;
    private string _vditorValueString = "## 所见即所得（WYSIWYG）\n所见即所得模式对不熟悉 Markdown 的用户较为友好，熟悉 Markdown 的话也可以无缝使用。";
    private VditorMode _mode = VditorMode.WYSIWYG;
    private ConsoleLogger _logger = default!;

    private async Task OnModeChanged(VditorMode mode)
    {
        _mode = mode;
        _vditorOptions.Mode = mode;
        if (mode == VditorMode.WYSIWYG)
        {
            _vditorValueString = "## 所见即所得（WYSIWYG）\n所见即所得模式对不熟悉 Markdown 的用户较为友好，熟悉 Markdown 的话也可以无缝使用。";
        }
        else if (mode == VditorMode.IR)
        {
            _vditorValueString = "## 即时渲染（IR）\n即时渲染模式对熟悉 Typora 的用户应该不会感到陌生，理论上这是最优雅的 Markdown 编辑方式。";
        }
        else if (mode == VditorMode.SV)
        {
            _vditorValueString = "## 分屏预览（SV）\n传统的分屏预览模式适合大屏下的 Markdown 编辑。";
        }

        _htmlString = await _vditor.GetHtmlAsync();
        StateHasChanged();
    }

    private Task OnRenderAsync()
    {
        _logger.Log($"Trigger OnRenderAsync");
        return Task.CompletedTask;
    }

    private Task OnFocusAsync(string value)
    {
        _logger.Log($"Trigger OnFocusAsync");
        return Task.CompletedTask;
    }

    private async Task OnBlurAsync(string value)
    {
        _vditorValueString = value;
        _logger.Log($"Trigger OnBlurAsync");

        _htmlString = await _vditor.GetHtmlAsync();
        StateHasChanged();
    }

    private Task OnEscapeAsync(string value)
    {
        _logger.Log($"Trigger OnEscapeAsync");
        return Task.CompletedTask;
    }

    private Task OnSelectAsync(string value)
    {
        _logger.Log($"Trigger OnSelectAsync");
        return Task.CompletedTask;
    }

    private async Task OnInputAsync(string value)
    {
        _vditorValueString = value;
        _htmlString = await _vditor.GetHtmlAsync();

        _logger.Log($"Trigger OnInputAsync");
        StateHasChanged();
    }

    private async Task OnCtrlEnterAsync(string value)
    {
        _vditorValueString = value;
        _htmlString = await _vditor.GetHtmlAsync();

        _logger.Log($"Trigger OnCtrlEnterAsync");
        StateHasChanged();
    }

    private async Task OnTriggerGetValueAsync()
    {
        _vditorValueString = await _vditor.GetValueAsync() ?? "";
    }

    private async Task OnTriggerInsertValueAsync()
    {
        await _vditor.InsertValueAsync("光标处插入当前值");
    }

    private async Task OnTriggerGetHtmlAsync()
    {
        _htmlString = await _vditor.GetHtmlAsync();
    }

    private async Task OnTriggerGetSelectionAsync()
    {
        var selection = await _vditor.GetSelectionAsync() ?? "";
        _logger.Log($"Trigger OnTriggerGetSelectionAsync: {selection}");
    }

    private bool _isDisabled = false;
    private async Task OnTriggerEnableAsync()
    {
        await _vditor.EnableAsync();
        _isDisabled = false;
    }

    private async Task OnTriggerDisableAsync()
    {
        await _vditor.DisableAsync();
        _isDisabled = true;
    }

    private async Task OnTriggerFocusAsync()
    {
        await _vditor.FocusAsync();
    }

    private async Task OnTriggerBlurAsync()
    {
        await _vditor.BlurAsync();
    }
}
