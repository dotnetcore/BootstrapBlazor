// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">保留百度返回错误信息的 OCR 示例结果。</para>
/// <para lang="en">An OCR sample result preserving the provider's error information.</para>
/// </summary>
public sealed class SampleOcrResult<T>
{
    /// <summary><para lang="zh">错误码。</para><para lang="en">Error code.</para></summary>
    public int ErrorCode { get; set; }
    /// <summary><para lang="zh">错误描述。</para><para lang="en">Error message.</para></summary>
    public string? ErrorMessage { get; set; }
    /// <summary><para lang="zh">返回结果。</para><para lang="en">Result entity.</para></summary>
    public T? Entity { get; set; }
}

/// <summary>
/// <para lang="zh">发票验证结果。</para>
/// <para lang="en">Invoice verification result.</para>
/// </summary>
public sealed class SampleInvoiceVerification
{
    /// <summary><para lang="zh">是否验证通过。</para><para lang="en">Whether verification succeeded.</para></summary>
    public bool Valid { get; set; }
    /// <summary><para lang="zh">验证结果描述。</para><para lang="en">Verification message.</para></summary>
    public string? VerifyMessage { get; set; }
}
