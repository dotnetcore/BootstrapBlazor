// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// <para lang="zh">OCR 示例显示的发票字段，不依赖百度 SDK。</para>
/// <para lang="en">Invoice fields displayed by the OCR sample, independent of the Baidu SDK.</para>
/// </summary>
public sealed class SampleInvoice
{
    /// <summary><para lang="zh">发票代码。</para><para lang="en">Invoice code.</para></summary>
    public string? InvoiceCode { get; set; }
    /// <summary><para lang="zh">发票号码。</para><para lang="en">Invoice number.</para></summary>
    public string? InvoiceNum { get; set; }
    /// <summary><para lang="zh">消费类型。</para><para lang="en">Service type.</para></summary>
    public string? ServiceType { get; set; }
    /// <summary><para lang="zh">联次信息。</para><para lang="en">Invoice copy information.</para></summary>
    public string? SheetNum { get; set; }
    /// <summary><para lang="zh">发票种类。</para><para lang="en">Invoice type.</para></summary>
    public string? InvoiceType { get; set; }
    /// <summary><para lang="zh">发票名称。</para><para lang="en">Invoice name.</para></summary>
    public string? InvoiceTypeOrg { get; set; }
    /// <summary><para lang="zh">发票标志。</para><para lang="en">Invoice tag.</para></summary>
    public string? InvoiceTag { get; set; }
    /// <summary><para lang="zh">开票日期。</para><para lang="en">Invoice date.</para></summary>
    public string? InvoiceDate { get; set; }
    /// <summary><para lang="zh">校验码。</para><para lang="en">Check code.</para></summary>
    public string? CheckCode { get; set; }
    /// <summary><para lang="zh">购方名称。</para><para lang="en">Purchaser name.</para></summary>
    public string? PurchaserName { get; set; }
    /// <summary><para lang="zh">购方纳税人识别号。</para><para lang="en">Purchaser taxpayer ID.</para></summary>
    public string? PurchaserRegisterNum { get; set; }
    /// <summary><para lang="zh">购方开户行及账号。</para><para lang="en">Purchaser bank account.</para></summary>
    public string? PurchaserBank { get; set; }
    /// <summary><para lang="zh">购方地址及电话。</para><para lang="en">Purchaser address and phone.</para></summary>
    public string? PurchaserAddress { get; set; }
    /// <summary><para lang="zh">销售方名称。</para><para lang="en">Seller name.</para></summary>
    public string? SellerName { get; set; }
    /// <summary><para lang="zh">销售方纳税人识别号。</para><para lang="en">Seller taxpayer ID.</para></summary>
    public string? SellerRegisterNum { get; set; }
    /// <summary><para lang="zh">销售方开户行及账号。</para><para lang="en">Seller bank account.</para></summary>
    public string? SellerBank { get; set; }
    /// <summary><para lang="zh">销售方地址及电话。</para><para lang="en">Seller address and phone.</para></summary>
    public string? SellerAddress { get; set; }
    /// <summary><para lang="zh">备注。</para><para lang="en">Remarks.</para></summary>
    public string? Remarks { get; set; }
    /// <summary><para lang="zh">价税合计。</para><para lang="en">Amount including tax.</para></summary>
    public string? AmountInFiguers { get; set; }
    /// <summary><para lang="zh">合计金额。</para><para lang="en">Total amount.</para></summary>
    public string? TotalAmount { get; set; }
    /// <summary><para lang="zh">合计税额。</para><para lang="en">Total tax.</para></summary>
    public string? TotalTax { get; set; }
    /// <summary><para lang="zh">货物名称。</para><para lang="en">Commodity names.</para></summary>
    public List<string> CommodityNames { get; set; } = [];
    /// <summary><para lang="zh">税率。</para><para lang="en">Commodity tax rates.</para></summary>
    public List<string> CommodityTaxRates { get; set; } = [];
}
