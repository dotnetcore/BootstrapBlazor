// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class SubCascader
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("has-leaf")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Parameter]
    public IEnumerable<CascaderItem> Items { get; set; } = Enumerable.Empty<CascaderItem>();

    /// <summary>
    /// 获得/设置 选择项点击回调委托
    /// </summary>
    [Parameter]
    public Func<CascaderItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;

    /// <summary>
    /// 获得/设置 选择项是否 Active 回调委托
    /// </summary>
    [Parameter]
    public Func<string, CascaderItem, string?> ActiveItem { get; set; } = (className, _) => CssBuilder.Default(className).Build();
}
