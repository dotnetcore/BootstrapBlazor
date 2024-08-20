using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

public partial class ColorPickerV2
{
    /// <summary>
    /// 是否需要设置透明度，默认是false，即透明度永远是100%，完全不透明
    /// </summary>
    [Parameter]
    public bool NeedAlpha { get; set; }

    /// <summary>
    /// 选中颜色的字符串结果显示的格式类型，默认为Hex格式
    /// </summary>
    [Parameter]
    public ColorPickerV2FormatType FormatType { get; set; } = ColorPickerV2FormatType.Hex;

    /// <summary>
    /// 颜色修改后的事件回调，触发时机为实际输入框内手动输入并回车或者点击关闭弹出的设置窗口
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnValueChanged { get; set; }

    /// <summary>
    /// 预设的颜色值
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    private bool _openSettingView;
    private string OpenSettingView => _openSettingView ? "block" : "none";
    private string OpenAlpha => NeedAlpha ? "block" : "none";

    private string _resultColor;

    /// <summary>
    /// 开关设置窗口，如果关闭，将最终颜色值推送出去
    /// </summary>
    /// <param name="e"></param>
    private async Task OpenSettingViewChanged(MouseEventArgs e)
    {
        _openSettingView = !_openSettingView;
        if (!_openSettingView)
        {
            Value = await GetFormatColorAsync();
            if (OnValueChanged != null)
                await OnValueChanged.Invoke(Value);
        }
    }

    private async Task<string> GetFormatColorAsync()
    {
        var result = await InvokeAsync<double[]>("getColorPickerResult", _selfId) ?? [0, 0, 0, 1];
        _resultColor =
            $"hsla({result![0]}, {DoubleToPercentage(result[1])}, {DoubleToPercentage(result[2])}, {result[3]:F2})";
        var formatResult = GetFormatColorString(result);
        return formatResult;
    }

    private string _selfId = string.Empty;
    private string _colorPaletteId = string.Empty;
    private string _colorPaletteRoundBlockId = string.Empty;
    private string _colorSliderId = string.Empty;
    private string _colorSliderRoundBlockId = string.Empty;
    private string _alphaSliderId = string.Empty;
    private string _alphaSliderRoundBlockId = string.Empty;

    /// <summary>
    /// 初始化id
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _selfId = $"color-picker-v2-{Guid.NewGuid().ToString()}";
        _colorPaletteId = $"color-palette-{Guid.NewGuid().ToString()}";
        _colorPaletteRoundBlockId = $"color-palette-round-block-{Guid.NewGuid().ToString()}";
        _colorSliderId = $"color-slider-{Guid.NewGuid().ToString()}";
        _colorSliderRoundBlockId = $"color-slider-round-block-{Guid.NewGuid().ToString()}";
        _alphaSliderId = $"alpha-slider-{Guid.NewGuid().ToString()}";
        _alphaSliderRoundBlockId = $"alpha-slider-round-block-{Guid.NewGuid().ToString()}";
    }

    /// <summary>
    /// 初始化事件
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            await InvokeVoidAsync(
                "initColorPicker", _selfId,
                _colorPaletteId, _colorPaletteRoundBlockId,
                _colorSliderId, _colorSliderRoundBlockId,
                _alphaSliderId, _alphaSliderRoundBlockId);
            //如果本身有值，直接走一遍set
            if (Value != null)
            {
                var hsla = GetFormatColorValue(Value);
                await InvokeVoidAsync("setColorPicker", _selfId, hsla.H, hsla.S, hsla.L, hsla.A);
            }
            Value = await GetFormatColorAsync();
            await InvokeAsync(StateHasChanged);
        }
    }

    /// <summary>
    /// 取消在jsmap中的缓存
    /// </summary>
    /// <param name="disposing"></param>
    protected override async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
            await InvokeVoidAsync("disposeColorPicker", _selfId);
        await base.DisposeAsync(disposing);
    }


    private async Task UpdateColorFromInputAsync(string value)
    {
        var hsla = GetFormatColorValue(value);
        await InvokeVoidAsync("setColorPicker", _selfId, hsla.H, hsla.S, hsla.L, hsla.A);
        Value = await GetFormatColorAsync();
        if (OnValueChanged != null)
            await OnValueChanged.Invoke(Value);
        await InvokeAsync(StateHasChanged);
    }
}

