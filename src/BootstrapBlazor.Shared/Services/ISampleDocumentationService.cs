// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// Provides sample source code and component documentation in either interactive host.
/// </summary>
public interface ISampleDocumentationService
{
    /// <summary>
    /// Gets localized sample source code.
    /// </summary>
    Task<string> GetCodeAsync(string codeFile);

    /// <summary>
    /// Gets an unmodified source file for a tutorial download bundle.
    /// </summary>
    Task<string> GetFileContentAsync(string fileName);

    /// <summary>
    /// Gets component parameter documentation.
    /// </summary>
    Task<List<AttributeItem>> GetAttributesAsync(Type componentType);
}
