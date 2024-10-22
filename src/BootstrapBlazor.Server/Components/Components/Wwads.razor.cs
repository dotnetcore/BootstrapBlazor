// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
