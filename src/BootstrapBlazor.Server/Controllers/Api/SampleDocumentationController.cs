// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// Serves documentation without exposing server file paths to WebAssembly.
/// </summary>
[ApiController]
public class SampleDocumentationController(ISampleDocumentationService documentation, IOptions<WebsiteOptions> options) : ControllerBase
{
    /// <summary>
    /// Gets localized source for a configured sample.
    /// </summary>
    [HttpGet("api/sample-documentation/code")]
    public async Task<IActionResult> Code(string codeFile)
    {
        var key = codeFile.Split('.')[0];
        if ((codeFile != $"{key}.razor" && codeFile != $"{key}.razor.cs")
            || !options.Value.SourceCodes.ContainsKey(key.ToLowerInvariant()))
        {
            return NotFound();
        }

        return Content(await documentation.GetCodeAsync(codeFile), "text/plain");
    }

    /// <summary>
    /// Gets localized parameter documentation for a library component or supported sample options.
    /// </summary>
    [HttpGet("api/sample-documentation/attributes")]
    public async Task<IActionResult> Attributes(string typeName)
    {
        var type = typeof(BootstrapBlazorRoot).Assembly.GetType(typeName);
        if (type == null || !type.IsPublic
            || (!typeof(IComponent).IsAssignableFrom(type)
                && type != typeof(CountUpOption)
                && type != typeof(NotificationItem)
                && type != typeof(MessageOption)
                && type != typeof(ConsoleMessageItem)
                && type != typeof(TimelineItem)))
        {
            return NotFound();
        }

        return Ok(await documentation.GetAttributesAsync(type));
    }
}
