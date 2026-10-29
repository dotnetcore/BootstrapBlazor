// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">编辑器及上传组件的附件上传服务，支持 Server 和 WebAssembly 模式。</para>
/// <para lang="en">Uploads attachments for editors and upload components in Server and WebAssembly modes.</para>
/// </summary>
public interface IEditorUploadService
{
    /// <summary>
    /// <para lang="zh">附件大小上限（字节）。</para>
    /// <para lang="en">Maximum size of an attachment, in bytes.</para>
    /// </summary>
    public const long MaxFileSize = 5 * 1024 * 1024;

    /// <summary>
    /// <para lang="zh">上传文件并更新读取进度，返回应用相对 URL。</para>
    /// <para lang="en">Uploads a file, updates its read progress and returns an application-relative URL.</para>
    /// </summary>
    /// <param name="file"><para lang="zh">上传文件。</para><para lang="en">The file to upload.</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌。</para><para lang="en">Cancellation token.</para></param>
    Task<string> UploadAsync(UploadFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">上传 Editor 图片并返回应用相对 URL。</para>
    /// <para lang="en">Uploads an Editor image and returns its application-relative URL.</para>
    /// </summary>
    /// <param name="file"><para lang="zh">图片上传参数。</para><para lang="en">Image upload arguments.</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌。</para><para lang="en">Cancellation token.</para></param>
    Task<string> UploadAsync(EditorUploadFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">上传 CherryMarkdown 附件并返回应用相对 URL。</para>
    /// <para lang="en">Uploads a CherryMarkdown attachment and returns its application-relative URL.</para>
    /// </summary>
    /// <param name="file"><para lang="zh">附件上传参数。</para><para lang="en">Attachment upload arguments.</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌。</para><para lang="en">Cancellation token.</para></param>
    Task<string> UploadAsync(CherryMarkdownUploadFile file, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">上传附件流并返回应用相对 URL。</para>
    /// <para lang="en">Uploads an attachment stream and returns its application-relative URL.</para>
    /// </summary>
    /// <param name="stream"><para lang="zh">附件内容流。</para><para lang="en">Attachment content stream.</para></param>
    /// <param name="fileName"><para lang="zh">原始文件名，用于校验扩展名。</para><para lang="en">Original file name used to validate the extension.</para></param>
    /// <param name="cancellationToken"><para lang="zh">取消令牌。</para><para lang="en">Cancellation token.</para></param>
    Task<string> UploadAsync(Stream stream, string fileName, CancellationToken cancellationToken = default);
}
