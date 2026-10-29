// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">IP 定位结果及服务商错误信息。</para>
/// <para lang="en">An IP location result and provider error information.</para>
/// </summary>
public sealed class SampleLocatorResult
{
    /// <summary><para lang="zh">地理位置。</para><para lang="en">Geographical location.</para></summary>
    public string? Location { get; set; }
    /// <summary><para lang="zh">服务商错误描述。</para><para lang="en">Provider error message.</para></summary>
    public string? ErrorMessage { get; set; }
}
