// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">多选类型搜索元数据类</para>
/// <para lang="en">Multiple select type search metadata class</para>
/// </summary>
public class MultipleSelectSearchMetadata1 : SelectSearchMetadata1
{
    /// <summary>
    /// <inheritdoc cref="SearchMetadataBase1.GetFilter(string)"/>
    /// </summary>
    public override FilterKeyValueAction? GetFilter(string fieldName)
    {
        // 如果用户设置了 GetFilterCallback 回调，则调用该回调获取过滤器实例
        if (GetFilterCallback != null)
        {
            return GetFilterCallback(Value);
        }

        if (string.IsNullOrEmpty(Value))
        {
            return null;
        }

        var valueList = Value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(v => v.Trim()).ToList();
        if (valueList.Count == 1)
        {
            return new FilterKeyValueAction()
            {
                FieldKey = fieldName,
                FieldValue = valueList[0],
                FilterLogic = FilterLogic.Or,
                FilterAction = FilterAction
            };
        }

        var filter = new FilterKeyValueAction() { Filters = [] };
        foreach (var value in valueList)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = fieldName,
                FieldValue = value,
                FilterLogic = FilterLogic.Or,
                FilterAction = FilterAction
            });
        }

        return filter;
    }
}
