// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Shared.Shared;

/// <summary>
/// 
/// </summary>
public sealed partial class ComponentLayout
{
    [NotNull]
    private string? VideoFileName { get; set; }

    [NotNull]
    private string? Title { get; set; }

    [NotNull]
    private string? Video { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<ComponentLayout>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? Navigator { get; set; }

    private string GVPUrl => $"{WebsiteOption.CurrentValue.BootstrapBlazorLink}/badge/star.svg?theme=gvp";

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];
        Video ??= Localizer[nameof(Video)];
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        var page = Navigator.ToBaseRelativePath(Navigator.Uri);
        var comNameWithHash = page.Split("/").LastOrDefault() ?? string.Empty;
        var comName = comNameWithHash.Split("#").FirstOrDefault() ?? string.Empty;
        VideoFileName = comName;
    }
}
