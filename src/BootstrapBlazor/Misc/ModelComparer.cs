// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 模型比较器
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class ModelComparer<TItem> : IEqualityComparer<TItem>
{
    private readonly Func<TItem, TItem, bool> _comparer;
    /// <summary>
    /// 构造函数
    /// </summary>
    public ModelComparer(Func<TItem, TItem, bool> comparer)
    {
        _comparer = comparer;
    }

    /// <summary>
    /// Equals 方法
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(TItem? x, TItem? y)
    {
        bool ret;
        if (x != null && y != null)
        {
            // 均不为空时走 comparer 方法判断
            ret = _comparer(x, y);
        }
        else
        {
            // 有一个为空时 判断是否均为空
            // 均为空时为 true 否则 false
            ret = x == null && y == null;
        }
        return ret;
    }

    /// <summary>
    /// GetHashCode 方法
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int GetHashCode([DisallowNull] TItem obj) => obj.GetHashCode();
}
