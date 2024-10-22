// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 动态组件类
/// </summary>
/// <param name="componentType"></param>
/// <param name="parameters">TCom 组件所需要的参数集合</param>
public class BootstrapDynamicComponent(Type componentType, IDictionary<string, object?>? parameters = null)
{
    /// <summary>
    /// 创建自定义组件方法
    /// </summary>
    /// <typeparam name="TCom"></typeparam>
    /// <param name="parameters">TCom 组件所需要的参数集合</param>
    /// <returns></returns>
    public static BootstrapDynamicComponent CreateComponent<TCom>(IDictionary<string, object?>? parameters = null) where TCom : IComponent => CreateComponent(typeof(TCom), parameters);

    /// <summary>
    /// 创建自定义组件方法
    /// </summary>
    /// <typeparam name="TCom"></typeparam>
    /// <returns></returns>
    public static BootstrapDynamicComponent CreateComponent<TCom>() where TCom : IComponent => CreateComponent<TCom>(new Dictionary<string, object?>());

    /// <summary>
    /// 创建自定义组件方法
    /// </summary>
    /// <param name="type"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static BootstrapDynamicComponent CreateComponent(Type type, IDictionary<string, object?>? parameters = null) => new(type, parameters);

    /// <summary>
    /// 创建组件实例并渲染
    /// </summary>
    /// <returns></returns>
    public RenderFragment Render() => builder =>
    {
        var index = 0;
        builder.OpenComponent(index++, componentType);
        if (parameters != null)
        {
            foreach (var p in parameters)
            {
                builder.AddAttribute(index++, p.Key, p.Value);
            }
        }
        builder.CloseComponent();
    };
}
