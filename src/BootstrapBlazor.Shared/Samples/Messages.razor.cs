// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Messages
{
    [NotNull]
    private Message? Message { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    public MessageService? MessageService { get; set; }

    private async Task ShowMessage()
    {
        Message.SetPlacement(Placement.Top);
        await MessageService.Show(new MessageOption()
        {
            Content = "这是一条提示消息"
        });
    }

    private async Task ShowIconMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "这是一条提示消息",
            Icon = "fa fa-info-circle"
        });
    }

    private async Task ShowCloseMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "这是一条提示消息",
            Icon = "fa fa-info-circle",
            ShowDismiss = true,
        });
    }

    private async Task ShowBarMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "这是一条提示消息",
            Icon = "fa fa-info-circle",
            ShowBar = true,
        });
    }

    private async Task ShowColorMessage(Color color)
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "这是带颜色的消息",
            Icon = "fa fa-info-circle",
            Color = color
        });
    }

    private async Task ShowBottomMessage()
    {
        await MessageService.Show(new MessageOption()
        {
            Content = "这是一条提示消息",
            Icon = "fa fa-info-circle",
        }, Message);
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
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
    private static IEnumerable<AttributeItem> GetMessageItemAttributes() => new AttributeItem[]
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
