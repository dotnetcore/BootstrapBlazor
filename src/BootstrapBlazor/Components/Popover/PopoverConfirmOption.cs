// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 确认弹窗相关配置实体类
    /// </summary>
    public class PopoverConfirmOption
    {
        /// <summary>
        /// 获得/设置 确认回调方法
        /// </summary>
        public Func<Task>? OnConfirm { get; set; }
        /// <summary>
        /// 获得/设置 关闭回调方法
        /// </summary>
        public Func<Task>? OnClose { get; set; }

        /// <summary>
        /// 获得/设置 确认弹窗回调方法
        /// </summary>
        public Func<Task>? Callback { get; set; }

        /// <summary>
        /// 获得/设置 弹框按钮触发源组件 Id
        /// </summary>
        public string? ButtonId { get; set; }

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        public Color CloseButtonColor { get; set; } = Color.Secondary;

        /// <summary>
        /// 获得/设置 关闭按钮显示文字
        /// </summary>
        public string? CloseButtonText { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮显示文字
        /// </summary>
        public string? ConfirmButtonText { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        public Color ConfirmButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 确认框图标
        /// </summary>
        public string Icon { get; set; } = "fa-exclamation-circle text-info";
    }
}
