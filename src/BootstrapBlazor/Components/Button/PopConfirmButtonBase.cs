// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 确认弹窗按钮组件
    /// </summary>
    public abstract class PopConfirmButtonBase : ButtonBase
    {
        /// <summary>
        /// 获得/设置 PopoverConfirm 服务实例
        /// </summary>
        [Inject]
        [NotNull]
        private PopoverService? PopoverService { get; set; }

        /// <summary>
        /// 获得/设置 弹窗显示位置
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Auto;

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter]
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 点击确认时回调方法
        /// </summary>
        [Parameter]
        public Func<Task> OnConfirm { get; set; } = () => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 点击关闭时回调方法
        /// </summary>
        [Parameter]
        public Func<Task> OnClose { get; set; } = () => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 点击确认弹窗前回调方法 返回真时弹出弹窗 返回假时不弹出
        /// </summary>
        [Parameter]
        public Func<Task<bool>> OnBeforeClick { get; set; } = () => Task.FromResult(true);

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        [Parameter]
        public Color CloseButtonColor { get; set; } = Color.Secondary;

        /// <summary>
        /// 获得/设置 关闭按钮显示文字 默认为 关闭
        /// </summary>
        [Parameter]
        [NotNull]
        public string? CloseButtonText { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮显示文字 默认为 确定
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ConfirmButtonText { get; set; }

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        [Parameter]
        public Color ConfirmButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 确认框图标
        /// </summary>
        [Parameter]
        public string ConfirmIcon { get; set; } = "fa fa-exclamation-circle text-info";

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 进行弹窗拦截，点击确认按钮后回调原有 OnClick
            OnClick = EventCallback.Factory.Create<MouseEventArgs>(this, e => Show());
        }

        /// <summary>
        /// 显示确认弹窗方法
        /// </summary>
        protected async Task Show()
        {
            // 回调消费者逻辑 判断是否需要弹出确认框
            if (await OnBeforeClick())
            {
                // 生成客户端弹窗
                PopoverService.Show(new PopoverConfirmOption()
                {
                    ButtonId = Id,
                    Title = Title,
                    Content = Content,
                    CloseButtonText = CloseButtonText,
                    CloseButtonColor = CloseButtonColor,
                    ConfirmButtonText = ConfirmButtonText,
                    ConfirmButtonColor = ConfirmButtonColor,
                    Icon = ConfirmIcon,
                    OnConfirm = OnConfirm,
                    OnClose = OnClose,
                    Callback = async () =>
                    {
                        // 调用 JS 进行弹窗 等待 弹窗点击确认回调
                        await JSRuntime.InvokeVoidAsync(Id, "bb_confirm");
                    }
                });
            }
        }
    }
}
