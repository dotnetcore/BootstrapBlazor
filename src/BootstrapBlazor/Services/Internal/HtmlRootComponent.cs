// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if !NET8_0_OR_GREATER
using Microsoft.AspNetCore.Components.HtmlRendering.Infrastructure;

namespace Microsoft.AspNetCore.Components.Web.HtmlRendering;

#pragma warning disable BL0006 // Do not use RenderTree types

/// <summary>
/// <para lang="zh">Represents the output of rendering a root component as HTML. 内容 can change if the component 实例 re-renders.
///</para>
/// <para lang="en">Represents the output of rendering a root component as HTML. The content can change if the component instance re-renders.
///</para>
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
    /// <para lang="zh">获得 the component ID.
    ///</para>
    /// <para lang="en">Gets the component ID.
    ///</para>
    /// </summary>
    public int ComponentId { get; }

    /// <summary>
    /// <para lang="zh">获得 a <see cref="Task"/> that completes when the component hierarchy has completed asynchronous tasks such as loading.
    ///</para>
    /// <para lang="en">Gets a <see cref="Task"/> that completes when the component hierarchy has completed asynchronous tasks such as loading.
    ///</para>
    /// </summary>
    public Task QuiescenceTask { get; } = Task.CompletedTask;

    /// <summary>
    /// <para lang="zh">Returns an HTML string representation of the component's latest output.
    ///</para>
    /// <para lang="en">Returns an HTML string representation of the component's latest output.
    ///</para>
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
    /// <para lang="zh">Writes the component's latest output as HTML to the specified writer.
    ///</para>
    /// <para lang="en">Writes the component's latest output as HTML to the specified writer.
    ///</para>
    /// </summary>
    /// <param name="output">The output destination.</param>
    public void WriteHtmlTo(TextWriter output)
        => _renderer?.WriteComponentHtml(ComponentId, output);
}

#pragma warning restore BL0006 // Do not use RenderTree types
#endif
