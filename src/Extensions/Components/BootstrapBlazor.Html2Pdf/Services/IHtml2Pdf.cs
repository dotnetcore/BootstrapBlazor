// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// Html export PDF service
/// </summary>
public interface IHtml2Pdf
{
    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="html">snippet html</param>
    /// <param name="fileName">the file name of pdf</param>
    /// <returns></returns>
    Task<bool> ExportAsync(string html, string? fileName = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="snippets">the collection of snippet html</param>
    /// <param name="fileName">the file name of pdf</param>
    /// <returns></returns>
    Task<bool> ExportAsync(List<string> snippets, string? fileName = null);

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="parameters">blazor component</param>
    /// <param name="fileName">the file name of pdf</param>
    /// <returns></returns>
    Task<bool> ExportAsync<TComponent>(IDictionary<string, object?>? parameters = null, string? fileName = null) where TComponent : IComponent;

    /// <summary>
    /// Export method
    /// </summary>
    /// <param name="componentType"></param>
    /// <param name="parameters">blazor component</param>
    /// <param name="fileName">the file name of pdf</param>
    /// <returns></returns>
    Task<bool> ExportAsync(Type componentType, IDictionary<string, object?>? parameters = null, string? fileName = null);
}
