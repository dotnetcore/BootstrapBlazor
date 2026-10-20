// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Data;

/// <summary>
/// Public layout configuration shared with the WebAssembly host.
/// </summary>
public sealed class LayoutSettings
{
    /// <summary>
    /// Gets or sets the public asset root path.
    /// </summary>
    public string AssetRootPath { get; set; } = "/";

    /// <summary>
    /// Gets or sets the public website link.
    /// </summary>
    public string ServerUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the administration demo link.
    /// </summary>
    public string AdminUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the administration repository link.
    /// </summary>
    public string BootstrapAdminLink { get; set; } = "";

    /// <summary>
    /// Gets or sets the available UI cultures.
    /// </summary>
    public List<string> SupportedCultures { get; set; } = [];

    /// <summary>
    /// Gets or sets the GitHub repository link.
    /// </summary>
    public string GithubRepositoryUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the Gitee repository link.
    /// </summary>
    public string GiteeRepositoryUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the release link.
    /// </summary>
    public string WikiUrl { get; set; } = "";

    /// <summary>
    /// Gets or sets the available themes.
    /// </summary>
    public HashSet<ThemeOption> Themes { get; set; } = [];

    /// <summary>
    /// Gets or sets the public search index name.
    /// </summary>
    public string? SearchIndex { get; set; }
}
