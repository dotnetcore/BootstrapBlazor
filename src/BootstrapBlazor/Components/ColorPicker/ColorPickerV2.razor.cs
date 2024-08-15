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
    /// 需要设置透明度，默认是false，即透明度永远是100%，完全不透明
    /// </summary>
    [Parameter]
    public bool NeedAlpha { get; set; }

    /// <summary>
    /// 选中颜色的字符串结果显示的格式类型，默认为Hex格式
    /// </summary>
    [Parameter]
    public ColorPickerV2FormatType FormatType { get; set; } = ColorPickerV2FormatType.Hex;

    /// <summary>
    /// 是否打开设置窗口
    /// </summary>
    private bool _openSettingView;

    /// <summary>
    /// _openSettingView对应的css样式
    /// </summary>
    private string OpenSettingView => _openSettingView ? "block" : "none";

    /// <summary>
    /// 最终展示色（无透明通道）
    /// </summary>
    private string _previewColor = "hsl(0, 50%, 50%)";

    private string PreviewColor => NeedAlpha ? _previewColor.Replace(")", $", {_alphaPercentage:F4})") : _previewColor;

    /// <summary>
    /// 开关设置窗口
    /// </summary>
    /// <param name="e"></param>
    private void OpenSettingViewChanged(MouseEventArgs e)
    {
        _openSettingView = !_openSettingView;
    }

    /// <summary>
    ///
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await SetHueParam(0);
    }

    #region 饱和度+明度设置区域

    /// <summary>
    /// 饱和度和明度区块id
    /// </summary>
    private string _colorPaletteId = $"color-palette-{Guid.NewGuid().ToString()}";

    /// <summary>
    /// 饱和度当前选中位置相对整个长度的0-1
    /// </summary>
    private double _saturationPercentage = 0.5;

    /// <summary>
    /// 明度当前选中位置相对整个长度的0-1
    /// </summary>
    private double _lightnessPercentage = 0.5;

    /// <summary>
    /// 点击色相选择滑块时，根据点击位置设置_huePercentage
    /// </summary>
    private async Task SelectColorPalette(MouseEventArgs e)
    {
        var colorPaletteSelectPercentage = await InvokeAsync<double[]>(
            "getElementClickLocation", _colorPaletteId, e);
        SetSaturationParam(colorPaletteSelectPercentage != null ? colorPaletteSelectPercentage[0] : 0);
        SetLightnessParam(colorPaletteSelectPercentage != null ? colorPaletteSelectPercentage[1] : 0);
        BlendXyColor();
        await Task.CompletedTask;
    }

    /// <summary>
    /// 饱和度明度选择圆形块是否选中
    /// </summary>
    private bool _isSelectColorPaletteRoundBlock;

    /// <summary>
    /// 选中色相选择圆形块时，根据鼠标在色相选择滑块的移动位置设置_huePercentage
    /// </summary>
    private async Task MoveOnColorPalette(MouseEventArgs e)
    {
        if (_isSelectColorPaletteRoundBlock)
        {
            var colorPaletteSelectPercentage = await InvokeAsync<double[]>(
                "getElementClickLocation", _colorPaletteId, e);
            SetSaturationParam(colorPaletteSelectPercentage != null ? colorPaletteSelectPercentage[0] : 0);
            SetLightnessParam(colorPaletteSelectPercentage != null ? colorPaletteSelectPercentage[1] : 0);
            BlendXyColor();
        }
        await Task.CompletedTask;
    }

    private (double hue, double saturation, double lightness) _xColor;

    /// <summary>
    /// 设置饱和度相关参数
    /// </summary>
    /// <param name="source"></param>
    private void SetSaturationParam(double source)
    {
        _saturationPercentage = Math.Clamp(source, 0, 1);
        _xColor = (_hueValue, _saturationPercentage, (1 - _saturationPercentage) / 2 + 0.5);
    }

    private (double hue, double saturation, double lightness) _yColor;

    /// <summary>
    /// 设置明度相关参数
    /// </summary>
    /// <param name="source"></param>
    private void SetLightnessParam(double source)
    {
        _lightnessPercentage = Math.Clamp(source, 0, 1);
        _yColor = (_hueValue,  0, 1 - _lightnessPercentage);
    }

    private (double r, double g, double b) _rgbX;
    private (double r, double g, double b) _rgbY;
    private (double r, double g, double b) _rgbResult;
    private (double hue, double saturation, double lightness) _hslResult;

    private void BlendXyColor()
    {
        _rgbX = HslToRgb(_xColor.hue, _xColor.saturation, _xColor.lightness);
        _rgbY = HslToRgb(_yColor.hue, _yColor.saturation, _yColor.lightness);
        _rgbResult = MultiplyBlend(_rgbX, _rgbY);
        _hslResult = RgbToHsl(_rgbResult.r, _rgbResult.g, _rgbResult.b);
        _previewColor = $"hsl({_hslResult.hue:F2}, {DoubleToPercentage(_hslResult.saturation)}, {DoubleToPercentage(_hslResult.lightness)})";
    }

    #endregion

    #region 色相设置区域

    /// <summary>
    /// 色相滑块id
    /// </summary>
    private string _colorSliderId = $"color-slider-{Guid.NewGuid().ToString()}";

    /// <summary>
    /// 当前色相滑块选中的颜色
    /// </summary>
    private double _hueValue = 0;

    /// <summary>
    /// 色相滑块当前选中位置相对整个长度的0-1
    /// </summary>
    private double _huePercentage = 0;

    /// <summary>
    /// 点击色相选择滑块时，根据点击位置设置_huePercentage
    /// </summary>
    private async Task SelectColorSlider(MouseEventArgs e)
    {
        var colorSliderSelectPercentage = await InvokeAsync<double[]>(
            "getElementClickLocation", _colorSliderId, e);
        await SetHueParam(colorSliderSelectPercentage != null ? colorSliderSelectPercentage[0] : 0);
        await Task.CompletedTask;
    }

    /// <summary>
    /// 色相选择圆形块是否选中
    /// </summary>
    private bool _isSelectColorSliderRoundBlock;

    /// <summary>
    /// 选中色相选择圆形块时，根据鼠标在色相选择滑块的移动位置设置_huePercentage
    /// </summary>
    private async Task MoveOnColorSlider(MouseEventArgs e)
    {
        if (_isSelectColorSliderRoundBlock)
        {
            var colorSliderSelectPercentage = await InvokeAsync<double[]>(
                "getElementClickLocation", _colorSliderId, e);
            await SetHueParam(colorSliderSelectPercentage != null ? colorSliderSelectPercentage[0] : 0);
        }
        await Task.CompletedTask;
    }

    /// <summary>
    /// 设置色相相关参数
    /// </summary>
    /// <param name="source"></param>
    private async Task SetHueParam(double source)
    {
        _huePercentage = Math.Clamp(source, 0, 1);
        _hueValue = _huePercentage * 360;
        SetSaturationParam(_saturationPercentage);
        SetLightnessParam(_lightnessPercentage);
        BlendXyColor();
        await Task.CompletedTask;
    }

    #endregion

    #region 透明度设置区域

    /// <summary>
    /// 色相滑块id
    /// </summary>
    private string _alphaSliderId = $"alpha-slider-{Guid.NewGuid().ToString()}";

    /// <summary>
    /// 透明通道设置组件对应的css样式
    /// </summary>
    private string OpenAlpha => NeedAlpha ? "block" : "none";

    /// <summary>
    /// 透明度滑块当前选中位置相对整个长度的0-1
    /// </summary>
    private double _alphaPercentage = 1;

    /// <summary>
    /// 点击色相选择滑块时，根据点击位置设置_huePercentage
    /// </summary>
    private async Task SelectAlphaSlider(MouseEventArgs e)
    {
        var alphaSliderSelectPercentage = await InvokeAsync<double[]>(
            "getElementClickLocation", _alphaSliderId, e);
        await SetAlphaParam(alphaSliderSelectPercentage != null ? alphaSliderSelectPercentage[0] : 0);
        await Task.CompletedTask;
    }

    /// <summary>
    /// 透明度选择圆形块是否选中
    /// </summary>
    private bool _isSelectAlphaSliderRoundBlock;

    /// <summary>
    /// 选中色相选择圆形块时，根据鼠标在色相选择滑块的移动位置设置_huePercentage
    /// </summary>
    private async Task MoveOnAlphaSlider(MouseEventArgs e)
    {
        if (_isSelectAlphaSliderRoundBlock)
        {
            var alphaSliderSelectPercentage = await InvokeAsync<double[]>(
                "getElementClickLocation", _alphaSliderId, e);
            await SetAlphaParam(alphaSliderSelectPercentage != null ? alphaSliderSelectPercentage[0] : 0);
            await Task.CompletedTask;
        }
        await Task.CompletedTask;
    }

    private async Task SetAlphaParam(double source)
    {
        _alphaPercentage = Math.Clamp(source, 0, 1);
        await SetHueParam(_huePercentage);
        await Task.CompletedTask;
    }

    #endregion


}

