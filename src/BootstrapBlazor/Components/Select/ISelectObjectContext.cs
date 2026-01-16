// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">SelectObject 上下文</para>
///  <para lang="en">SelectObject Context</para>
/// </summary>
public interface ISelectObjectContext<TItem>
{
    /// <summary>
    ///  <para lang="zh">获得/设置 SelectObject 组件实例对象引用</para>
    ///  <para lang="en">Get/Set SelectObject Component Instance</para>
    /// </summary>
    [NotNull]
    SelectObject<TItem>? Component { get; set; }

    /// <summary>
    ///  <para lang="zh">设置组件当前值方法</para>
    ///  <para lang="en">Set Component Current Value Method</para>
    /// </summary>
    /// <param name="value"></param>
    void SetValue(TItem value);

    /// <summary>
    ///  <para lang="zh">关闭当前弹窗方法</para>
    ///  <para lang="en">Close Current Popover Method</para>
    /// </summary>
    /// <returns></returns>
    Task CloseAsync();
}
