// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">通过同源后台接口识别及验证发票。</para>
/// <para lang="en">Recognizes and verifies invoices through the same-origin API.</para>
/// </summary>
public sealed class HttpSampleOcrService(HttpClient client) : ISampleOcrService
{
    /// <inheritdoc />
    public async Task<SampleOcrResult<SampleInvoice>> RecognizeAsync(byte[] image, CancellationToken cancellationToken = default)
    {
        using var content = new ByteArrayContent(image);
        content.Headers.ContentType = new("application/octet-stream");
        using var response = await client.PostAsync("api/ocr/recognize", content, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SampleOcrResult<SampleInvoice>>(cancellationToken)
            ?? throw new InvalidOperationException("The server did not return OCR results.");
    }

    /// <inheritdoc />
    public async Task<SampleOcrResult<SampleInvoiceVerification>> VerifyAsync(InvoiceVerificationRequest request, CancellationToken cancellationToken = default)
    {
        using var response = await client.PostAsJsonAsync("api/ocr/verify", request, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SampleOcrResult<SampleInvoiceVerification>>(cancellationToken)
            ?? throw new InvalidOperationException("The server did not return verification results.");
    }
}
