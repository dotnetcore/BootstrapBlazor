// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Dialog 对话框组件
    /// </summary>
    public sealed partial class Dialog
    {
        /// <summary>
        /// 获得/设置 Modal 容器组件实例
        /// </summary>
        [NotNull]
        private Modal? ModalContainer { get; set; }

        /// <summary>
        /// 获得/设置 弹出对话框实例
        /// </summary>
        [NotNull]
        private ModalDialog? ModalDialog { get; set; }

        /// <summary>
        /// DialogServices 服务实例
        /// </summary>
        [Inject]
        [NotNull]
        private DialogService? DialogService { get; set; }

        private bool IsShowDialog { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Toast 弹窗事件
            DialogService.Register(this, Show);
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (ModalContainer != null && IsShowDialog)
            {
                IsShowDialog = false;
                await ModalContainer.Toggle();
            }
        }

        private async Task Show(DialogOption option)
        {
            option.Dialog = ModalContainer;
            var parameters = option.ToAttributes().ToList();

            var content = option.BodyTemplate ?? option.Component?.Render();
            if (content != null)
            {
                parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.BodyTemplate), option.KeepChildrenState ? content : new RenderFragment(builder =>
                {
                    builder.OpenElement(0, "div");
                    builder.SetKey(option);
                    builder.AddContent(1, content);
                    builder.CloseElement();
                })));
            }

            if (option.FooterTemplate != null)
            {
                parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.FooterTemplate), option.FooterTemplate));
            }

            parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.OnClose), new Func<Task>(async () =>
            {
                // 回调 OnClose 方法
                if (option.OnCloseAsync != null) await option.OnCloseAsync();
            })));

            await ModalDialog.SetParametersAsync(ParameterView.FromDictionary(parameters.ToDictionary(key => key.Key, value => value.Value)));
            IsShowDialog = true;
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                DialogService.UnRegister(this);
            }
        }
    }
}
