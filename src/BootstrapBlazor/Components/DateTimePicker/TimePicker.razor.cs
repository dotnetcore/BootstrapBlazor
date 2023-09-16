﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// TimePicker 组件
/// </summary>
public partial class TimePicker
{
    /// <summary>
    /// 获得/设置 样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("bb-time-picker")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件值
    /// </summary>
    [Parameter]
    public TimeSpan Value { get; set; }

    /// <summary>
    /// 获得/设置 组件值变化时委托方法
    /// </summary>
    [Parameter]
    public EventCallback<TimeSpan> ValueChanged { get; set; }

}
