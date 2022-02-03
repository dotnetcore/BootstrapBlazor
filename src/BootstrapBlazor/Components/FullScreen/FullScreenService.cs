// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// FullScreen 服务
/// </summary>
public class FullScreenService : BootstrapServiceBase<FullScreenOption>
{
    /// <summary>
    /// 全屏方法，已经全屏时再次调用后退出全屏
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public Task Toggle(FullScreenOption? option = null) => Invoke(option ?? new());

    /// <summary>
    /// 通过 ElementReference 将指定元素进行全屏
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public Task ToggleByElement(ElementReference element) => Invoke(new() { Element = element });

    /// <summary>
    /// 通过元素 Id 将指定元素进行全屏
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Task ToggleById(string id) => Invoke(new() { Id = id });
}
