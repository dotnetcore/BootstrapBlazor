// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">时间区间搜索元数据类</para>
/// <para lang="en">DateTime range search meta data class</para>
/// </summary>
public class DateTimeRangeSearchMetaData : SearchMetaDataBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索值</para>
    /// <para lang="en">Gets or sets the search start value</para>
    /// </summary>
    public DateTimeRangeValue? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索值类型名称 用于将输入的字符串转换为指定类型进行比较</para>
    /// <para lang="en">Gets or sets the search value type name, used to convert the input string to the specified type for comparison</para>
    /// </summary>
    public Type? ValueType { get; set; }

    public override FilterKeyValueAction? GetFilter(string fieldName)
    {
        // 如果用户设置了 GetFilterCallback 回调，则调用该回调获取过滤器实例
        if (GetFilterCallback != null)
        {
            return GetFilterCallback(Value);
        }

        if (Value == null)
        {
            return null;
        }

        var filter = new FilterKeyValueAction() { Filters = [] };
        if (Value.Start > DateTime.MinValue)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = fieldName,
                FieldValue = Value.Start,
                FilterLogic = FilterLogic,
                FilterAction = FilterAction.GreaterThanOrEqual
            });
        }
        if (Value.End > DateTime.MinValue)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = fieldName,
                FieldValue = Value.End,
                FilterLogic = FilterLogic,
                FilterAction = FilterAction.LessThanOrEqual
            });
        }

        return filter;
    }

    /// <summary>
    /// <para lang="zh">搜索值变化事件处理器</para>
    /// <para lang="en">Search value changed event handler</para>
    /// </summary>
    /// <param name="value"></param>
    public async Task ValueChangedHandler(DateTimeRangeValue? value)
    {
        Value = value;

        if (ValueChanged != null)
        {
            await ValueChanged();
        }
    }
}
