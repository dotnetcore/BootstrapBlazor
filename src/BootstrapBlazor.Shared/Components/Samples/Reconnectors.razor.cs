﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// Reconnectors 示例
/// </summary>
public partial class Reconnectors
{
    private string TemplateUrl => $"{WebsiteOption.CurrentValue.GiteeRepositoryUrl}/wikis/%E9%A1%B9%E7%9B%AE%E6%A8%A1%E6%9D%BF%E4%BD%BF%E7%94%A8%E6%95%99%E7%A8%8B";

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(Reconnector.ReconnectingTemplate),
            Description = Localizer["ReconnectingTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Reconnector.ReconnectFailedTemplate),
            Description = Localizer["ReconnectFailedTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Reconnector.ReconnectRejectedTemplate),
            Description = Localizer["ReconnectRejectedTemplateAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
    ];
}
