// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">百度文字识别示例</para>
/// <para lang="en">Baidu OCR sample.</para>
/// </summary>
public partial class BaiduOcr : IDisposable
{
    [Inject, NotNull]
    private ISampleOcrService? OcrService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<BaiduOcr>? Localizer { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    [Inject, NotNull]
    private ILogger<BaiduOcr>? Logger { get; set; }

    private SampleInvoice? Invoice { get; set; }

    /*以下示例为本站特殊处理逻辑可不参考*/
    private bool _isLoading;

    private string Icon => _isLoading ? "fa-solid fa-spinner fa-spin-pulse" : "fa-solid fa-cloud-arrow-up";

    /// <summary>
    /// 取消请求令牌
    /// </summary>
    [NotNull]
    private CancellationTokenSource? TokenSource { get; set; } = new();

    private async Task OnClickToUploadBlock(UploadFile file)
    {
        if (file.File != null && !_isLoading)
        {
            if (file.File.Size is <= 0 or > ISampleOcrService.MaxImageSize)
            {
                await ToastService.Information(Localizer["VatInvoiceToastTitle"], Localizer["ImageSizeError"]);
                return;
            }
            // 设置 按钮禁用
            _isLoading = true;
            Invoice = null;
            StateHasChanged();

            try
            {
                // 获得上传文件
                // Read the browser file with a fixed limit in both hosting modes.
                await using var stream = file.File.OpenReadStream(ISampleOcrService.MaxImageSize, TokenSource.Token);
                using var buffer = new MemoryStream();
                await stream.CopyToAsync(buffer, TokenSource.Token);
                var result = await OcrService.RecognizeAsync(buffer.ToArray(), TokenSource.Token);
                Invoice = result.Entity;
                if (result.ErrorCode == 0 && result.Entity != null)
                {
                    await ToastService.Success(Localizer["VatInvoiceToastTitle"], Localizer["VatInvoiceToastSuccess"]);
                }
                else
                {
                    await ToastService.Information(Localizer["VatInvoiceToastTitle"],
                        result.ErrorMessage == null ? Localizer["UnknownErrorText"] : $"{result.ErrorCode}: {result.ErrorMessage}");
                }
            }
            catch (OperationCanceledException) when (TokenSource.IsCancellationRequested)
            {
                Logger.LogDebug("OCR sample request was cancelled on disposal.");
            }
            catch (Exception ex) when (ex is HttpRequestException or IOException or InvalidOperationException or OperationCanceledException)
            {
                Logger.LogError(ex, "OCR sample recognition failed.");
                await ToastService.Error(Localizer["VatInvoiceToastTitle"], Localizer["RequestFailed"]);
            }
            finally
            {
                _isLoading = false;
                if (!TokenSource.IsCancellationRequested)
                {
                    StateHasChanged();
                }
            }
        }
    }

    private InvoiceForm Model { get; set; } = new() { InvoiceType = "elec_special_vat_invoice", TotalAmount = "0" };

    private async Task Verify(EditContext context)
    {
        try
        {
            var result = await OcrService.VerifyAsync(new InvoiceVerificationRequest
            {
                InvoiceCode = Model.InvoiceCode,
                InvoiceNum = Model.InvoiceNum,
                InvoiceDate = Model.InvoiceDate,
                InvoiceType = Model.InvoiceType,
                CheckCode = Model.CheckCode,
                TotalAmount = Model.TotalAmount
            }, TokenSource.Token);
            if (result.ErrorCode != 0 || result.Entity == null)
            {
                await ToastService.Information(Localizer["VerifyVatToastTitle"],
                    result.ErrorMessage == null ? Localizer["UnknownErrorText"] : $"{result.ErrorCode}: {result.ErrorMessage}");
            }
            else if (result.Entity.Valid)
            {
                await ToastService.Success(Localizer["VerifyVatToastTitle"], result.Entity.VerifyMessage ?? Localizer["UnknownErrorText"]);
            }
            else
            {
                await ToastService.Information(Localizer["VerifyVatToastTitle"], result.Entity.VerifyMessage ?? Localizer["UnknownErrorText"]);
            }
        }
        catch (OperationCanceledException) when (TokenSource.IsCancellationRequested)
        {
            Logger.LogDebug("OCR sample verification was cancelled on disposal.");
        }
        catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or OperationCanceledException)
        {
            Logger.LogError(ex, "OCR sample verification failed.");
            await ToastService.Error(Localizer["VerifyVatToastTitle"], Localizer["RequestFailed"]);
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
