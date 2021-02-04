// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// SweetAlert 弹窗服务
    /// </summary>
    public class SwalService : PopupServiceBase<SwalOption>
    {
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
    }
}
