// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IModelEqualComparer 扩展方法类</para>
///  <para lang="en">IModelEqualComparer extension methods</para>
/// </summary>
public static class IModelEqualityComparerExtensions
{
    /// <summary>
    ///  <para lang="zh">Equals 扩展方法</para>
    ///  <para lang="en">Equals extension method</para>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="comparer"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public static bool Equals<TItem>(this IModelEqualityComparer<TItem> comparer, TItem? x, TItem? y)
    {
        bool ret;
        if (x == null && y == null)
        {
            ret = true;
        }
        else if (x == null || y == null)
        {
            ret = false;
        }
        else
        {
            ret = comparer.ModelEqualityComparer?.Invoke(x, y)
                ?? Utility.GetKeyValue<TItem, object>(x, comparer.CustomKeyAttribute)?.Equals(Utility.GetKeyValue<TItem, object>(y, comparer.CustomKeyAttribute))
                ?? EqualityComparer();
        }
        return ret;

        bool EqualityComparer()
        {
            bool ret;
            if (x is IEqualityComparer<TItem> comparer)
            {
                // <para lang="zh">显式调用 IEqualityComparer 接口的 Equals 方法</para>
                // <para lang="en">Explicitly call the Equals method of the IEqualityComparer interface</para>
                ret = comparer.Equals(x, y);
            }
            else
            {
                // <para lang="zh">调用 Object 对象的 Equals 方法</para>
                // <para lang="en">Call the Equals method of the Object object</para>
                ret = x.Equals(y);
            }
            return ret;
        }
    }
}
