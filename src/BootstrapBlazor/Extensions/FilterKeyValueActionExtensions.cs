// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FilterKeyValueAction 扩展方法</para>
/// <para lang="en">FilterKeyValueAction extension methods</para>
/// </summary>
public static class FilterKeyValueActionExtensions
{
    extension(FilterKeyValueAction? action)
    {
        /// <summary>
        /// <para lang="zh">将 FilterKeyValueAction 转化为 IFilterAction 集合扩展方法</para>
        /// <para lang="en">Convert FilterKeyValueAction to IFilterAction collection extension method</para>
        /// </summary>
        public List<IFilterAction> ToSearches()
        {
            var ret = new List<IFilterAction>();
            if (action != null)
            {
                ret.Add(new SerializeFilterAction() { Filter = action });
            }

            return ret;
        }
    }
}
