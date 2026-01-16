// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MouseFollowers
/// </summary>
public partial class MouseFollowers
{
    [Inject]
    [NotNull]
    private IStringLocalizer<MouseFollowers>? Localizer { get; set; }

    [NotNull]
    private static string NugetPackageName => "BootstrapBlazor.MouseFollower";

    private readonly MouseFollowerOptions FollowerOptions = new() { ClassName = "mf-cursor bb-cursor" };

    private readonly MouseFollowerOptions FollowerImageOptions = new MouseFollowerOptions()
    {
        ClassName = "mf-cursor bb-cursor",
        MediaClassName = "mf-cursor-media bb-cursor-media"
    };
}
