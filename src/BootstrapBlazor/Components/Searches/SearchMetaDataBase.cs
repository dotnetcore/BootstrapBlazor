// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">搜索元数据基类</para>
/// <para lang="en">Search meta data base class</para>
/// </summary>
public abstract class SearchMetaDataBase : ISearchFormItemMetaData
{
    /// <summary>
    /// <para lang="zh">获得/设置 占位符文本</para>
    /// <para lang="en">Gets or sets the placeholder text</para>
    /// </summary>
    public string? PlaceHolder { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchFormItemMetaData.ValueChanged"/>
    /// </summary>
    public Func<Task>? ValueChanged { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchFormItemMetaData.FilterLogic"/>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterLogic FilterLogic { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchFormItemMetaData.FilterAction"/>
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FilterAction FilterAction { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchFormItemMetaData.GetFilterCallback"/>
    /// </summary>
    public Func<object?, FilterKeyValueAction>? GetFilterCallback { get; set; }

    /// <summary>
    /// <inheritdoc cref="ISearchFormItemMetaData.GetFilter(string)"/>
    /// </summary>
    public abstract FilterKeyValueAction? GetFilter(string fieldName);

    /// <summary>
    /// <para lang="zh">将输入的字符串转换为指定类型进行比较</para>
    /// <para lang="en">Convert the input string to the specified type for comparison</para>
    /// </summary>
    /// <param name="valueType"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    protected virtual object? ParseTypeValue(Type? valueType, string value)
    {
        if (valueType == null)
        {
            return value;
        }

        try
        {
            return Convert.ChangeType(value, valueType);
        }
        catch
        {
            // ignored
        }
        return null;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void Reset() { }
}
