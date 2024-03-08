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
        return await option.ConfirmContext.ConfirmTask.Task;
    }

    /// <summary>
    /// 将配置信息转化为参数集合
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static IDictionary<string, object?> Parse(this SwalOption option) => new Dictionary<string, object?>()
    {
        [nameof(SweetAlertBody.Category)] = option.Category,
        [nameof(SweetAlertBody.ShowClose)] = option.ShowClose,
        [nameof(SweetAlertBody.IsConfirm)] = option.IsConfirm,
        [nameof(SweetAlertBody.ShowFooter)] = option.ShowFooter,
        [nameof(SweetAlertBody.OnCloseAsync)] = async () =>
        {
            if (option.IsConfirm)
            {
                option.ConfirmContext.Value = false;
            }
            if (option.OnCloseAsync != null)
            {
                await option.OnCloseAsync();
            }
        },
        [nameof(SweetAlertBody.OnConfirmAsync)] = async () =>
        {
            if (option.IsConfirm)
            {
                option.ConfirmContext.Value = true;
            }
            if (option.OnConfirmAsync != null)
            {
                await option.OnConfirmAsync();
            }
        },
        [nameof(SweetAlertBody.Title)] = option.Title,
        [nameof(SweetAlertBody.Content)] = option.Content,
        [nameof(SweetAlertBody.BodyTemplate)] = option.BodyTemplate,
        [nameof(SweetAlertBody.FooterTemplate)] = option.FooterTemplate,
        [nameof(SweetAlertBody.ButtonTemplate)] = option.ButtonTemplate,
        [nameof(SweetAlertBody.CloseButtonIcon)] = option.CloseButtonIcon,
        [nameof(SweetAlertBody.ConfirmButtonIcon)] = option.ConfirmButtonIcon,
        [nameof(SweetAlertBody.CloseButtonText)] = option.CloseButtonText,
        [nameof(SweetAlertBody.CancelButtonText)] = option.CancelButtonText,
        [nameof(SweetAlertBody.ConfirmButtonText)] = option.ConfirmButtonText
    };
}
