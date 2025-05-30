// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Pages;

/// <summary>
/// ErrorPage 组件用于测试全局异常处理功能
/// </summary>
public partial class ErrorPage
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var a = 1;
        var b = 0;

        // 这里会抛出异常
        var c = a / b;
    }
}
