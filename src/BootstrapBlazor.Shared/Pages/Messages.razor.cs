// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Messages
    {
#nullable disable
        private Message MessageElement { get; set; }
#nullable restore

        /// <summary>
        /// 
        /// </summary>
        [Inject] public MessageService? MessageService { get; set; }

        private void ShowMessage()
        {
            MessageElement.SetPlacement(Placement.Top);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息"
            });
        }

        private void ShowIconMessage()
        {
            MessageElement.SetPlacement(Placement.Top);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息",
                Icon = "fa fa-info-circle"
            });
        }

        private void ShowCloseMessage()
        {
            MessageElement.SetPlacement(Placement.Top);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息",
                Icon = "fa fa-info-circle",
                ShowDismiss = true,
            });
        }

        private void ShowBarMessage()
        {
            MessageElement.SetPlacement(Placement.Top);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息",
                Icon = "fa fa-info-circle",
                ShowBar = true,
            });
        }

        private void ShowColorMessage(Color color)
        {
            MessageElement.SetPlacement(Placement.Top);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是带颜色的消息",
                Icon = "fa fa-info-circle",
                Color = color
            });
        }

        private void ShowBottomMessage()
        {
            MessageElement.SetPlacement(Placement.Bottom);
            MessageService?.Show(new MessageOption()
            {
                Host = MessageElement,
                Content = "这是一条提示消息",
                Icon = "fa fa-info-circle",
            });
        }

        private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Placement",
                Description = "消息弹出位置",
                Type = "Placement",
                ValueList = "Top|Bottom",
                DefaultValue = "Top"
            }
        };

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private IEnumerable<AttributeItem> GetMessageItemAttributes() => new AttributeItem[]
        {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ChildContent",
                Description = "内容",
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Class",
                Description = "样式",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Color",
                Description = "颜色",
                Type = "Color",
                ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "Icon",
                Description = "图标",
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowDismiss",
                Description = "关闭按钮",
                Type = "bool",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "ShowBar",
                Description = "是否显示左侧 Bar",
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            }
        };
    }
}
