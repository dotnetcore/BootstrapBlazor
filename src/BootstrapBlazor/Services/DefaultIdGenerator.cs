// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认组件 ID 生成器
/// </summary>
internal class DefaultIdGenerator : IComponentIdGenerator
{
    /// <summary>
    /// 生成组件 Id 字符串
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public string Generate(object component) => $"bb_{component.GetHashCode()}";
}
