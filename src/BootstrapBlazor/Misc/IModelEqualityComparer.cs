// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 模型比较接口
/// </summary>
public interface IModelEqualityComparer<TItem>
{
    /// <summary>
    /// 
    /// </summary>
    Func<TItem, TItem, bool>? ModelEqualityComparer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    Type CustomKeyAttribute { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    bool Equals(TItem? x, TItem? y);
}
