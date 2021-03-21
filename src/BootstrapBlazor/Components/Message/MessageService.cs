// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class MessageService : PopupServiceBase<MessageOption>
    {
        private readonly IDisposable _optionsReloadToken;
        private BootstrapBlazorOptions _option;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="option"></param>
        public MessageService(IOptionsMonitor<BootstrapBlazorOptions> option)
        {
            _option = option.CurrentValue;
            _optionsReloadToken = option.OnChange(op => _option = op);
        }

        /// <summary>
        /// Show 方法
        /// </summary>
        /// <param name="option"></param>
        public override async Task Show(MessageOption option)
        {
            if (!option.ForceDelay && _option.MessageDelay != 0)
            {
                option.Delay = _option.MessageDelay;
            }

            await base.Show(option);
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
