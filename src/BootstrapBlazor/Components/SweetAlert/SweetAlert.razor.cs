// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SweetAlert 组件
    /// </summary>
    public sealed partial class SweetAlert
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
        public SwalService? SwalService { get; set; }

        private bool IsShowDialog { get; set; }

        private bool IsAutoHide { get; set; }

        private int Delay { get; set; }

        private CancellationTokenSource? DelayToken { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Swal 弹窗事件
            SwalService.Register(this, Show);
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

                if (IsAutoHide && Delay > 0)
                {
                    if (DelayToken == null) DelayToken = new CancellationTokenSource();
                    await Task.Delay(Delay, DelayToken.Token);

                    if (!DelayToken.IsCancellationRequested)
                    {
                        // 自动关闭弹窗
                        await ModalContainer.Toggle();
                    }
                }
            }
        }

        private async Task Show(SwalOption option)
        {
            IsAutoHide = option.IsAutoHide;
            Delay = option.Delay;

            option.Dialog = ModalContainer;
            option.Body = ModalDialog;
            var parameters = option.ToAttributes().ToList();

            // 不保持状态
            parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.OnClose), new Func<Task>(async () =>
            {
                if (IsAutoHide && DelayToken != null)
                {
                    DelayToken.Cancel();
                    DelayToken = null;
                }
                if (!option.KeepChildrenState)
                {
                    await ModalDialog.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object>()
                    {
                        [nameof(ModalDialogBase.BodyContext)] = null!,
                        [nameof(ModalDialogBase.BodyTemplate)] = null!
                    }));
                }
            })));

            parameters.Add(new KeyValuePair<string, object>(nameof(ModalDialogBase.BodyTemplate), DynamicComponent.CreateComponent<SweetAlertBody>(SweetAlertBody.Parse(option)).Render()));

            await ModalDialog.SetParametersAsync(ParameterView.FromDictionary(parameters.ToDictionary(item => item.Key, item => item.Value)));
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
                DelayToken?.Dispose();
                DelayToken = null;
                SwalService.UnRegister(this);
            }
        }
    }
}
