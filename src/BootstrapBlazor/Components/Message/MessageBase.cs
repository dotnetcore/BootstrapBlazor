// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Message 组件基类
    /// </summary>
    public abstract class MessageBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 显示位置 默认为 Top
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Top;

        /// <summary>
        /// ToastServices 服务实例
        /// </summary>
        [Inject]
        [NotNull]
        public MessageService? MessageService { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Toast 弹窗事件
            MessageService.Register(this, Show);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        protected abstract Task Show(MessageOption option);

        /// <summary>
        /// 设置 Toast 容器位置方法
        /// </summary>
        /// <param name="placement"></param>
        public void SetPlacement(Placement placement)
        {
            Placement = placement;
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
                MessageService.UnRegister(this);
            }
        }
    }
}
