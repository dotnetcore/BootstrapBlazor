// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Toggle 开关组件
/// </summary>
public class ToggleBase<TValue> : ValidateBase<TValue>
{
    /// <summary>
    /// 获得 Style 集合
    /// </summary>
    protected virtual string? StyleName => CssBuilder.Default()
        .AddClass($"width: {Width}px;", Width > 0)
        .Build();

    /// <summary>
    /// 获得/设置 组件宽度
    /// </summary>
    [Parameter]
    public virtual int Width { get; set; } = 120;

    /// <summary>
    /// 获得/设置 组件 On 时显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? OnText { get; set; }

    /// <summary>
    /// 获得/设置 组件 Off 时显示文本
    /// </summary>
    [Parameter]
    [NotNull]
    public virtual string? OffText { get; set; }
}
