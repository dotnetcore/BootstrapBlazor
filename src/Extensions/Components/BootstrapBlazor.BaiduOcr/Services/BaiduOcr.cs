// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Baidu.Aip.Ocr;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace BootstrapBlazor.Components;

internal class BaiduOcr : IBaiduOcr
{
    protected IOptionsMonitor<BaiduOcrOption> Options { get; set; }

    protected IHttpClientFactory HttpClientFactory { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="options"></param>
    /// <param name="httpClientFactory"></param>
    public BaiduOcr(IOptionsMonitor<BaiduOcrOption> options, IHttpClientFactory httpClientFactory)
    {
        Options = options;
        HttpClientFactory = httpClientFactory;
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
    public Task<string> CheckVatInvoiceJsonAsync(byte[] image, CancellationToken token = default) => Task.Run(() =>
    {
        var client = new Ocr(Options.CurrentValue.ApiKey, Options.CurrentValue.Secret);
        var resp = client.VatInvoice(image);
        return resp.ToString();
    }, token);

    /// <summary>
    /// 识别增值税发票方法
    /// </summary>
    public Task<BaiduOcrResult<InvoiceEntity>> CheckVatInvoiceAsync(byte[] image, CancellationToken token = default) => Task.Run(() =>
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
    }, token);

    /// <summary>
    /// 获得 Baidu AI AccessToken 方法
    /// </summary>
    /// <returns></returns>
    protected async Task<string> GetAccessToken()
    {
        var client = HttpClientFactory.CreateClient();
        var para = new List<KeyValuePair<string?, string?>>()
        {
            new("grant_type", "client_credentials"),
            new("client_id", Options.CurrentValue.ApiKey),
            new("client_secret", Options.CurrentValue.Secret)
        };

        var resp = await client.PostAsync("https://aip.baidubce.com/oauth/2.0/token", new FormUrlEncodedContent(para));
        var ret = await resp.Content.ReadAsStringAsync();
        var doc = JObject.Parse(ret);
        return doc.Value<string>("access_token") ?? string.Empty;
    }

    /// <summary>
    /// 增值税发票验真方法
    /// </summary>
    /// <param name="invoiceCode">发票代码</param>
    /// <param name="invoiceNum">发票号码</param>
    /// <param name="invoiceDate">开票日期格式 YYYYMMDD</param>
    /// <param name="invoiceType">发票种类 增值税专用发票：special_vat_invoice 增值税电子专用发票：elec_special_vat_invoice 增值税普通发票：normal_invoice 增值税普通发票（电子）：elec_normal_invoice 增值税普通发票（卷式）：roll_normal_invoice 通行费增值税电子普通发票：toll_elec_normal_invoice 区块链电子发票（目前仅支持深圳地区）：blockchain_invoice 全电发票（专用发票）：elec_invoice_special 全电发票（普通发票）：elec_invoice_normal 货运运输业增值税专用发票：special_freight_transport_invoice 机动车销售发票：motor_vehicle_invoice 二手车销售发票：used_vehicle_invoice</param>
    /// <param name="checkCode">校验码 后六位</param>
    /// <param name="totalAmount">发票金额</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<BaiduOcrResult<InvoiceVerifyResult>> VerifyInvoiceAsync(string invoiceCode, string invoiceNum, string invoiceDate, string invoiceType, string? checkCode, string? totalAmount, CancellationToken token = default) => Task.Run(async () =>
    {
        var token = await GetAccessToken();
        var url = $"https://aip.baidubce.com/rest/2.0/ocr/v1/vat_invoice_verification?access_token={token}";
        var client = HttpClientFactory.CreateClient();

        // 拼装参数
        var para = new List<KeyValuePair<string?, string?>>()
        {
            new("invoice_code", invoiceCode),
            new("invoice_num", invoiceNum),
            new("invoice_date", invoiceDate),
            new("invoice_type", invoiceType),
        };

        if (!string.IsNullOrEmpty(checkCode))
        {
            para.Add(new("check_code", checkCode));
        }
        if (!string.IsNullOrEmpty(totalAmount))
        {
            para.Add(new("total_amount", totalAmount));
        }

        // 提交数据
        var resp = await client.PostAsync(url, new FormUrlEncodedContent(para));
        var payload = await resp.Content.ReadAsStringAsync();
        var doc = JObject.Parse(payload);

        var ret = new BaiduOcrResult<InvoiceVerifyResult>();
        if (doc.TryGetValue("words_result", out var v))
        {
            ret.Entity = new InvoiceVerifyResult()
            {
                MachineCode = doc.Value<string>("MachineCode"),
                InvoiceNum = doc.Value<string>("InvoiceNum"),
                InvoiceDate = doc.Value<string>("InvoiceDate"),
                VerifyResult = doc.Value<string>("VerifyResult"),
                InvalidSign = doc.Value<string>("InvalidSign"),
                InvoiceCode = doc.Value<string>("InvoiceCode"),
                InvoiceType = doc.Value<string>("InvoiceType"),
                CheckCode = doc.Value<string>("CheckCode"),
                VerifyMessage = doc.Value<string>("VerifyMessage"),
                Invoice = v.ToObject(typeof(InvoiceEntity)) as InvoiceEntity
            };
        }
        else
        {
            ret.ErrorCode = doc.Value<int>("error_code");
            ret.ErrorMessage = doc.Value<string>("error_msg");
        }
        return ret;
    }, token);
}
