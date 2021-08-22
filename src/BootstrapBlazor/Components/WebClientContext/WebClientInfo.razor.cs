// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class WebClientInfo : IDisposable
    {
        [Inject]
        [NotNull]
        private HttpContextService? WebClientContext { get; set; }

        private ElementReference WebClientElement { get; set; }

        private JSInterop<WebClientInfo>? Interop { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            WebClientContext.Register(this, RetrieveIp);
        }

        private async ValueTask RetrieveIp(string url)
        {
            Interop ??= new JSInterop<WebClientInfo>(JSRuntime);
            await Interop.InvokeVoidAsync(this, WebClientElement, "browser.ip", url, nameof(UpdateIp));
        }

        /// <summary>
        /// 清除 ToastBox 方法
        /// </summary>
        [JSInvokable]
        public void UpdateIp(string ip)
        {
            WebClientContext.SetIp(ip);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                WebClientContext.UnRegister(this);
                Interop?.Dispose();
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
