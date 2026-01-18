// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ITabHeader 接口</para>
/// <para lang="en">ITabHeader Interface</para>
/// </summary>
public interface ITabHeader
{
    /// <summary>
    /// <para lang="zh">呈现标签头的方法</para>
    /// <para lang="en">Render method</para>
    /// </summary>
    /// <param name="renderFragment"></param>
    void Render(RenderFragment renderFragment);

    /// <summary>
    /// <para lang="zh">获取标签头的 id</para>
    /// <para lang="en">Gets the id of the tab header</para>
    /// </summary>
    string GetId();
}
