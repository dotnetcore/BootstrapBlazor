// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class ComplexDialogs
    {

        private Logger? Trace { get; set; }

        private string? InputValue { get; set; }


        /// <summary>
        ///
        /// </summary>
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }


        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private async Task OnClick()
        {
            var result = await DialogService.ShowDialog(new ComplexDialogOption<ComplexDialogDemo>() {Title = "我是服务创建的弹出框",});
            Trace?.Log($"DialogResult:{result.dialogResult}, 获取到的值为{(result.component == null ? "NULL" : result.component.SelectedValue)}");
        }

        private async Task CardButtonClick()
        {
            var result = await DialogService.ShowDialog(new ComplexDialogOption<ComplexDialogDemo2>()
            {
                Title = "选择收件人", BodyContext = 10, YesButtonText = "选择", YesButtonIcon = "fa fa-search"
            });
            if (result.dialogResult == DialogResult.Yes && result.component != null && result.component.SelectedEmails != null)
            {
                InputValue = string.Join(";", result.component.SelectedEmails);
            }
        }

        /// <summary>
        /// 获得属性方法
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<AttributeItem> GetAttributes()
        {
            return new AttributeItem[]
            {
                new AttributeItem() {
                    Name = "BodyContext",
                    Description = "弹窗传参",
                    Type = "object",
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
                    Name = "KeepChildrenState",
                    Description = "是否保持弹窗内组件状态",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "IsCentered",
                    Description = "是否垂直居中",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "true"
                },
                new AttributeItem() {
                    Name = "IsScrolling",
                    Description = "是否弹窗正文超长时滚动",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "ShowFooter",
                    Description = "是否显示 Footer",
                    Type = "boolean",
                    ValueList = "true|false",
                    DefaultValue = "false"
                },
                new AttributeItem() {
                    Name = "Size",
                    Description = "尺寸",
                    Type = "Size",
                    ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                    DefaultValue = "Large"
                },
                new AttributeItem() {
                    Name = "Title",
                    Description = "弹窗标题",
                    Type = "string",
                    ValueList = " — ",
                    DefaultValue = " 未设置 "
                },
                new AttributeItem(true) {
                    Name = "ShowButtons",
                    Description = "是否显示下方按钮",
                },
                new AttributeItem(true) {
                    Name = "ShowYesButton",
                    Description = "是否显示Yes按钮",
                },
                new AttributeItem("确认") {
                    Name = "YesButtonText",
                    Description = "Yes按钮文字",
                },
                new AttributeItem("fa fa-check") {
                    Name = "YesButtonIcon",
                    Description = "Yes按钮图标",
                },
                new AttributeItem(Color.Primary) {
                    Name = "YesButtonColor",
                    Description = "Yes按钮颜色",
                },
                new AttributeItem(true) {
                    Name = "ShowNoButton",
                    Description = "是否显示No按钮",
                },
                new AttributeItem("取消") {
                    Name = "NoButtonText",
                    Description = "No按钮文字",
                },
                new AttributeItem("fa fa-close") {
                    Name = "NoButtonIcon",
                    Description = "No按钮图标",
                },
                new AttributeItem(Color.None) {
                    Name = "NoButtonColor",
                    Description = "No按钮颜色",
                },
                new AttributeItem(false) {
                    Name = "ShowCloseButton",
                    Description = "是否显示Close按钮",
                },
                new AttributeItem("关闭") {
                    Name = "CloseButtonText",
                    Description = "Close按钮文字",
                },
                new AttributeItem("fa fa-close") {
                    Name = "CloseButtonIcon",
                    Description = "Close按钮图标",
                },
                new AttributeItem(Color.None) {
                    Name = "CloseButtonColor",
                    Description = "Close按钮颜色",
                },
            };
        }
    }
}
