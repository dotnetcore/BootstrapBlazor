// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreenService 服务扩展方法
/// </summary>
public static class FullScreenServiceExtensions
{
    /// <summary>
    /// 通过 ElementReference 将指定元素进行全屏
    /// </summary>
    /// <param name="element"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static Task ToggleByElement(this FullScreenService service, ElementReference element) => service.Toggle(new() { Element = element });

    /// <summary>
    /// 通过元素 Id 将指定元素进行全屏
    /// </summary>
    /// <param name="id"></param>
    /// <param name="service"></param>
    /// <returns></returns>
    public static Task ToggleById(this FullScreenService service, string? id = null) => service.Toggle(new() { Id = id });
}
