using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components;

public partial class ColorPickerV2
{
    #region Old

    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    protected string? ClassName => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 获得/设置 显示模板
    /// </summary>
    [Parameter]
    public RenderFragment<string>? Template { get; set; }

    /// <summary>
    /// 获得/设置 显示颜色值格式化回调方法
    /// </summary>
    [Parameter]
    public Func<string, Task<string>>? Formatter { get; set; }

    private string? _formattedValueString;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await FormatValue();
    }

    private async Task Setter(string v)
    {
        CurrentValueAsString = v;
        await FormatValue();
    }

    private async Task FormatValue()
    {
        if (Formatter != null)
        {
            // 此处未使用父类 FormatValueAsString 方法
            // 使用者可能需要通过回调通过异步方式获得显示数据
            _formattedValueString = await Formatter(CurrentValueAsString);
        }
    }

    #endregion

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
    /// 当关闭弹出窗口时，将结果颜色值推送出去
    /// </summary>
    [Parameter]
    public Func<string, Task>? OnFinishSettingTask { get; set; }

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
            _resultColor = await GetFormatColorAsync();
            if (OnFinishSettingTask != null)
                await OnFinishSettingTask.Invoke(_resultColor);
        }
    }

    private async Task<string> GetFormatColorAsync()
    {
        var result = await InvokeAsync<double[]>("getColorPickerResult", _selfId);
        var formatResult = GetFormatColor(result);
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
            _resultColor = await GetFormatColorAsync();
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


}

