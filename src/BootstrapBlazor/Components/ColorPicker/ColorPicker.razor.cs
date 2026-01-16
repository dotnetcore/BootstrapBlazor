// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ColorPicker 颜色拾取器组件</para>
/// <para lang="en">ColorPicker component</para>
/// </summary>
public partial class ColorPicker
{
    /// <summary>
    /// <para lang="zh">获得 class 样式集合</para>
    /// <para lang="en">Get class style collection</para>
    /// </summary>
    protected string? ClassName => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 显示模板</para>
    /// <para lang="en">Get/Set display template</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment<string>? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示颜色值格式化回调方法</para>
    /// <para lang="en">Get/Set display color value formatting callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<string, Task<string>>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否支持透明度 默认 false 不支持</para>
    /// <para lang="en">Get/Set whether to support opacity, default is false(not supported)</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsSupportOpacity { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 预设候选颜色 <see cref="IsSupportOpacity"/> 开启时生效 默认 null</para>
    /// <para lang="en">Get/Set preset candidate colors, effective when <see cref="IsSupportOpacity"/> is enabled, default is null</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">字符串集合格式为 ["rgba(244, 67, 54, 1)", "rgba(233, 30, 99, 0.95)"]</para>
    /// <para lang="en">String collection format is ["rgba(244, 67, 54, 1)", "rgba(233, 30, 99, 0.95)"]</para>
    /// </remarks>
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
    /// <para lang="zh">选中颜色值变化时回调此方法 由 JavaScript 调用</para>
    /// <para lang="en">Callback method when selected color value changes, called by JavaScript</para>
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
