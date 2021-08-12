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
    /// Dialog 对话框组件
    /// </summary>
    public partial class Dialog : IDisposable
    {
        /// <summary>
        /// 获得/设置 Modal 容器组件实例
        /// </summary>
        [NotNull]
        private Modal? ModalContainer { get; set; }

        /// <summary>
        /// 获得/设置 弹出对话框实例集合
        /// </summary>
        private List<List<KeyValuePair<string, object>>> DialogParameters { get; } = new();

        private bool IsKeyboard { get; set; }

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

            // 注册 Dialog 弹窗事件
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
                await ModalContainer.Show();
            }
        }

        private Task Show(DialogOption option)
        {
            IsKeyboard = option.IsKeyboard;
            option.Dialog = ModalContainer;
            var parameters = option.ToAttributes().ToList();

            var content = option.BodyTemplate ?? option.Component?.Render();
            if (content != null)
            {
                parameters.Add(new(nameof(ModalDialog.BodyTemplate), content));
            }

            if (option.HeaderTemplate != null)
            {
                parameters.Add(new(nameof(ModalDialog.HeaderTemplate), option.HeaderTemplate));
            }

            if (option.FooterTemplate != null)
            {
                parameters.Add(new(nameof(ModalDialog.FooterTemplate), option.FooterTemplate));
            }

            if (!string.IsNullOrEmpty(option.Class))
            {
                parameters.Add(new(nameof(ModalDialog.Class), option.Class));
            }

            parameters.Add(new(nameof(ModalDialog.OnClose), new Func<Task>(async () =>
            {
                // 回调 OnClose 方法
                // 移除当前对话框
                if (option.OnCloseAsync != null)
                {
                    await option.OnCloseAsync();
                }
                DialogParameters.Remove(parameters);

                // 支持多级弹窗
                await ModalContainer.CloseOrPopDialog();
                StateHasChanged();
            })));

            DialogParameters.Add(parameters);
            if (DialogParameters.Count == 1)
            {
                IsShowDialog = true;
            }
            StateHasChanged();
            return Task.CompletedTask;
        }

        private RenderFragment RenderDialog(IEnumerable<KeyValuePair<string, object>> parameter) => builder =>
        {
            builder.OpenComponent<ModalDialog>(0);
            builder.AddMultipleAttributes(1, parameter);
            builder.AddComponentReferenceCapture(2, dialog =>
            {
                var modal = (ModalDialog)dialog;
                ModalContainer.ShowDialog(modal);
            });
            builder.CloseComponent();
        };

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DialogService.UnRegister(this);
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
