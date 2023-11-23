// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 百度文字识别示例
/// </summary>
public partial class BaiduOcr : IDisposable
{
    private InvoiceEntity? Invoice { get; set; }

    /*以下示例为本站特殊处理逻辑可不参考*/
    private bool IsLoading { get; set; }

    private string ButtonIcon { get; } = "fa-solid fa-cloud-arrow-up";

    private string LoadingIcon { get; } = "fa-solid fa-spinner fa-spin-pulse";

    private string Icon => IsLoading ? LoadingIcon : ButtonIcon;

    /// <summary>
    /// 取消请求令牌
    /// </summary>
    private CancellationTokenSource? TokenSource { get; set; }

    private async Task OnClickToUploadBlock(UploadFile file)
    {
        if (file.File != null)
        {
            // 设置 按钮禁用
            IsLoading = true;
            StateHasChanged();

            // 获得上传文件
            var payload = await file.GetBytesAsync(file.File.Size);
            if (payload?.Any() ?? false)
            {
                try
                {
                    TokenSource ??= new();
                    var result = await OcrService.CheckVatInvoiceAsync(payload, TokenSource.Token);
                    Invoice = result.Entity;
                    if (result.Entity != null)
                    {
                        await ToastService.Success("Vat Invoice", "VAT Invoice success!");
                    }
                    else
                    {
                        await ToastService.Information("Vat Invoice", $"{result.ErrorCode}: {result.ErrorMessage}");
                    }
                }
                catch (TaskCanceledException)
                {

                }
            }

            IsLoading = false;
            StateHasChanged();
        }
    }

    private InvoiceForm Model { get; set; } = new() { InvoiceType = "elec_special_vat_invoice", TotalAmount = "0" };

    private async Task Verify(EditContext context)
    {
        var result = await OcrService.VerifyInvoiceAsync(Model.InvoiceCode, Model.InvoiceNum, Model.InvoiceDate, Model.InvoiceType, Model.CheckCode, Model.TotalAmount);
        if (result.ErrorCode != 0)
        {
            await ToastService.Information("Verify Vat", $"{result.ErrorCode}: {result.ErrorMessage}");
        }
        else if (result.Entity != null)
        {
            await ToastService.Success("Verify Vat", result.Entity?.VerifyMessage);
        }
    }

    private class InvoiceForm
    {
        [Required(ErrorMessage = "发票代码不能为空")]
        [NotNull]
        public string? InvoiceCode { get; set; }

        [Required(ErrorMessage = "发票号码不能为空")]
        [NotNull]
        public string? InvoiceNum { get; set; }

        [Required(ErrorMessage = "开票日期不能为空")]
        [NotNull]
        public string? InvoiceDate { get; set; }

        [NotNull]
        public string? InvoiceType { get; set; }

        [Required(ErrorMessage = "校验码不能为空")]
        [NotNull]
        public string? CheckCode { get; set; }

        [NotNull]
        public string? TotalAmount { get; set; }
    }

    /// <summary>
    /// 关闭网页时调用
    /// </summary>
    /// <param name="disposing"></param>
    /// <returns></returns>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing && TokenSource != null)
        {
            TokenSource.Cancel();
            TokenSource.Dispose();
        }
    }

    /// <summary>
    /// 关闭网页时调用
    /// </summary>
    /// <returns></returns>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
