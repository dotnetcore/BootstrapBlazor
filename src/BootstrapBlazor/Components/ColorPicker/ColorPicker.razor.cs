// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// ColorPicker 颜色拾取器组件
/// </summary>
public partial class ColorPicker
{
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

    /// <summary>
    /// 获得/设置 是否支持透明度 默认 false 不支持
    /// </summary>
    [Parameter]
    public bool IsSupportOpacity { get; set; }

    /// <summary>
    /// 获得/设置 预设候选颜色 <see cref="IsSupportOpacity"/> 开启时生效 默认 null
    /// </summary>
    /// <remarks>字符串集合格式为 ["rgba(244, 67, 54, 1)", "rgba(233, 30, 99, 0.95)"]</remarks>
    [Parameter]
    public List<string>? Swatches { get; set; }

    private string? _formattedValueString;

    private bool _originalSupportOpacityValue;

    private bool _originalIsDisabledValue;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        await FormatValue();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            _originalSupportOpacityValue = IsSupportOpacity;
            _originalIsDisabledValue = IsDisabled;
        }

        if (_originalSupportOpacityValue != IsSupportOpacity || _originalIsDisabledValue != IsDisabled)
        {
            _originalSupportOpacityValue = IsSupportOpacity;
            _originalIsDisabledValue = IsDisabled;
            await InvokeVoidAsync("update", Id, new { IsSupportOpacity, Value, Disabled = IsDisabled, Lang = CultureInfo.CurrentUICulture.Name, Swatches });
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, new { IsSupportOpacity, Default = Value, Disabled = IsDisabled, Lang = CultureInfo.CurrentUICulture.Name, Swatches });

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

    /// <summary>
    /// 选中颜色值变化时回调此方法 由 JavaScript 调用
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task OnColorChanged(string value)
    {
        CurrentValueAsString = value;
        if (!ValueChanged.HasDelegate)
        {
            StateHasChanged();
        }
        return Task.CompletedTask;
    }
}
