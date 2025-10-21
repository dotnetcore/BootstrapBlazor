// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// DynamicObjectContext 扩展方法辅助类
/// </summary>
public static class DynamicObjectContextExtensions
{
    /// <summary>
    /// 增加 RequiredAttribute 扩展方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="errorMessage"></param>
    /// <param name="allowEmptyStrings"></param>
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
    /// 增加 AutoGenerateColumnAttribute 扩展方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="parameters"></param>
    public static void AddAutoGenerateColumnAttribute(this DynamicObjectContext context, string columnName, IEnumerable<KeyValuePair<string, object?>> parameters) => context.AddMultipleParameterAttribute<AutoGenerateColumnAttribute>(columnName, parameters);

    /// <summary>
    /// 增加 DisplayAttribute 扩展方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="parameters"></param>
    public static void AddDisplayAttribute(this DynamicObjectContext context, string columnName, IEnumerable<KeyValuePair<string, object?>> parameters) => context.AddMultipleParameterAttribute<DisplayAttribute>(columnName, parameters);

    /// <summary>
    /// 增加多参数自定义标签泛型方法
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="parameters"></param>
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
    /// 增加 DisplayNameAttribute 扩展方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="displayName"></param>
    public static void AddDisplayNameAttribute(this DynamicObjectContext context, string columnName, string displayName) => context.AddAttribute<DisplayNameAttribute>(columnName, [typeof(string)], [displayName]);

    /// <summary>
    /// 增加 DescriptionAttribute 扩展方法
    /// </summary>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="description"></param>
    public static void AddDescriptionAttribute(this DynamicObjectContext context, string columnName, string description) => context.AddAttribute<DescriptionAttribute>(columnName, [typeof(string)], [description]);

    /// <summary>
    /// 增加自定义标签泛型方法
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    /// <param name="context"></param>
    /// <param name="columnName"></param>
    /// <param name="types"></param>
    /// <param name="constructorArgs"></param>
    /// <param name="propertyInfos"></param>
    /// <param name="propertyValues"></param>
    public static void AddAttribute<TAttribute>(this DynamicObjectContext context, string columnName, Type[] types, object?[] constructorArgs, PropertyInfo[]? propertyInfos = null, object?[]? propertyValues = null) where TAttribute : Attribute
    {
        var type = typeof(TAttribute);
        context.AddAttribute(columnName, type, types, constructorArgs, propertyInfos, propertyValues);
    }

    /// <summary>
    /// 扩展方法将指定模型赋值给 context 实例
    /// </summary>
    /// <param name="context">DynamicObjectContext 实例</param>
    /// <param name="model">模型实例</param>
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
