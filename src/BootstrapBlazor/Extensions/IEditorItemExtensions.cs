// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IEditorItem 扩展方法</para>
///  <para lang="en">IEditorItem extension methods</para>
/// </summary>
public static class IEditorItemExtensions
{
    /// <summary>
    ///  <para lang="zh">判断当前 IEditorItem 实例是否可以编辑</para>
    ///  <para lang="en">Determine whether the current IEditorItem instance is editable</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static bool IsEditable(this IEditorItem item, ItemChangedType changedType, bool search = false) => search || !item.IsReadonly(changedType);

    private static bool IsReadonly(this IEditorItem item, ItemChangedType changedType)
    {
        bool ret = item.GetReadonly();
        if (item is ITableColumn col)
        {
            ret = changedType switch
            {
                ItemChangedType.Add => (col.IsReadonlyWhenAdd ?? false) || col.GetReadonly(),
                _ => (col.IsReadonlyWhenEdit ?? false) || col.GetReadonly()
            };
        }
        return ret;
    }

    /// <summary>
    ///  <para lang="zh">判断当前 IEditorItem 实例是否显示</para>
    ///  <para lang="en">Determine whether the current IEditorItem instance is visible</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static bool IsVisible(this IEditorItem item, ItemChangedType changedType, bool search = false) => search || item.IsVisible(changedType);

    private static bool IsVisible(this IEditorItem item, ItemChangedType changedType)
    {
        // <para lang="zh">IEditorItem 无 Visible 属性</para>
        // <para lang="en">IEditorItem has no Visible property</para>
        bool ret = !item.GetIgnore();
        if (item is ITableColumn col)
        {
            ret = changedType switch
            {
                ItemChangedType.Add => col.IsVisibleWhenAdd ?? col.GetVisible(),
                _ => col.IsVisibleWhenEdit ?? col.GetVisible()
            };
        }
        return ret;
    }

    /// <summary>
    ///  <para lang="zh">判断当前 IEditorItem 示例是否可以编辑</para>
    ///  <para lang="en">Determine whether the current IEditorItem instance is editable</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="modelType"></param>
    /// <param name="changedType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static bool CanWrite(this IEditorItem item, Type modelType, ItemChangedType changedType, bool search = false) => item.CanWrite(modelType) && item.IsEditable(changedType, search);

    /// <summary>
    ///  <para lang="zh">判断模型是否可写</para>
    ///  <para lang="en">Determine whether the model is writable</para>
    /// </summary>
    /// <param name="item"></param>
    /// <param name="modelType"></param>
    /// <returns></returns>
    public static bool CanWrite(this IEditorItem item, Type modelType)
    {
        return modelType == typeof(DynamicObject) || modelType.IsSubclassOf(typeof(DynamicObject)) || ComplexCanWrite();

        bool ComplexCanWrite()
        {
            var ret = false;
            var fieldName = item.GetFieldName();
            var propertyNames = fieldName.Split('.', StringSplitOptions.RemoveEmptyEntries);
            PropertyInfo? propertyInfo = null;
            Type? propertyType = null;
            foreach (var name in propertyNames)
            {
                if (propertyType == null)
                {
                    propertyInfo = modelType.GetPropertyByName(name) ?? throw new InvalidOperationException();
                    propertyType = propertyInfo.PropertyType;
                }
                else
                {
                    propertyInfo = propertyType.GetPropertyByName(name) ?? throw new InvalidOperationException();
                    propertyType = propertyInfo.PropertyType;
                }
            }
            if (propertyInfo != null)
            {
                ret = propertyInfo.IsCanWrite();
            }
            return ret;
        }
    }

    internal static bool GetIgnore(this IEditorItem col) => col.Ignore ?? false;

    internal static bool GetReadonly(this IEditorItem col) => col.Readonly ?? false;
}
