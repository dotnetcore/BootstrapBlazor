// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// DateTimePickerCell 组件
/// </summary>
public sealed partial class DatePickerCell
{
    private string? ClassString => CssBuilder.Default("cell")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 日期
    /// </summary>
    [Parameter]
    public DateTime Value { get; set; }

    /// <summary>
    /// 获得/设置 日期
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 按钮点击回调方法
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<DateTime, Task>? OnClick { get; set; }
}
