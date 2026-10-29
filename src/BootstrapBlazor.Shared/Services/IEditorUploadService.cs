// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// Stores editor attachments on the server in either interactive hosting mode.
/// </summary>
public interface IEditorUploadService
{
    /// <summary>
    /// Maximum size of an editor attachment, in bytes.
    /// </summary>
    public const long MaxFileSize = 5 * 1024 * 1024;

    /// <summary>
    /// Uploads an Editor image and returns its application-relative URL.
    /// </summary>
    Task<string> UploadAsync(EditorUploadFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads a CherryMarkdown attachment and returns its application-relative URL.
    /// </summary>
    Task<string> UploadAsync(CherryMarkdownUploadFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// Uploads an attachment and returns its application-relative URL.
    /// </summary>
    Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default);
}
