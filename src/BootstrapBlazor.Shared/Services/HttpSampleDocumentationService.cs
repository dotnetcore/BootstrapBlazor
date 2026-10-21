// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// Loads documentation from the server for WebAssembly samples.
/// </summary>
public sealed class HttpSampleDocumentationService(HttpClient client) : ISampleDocumentationService
{
    /// <inheritdoc />
    public Task<string> GetCodeAsync(string codeFile) =>
        client.GetStringAsync($"api/sample-documentation/code?codeFile={Uri.EscapeDataString(codeFile)}&culture={CultureInfo.CurrentUICulture.Name}");

    /// <inheritdoc />
    public Task<string> GetFileContentAsync(string fileName) =>
        client.GetStringAsync($"api/sample-documentation/source?fileName={Uri.EscapeDataString(fileName)}");

    /// <inheritdoc />
    public async Task<List<AttributeItem>> GetAttributesAsync(Type componentType) =>
        await client.GetFromJsonAsync<List<AttributeItem>>($"api/sample-documentation/attributes?typeName={Uri.EscapeDataString(componentType.FullName!)}&culture={CultureInfo.CurrentUICulture.Name}")
        ?? throw new InvalidOperationException("Component documentation is unavailable.");
}
