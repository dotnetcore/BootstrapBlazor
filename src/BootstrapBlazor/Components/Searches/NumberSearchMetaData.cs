// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">数字类型搜索元数据类</para>
/// <para lang="en">Number type search metadata class</para>
/// </summary>
public class NumberSearchMetaData : SearchMetaDataBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索开始值</para>
    /// <para lang="en">Gets or sets the search start value</para>
    /// </summary>
    public string? StartValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索开始值标签文本</para>
    /// <para lang="en">Gets or sets the search start value label text</para>
    /// </summary>
    public string? StartValueLabelText { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索结束值</para>
    /// <para lang="en">Gets or sets the search end value</para>
    /// </summary>
    public string? EndValue { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 搜索结束值标签文本</para>
    /// <para lang="en">Gets or sets the search end value label text</para>
    /// </summary>
    public string? EndValueLabelText { get; set; }

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
            return GetFilterCallback((StartValue: StartValue, EndValue: EndValue));
        }

        if (string.IsNullOrEmpty(StartValue) && string.IsNullOrEmpty(EndValue))
        {
            return null;
        }

        var filter = new FilterKeyValueAction() { Filters = [] };
        if (!string.IsNullOrEmpty(StartValue))
        {
            var v = ParseTypeValue(ValueType, StartValue);
            if (v != null)
            {
                filter.Filters.Add(new FilterKeyValueAction()
                {
                    FieldKey = fieldName,
                    FieldValue = StartValue,
                    FilterLogic = FilterLogic,
                    FilterAction = FilterAction.GreaterThanOrEqual
                });
            }
            else
            {
                StartValue = null;
            }
        }
        if (!string.IsNullOrEmpty(EndValue))
        {
            var v = ParseTypeValue(ValueType, EndValue);
            if (v != null)
            {
                filter.Filters.Add(new FilterKeyValueAction()
                {
                    FieldKey = fieldName,
                    FieldValue = EndValue,
                    FilterLogic = FilterLogic,
                    FilterAction = FilterAction.LessThanOrEqual
                });
            }
            else
            {
                EndValue = null;
            }
        }

        return filter;
    }

    /// <summary>
    /// <para lang="zh">搜索值变化事件处理器</para>
    /// <para lang="en">Search value changed event handler</para>
    /// </summary>
    /// <param name="value"></param>
    public async Task StartValueChangedHandler(string? value)
    {
        StartValue = value;

        if (ValueChanged != null)
        {
            await ValueChanged();
        }
    }

    /// <summary>
    /// <para lang="zh">搜索值变化事件处理器</para>
    /// <para lang="en">Search value changed event handler</para>
    /// </summary>
    /// <param name="value"></param>
    public async Task EndValueChangedHandler(string? value)
    {
        EndValue = value;

        if (ValueChanged != null)
        {
            await ValueChanged();
        }
    }
}
