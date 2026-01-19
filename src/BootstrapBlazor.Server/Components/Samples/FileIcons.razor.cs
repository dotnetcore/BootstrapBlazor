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

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(FileIcon.Extension),
            Description = Localizer["ExtensionAttr"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(FileIcon.IconColor),
            Description = Localizer["IconColorAttr"].Value,
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = nameof(FileIcon.BackgroundTemplate),
            Description = Localizer["BackgroundTemplateAttr"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
