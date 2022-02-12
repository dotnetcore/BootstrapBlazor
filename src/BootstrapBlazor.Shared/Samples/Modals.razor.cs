// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Modals
{
    [NotNull]
    private Modal? Modal { get; set; }

    [NotNull]
    private Modal? BackdropModal { get; set; }

    [NotNull]
    private Modal? SmailModal { get; set; }

    [NotNull]
    private Modal? LargeModal { get; set; }

    [NotNull]
    private Modal? ExtraLargeModal { get; set; }

    [NotNull]
    private Modal? SmailFullScreenModal { get; set; }

    [NotNull]
    private Modal? LargeFullScreenModal { get; set; }

    [NotNull]
    private Modal? ExtraLargeFullScreenModal { get; set; }

    [NotNull]
    private Modal? CenterModal { get; set; }

    [NotNull]
    private Modal? LongContentModal { get; set; }

    [NotNull]
    private Modal? ScrollModal { get; set; }

    [NotNull]
    private Modal? DragModal { get; set; }

    [NotNull]
    private Modal? MaximizeModal { get; set; }

    private bool IsKeyboard { get; set; }

    private void OnClickKeyboard()
    {
        IsKeyboard = !IsKeyboard;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "HeaderTemplate",
            Description = "模态主体 ModalHeader 模板",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "BodyTemplate",
            Description = "模态主体 ModalBody 组件",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ChildContent",
            Description = "内容",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "FooterTemplate",
            Description = "模态底部 ModalFooter 组件",
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsBackdrop",
            Description = "是否后台关闭弹窗",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsKeyboard",
            Description = "是否响应 ESC 键盘",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsCentered",
            Description = "是否垂直居中",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsScrolling",
            Description = "是否弹窗正文超长时滚动",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsFade",
            Description = "是否开启淡入淡出动画效果",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsDraggable",
            Description = "是否开启可拖拽效果",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowCloseButton",
            Description = "是否显示关闭按钮",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "ShowFooter",
            Description = "是否显示 Footer",
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Size",
            Description = "尺寸",
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "Large"
        },
        new AttributeItem() {
            Name = nameof(ModalDialog.FullScreenSize),
            Description = "小于特定尺寸时全屏",
            Type = "Size",
            ValueList = "None / Always / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new AttributeItem() {
            Name = "Title",
            Description = "弹窗标题",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " 未设置 "
        },
        new AttributeItem() {
            Name = nameof(ModalDialog.ShowMaximizeButton),
            Description = "是否显示弹窗最大化按钮",
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    };
}
