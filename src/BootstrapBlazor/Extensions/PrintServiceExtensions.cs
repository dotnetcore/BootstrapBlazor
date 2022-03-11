// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// PrintService 扩展方法
/// </summary>
public static class PrintServiceExtensions
{
    /// <summary>
    /// 打印方法
    /// </summary>
    /// <typeparam name="TComponent"></typeparam>
    /// <param name="service"></param>
    /// <param name="parametersFactory"></param>
    /// <returns></returns>
    public static async Task PrintAsync<TComponent>(this PrintService service, Func<DialogOption, IDictionary<string, object?>> parametersFactory) where TComponent : ComponentBase
    {
        var option = new DialogOption();
        var parameters = parametersFactory(option);
        if (option.Component == null)
        {
            option.Component = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters);
        }
        await service.PrintAsync(option);
    }
}
