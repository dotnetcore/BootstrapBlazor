// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
