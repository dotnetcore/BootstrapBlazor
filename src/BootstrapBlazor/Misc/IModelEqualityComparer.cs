// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">模型比较接口</para>
/// <para lang="en">Model equality comparer interface</para>
/// </summary>
public interface IModelEqualityComparer<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 模型比对回调方法</para>
    /// <para lang="en">Get/Set model comparison callback method</para>
    /// </summary>
    Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 模型键值标签</para>
    /// <para lang="en">Get/Set model key attribute</para>
    /// </summary>
    Type CustomKeyAttribute { get; set; }

    /// <summary>
    /// <para lang="zh">相等判定方法</para>
    /// <para lang="en">Equality determination method</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    bool Equals(TItem? x, TItem? y);
}
