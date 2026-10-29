// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// <para lang="zh">接收编辑器示例的附件上传请求。</para>
/// <para lang="en">Receives attachment uploads for the editor examples.</para>
/// </summary>
[ApiController]
public sealed class EditorUploadController(IEditorUploadService uploadService) : ControllerBase
{
    /// <summary>
    /// <para lang="zh">保存原始二进制请求体并返回附件的应用相对 URL。</para>
    /// <para lang="en">Saves the raw binary request body and returns the attachment's application-relative URL.</para>
    /// </summary>
    /// <param name="fileName"><para lang="zh">原始文件名，用于校验扩展名。</para><para lang="en">Original file name used to validate the extension.</para></param>
    /// <param name="cancellationToken"><para lang="zh">请求取消令牌。</para><para lang="en">Request cancellation token.</para></param>
    [HttpPost("api/editor-upload")]
    [Consumes("application/octet-stream")]
    [RequestSizeLimit(IEditorUploadService.MaxFileSize)]
    public async Task<IActionResult> Upload([FromQuery] string fileName, CancellationToken cancellationToken)
    {
        try
        {
            var url = await uploadService.UploadAsync(Request.Body, fileName, cancellationToken);
            return new JsonResult(url);
        }
        catch (InvalidDataException ex)
        {
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch (BadHttpRequestException ex) when (ex.StatusCode == StatusCodes.Status413PayloadTooLarge)
        {
            return Problem(detail: "Attachments must not exceed 5 MB.", statusCode: StatusCodes.Status413PayloadTooLarge);
        }
    }
}
