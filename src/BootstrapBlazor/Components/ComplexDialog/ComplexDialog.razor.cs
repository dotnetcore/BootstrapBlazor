// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 复杂对话框类
    /// </summary>
    /// <typeparam name="TCom"></typeparam>
    public partial class ComplexDialog<TCom> where TCom: ComplexDialogBase
    {

        /// <summary>
        /// 构造方法
        /// </summary>
        public ComplexDialog()
        {
            BodyFragment = builder =>
            {
                builder.OpenComponent(0, typeof(TCom));

                builder.AddComponentReferenceCapture(1, o =>
                {
                    Result = o as TCom;
                    if (Result != null)
                    {
                        Result.DialogCloseAction += result =>
                        {
                            OnCloseDialog?.Invoke(result, Result);
                        };
                    }
                });
                builder.CloseComponent();
            };
        }

        /// <summary>
        /// 获取的实例
        /// </summary>
        private TCom? Result { get; set; }

        private ComplexDialogOption<TCom>? ComplexDialogOption { get; set; }

        private RenderFragment? BodyFragment { get; set; }

        /// <summary>
        /// 关闭事件
        /// </summary>
        [Parameter]
        public Action<DialogResult, TCom?>? OnCloseDialog { get; set; }


        #region 按钮相关

        /// <summary>
        /// 是否显示下方按钮
        /// </summary>
        [Parameter] [NotNull] public bool ShowFooterButtons { get; set; } = true;

        /// <summary>
        /// 显示确认按钮
        /// </summary>
        [Parameter]
        [NotNull]
        public bool ShowYesButton { get; set; } = true;

        /// <summary>
        /// 确认按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? YesButtonText { get; set; } = "确认";

        /// <summary>
        /// 确认按钮图标
        /// </summary>
        [Parameter]
        [NotNull]
        public string? YesButtonIcon { get; set; } = "fa fa-check";

        /// <summary>
        /// 确认按钮颜色
        /// </summary>
        [Parameter] public Color YesButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 显示取消按钮
        /// </summary>
        [Parameter]
        [NotNull]
        public bool ShowNoButton { get; set; } = true;

        /// <summary>
        /// 取消按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? NoButtonText { get; set; } = "取消";

        /// <summary>
        /// 取消按钮图标
        /// </summary>
        [Parameter]
        [NotNull]
        public string? NoButtonIcon { get; set; } = "fa fa-close";

        /// <summary>
        /// 取消按钮颜色
        /// </summary>
        [Parameter] public Color NoButtonColor { get; set; } = Color.None;

        /// <summary>
        /// 显示关闭按钮
        /// </summary>
        [Parameter]
        [NotNull]
        public bool ShowCloseButton { get; set; }

        /// <summary>
        /// 关闭按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CloseButtonText { get; set; } = "关闭";

        /// <summary>
        /// 关闭按钮图标
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CloseButtonIcon { get; set; } = "fa fa-close";

        /// <summary>
        /// 关闭按钮颜色
        /// </summary>
        [Parameter] public Color CloseButtonColor { get; set; } = Color.None;

        #endregion

        private void ButtonClick(DialogResult dialogResult)
        {
            DialogEventArgs e = new() {DialogResult = dialogResult};
            Result?.OnDialogClosing(e);
            if (!e.Cancel)
            {
                OnCloseDialog?.Invoke(dialogResult, Result);
                Result?.OnDialogClosed(e);
            }
        }
    }
}
