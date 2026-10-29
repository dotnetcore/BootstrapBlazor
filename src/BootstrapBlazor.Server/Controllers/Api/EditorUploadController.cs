// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// Receives attachments for the editor examples.
/// </summary>
[ApiController]
public sealed class EditorUploadController(IEditorUploadService uploadService) : ControllerBase
{
    /// <summary>
    /// Stores an attachment and returns its application-relative URL.
    /// </summary>
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
