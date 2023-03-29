// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Textarea 组件
/// </summary>
[JSModuleAutoLoader]
public partial class Textarea
{
    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    private string? ClassName => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .Build();

    /// <summary>
    /// 滚动到顶部
    /// </summary>
    /// <returns></returns>
    public Task ScrollToTop() => InvokeExecuteAsync(Id, "toTop");

    /// <summary>
    /// 滚动到数值
    /// </summary>
    /// <returns></returns>
    public Task ScrollTo(int value) => InvokeExecuteAsync(Id, "to", value);

    /// <summary>
    /// 滚动到底部
    /// </summary>
    /// <returns></returns>
    public Task ScrollToBottom() => InvokeExecuteAsync(Id, "toBottom");

    /// <summary>
    /// 获得/设置 是否自动滚屏 默认 false
    /// </summary>
    [Parameter]
    public bool IsAutoScroll { get; set; }

    /// <summary>
    /// 获得 客户端是否自动滚屏标识
    /// </summary>
    private string? AutoScrollString => IsAutoScroll ? "auto" : null;
}
