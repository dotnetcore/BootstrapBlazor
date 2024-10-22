// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// SelectObject 上下文
/// </summary>
public interface ISelectObjectContext<TItem>
{
    /// <summary>
    /// 获得/设置 SelectObject 组件实例对象引用
    /// </summary>
    [NotNull]
    SelectObject<TItem>? Component { get; set; }

    /// <summary>
    /// 设置组件当前值方法
    /// </summary>
    /// <param name="value"></param>
    void SetValue(TItem value);

    /// <summary>
    /// 关闭当前弹窗方法
    /// </summary>
    /// <returns></returns>
    Task CloseAsync();
}
