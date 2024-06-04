// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// 广告组件
/// </summary>
public partial class Wwads
{
    private string? _debugString;

    private string? ClassString => CssBuilder.Default("bb-ad")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

#if DEBUG
        _debugString = "true";
#endif
    }
}
