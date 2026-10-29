// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">聊天示例请求，密钥不进入浏览器。</para>
/// <para lang="en">A chat sample request without browser-side credentials.</para>
/// </summary>
public sealed class SampleChatRequest
{
    /// <summary><para lang="zh">浏览器额度标识，不作为身份认证。</para><para lang="en">Browser quota identifier, not authentication.</para></summary>
    [Required, StringLength(256)]
    public string Code { get; set; } = "";

    /// <summary><para lang="zh">本次问题。</para><para lang="en">The current question.</para></summary>
    [Required, StringLength(8000)]
    public string Content { get; set; } = "";
}
