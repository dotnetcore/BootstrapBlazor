// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">字符串搜索元数据类</para>
/// <para lang="en">String search meta data class</para>
/// </summary>
public class StringSearchMetaData : SearchMetaDataBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索值</para>
    /// <para lang="en">Gets or sets the search value</para>
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchFormItemMetaData.GetFilter(string)"/>
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

        return new FilterKeyValueAction()
        {
            FieldKey = fieldName,
            FieldValue = Value,
            FilterLogic = FilterLogic,
            FilterAction = FilterAction
        };
    }

    /// <summary>
    /// <para lang="zh">搜索值变化事件处理器</para>
    /// <para lang="en">Search value changed event handler</para>
    /// </summary>
    /// <param name="value"></param>
    public async Task ValueChangedHandler(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Value = null;
        }
        else
        {
            Value = value;
        }

        if (ValueChanged != null)
        {
            await ValueChanged();
        }
    }
}
