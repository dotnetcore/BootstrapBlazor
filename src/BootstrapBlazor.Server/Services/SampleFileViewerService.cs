// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using ce.office.extension;
using NPOI.Util;
using System.Globalization;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// <para lang="zh">使用 FileViewer 原有的 Word/Excel 转换器处理站点示例文件。</para>
/// <para lang="en">Processes site sample files with FileViewer's existing Word/Excel converters.</para>
/// </summary>
public sealed class SampleFileViewerService(IWebHostEnvironment environment, ISampleDocumentationService documentation) : ISampleFileViewerService
{
    private static readonly SemaphoreSlim _conversionLock = new(1, 1);

    /// <inheritdoc />
    public async Task<string> PreviewAsync(string fileName, CancellationToken cancellationToken = default)
    {
        if (fileName is not ("sample.xlsx" or "sample.docx"))
        {
            throw new ArgumentException("Only site sample files are supported.", nameof(fileName));
        }
        await _conversionLock.WaitAsync(cancellationToken);
        try
        {
            var file = Path.Combine(environment.WebRootPath, "samples", fileName);
            var html = fileName == "sample.xlsx" ? ConvertExcel(file) : WordHelper.ToHtml(file);
            cancellationToken.ThrowIfCancellationRequested();
            return html;
        }
        finally
        {
            _conversionLock.Release();
        }
    }

    private static string ConvertExcel(string file)
    {
        var locale = LocaleUtil.GetUserLocale();
        var timeZone = LocaleUtil.GetUserTimeZone();
        try
        {
            // <para lang="zh">旧版 NPOI 的线程本地默认值可能为空；在转换线程显式初始化。</para>
            // <para lang="en">Older NPOI thread-local defaults can be null; initialize them on the conversion thread.</para>
            LocaleUtil.SetUserLocale(CultureInfo.CurrentCulture);
#pragma warning disable CS0618 // The existing converter's NPOI version requires System.TimeZone.
            LocaleUtil.SetUserTimeZone(timeZone ?? TimeZone.CurrentTimeZone);
#pragma warning restore CS0618
            return ExcelHelper.ToHtml(file);
        }
        finally
        {
            LocaleUtil.SetUserLocale(locale);
            LocaleUtil.SetUserTimeZone(timeZone);
        }
    }

    /// <inheritdoc />
    public Task<List<AttributeItem>> GetAttributesAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return documentation.GetAttributesAsync(typeof(FileViewer));
    }
}
