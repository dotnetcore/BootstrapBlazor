// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Reconnectors 示例
/// </summary>
public partial class Reconnectors
{
    private string TemplateUrl => $"{WebsiteOption.CurrentValue.BootstrapBlazorLink}/wikis/%E9%A1%B9%E7%9B%AE%E6%A8%A1%E6%9D%BF%E4%BD%BF%E7%94%A8%E6%95%99%E7%A8%8B";

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
    };
}
