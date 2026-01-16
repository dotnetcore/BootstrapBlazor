// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">组件 ID 生成器接口</para>
/// <para lang="en">Component ID Generator Interface</para>
/// </summary>
public interface IComponentIdGenerator
{
    /// <summary>
    /// <para lang="zh">生成组件 Id 方法</para>
    /// <para lang="en">Generate Component ID Method</para>
    /// </summary>
    /// <param name="component"></param>
    /// <returns></returns>
    string Generate(object component);
}
