// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 百度文字识别服务
/// </summary>
public interface IBaiduOcr
{
    /// <summary>
    /// 增值税发票识别
    /// </summary>
    /// <param name="image"></param>
    /// <param name="token"></param>
    Task<BaiduOcrResult<InvoiceEntity>> CheckVatInvoiceAsync(byte[] image, CancellationToken token = default);

    /// <summary>
    /// 增值税发票识别
    /// </summary>
    /// <param name="image"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<string> CheckVatInvoiceJsonAsync(byte[] image, CancellationToken token = default);

    /// <summary>
    /// 增值税发票验真方法
    /// </summary>
    /// <param name="invoiceCode">发票代码</param>
    /// <param name="invoiceNum">发票号码</param>
    /// <param name="invoiceDate">开票日期格式 YYYYMMDD</param>
    /// <param name="invoiceType">发票种类 增值税专用发票：special_vat_invoice 增值税电子专用发票：elec_special_vat_invoice 增值税普通发票：normal_invoice 增值税普通发票（电子）：elec_normal_invoice 增值税普通发票（卷式）：roll_normal_invoice 通行费增值税电子普通发票：toll_elec_normal_invoice 区块链电子发票（目前仅支持深圳地区）：blockchain_invoice 全电发票（专用发票）：elec_invoice_special 全电发票（普通发票）：elec_invoice_normal 货运运输业增值税专用发票：special_freight_transport_invoice 机动车销售发票：motor_vehicle_invoice 二手车销售发票：used_vehicle_invoice</param>
    /// <param name="checkCode">校验码</param>
    /// <param name="totalAmount">发票金额</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<BaiduOcrResult<InvoiceVerifyResult>> VerifyInvoiceAsync(string invoiceCode, string invoiceNum, string invoiceDate, string invoiceType, string? checkCode, string? totalAmount, CancellationToken token = default);
}
