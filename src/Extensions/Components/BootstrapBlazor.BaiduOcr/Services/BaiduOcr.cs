// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Baidu.Aip.Ocr;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

internal class BaiduOcr : IBaiduOcr
{
    protected IOptionsMonitor<BaiduOcrOption> Options { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    public BaiduOcr(IOptionsMonitor<BaiduOcrOption> options)
    {
        Options = options;
    }

    /// <summary>
    /// 身份证识别
    /// </summary>
    public void Scan(byte[] image)
    {
        var client = new Ocr(Options.CurrentValue.ApiKey, Options.CurrentValue.Secret);
        client.GeneralBasic(image);
    }

    /// <summary>
    /// 识别增值税发票方法
    /// </summary>
    public Task<InvoiceEntity> CheckVatInvoiceAsync(byte[] image) => Task.Run(() =>
    {
        var client = new Ocr(Options.CurrentValue.ApiKey, Options.CurrentValue.Secret);
        var resp = client.VatInvoice(image);
        var ret = resp.GetValue("words_result").ToObject(typeof(InvoiceEntity)) as InvoiceEntity;
        return ret ?? new InvoiceEntity() { CommodityName = new(), CommodityTaxRate = new() };
    });
}
