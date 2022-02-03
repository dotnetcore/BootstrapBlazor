// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class HandwrittenPage
{
    [NotNull]
    private string? Title { get; set; }

    [NotNull]
    private string? SubTitle { get; set; }

    [NotNull]
    private string? BaseUsageText { get; set; }

    [NotNull]
    private string? IntroText1 { get; set; }

    [NotNull]
    private string? IntroText2 { get; set; }

    [NotNull]
    private string? HandwrittenButtonText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<HandwrittenPage>? Localizer { get; set; }


    /// <summary>
    /// 签名Base64
    /// </summary>
    public string? DrawBase64 { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Title ??= Localizer[nameof(Title)];
        SubTitle ??= Localizer[nameof(SubTitle)];
        BaseUsageText ??= Localizer[nameof(BaseUsageText)];
        IntroText1 ??= Localizer[nameof(IntroText1)];
        IntroText2 ??= Localizer[nameof(IntroText2)];
        HandwrittenButtonText ??= Localizer[nameof(HandwrittenButtonText)];
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem()
            {
                Name = "SaveButtonText",
                Description = Localizer["SaveButtonText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["SaveButtonTextDefaultValue"]
            },
            new AttributeItem()
            {
                Name = "ClearButtonText",
                Description = Localizer["ClearButtonText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["ClearButtonTextDefaultValue"]
            },
            new AttributeItem()
            {
                Name = "Result",
                Description = Localizer["Result"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "HandwrittenBase64",
                Description = Localizer["HandwrittenBase64"],
                Type = "EventCallback<string>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
