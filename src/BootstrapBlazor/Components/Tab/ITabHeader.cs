// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITabHeader interface
/// </summary>
public interface ITabHeader
{
    /// <summary>
    /// Render method
    /// </summary>
    /// <param name="renderFragment"></param>
    void Render(RenderFragment renderFragment);

    /// <summary>
    /// Get the id of the tab header
    /// </summary>
    /// <returns></returns>
    string GetId();
}
