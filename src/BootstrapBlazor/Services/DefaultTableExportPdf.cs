// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

[ExcludeFromCodeCoverage]
class DefaultTableExportPdf : ITableExportPdf
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<byte[]> PdfDataAsync(string content) => Task.FromResult(Array.Empty<byte>());

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<Stream> PdfStreamAsync(string content) => Task.FromResult(Stream.Null);
}
