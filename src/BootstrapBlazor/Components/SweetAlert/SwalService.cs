// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SweetAlert 弹窗服务
    /// </summary>
    public class SwalService : PopupServiceBase<SwalOption>, IDisposable
    {
        private readonly IDisposable _optionsReloadToken;
        private BootstrapBlazorOptions _option;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="option"></param>
        public SwalService(IOptionsMonitor<BootstrapBlazorOptions> option)
        {
            _option = option.CurrentValue;
            _optionsReloadToken = option.OnChange(op => _option = op);
        }

        /// <summary>
        /// Show 方法
        /// </summary>
        /// <param name="option"></param>
        public override async Task Show(SwalOption option)
        {
            if (!option.ForceDelay && _option.SwalDelay != 0)
            {
                option.Delay = _option.SwalDelay;
            }

            await base.Show(option);
        }

        /// <summary>
        /// 异步回调方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<bool> ShowModal(SwalOption option)
        {
            var cb = Cache.FirstOrDefault().Callback;
            if (cb != null)
            {
                await cb.Invoke(option);
            }
            return option.IsConfirm != true || await option.ReturnTask.Task;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _optionsReloadToken.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
