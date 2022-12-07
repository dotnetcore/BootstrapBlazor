// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Footer 组件
/// </summary>
public partial class Footer
{
    /// <summary>
    /// 获得 按钮样式集合
    /// </summary>
    /// <returns></returns>
    protected string? ClassName => CssBuilder.Default("footer")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 Footer 显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 Footer 组件中返回顶端按钮控制的滚动条所在组件 设置 <see cref="ShowGoto"/> 为 true 时生效
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Goto 小组件 默认 true 显示
    /// </summary>
    [Parameter]
    public bool ShowGoto { get; set; } = true;
}
