﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        option.Component = BootstrapDynamicComponent.CreateComponent<TComponent>(parameters);
        await service.PrintAsync(option);
    }
}
