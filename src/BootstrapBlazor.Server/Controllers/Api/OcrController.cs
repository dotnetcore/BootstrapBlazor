// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// <para lang="zh">WebAssembly 发票识别及验证接口。</para>
/// <para lang="en">Invoice recognition and verification API for WebAssembly.</para>
/// </summary>
[ApiController]
public sealed class OcrController(ISampleOcrService ocr) : ControllerBase
{
    /// <summary><para lang="zh">识别最多 4 MB 的发票图片，不保存文件。</para><para lang="en">Recognizes an invoice image up to 4 MB without saving a file.</para></summary>
    [HttpPost("api/ocr/recognize")]
    [Consumes("application/octet-stream")]
    [RequestSizeLimit(ISampleOcrService.MaxImageSize)]
    public async Task<ActionResult<SampleOcrResult<SampleInvoice>>> Recognize(CancellationToken cancellationToken)
    {
        if (Request.ContentLength > ISampleOcrService.MaxImageSize)
        {
            return Problem(detail: "The image exceeds 4 MB.", statusCode: 413);
        }
        using var image = new MemoryStream();
        try
        {
            var buffer = new byte[81920];
            int read;
            while ((read = await Request.Body.ReadAsync(buffer, cancellationToken)) > 0)
            {
                if (image.Length + read > ISampleOcrService.MaxImageSize)
                {
                    return Problem(detail: "The image exceeds 4 MB.", statusCode: 413);
                }
                await image.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
            }
        }
        catch (BadHttpRequestException ex) when (ex.StatusCode == StatusCodes.Status413PayloadTooLarge)
        {
            return Problem(detail: "The image exceeds 4 MB.", statusCode: 413);
        }
        if (image.Length == 0)
        {
            return Problem(detail: "An invoice image is required.", statusCode: 400);
        }
        Response.Headers.CacheControl = "no-store";
        return await ocr.RecognizeAsync(image.ToArray(), cancellationToken);
    }

    /// <summary><para lang="zh">验证发票，保留服务商错误信息。</para><para lang="en">Verifies an invoice, preserving provider errors.</para></summary>
    [HttpPost("api/ocr/verify")]
    [Consumes("application/json")]
    [RequestSizeLimit(16 * 1024)]
    public async Task<ActionResult<SampleOcrResult<SampleInvoiceVerification>>> Verify(InvoiceVerificationRequest request, CancellationToken cancellationToken)
    {
        Response.Headers.CacheControl = "no-store";
        return await ocr.VerifyAsync(request, cancellationToken);
    }
}
