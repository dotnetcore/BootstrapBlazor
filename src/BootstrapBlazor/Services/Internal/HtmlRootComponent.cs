﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Microsoft.AspNetCore.Components.HtmlRendering.Infrastructure;
using System.IO;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Web.HtmlRendering;

#pragma warning disable BL0006 // Do not use RenderTree types

/// <summary>
/// Represents the output of rendering a root component as HTML. The content can change if the component instance re-renders.
/// </summary>
[ExcludeFromCodeCoverage]
readonly struct HtmlRootComponent
{
    private readonly StaticHtmlRenderer? _renderer;

    internal HtmlRootComponent(StaticHtmlRenderer renderer, int componentId, Task quiescenceTask)
    {
        _renderer = renderer;
        ComponentId = componentId;
        QuiescenceTask = quiescenceTask;
    }

    /// <summary>
    /// Gets the component ID.
    /// </summary>
    public int ComponentId { get; }

    /// <summary>
    /// Gets a <see cref="Task"/> that completes when the component hierarchy has completed asynchronous tasks such as loading.
    /// </summary>
    public Task QuiescenceTask { get; } = Task.CompletedTask;

    /// <summary>
    /// Returns an HTML string representation of the component's latest output.
    /// </summary>
    /// <returns>An HTML string representation of the component's latest output.</returns>
    public string ToHtmlString()
    {
        if (_renderer is null)
        {
            return string.Empty;
        }

        using var writer = new StringWriter();
        WriteHtmlTo(writer);
        return writer.ToString();
    }

    /// <summary>
    /// Writes the component's latest output as HTML to the specified writer.
    /// </summary>
    /// <param name="output">The output destination.</param>
    public void WriteHtmlTo(TextWriter output)
        => _renderer?.WriteComponentHtml(ComponentId, output);
}

#pragma warning restore BL0006 // Do not use RenderTree types
