// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Hosting;

namespace BootstrapBlazor.Components;

/// <summary>
/// Link 组件
/// </summary>
public partial class Link
{
    /// <summary>
    /// 获得/设置 href 属性值
    /// </summary>
    [Parameter]
    [EditorRequired]
    public string? Href { get; set; }

    /// <summary>
    /// 获得/设置 版本号 默认 null 自动生成
    /// </summary>
    [Parameter]
    public string? Version { get; set; }

    [Inject, NotNull]
    private IWebHostEnvironment? Env { get; set; }

    private string GetHref() => $"{Href}?v={Version ?? Env.GetVersionHash(Href)}";
}
