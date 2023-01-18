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
    public Task<string> CheckVatInvoiceJsonAsync(byte[] image) => Task.Run(() =>
    {
        var client = new Ocr(Options.CurrentValue.ApiKey, Options.CurrentValue.Secret);
        var resp = client.VatInvoice(image);
        return resp.ToString();
    });

    /// <summary>
    /// 识别增值税发票方法
    /// </summary>
    public Task<BaiduOcrResult<InvoiceEntity>> CheckVatInvoiceAsync(byte[] image) => Task.Run(() =>
    {
        var ret = new BaiduOcrResult<InvoiceEntity>();
        var client = new Ocr(Options.CurrentValue.ApiKey, Options.CurrentValue.Secret);
        var resp = client.VatInvoice(image);
        if (resp.TryGetValue("words_result", out var value))
        {
            ret.Entity = value.ToObject(typeof(InvoiceEntity)) as InvoiceEntity;
        }
        else
        {
            ret.ErrorCode = resp.Value<int>("error_code");
            ret.ErrorMessage = resp.Value<string>("error_msg");
        }
        return ret;
    });
}
