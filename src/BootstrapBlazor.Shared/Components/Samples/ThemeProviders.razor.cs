// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// ThemeProvider 示例文档
/// </summary>
public partial class ThemeProviders
{
    [Inject]
    [NotNull]
    private NavigationManager? Navigator { get; set; }

    private string? _videoFileName;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var url = Navigator.ToBaseRelativePath(Navigator.Uri);
        var comNameWithHash = url.Split('#').First();
        _videoFileName = comNameWithHash.Split('?').First();
    }
}
