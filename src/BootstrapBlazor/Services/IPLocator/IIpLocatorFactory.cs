// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IIpLocatorFactory 接口</para>
/// <para lang="en">IIpLocatorFactory Interface</para>
/// </summary>
public interface IIpLocatorFactory
{
    /// <summary>
    /// <para lang="zh">创建 IIPLocator 实例方法</para>
    /// <para lang="en">Create IIPLocator Instance Method</para>
    /// </summary>
    /// <param name="key"><para lang="zh">注入时使用的 key 值</para><para lang="en">Key used when injecting</para></param>
    /// <returns></returns>
    IIpLocatorProvider Create(string? key = null);
}
