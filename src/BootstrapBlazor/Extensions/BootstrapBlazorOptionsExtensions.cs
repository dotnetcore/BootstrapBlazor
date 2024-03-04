// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazorOptions 配置类扩展方法
/// </summary>
public static class BootstrapBlazorOptionsExtensions
{
    /// <summary>
    /// 获取步长泛型方法
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string? GetStep<TType>(this BootstrapBlazorOptions options) => options.GetStep(typeof(TType));

    /// <summary>
    /// 获取步长方法
    /// </summary>
    /// <param name="options">配置实体类实例</param>
    /// <param name="type">数据类型</param>
    /// <returns></returns>
    public static string? GetStep(this BootstrapBlazorOptions options, Type type)
    {
        var t = Nullable.GetUnderlyingType(type) ?? type;
        return options.StepSettings.GetStep(t);
    }
}
