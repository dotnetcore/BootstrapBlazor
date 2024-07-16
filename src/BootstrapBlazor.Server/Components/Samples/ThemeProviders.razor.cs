// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

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
