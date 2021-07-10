// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
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
    public partial class Transitions
    {
        [NotNull]
        [Inject]
        private IStringLocalizer<Transitions>? Localizer { get; set; }

        [NotNull]
        [Inject]
        private MessageService? MessageService { get; set; }

        [NotNull]
        private Message? MessageElement { get; set; }

        private bool Show { get; set; }

        private void OnShow()
        {
            Show = !Show;
        }

        private bool TransitionedShow { get; set; }

        private void OnCallBackShow()
        {
            TransitionedShow = !TransitionedShow;
        }

        private async Task Transitioned()
        {
            await MessageService.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息"
            });
        }

        private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "TransitionType",
                Description = "动画效果名称",
                Type = "TransitionType",
                ValueList = "FadeIn/FadeOut",
                DefaultValue = "FadeIn"
            },
             new AttributeItem() {
                Name = "Show",
                Description = "控制动画执行",
                Type = "Boolean",
                ValueList = "true|false",
                DefaultValue = "true"
            },
             new AttributeItem() {
                Name = "OnTransitionEnd",
                Description = "动画执行完成回调",
                Type = "Func<Task>",
                ValueList = " - ",
                DefaultValue = " - "
            }
      };
    }
}
