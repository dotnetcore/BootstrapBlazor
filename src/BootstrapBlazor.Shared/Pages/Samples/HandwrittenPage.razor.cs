// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class HandwrittenPage
    {
        [NotNull]
        private string? Title { get; set; }

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
                Description = "保存按钮文本",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "保存"
            },
            new AttributeItem()
            {
                Name = "ClearButtonText",
                Description = "清除按钮文本",
                Type = "string",
                ValueList = " - ",
                DefaultValue = "清除"
            }, 
            new AttributeItem()
            {
                Name = "Result",
                Description = "手写签名imgBase64字符串",
                Type = "string",
                ValueList = " - ",
                DefaultValue = " - "
            },
            new AttributeItem()
            {
                Name = "HandwrittenBase64",
                Description = "手写结果回调方法",
                Type = "EventCallback<string>",
                ValueList = " - ",
                DefaultValue = " - "
            } 
        };

    }
}
