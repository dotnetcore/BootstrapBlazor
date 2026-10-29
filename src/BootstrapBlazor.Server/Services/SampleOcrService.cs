// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// <para lang="zh">保留 Server 原有的百度 OCR 调用，仅转换示例结果。</para>
/// <para lang="en">Preserves the Server's Baidu OCR calls, adapting only sample results.</para>
/// </summary>
public sealed class SampleOcrService(IBaiduOcr ocr) : ISampleOcrService
{
    /// <inheritdoc />
    public async Task<SampleOcrResult<SampleInvoice>> RecognizeAsync(byte[] image, CancellationToken cancellationToken = default)
    {
        var result = await ocr.CheckVatInvoiceAsync(image, cancellationToken);
        var invoice = result.Entity;
        return new()
        {
            ErrorCode = result.ErrorCode,
            ErrorMessage = result.ErrorMessage,
            Entity = invoice == null ? null : new()
            {
                InvoiceCode = invoice.InvoiceCode,
                InvoiceNum = invoice.InvoiceNum,
                ServiceType = invoice.ServiceType,
                SheetNum = invoice.SheetNum,
                InvoiceType = invoice.InvoiceType,
                InvoiceTypeOrg = invoice.InvoiceTypeOrg,
                InvoiceTag = invoice.InvoiceTag,
                InvoiceDate = invoice.InvoiceDate,
                CheckCode = invoice.CheckCode,
                PurchaserName = invoice.PurchaserName,
                PurchaserRegisterNum = invoice.PurchaserRegisterNum,
                PurchaserBank = invoice.PurchaserBank,
                PurchaserAddress = invoice.PurchaserAddress,
                SellerName = invoice.SellerName,
                SellerRegisterNum = invoice.SellerRegisterNum,
                SellerBank = invoice.SellerBank,
                SellerAddress = invoice.SellerAddress,
                Remarks = invoice.Remarks,
                AmountInFiguers = invoice.AmountInFiguers,
                TotalAmount = invoice.TotalAmount,
                TotalTax = invoice.TotalTax,
                CommodityNames = invoice.CommodityName?.Select(item => item.CommodityName ?? "").ToList() ?? [],
                CommodityTaxRates = invoice.CommodityTaxRate?.Select(item => item.CommodityTaxRate ?? "").ToList() ?? []
            }
        };
    }

    /// <inheritdoc />
    public async Task<SampleOcrResult<SampleInvoiceVerification>> VerifyAsync(InvoiceVerificationRequest request, CancellationToken cancellationToken = default)
    {
        var result = await ocr.VerifyInvoiceAsync(request.InvoiceCode, request.InvoiceNum, request.InvoiceDate,
            request.InvoiceType, request.CheckCode, request.TotalAmount, cancellationToken);
        return new()
        {
            ErrorCode = result.ErrorCode,
            ErrorMessage = result.ErrorMessage,
            Entity = result.Entity == null ? null : new()
            {
                Valid = result.Entity.Valid,
                VerifyMessage = result.Entity.VerifyMessage
            }
        };
    }
}
