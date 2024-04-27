// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// IEditorItem 扩展方法
/// </summary>
public static class IEditorItemExtensions
{
    /// <summary>
    /// 判断当前 IEditorItem 实例是否可以编辑
    /// </summary>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static bool IsEditable(this IEditorItem item, ItemChangedType changedType, bool search = false) => search || !item.IsReadonly(changedType);

    private static bool IsReadonly(this IEditorItem item, ItemChangedType changedType)
    {
        bool ret = item.Readonly;
        if (item is ITableColumn col)
        {
            ret = changedType switch
            {
                ItemChangedType.Add => col.IsReadonlyWhenAdd ?? col.Readonly,
                _ => col.IsReadonlyWhenEdit ?? col.Readonly
            };
        }
        return ret;
    }

    /// <summary>
    /// 判断当前 IEditorItem 实例是否显示
    /// </summary>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static bool IsVisible(this IEditorItem item, ItemChangedType changedType, bool search = false) => search || item.IsVisible(changedType);

    private static bool IsVisible(this IEditorItem item, ItemChangedType changedType)
    {
        // IEditorItem 无 Visible 属性
        bool ret = !item.Ignore;
        if (item is ITableColumn col)
        {
            ret = changedType switch
            {
                ItemChangedType.Add => col.IsVisibleWhenAdd ?? col.Visible,
                _ => col.IsVisibleWhenEdit ?? col.Visible
            };
        }
        return ret;
    }

    /// <summary>
    /// 判断当前 IEditorItem 示例是否可以编辑
    /// </summary>
    /// <param name="item"></param>
    /// <param name="modelType"></param>
    /// <param name="changedType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
    public static bool CanWrite(this IEditorItem item, Type modelType, ItemChangedType changedType, bool search = false) => item.CanWrite(modelType) && item.IsEditable(changedType, search);

    /// <summary>
    /// 判断模型是否可写
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
            var propertyNames = fieldName.Split('.');
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
                ret = propertyInfo.CanWrite;
            }
            return ret;
        }
    }
}
