// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    public static Task ToggleById(this FullScreenService service, string id) => service.Toggle(new() { Id = id });
}
