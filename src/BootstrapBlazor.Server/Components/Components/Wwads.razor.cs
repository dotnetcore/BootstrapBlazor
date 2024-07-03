// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 广告组件
/// </summary>
public partial class Wwads
{
    /// <summary>
    /// 获得/设置 是否垂直布局
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    private string? ClassString => CssBuilder.Default("bb-ad")
        .AddClass("bb-ad-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

#if DEBUG
    private readonly bool isDebug = true;
#else
    private readonly bool isDebug = false;
#endif

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, new { IsVertical, IsDebug = isDebug });
}
