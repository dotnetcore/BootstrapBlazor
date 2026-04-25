// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">DynamicObjectContext 扩展方法辅助类</para>
/// <para lang="en">DynamicObjectContext Extension Methods Helper</para>
/// </summary>
public static class DynamicObjectContextExtensions
{
    /// <summary>
    /// <para lang="zh">增加 RequiredAttribute 扩展方法</para>
    /// <para lang="en">Add RequiredAttribute Extension</para>
    /// </summary>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="errorMessage">
    ///   <para lang="zh">错误信息</para>
    ///   <para lang="en">Error message</para>
    /// </param>
    /// <param name="allowEmptyStrings">
    ///   <para lang="zh">是否允许空字符串</para>
    ///   <para lang="en">Whether to allow empty strings</para>
    /// </param>
    public static void AddRequiredAttribute(this DynamicObjectContext context, string columnName, string? errorMessage = null, bool allowEmptyStrings = false)
    {
        var parameters = new KeyValuePair<string, object?>[]
        {
            new(nameof(RequiredAttribute.ErrorMessage), errorMessage),
            new(nameof(RequiredAttribute.AllowEmptyStrings), allowEmptyStrings)
        };
        context.AddMultipleParameterAttribute<RequiredAttribute>(columnName, parameters);
    }

    /// <summary>
    /// <para lang="zh">增加 AutoGenerateColumnAttribute 扩展方法</para>
    /// <para lang="en">Add AutoGenerateColumnAttribute Extension</para>
    /// </summary>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="parameters">
    ///   <para lang="zh">参数字典</para>
    ///   <para lang="en">Parameters dictionary</para>
    /// </param>
    public static void AddAutoGenerateColumnAttribute(this DynamicObjectContext context, string columnName, IEnumerable<KeyValuePair<string, object?>> parameters) => context.AddMultipleParameterAttribute<AutoGenerateColumnAttribute>(columnName, parameters);

    /// <summary>
    /// <para lang="zh">增加 DisplayAttribute 扩展方法</para>
    /// <para lang="en">Add DisplayAttribute Extension</para>
    /// </summary>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="parameters">
    ///   <para lang="zh">参数字典</para>
    ///   <para lang="en">Parameters dictionary</para>
    /// </param>
    public static void AddDisplayAttribute(this DynamicObjectContext context, string columnName, IEnumerable<KeyValuePair<string, object?>> parameters) => context.AddMultipleParameterAttribute<DisplayAttribute>(columnName, parameters);

    /// <summary>
    /// <para lang="zh">增加多参数自定义标签泛型方法</para>
    /// <para lang="en">Add multiple reference custom labels common method</para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="parameters">
    ///   <para lang="zh">参数字典</para>
    ///   <para lang="en">Parameters dictionary</para>
    /// </param>
    public static void AddMultipleParameterAttribute<TAttribute>(this DynamicObjectContext context, string columnName, IEnumerable<KeyValuePair<string, object?>> parameters) where TAttribute : Attribute
    {
        var type = typeof(TAttribute);
        var propertyInfos = new List<PropertyInfo>();
        var propertyValues = new List<object?>();
        foreach (var kv in parameters)
        {
            var pInfo = type.GetProperty(kv.Key);
            if (pInfo != null)
            {
                propertyInfos.Add(pInfo);
                propertyValues.Add(kv.Value);
            }
        }
        context.AddAttribute(columnName, type, Type.EmptyTypes, [], [.. propertyInfos], [.. propertyValues]);
    }

    /// <summary>
    /// <para lang="zh">增加 DisplayNameAttribute 扩展方法</para>
    /// <para lang="en">Add DisplayNameAttribute Extension</para>
    /// </summary>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="displayName">
    ///   <para lang="zh">显示名称</para>
    ///   <para lang="en">Display name</para>
    /// </param>
    public static void AddDisplayNameAttribute(this DynamicObjectContext context, string columnName, string displayName) => context.AddAttribute<DisplayNameAttribute>(columnName, [typeof(string)], [displayName]);

    /// <summary>
    /// <para lang="zh">增加 DescriptionAttribute 扩展方法</para>
    /// <para lang="en">Add DescriptionAttribute Extension</para>
    /// </summary>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="description">
    ///   <para lang="zh">描述</para>
    ///   <para lang="en">Description</para>
    /// </param>
    public static void AddDescriptionAttribute(this DynamicObjectContext context, string columnName, string description) => context.AddAttribute<DescriptionAttribute>(columnName, [typeof(string)], [description]);

    /// <summary>
    /// <para lang="zh">增加自定义标签泛型方法</para>
    /// <para lang="en">Add Custom Attribute Generic Method</para>
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="columnName">
    ///   <para lang="zh">列名</para>
    ///   <para lang="en">Column name</para>
    /// </param>
    /// <param name="types">
    ///   <para lang="zh">构造函数参数类型数组</para>
    ///   <para lang="en">Array of constructor parameter types</para>
    /// </param>
    /// <param name="constructorArgs">
    ///   <para lang="zh">构造函数参数值数组</para>
    ///   <para lang="en">Array of constructor parameter values</para>
    /// </param>
    /// <param name="propertyInfos">
    ///   <para lang="zh">属性信息数组</para>
    ///   <para lang="en">Array of property information</para>
    /// </param>
    /// <param name="propertyValues">
    ///   <para lang="zh">属性值数组</para>
    ///   <para lang="en">Array of property values</para>
    /// </param>
    public static void AddAttribute<TAttribute>(this DynamicObjectContext context, string columnName, Type[] types, object?[] constructorArgs, PropertyInfo[]? propertyInfos = null, object?[]? propertyValues = null) where TAttribute : Attribute
    {
        var type = typeof(TAttribute);
        context.AddAttribute(columnName, type, types, constructorArgs, propertyInfos, propertyValues);
    }

    /// <summary>
    /// <para lang="zh">扩展方法将指定模型赋值给 context 实例</para>
    /// <para lang="en">Extension method allows assigning a specified model to a context instance</para>
    /// </summary>
    /// <param name="context">
    ///   <para lang="zh">DynamicObjectContext 实例</para>
    ///   <para lang="en">DynamicObjectContext instance</para>
    /// </param>
    /// <param name="model">
    ///   <para lang="zh">模型实例</para>
    ///   <para lang="en">Model instance</para>
    /// </param>
    public static async Task SetValue(this IDynamicObjectContext context, object model)
    {
        if (model is IDynamicObject v)
        {
            var item = context.GetItems().FirstOrDefault(i => i.DynamicObjectPrimaryKey == v.DynamicObjectPrimaryKey);
            if (item != null && context.OnValueChanged != null)
            {
                foreach (var col in context.GetColumns())
                {
                    await context.OnValueChanged(item, col, v.GetValue(col.GetFieldName()));
                }
            }
        }
    }
}
