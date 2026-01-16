// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">FullScreenService 服务扩展方法</para>
///  <para lang="en">FullScreenService Service Extension Methods</para>
/// </summary>
public static class FullScreenServiceExtensions
{
    /// <summary>
    ///  <para lang="zh">通过 ElementReference 将指定元素进行全屏</para>
    ///  <para lang="en">FullScreen by ElementReference</para>
    /// </summary>
    /// <param name="element"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static Task ToggleByElement(this FullScreenService service, ElementReference element) => service.Toggle(new() { Element = element });

    /// <summary>
    ///  <para lang="zh">通过元素 Id 将指定元素进行全屏</para>
    ///  <para lang="en">FullScreen by Element Id</para>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static Task ToggleById(this FullScreenService service, string? id = null) => service.Toggle(new() { Id = id });
}
