// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// <para lang="zh">为浏览器提供文件预览，不接受任意路径或远程 URL。</para>
/// <para lang="en">Provides browser file previews without accepting arbitrary paths or remote URLs.</para>
/// </summary>
[ApiController]
public sealed class FileViewerController(ISampleFileViewerService viewer) : ControllerBase
{
    /// <summary>
    /// <para lang="zh">返回转换后的 HTML 文本，供沙箱框架展示。</para>
    /// <para lang="en">Returns converted HTML text for display in a sandboxed frame.</para>
    /// </summary>
    [HttpGet("api/file-viewer/preview")]
    public async Task<IActionResult> Preview(string fileName, CancellationToken cancellationToken)
    {
        if (fileName is not ("sample.xlsx" or "sample.docx"))
        {
            return Problem(detail: "Only site sample files are supported.", statusCode: 400);
        }
        Response.Headers.CacheControl = "no-store";
        Response.Headers["X-Content-Type-Options"] = "nosniff";
        return Content(await viewer.PreviewAsync(fileName, cancellationToken), "text/plain");
    }

    /// <summary>
    /// <para lang="zh">返回本地化组件参数文档。</para>
    /// <para lang="en">Returns localized component parameter documentation.</para>
    /// </summary>
    [HttpGet("api/file-viewer/attributes")]
    public Task<List<AttributeItem>> Attributes(CancellationToken cancellationToken) =>
        viewer.GetAttributesAsync(cancellationToken);
}
