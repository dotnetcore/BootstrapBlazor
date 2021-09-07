// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 弹窗类服务基类
    /// </summary>
    /// <typeparam name="TOption"></typeparam>
    public abstract class PopupServiceBase<TOption> : BootstrapServiceBase<TOption>
    {
        /// <summary>
        /// 
        /// </summary>
        public PopupServiceBase(IStringLocalizer? localizer) : base(localizer)
        {

        }

        /// <summary>
        /// 异步回调方法
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public virtual async Task Show(TOption option)
        {
            Func<TOption, Task>? cb = null;
            if (typeof(IPopupHost).IsAssignableFrom(typeof(TOption)))
            {
                var op = option as IPopupHost;
                cb = Cache.FirstOrDefault(i => i.Key == op!.Host).Callback;
            }
            if (cb == null)
            {
                await base.Invoke(option);
            }
            else
            {
                await cb.Invoke(option);
            }
        }
    }
}
