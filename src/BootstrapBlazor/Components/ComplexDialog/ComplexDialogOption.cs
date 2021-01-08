// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 复杂对话框配置类
    /// </summary>
    public class ComplexDialogOption<TCom>:DialogOption where TCom: ComplexDialogBase
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComplexDialogOption()
        {
            ShowFooter = false;
            OnCloseAsync += async () => { await Close(DialogResult.Unknown, null); };
        }

        #region 底部按钮相关

        /// <summary>
        /// 是否显示下方按钮
        /// </summary>
        public bool ShowButtons { get; set; } = true;

        /// <summary>
        /// 显示确认按钮
        /// </summary>
        public bool ShowYesButton { get; set; } = true;

        /// <summary>
        /// 确认按钮文本
        /// </summary>
        public string YesButtonText { get; set; } = "确认";

        /// <summary>
        /// 确认按钮图标
        /// </summary>
        public string YesButtonIcon { get; set; } = "fa fa-check";

        /// <summary>
        /// 确认按钮颜色
        /// </summary>
        public Color YesButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 显示取消按钮
        /// </summary>
        public bool ShowNoButton { get; set; } = true;

        /// <summary>
        /// 取消按钮文本
        /// </summary>
        public string NoButtonText { get; set; } = "取消";

        /// <summary>
        /// 取消按钮图标
        /// </summary>
        public string NoButtonIcon { get; set; } = "fa fa-close";

        /// <summary>
        /// 取消按钮颜色
        /// </summary>
        public Color NoButtonColor { get; set; } = Color.None;

        /// <summary>
        /// 显示关闭按钮
        /// </summary>
        public new bool ShowCloseButton { get; set; } = false;

        /// <summary>
        /// 关闭按钮文本
        /// </summary>
        public string CloseButtonText { get; set; } = "关闭";

        /// <summary>
        /// 关闭按钮图标
        /// </summary>
        public string CloseButtonIcon { get; set; } = "fa fa-close";

        /// <summary>
        /// 关闭按钮颜色
        /// </summary>
        public Color CloseButtonColor { get; set; } = Color.None;

        #endregion


        /// <summary>
        /// 获得/设置 模态弹窗返回值任务实例
        /// </summary>
        internal TaskCompletionSource<(DialogResult dialogResult, TCom? result)> ReturnTask { get; } = new();

        internal async Task Close(DialogResult dialogResult, TCom? component)
        {
            if (Dialog != null)
            {
                await Dialog.Close();
            }

            ReturnTask.TrySetResult(new(dialogResult, component));
        }
    }
}
