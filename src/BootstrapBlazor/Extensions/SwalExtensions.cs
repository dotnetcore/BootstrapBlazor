// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Swal 扩展类
/// </summary>
public static class SwalExtensions
{
    /// <summary>
    /// 异步回调方法
    /// </summary>
    /// <param name="service"></param>
    /// <param name="option"></param>
    /// <param name="swal">指定弹窗组件 默认为 null 使用 <see cref="BootstrapBlazorRoot"/> 组件内置弹窗组件</param>
    /// <returns></returns>
    public static async Task<bool> ShowModal(this SwalService service, SwalOption option, SweetAlert? swal = null)
    {
        option.IsConfirm = true;
        await service.Show(option, swal);
        return await option.ReturnTask.Task;
    }
}
