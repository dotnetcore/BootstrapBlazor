// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 图标库
/// </summary>
public partial class FileIcons
{
    [Inject]
    [NotNull]
    private IStringLocalizer<FileIcons>? Localizer { get; set; }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
    };
}
