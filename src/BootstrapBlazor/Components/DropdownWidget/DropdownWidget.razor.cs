// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// DropdownWidget 组件
/// </summary>
public sealed partial class DropdownWidget
{
    private string? ClassString => CssBuilder.Default("widget")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 选项模板支持静态数据
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 挂件数据集合
    /// </summary>
    [Parameter]
    public IEnumerable<DropdownWidgetItem>? Items { get; set; }

    private List<DropdownWidgetItem> Childs { get; set; } = new List<DropdownWidgetItem>(20);

    /// <summary>
    /// 添加 DropdownWidgetItem 方法
    /// </summary>
    /// <param name="item"></param>
    internal void Add(DropdownWidgetItem item)
    {
        Childs.Add(item);
    }

    private IEnumerable<DropdownWidgetItem> GetItems() => Items == null ? Childs : Childs.Concat(Items);
}
