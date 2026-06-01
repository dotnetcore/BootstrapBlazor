// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 百度文字识别示例
/// </summary>
public partial class BaiduOcr : IDisposable
{
    [Inject, NotNull]
    private IBaiduOcr? OcrService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<BaiduOcr>? Localizer { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private InvoiceEntity? Invoice { get; set; }

    /*以下示例为本站特殊处理逻辑可不参考*/
    private bool _isLoading;

    private string Icon => _isLoading ? "fa-solid fa-spinner fa-spin-pulse" : "fa-solid fa-cloud-arrow-up";

    /// <summary>
    /// 取消请求令牌
    /// </summary>
    private CancellationTokenSource? TokenSource { get; set; }

    private async Task OnClickToUploadBlock(UploadFile file)
    {
        if (file.File != null)
        {
            // 设置 按钮禁用
            _isLoading = true;
            StateHasChanged();

            // 获得上传文件
            var payload = await file.GetBytesAsync(file.File.Size);
            if (payload is { Length: > 0 })
            {
                try
                {
                    TokenSource ??= new();
                    var result = await OcrService.CheckVatInvoiceAsync(payload, TokenSource.Token);
                    Invoice = result.Entity;
                    if (result.Entity != null)
                    {
                        await ToastService.Success(Localizer["VatInvoiceToastTitle"], Localizer["VatInvoiceToastSuccess"]);
                    }
                    else
                    {
                        await ToastService.Information(Localizer["VatInvoiceToastTitle"], $"{result.ErrorCode}: {result.ErrorMessage}");
                    }
                }
                catch (TaskCanceledException)
                {

                }
            }

            _isLoading = false;
            StateHasChanged();
        }
    }

    private InvoiceForm Model { get; set; } = new() { InvoiceType = "elec_special_vat_invoice", TotalAmount = "0" };

    private async Task Verify(EditContext context)
    {
        var result = await OcrService.VerifyInvoiceAsync(Model.InvoiceCode, Model.InvoiceNum, Model.InvoiceDate, Model.InvoiceType, Model.CheckCode, Model.TotalAmount);
        if (result.ErrorCode != 0)
        {
            await ToastService.Information(Localizer["VerifyVatToastTitle"], $"{result.ErrorCode}: {result.ErrorMessage}");
        }
        else if (result.Entity != null)
        {
            await ToastService.Success(Localizer["VerifyVatToastTitle"], result.Entity?.VerifyMessage ?? Localizer["UnknownErrorText"]);
        }
    }

    private class InvoiceForm : IValidatableObject
    {
        [NotNull]
        public string? InvoiceCode { get; set; }

        [NotNull]
        public string? InvoiceNum { get; set; }

        [NotNull]
        public string? InvoiceDate { get; set; }

        [NotNull]
        public string? InvoiceType { get; set; }

        [NotNull]
        public string? CheckCode { get; set; }

        [NotNull]
        public string? TotalAmount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var localizer = validationContext.GetRequiredService<IStringLocalizer<BaiduOcr>>();

            if (string.IsNullOrWhiteSpace(InvoiceCode))
            {
                yield return new ValidationResult(localizer["InvoiceCodeRequiredError"], [nameof(InvoiceCode)]);
            }

            if (string.IsNullOrWhiteSpace(InvoiceNum))
            {
                yield return new ValidationResult(localizer["InvoiceNumRequiredError"], [nameof(InvoiceNum)]);
            }

            if (string.IsNullOrWhiteSpace(InvoiceDate))
            {
                yield return new ValidationResult(localizer["InvoiceDateRequiredError"], [nameof(InvoiceDate)]);
            }

            if (string.IsNullOrWhiteSpace(CheckCode))
            {
                yield return new ValidationResult(localizer["CheckCodeRequiredError"], [nameof(CheckCode)]);
            }
        }
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
