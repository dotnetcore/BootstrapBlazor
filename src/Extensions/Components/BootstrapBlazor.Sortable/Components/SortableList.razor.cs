// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// SortableList 组件
/// </summary>
public partial class SortableList
{
    /// <summary>
    /// 获得/设置 配置项实例 <see cref="SortableOption"/>
    /// </summary>
    [Parameter]
    public SortableOption? Option {get;set;}

    /// <summary>
    /// 获得/设置 子组件 必填项不可为空
    /// </summary>
    [Parameter]
    [EditorRequired]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-sortable")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, Option);
}
