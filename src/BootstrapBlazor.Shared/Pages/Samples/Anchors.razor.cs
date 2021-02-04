// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            IntroText2 = new MarkupString(Localizer[nameof(IntroText2)] ?? "");
            ContentText1 = new MarkupString(Localizer[nameof(ContentText1)] ?? "");
        }


        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Target",
                Description = "锚点目标 Id",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Container",
                Description = "滚动条所在元素 Id",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Offset",
                Description = "偏移量用于调整间隙使用",
                Type = "int",
                ValueList = " — ",
                DefaultValue = "0"
            },
            new AttributeItem() {
                Name = "ChildContent",
                Description = "内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            }
        };
    }
}
