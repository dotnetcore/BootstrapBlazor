// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">两种运行模式共用的 OCR 示例服务。</para>
/// <para lang="en">The OCR sample service shared by both hosting modes.</para>
/// </summary>
public interface ISampleOcrService
{
    /// <summary><para lang="zh">发票图片最大字节数。</para><para lang="en">Maximum invoice image size in bytes.</para></summary>
    const int MaxImageSize = 4 * 1024 * 1024;

    /// <summary><para lang="zh">识别最多 4 MB 的发票图片。</para><para lang="en">Recognizes an invoice image up to 4 MB.</para></summary>
    Task<SampleOcrResult<SampleInvoice>> RecognizeAsync(byte[] image, CancellationToken cancellationToken = default);

    /// <summary><para lang="zh">验证发票。</para><para lang="en">Verifies an invoice.</para></summary>
    Task<SampleOcrResult<SampleInvoiceVerification>> VerifyAsync(InvoiceVerificationRequest request, CancellationToken cancellationToken = default);
}
