// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">默认组件 ID 生成器</para>
/// <para lang="en">Default Component ID Generator</para>
/// </summary>
internal class DefaultIdGenerator : IComponentIdGenerator
{
    /// <summary>
    /// <para lang="zh">生成组件 Id 字符串</para>
    /// <para lang="en">Generate Component ID String</para>
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    public string Generate(object component) => $"bb_{component.GetHashCode()}";
}
