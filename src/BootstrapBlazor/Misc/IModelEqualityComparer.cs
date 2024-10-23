// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 模型比较接口
/// </summary>
public interface IModelEqualityComparer<TItem>
{
    /// <summary>
    /// 获得/设置 模型比对回调方法
    /// </summary>
    Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// 获得/设置 模型键值标签
    /// </summary>
    Type CustomKeyAttribute { get; set; }

    /// <summary>
    /// 相等判定方法
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    bool Equals(TItem? x, TItem? y);
}
