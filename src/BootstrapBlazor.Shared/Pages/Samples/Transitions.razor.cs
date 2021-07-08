// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
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
    public partial class Transitions
    {
        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        [Inject]
        private IStringLocalizer<Transitions>? Localizer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotNull]
        [Inject]
        public MessageService? MessageService { get; set; }

#nullable disable
        private Message MessageElement { get; set; }
#nullable restore

        private bool Show { get; set; }

        private void OnShow()
        {
            Show = !Show;
        }

        private bool TransitionedShow { get; set; }

        private bool CallBackShow { get; set; }

        private void OnCallBackShow()
        {
            TransitionedShow = !TransitionedShow;
        }

        private void Transitioned()
        {
            MessageElement.SetPlacement(Placement.Top);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息"
            });
        }

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
      {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Name",
                Description = "动画效果名称",
                Type = "Adimate",
                ValueList = " ",
                DefaultValue = " "
            },
             new AttributeItem() {
                Name = "Show",
                Description = "控制动画执行",
                Type = "Boolean",
                ValueList = "true|false",
                DefaultValue = ""
            },
             new AttributeItem() {
                Name = "Transitioned",
                Description = "动画执行完成回调",
                Type = "Action",
                ValueList = "",
                DefaultValue = ""
            }
      };
    }
}
