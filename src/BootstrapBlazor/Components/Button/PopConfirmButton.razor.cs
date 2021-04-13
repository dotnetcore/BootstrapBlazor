// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class PopConfirmButton
    {
        /// <summary>
        /// 获得/设置 PopoverConfirm 服务实例
        /// </summary>
        [Inject]
        [NotNull]
        private PopoverService? PopoverService { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<PopConfirmButton>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ConfirmButtonText ??= Localizer[nameof(ConfirmButtonText)];
            CloseButtonText ??= Localizer[nameof(CloseButtonText)];
            Content ??= Localizer[nameof(Content)];
        }

        /// <summary>
        /// 显示确认弹窗方法
        /// </summary>
        private async Task Show()
        {
            // 回调消费者逻辑 判断是否需要弹出确认框
            if (await OnBeforeClick())
            {
                // 生成客户端弹窗
                await PopoverService.Show(new PopoverConfirmOption()
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
