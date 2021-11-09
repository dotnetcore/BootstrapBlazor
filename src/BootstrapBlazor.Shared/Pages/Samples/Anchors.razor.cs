// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Anchors
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
        private MarkupString IntroText2 { get; set; }

        [NotNull]
        private MarkupString ContentText1 { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Anchors>? Localizer { get; set; }

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
            IntroText2 = new MarkupString(Localizer[nameof(IntroText2)]?.Value ?? "");
            ContentText1 = new MarkupString(Localizer[nameof(ContentText1)]?.Value ?? "");
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Target",
                Description = Localizer["Desc1"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Container",
                Description = Localizer["Desc2"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Offset",
                Description = Localizer["Desc3"],
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["Desc4"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
