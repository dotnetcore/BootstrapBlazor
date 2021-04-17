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
    public class Download : BootstrapComponentBase, IDisposable
    {
        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        [Inject]
        [NotNull]
        private DownloadService? DownloadService { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            DownloadService.Register(this, DownloadFile);
        }

        private async Task DownloadFile(DownloadOption option)
        {
            if (JSRuntime is IJSUnmarshalledRuntime webAssemblyJsRuntime)
            {
                webAssemblyJsRuntime.InvokeUnmarshalled<string?, string, byte[], bool>("$.bb_download_wasm", option.FileName,
                    option.Mime, option.File);
            }
            else
            {
                await JSRuntime.InvokeVoidAsync(identifier:"$.bb_download", option.FileName, option.Mime, option.File);
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DownloadService.UnRegister(this);
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
