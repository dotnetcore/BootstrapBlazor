// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 图标库
/// </summary>
public partial class FileIcons
{
    [Inject]
    [NotNull]
    private IStringLocalizer<FileIcons>? Localizer { get; set; }
}
