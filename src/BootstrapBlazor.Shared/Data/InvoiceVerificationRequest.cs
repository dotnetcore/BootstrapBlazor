// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">发票验证请求。</para>
/// <para lang="en">An invoice verification request.</para>
/// </summary>
public sealed class InvoiceVerificationRequest
{
    /// <summary><para lang="zh">发票代码。</para><para lang="en">Invoice code.</para></summary>
    [Required, StringLength(32)]
    public string InvoiceCode { get; set; } = "";
    /// <summary><para lang="zh">发票号码。</para><para lang="en">Invoice number.</para></summary>
    [Required, StringLength(32)]
    public string InvoiceNum { get; set; } = "";
    /// <summary><para lang="zh">开票日期 YYYYMMDD。</para><para lang="en">Invoice date in YYYYMMDD format.</para></summary>
    [Required, RegularExpression(@"^\d{8}$")]
    public string InvoiceDate { get; set; } = "";
    /// <summary><para lang="zh">发票种类。</para><para lang="en">Invoice type.</para></summary>
    [Required, StringLength(64)]
    public string InvoiceType { get; set; } = "";
    /// <summary><para lang="zh">校验码。</para><para lang="en">Check code.</para></summary>
    [Required, StringLength(32)]
    public string CheckCode { get; set; } = "";
    /// <summary><para lang="zh">发票金额。</para><para lang="en">Invoice amount.</para></summary>
    [StringLength(32)]
    public string? TotalAmount { get; set; }
}
