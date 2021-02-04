// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Popover 服务类
    /// </summary>
    internal class PopoverService
    {
        private Action? Callback { get; set; }

        /// <summary>
        /// 获得/设置 内部确认弹窗引用
        /// </summary>
        public (PopoverConfirmOption Option, RenderFragment RenderFragment)? ConfirmBox { get; set; }

        /// <summary>
        /// 显示确认弹窗方法
        /// </summary>
        /// <param name="option"></param>
        public void Show(PopoverConfirmOption option)
        {
            ConfirmBox = (option, new RenderFragment(builder =>
            {
                var index = 0;
                builder.OpenComponent<PopoverConfirmBox>(index++);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.SourceId), option.ButtonId);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.OnConfirm), option.OnConfirm);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.OnClose), option.OnClose);

                builder.AddAttribute(index++, nameof(PopoverConfirmBox.Title), option.Title);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.Content), option.Content);

                builder.AddAttribute(index++, nameof(PopoverConfirmBox.CloseButtonText), option.CloseButtonText);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.CloseButtonColor), option.CloseButtonColor);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.ConfirmButtonText), option.ConfirmButtonText);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.ConfirmButtonColor), option.ConfirmButtonColor);
                builder.AddAttribute(index++, nameof(PopoverConfirmBox.Icon), option.Icon);

                builder.CloseComponent();
            }));

            Callback?.Invoke();
        }

        /// <summary>
        /// InvokeRun 方法
        /// </summary>
        public async Task InvokeRun()
        {
            if (ConfirmBox.HasValue && ConfirmBox.Value.Option.Callback != null)
            {
                await ConfirmBox.Value.Option.Callback.Invoke();
            }
        }

        /// <summary>
        /// 关闭确认弹窗方法
        /// </summary>
        public void Hide()
        {
            ConfirmBox = null;
            Callback?.Invoke();
        }

        /// <summary>
        /// 订阅弹窗事件
        /// </summary>
        /// <param name="callback"></param>
        public void Register(Action callback) => Callback = callback;
    }
}
