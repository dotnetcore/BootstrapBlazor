// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.Extensions.Localization;
using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Utility 帮助类
/// </summary>
public static class Utility
{
    /// <summary>
    /// 获取资源文件中 DisplayAttribute/DisplayNameAttribute 标签名称方法
    /// </summary>
    /// <param name="model">模型实例</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static string GetDisplayName(object model, string fieldName) => GetDisplayName(model.GetType(), fieldName);

    /// <summary>
    /// 获取显示名称方法
    /// </summary>
    /// <param name="modelType">模型类型</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static string GetDisplayName(Type modelType, string fieldName) => CacheManager.GetDisplayName(Nullable.GetUnderlyingType(modelType) ?? modelType, fieldName);

    /// <summary>
    /// 获取显示名称方法
    /// </summary>
    /// <typeparam name="TModel">模型</typeparam>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static string GetDisplayName<TModel>(string fieldName) => GetDisplayName(typeof(TModel), fieldName);

    /// <summary>
    /// 获取 RangeAttribute 标签值
    /// </summary>
    /// <param name="model">模型实例</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static RangeAttribute? GetRange(object model, string fieldName) => GetRange(model.GetType(), fieldName);

    /// <summary>
    /// 获得 RangeAttribute 标签值
    /// </summary>
    /// <param name="modelType">模型类型</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static RangeAttribute? GetRange(Type modelType, string fieldName) => CacheManager.GetRange(Nullable.GetUnderlyingType(modelType) ?? modelType, fieldName);

    /// <summary>
    /// 获得 RangeAttribute 标签值
    /// </summary>
    /// <typeparam name="TModel">模型</typeparam>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static RangeAttribute? GetRange<TModel>(string fieldName) => GetRange(typeof(TModel), fieldName);

    /// <summary>
    /// 获取资源文件中 NullableBoolItemsAttribute 标签名称方法
    /// </summary>
    /// <param name="model">模型实例</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static List<SelectedItem> GetNullableBoolItems(object model, string fieldName) => GetNullableBoolItems(model.GetType(), fieldName);

    /// <summary>
    /// 获取资源文件中 NullableBoolItemsAttribute 标签名称方法
    /// </summary>
    /// <param name="modelType">模型实例</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static List<SelectedItem> GetNullableBoolItems(Type modelType, string fieldName) => CacheManager.GetNullableBoolItems(modelType, fieldName);

    /// <summary>
    /// 获得 指定模型标记 <see cref="KeyAttribute"/> 的属性值
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="model"></param>
    /// <param name="customAttribute"></param>
    /// <returns></returns>
    public static TValue? GetKeyValue<TModel, TValue>(TModel model, Type? customAttribute = null) => CacheManager.GetKeyValue<TModel, TValue>(model, customAttribute);

    /// <summary>
    /// 获得 指定模型属性值
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="model"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static TResult GetPropertyValue<TModel, TResult>(TModel model, string fieldName) => CacheManager.GetPropertyValue<TModel, TResult>(model, fieldName);

    /// <summary>
    /// 获取 指定对象的属性值
    /// </summary>
    /// <param name="model"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static object? GetPropertyValue(object model, string fieldName)
    {
        return model.GetType().Assembly.IsDynamic ? ReflectionInvoke() : LambdaInvoke();

        object? ReflectionInvoke()
        {
            object? ret = null;
            var propertyInfo = model.GetType().GetRuntimeProperties().FirstOrDefault(i => i.Name == fieldName);
            if (propertyInfo != null)
            {
                ret = propertyInfo.GetValue(model);
            }
            return ret;
        }

        object? LambdaInvoke() => GetPropertyValue<object, object?>(model, fieldName);
    }

    /// <summary>
    /// 设置指定模型属性值方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="model"></param>
    /// <param name="fieldName"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static void SetPropertyValue<TModel, TValue>(TModel model, string fieldName, TValue value) => CacheManager.SetPropertyValue(model, fieldName, value);

    /// <summary>
    /// 获得 排序方法
    /// </summary>
    /// <returns></returns>
    public static Func<IEnumerable<T>, string, SortOrder, IEnumerable<T>> GetSortFunc<T>() => CacheManager.GetSortFunc<T>();

    /// <summary>
    /// 获得 通过排序集合进行排序 Func 方法
    /// </summary>
    /// <returns></returns>
    public static Func<IEnumerable<T>, List<string>, IEnumerable<T>> GetSortListFunc<T>() => CacheManager.GetSortListFunc<T>();

    /// <summary>
    /// 通过指定程序集获取所有本地化信息键值集合
    /// </summary>
    /// <param name="option">JsonLocalizationOptions 实例</param>
    /// <param name="assembly">Assembly 程序集实例</param>
    /// <param name="typeName">类名称</param>
    /// <param name="cultureName">cultureName 未空时使用 CultureInfo.CurrentUICulture.Name</param>
    /// <param name="forceLoad">默认 false 使用缓存值 设置 true 时内部强制重新加载</param>
    /// <returns></returns>
    public static IEnumerable<LocalizedString> GetJsonStringByTypeName(JsonLocalizationOptions option, Assembly assembly, string typeName, string? cultureName = null, bool forceLoad = false) => CacheManager.GetJsonStringByTypeName(option, assembly, typeName, cultureName, forceLoad) ?? [];

    /// <summary>
    /// 通过指定程序集与类型获得 IStringLocalizer 实例
    /// </summary>
    /// <param name="assembly"></param>
    /// <param name="typeName"></param>
    public static IStringLocalizer? GetStringLocalizerFromService(Assembly assembly, string typeName) => CacheManager.GetStringLocalizerFromService(assembly, typeName);

    /// <summary>
    /// 获取 PlaceHolder 方法
    /// </summary>
    /// <typeparam name="TModel">模型类型</typeparam>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static string? GetPlaceHolder<TModel>(string fieldName) => GetPlaceHolder(typeof(TModel), fieldName);

    /// <summary>
    /// 获取 PlaceHolder 方法
    /// </summary>
    /// <param name="model">模型实例</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static string? GetPlaceHolder(object model, string fieldName) => GetPlaceHolder(model.GetType(), fieldName);

    /// <summary>
    /// 获取 PlaceHolder 方法
    /// </summary>
    /// <param name="modelType">模型类型</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public static string? GetPlaceHolder(Type modelType, string fieldName) => modelType.Assembly.IsDynamic
        ? null
        : CacheManager.GetPlaceholder(modelType, fieldName);

    /// <summary>
    /// 通过 数据类型与字段名称获取 PropertyInfo 实例方法
    /// </summary>
    /// <param name="modelType"></param>
    /// <param name="fieldName"></param>
    /// <param name="propertyInfo"></param>
    /// <returns></returns>
    public static bool TryGetProperty(Type modelType, string fieldName, [NotNullWhen(true)] out PropertyInfo? propertyInfo) => CacheManager.TryGetProperty(modelType, fieldName, out propertyInfo);

    /// <summary>
    /// 重置对象属性值到默认值方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public static void Reset<TModel>(TModel source) where TModel : class, new()
    {
        var v = new TModel();
        foreach (var pi in source.GetType().GetRuntimeProperties().Where(p => p.CanWrite))
        {
            var pInfo = v.GetType().GetPropertyByName(pi.Name);
            if (pInfo != null)
            {
                pi.SetValue(source, pInfo.GetValue(v));
            }
        }
    }

    /// <summary>
    /// 泛型 Clone 方法
    /// <para>仅克隆类 公开 Field 与 Property</para>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="item">克隆对象</param>
    /// <remarks>简单的深克隆方法，内部未使用序列化技术</remarks>
    /// <returns></returns>
    public static TModel Clone<TModel>(TModel item)
    {
        var ret = item;
        if (item != null)
        {
            if (item is ICloneable cloneable)
            {
                ret = (TModel)cloneable.Clone();
            }
            else
            {
                var type = item.GetType();
                if (type.IsClass)
                {
                    var newVal = (TModel)Activator.CreateInstance(type)!;
                    newVal.Clone(item);
                    ret = newVal;
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// 泛型 Copy 方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static void Copy<TModel>(TModel source, TModel destination) where TModel : class
    {
        var type = source.GetType();
        var valType = destination.GetType();
        foreach (var f in type.GetFields())
        {
            var v = f.GetValue(source);
            var field = valType.GetField(f.Name)!;
            field.SetValue(destination, v);
        }
        foreach (var p in type.GetRuntimeProperties())
        {
            if (p.CanWrite)
            {
                var v = p.GetValue(source);
                var property = valType.GetRuntimeProperties().First(i => i.Name == p.Name && i.PropertyType == p.PropertyType);
                property.SetValue(destination, v);
            }
        }
    }

    #region GenerateColumns

    /// <summary>
    /// 通过特定类型模型获取模型属性集合
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="source"></param>
    /// <param name="defaultOrderCallback">默认排序回调方法</param>
    /// <returns></returns>
    public static IEnumerable<ITableColumn> GetTableColumns<TModel>(IEnumerable<ITableColumn>? source = null, Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>? defaultOrderCallback = null) => GetTableColumns(typeof(TModel), source, defaultOrderCallback);

    /// <summary>
    /// 通过特定类型模型获取模型属性集合
    /// </summary>
    /// <param name="type">绑定模型类型</param>
    /// <param name="source">Razor 文件中列集合</param>
    /// <param name="defaultOrderCallback">默认排序回调方法</param>
    /// <returns></returns>
    public static IEnumerable<ITableColumn> GetTableColumns(Type type, IEnumerable<ITableColumn>? source = null, Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>? defaultOrderCallback = null)
    {
        var columns = new List<ITableColumn>();
        if (source != null)
        {
            columns.AddRange(source);
        }

        var cols = new List<ITableColumn>(50);
        var metadataType = TableMetadataTypeService.GetMetadataType(type);
        var classAttribute = metadataType.GetCustomAttribute<AutoGenerateClassAttribute>(true);
        // to make it simple, we just check the property name should exist in target data type properties
        var targetProperties = type.GetProperties().Where(p => !p.IsStatic());
        var props = metadataType.GetProperties().Where(p => !p.IsStatic() && targetProperties.Any(o => o.Name == p.Name));
        foreach (var prop in props)
        {
            ITableColumn? tc;
            var columnAttribute = prop.GetCustomAttribute<AutoGenerateColumnAttribute>(true);
            var displayName = columnAttribute?.Text ?? GetDisplayName(metadataType, prop.Name);
            if (columnAttribute == null)
            {
                // 未设置 AutoGenerateColumnAttribute 时使用默认值
                tc = new InternalTableColumn(prop.Name, prop.PropertyType, displayName);

                if (classAttribute != null)
                {
                    // AutoGenerateClassAttribute 设置时继承类标签
                    tc.InheritValue(classAttribute);
                }
            }
            else
            {
                // 设置 AutoGenerateColumnAttribute 时
                if (columnAttribute.Ignore) continue;

                columnAttribute.Text = displayName;
                columnAttribute.FieldName = prop.Name;
                columnAttribute.PropertyType = prop.PropertyType;

                if (classAttribute != null)
                {
                    // AutoGenerateClassAttribute 设置时继承类标签
                    columnAttribute.InheritValue(classAttribute);
                }
                tc = columnAttribute;
            }

            // 替换属性 手写优先
            var col = columns.Find(c => c.GetFieldName() == tc.GetFieldName());
            if (col != null)
            {
                tc.CopyValue(col);
                columns.Remove(col);
            }
            cols.Add(tc);
        }

        if (columns.Count > 0)
        {
            cols.AddRange(columns);
        }
        return defaultOrderCallback?.Invoke(cols) ?? cols.OrderFunc();
    }

    private static IEnumerable<ITableColumn> OrderFunc(this List<ITableColumn> cols) => cols.Where(a => a.Order > 0).OrderBy(a => a.Order)
        .Concat(cols.Where(a => a.Order == 0))
        .Concat(cols.Where(a => a.Order < 0).OrderBy(a => a.Order));

    /// <summary>
    /// 通过指定 Model 获得 IEditorItem 集合方法
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<ITableColumn> GenerateColumns<TModel>(Func<ITableColumn, bool> predicate) => GetTableColumns<TModel>().Where(predicate);

    /// <summary>
    /// RenderTreeBuilder 扩展方法 通过 IEditorItem 与 model 创建 Display 组件
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="item"></param>
    /// <param name="model"></param>
    public static void CreateDisplayByFieldType(this RenderTreeBuilder builder, IEditorItem item, object model)
    {
        var fieldType = item.PropertyType;
        var fieldName = item.GetFieldName();
        var displayName = item.GetDisplayName() ?? GetDisplayName(model, fieldName);
        var fieldValue = GenerateValue(model, fieldName);
        var type = (Nullable.GetUnderlyingType(fieldType) ?? fieldType);
        if (type == typeof(bool) || fieldValue?.GetType() == typeof(bool))
        {
            builder.OpenComponent<Switch>(0);
            builder.AddAttribute(10, nameof(Switch.Value), fieldValue);
            builder.AddAttribute(20, nameof(Switch.IsDisabled), true);
            builder.AddAttribute(30, nameof(Switch.DisplayText), displayName);
            builder.AddAttribute(40, nameof(Switch.ShowLabelTooltip), item.ShowLabelTooltip);
            if (item is ITableColumn col)
            {
                builder.AddAttribute(50, "class", col.CssClass);
            }
            builder.AddMultipleAttributes(60, item.ComponentParameters);
            builder.CloseComponent();
        }
        else if (item.ComponentType == typeof(Textarea) || item.Rows > 0)
        {
            builder.OpenComponent(0, typeof(Textarea));
            builder.AddAttribute(10, nameof(Textarea.DisplayText), displayName);
            builder.AddAttribute(20, nameof(Textarea.Value), fieldValue);
            builder.AddAttribute(30, nameof(Textarea.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(40, "readonly", true);
            if (item.Rows > 0)
            {
                builder.AddAttribute(50, "rows", item.Rows);
            }
            if (item is ITableColumn col)
            {
                builder.AddAttribute(60, "class", col.CssClass);
            }
            builder.AddMultipleAttributes(70, item.ComponentParameters);
            builder.CloseComponent();
        }
        else
        {
            builder.OpenComponent(0, typeof(Display<>).MakeGenericType(fieldType));
            builder.AddAttribute(10, nameof(Display<string>.DisplayText), displayName);
            builder.AddAttribute(20, nameof(Display<string>.Value), fieldValue);
            builder.AddAttribute(30, nameof(Display<string>.LookupServiceKey), item.LookupServiceKey);
            builder.AddAttribute(40, nameof(Display<string>.LookupServiceData), item.LookupServiceData);
            builder.AddAttribute(50, nameof(Display<string>.Lookup), item.Lookup);
            builder.AddAttribute(60, nameof(Display<string>.ShowLabelTooltip), item.ShowLabelTooltip);
            if (item is ITableColumn col)
            {
                if (col.Formatter != null)
                {
                    builder.AddAttribute(70, nameof(Display<string>.FormatterAsync), CacheManager.GetFormatterInvoker(fieldType, col.Formatter));
                }
                else if (!string.IsNullOrEmpty(col.FormatString))
                {
                    builder.AddAttribute(80, nameof(Display<string>.FormatString), col.FormatString);
                }
                builder.AddAttribute(90, "class", col.CssClass);
            }
            builder.AddMultipleAttributes(100, item.ComponentParameters);
            builder.CloseComponent();
        }
    }

    /// <summary>
    /// RenderTreeBuilder 扩展方法 通过指定模型与属性生成编辑组件
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="model"></param>
    /// <param name="component"></param>
    /// <param name="item"></param>
    /// <param name="changedType"></param>
    /// <param name="isSearch"></param>
    /// <param name="lookUpService"></param>
    public static void CreateComponentByFieldType(this RenderTreeBuilder builder, ComponentBase component, IEditorItem item, object model, ItemChangedType changedType = ItemChangedType.Update, bool isSearch = false, ILookupService? lookUpService = null)
    {
        var fieldType = item.PropertyType;
        var fieldName = item.GetFieldName();
        var displayName = item.GetDisplayName() ?? GetDisplayName(model, fieldName);

        var fieldValue = GenerateValue(model, fieldName);
        var fieldValueChanged = GenerateValueChanged(component, model, fieldName, fieldType);
        var valueExpression = GenerateValueExpression(model, fieldName, fieldType);
        var lookup = item.Lookup ?? lookUpService?.GetItemsByKey(item.LookupServiceKey, item.LookupServiceData);
        var componentType = item.ComponentType ?? GenerateComponentType(fieldType, item.Rows != 0, lookup);
        builder.OpenComponent(0, componentType);
        if (componentType.IsSubclassOf(typeof(ValidateBase<>).MakeGenericType(fieldType)))
        {
            builder.AddAttribute(10, nameof(ValidateBase<string>.DisplayText), displayName);
            builder.AddAttribute(20, nameof(ValidateBase<string>.Value), fieldValue);
            builder.AddAttribute(30, nameof(ValidateBase<string>.ValueChanged), fieldValueChanged);
            builder.AddAttribute(40, nameof(ValidateBase<string>.ValueExpression), valueExpression);

            if (!item.CanWrite(model.GetType(), changedType, isSearch))
            {
                builder.AddAttribute(50, nameof(ValidateBase<string>.IsDisabled), true);
            }

            if (item.ValidateRules != null)
            {
                builder.AddAttribute(60, nameof(ValidateBase<string>.ValidateRules), item.ValidateRules);
            }

            if (item.ShowLabelTooltip != null)
            {
                builder.AddAttribute(70, nameof(ValidateBase<string>.ShowLabelTooltip), item.ShowLabelTooltip);
            }
        }

        if (componentType == typeof(NullSwitch) && TryGetProperty(model.GetType(), fieldName, out var propertyInfo))
        {
            // 读取默认值
            var defaultValueAttr = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null)
            {
                var dv = defaultValueAttr.Value is true;
                builder.AddAttribute(80, nameof(NullSwitch.DefaultValueWhenNull), dv);
            }
        }

        if (IsCheckboxList(fieldType, componentType) && item.Items != null)
        {
            builder.AddAttribute(90, nameof(CheckboxList<IEnumerable<string>>.Items), item.Items.Clone());
        }

        // Nullable<bool?>
        if (item.ComponentType == typeof(Select<bool?>) && fieldType == typeof(bool?) && lookup == null && item.Items == null)
        {
            builder.AddAttribute(100, nameof(Select<bool?>.Items), GetNullableBoolItems(model, fieldName));
        }

        // Lookup
        if (lookup != null && item.Items == null)
        {
            builder.AddAttribute(110, nameof(Select<SelectedItem>.ShowSearch), item.ShowSearchWhenSelect);
            builder.AddAttribute(120, nameof(Select<SelectedItem>.Items), lookup.Clone());
            builder.AddAttribute(130, nameof(Select<SelectedItem>.StringComparison), item.LookupStringComparison);
        }

        // 增加非枚举类,手动设定 ComponentType 为 Select 并且 Data 有值 自动生成下拉框
        if (item.Items != null && item.ComponentType == typeof(Select<>).MakeGenericType(fieldType))
        {
            builder.AddAttribute(140, nameof(Select<SelectedItem>.Items), item.Items.Clone());
            builder.AddAttribute(150, nameof(Select<SelectedItem>.ShowSearch), item.ShowSearchWhenSelect);
        }

        // 设置 SkipValidate 参数
        if (IsValidComponent(componentType))
        {
            builder.AddAttribute(160, nameof(IEditorItem.SkipValidate), item.SkipValidate);
        }

        builder.AddMultipleAttributes(170, CreateMultipleAttributes(fieldType, model, fieldName, item));

        builder.AddMultipleAttributes(180, item.ComponentParameters);

        // 设置 IsPopover
        if (componentType.GetPropertyByName(nameof(Select<string>.IsPopover)) != null)
        {
            builder.AddAttribute(190, nameof(Select<string>.IsPopover), item.IsPopover);
        }
        builder.CloseComponent();
    }

    private static List<SelectedItem> Clone(this IEnumerable<SelectedItem> source) => source.Select(d => new SelectedItem(d.Value, d.Text)
    {
        Active = d.Active,
        IsDisabled = d.IsDisabled,
        GroupName = d.GroupName
    }).ToList();

    private static object? GenerateValue(object model, string fieldName) => GetPropertyValue<object, object?>(model, fieldName);

    /// <summary>
    /// 通过指定类型实例获取属性 Lambda 表达式
    /// </summary>
    /// <param name="model"></param>
    /// <param name="fieldName"></param>
    /// <param name="fieldType"></param>
    /// <returns></returns>
    public static object GenerateValueExpression(object model, string fieldName, Type fieldType)
    {
        var type = model.GetType();
        return fieldName.Contains('.') ? ComplexPropertyValueExpression() : SimplePropertyValueExpression();

        object SimplePropertyValueExpression()
        {
            // ValueExpression
            var pi = type.GetPropertyByName(fieldName) ?? throw new InvalidOperationException($"the model {type.Name} not found the property {fieldName}");
            var body = Expression.Property(Expression.Constant(model), pi);
            var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
            return Expression.Lambda(tDelegate, body);
        }

        object ComplexPropertyValueExpression()
        {
            var propertyNames = fieldName.Split(".");
            Expression? body = null;
            Type t = type;
            object? propertyInstance = model;
            foreach (var name in propertyNames)
            {
                var p = t.GetPropertyByName(name) ?? throw new InvalidOperationException($"the model {model.GetType().Name} not found the property {fieldName}");
                propertyInstance = p.GetValue(propertyInstance);
                if (propertyInstance != null)
                {
                    t = propertyInstance.GetType();
                }
                body = Expression.Property(body ?? Expression.Convert(Expression.Constant(model), type), p);
            }
            var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
            return Expression.Lambda(tDelegate, body!);
        }
    }

    /// <summary>
    /// 通过指定类型生成组件类型
    /// </summary>
    /// <param name="fieldType"></param>
    /// <param name="hasRows">是否为 TextArea 组件</param>
    /// <param name="lookup"></param>
    /// <returns></returns>
    private static Type GenerateComponentType(Type fieldType, bool hasRows, IEnumerable<SelectedItem>? lookup)
    {
        Type? ret = null;
        var type = (Nullable.GetUnderlyingType(fieldType) ?? fieldType);
        if (type.IsEnum || lookup != null)
        {
            ret = typeof(Select<>).MakeGenericType(fieldType);
        }
        else if (IsCheckboxList(type))
        {
            ret = typeof(CheckboxList<IEnumerable<string>>);
        }
        else if (fieldType == typeof(bool?))
        {
            ret = typeof(NullSwitch);
        }
        else if (fieldType.IsNumber())
        {
            ret = typeof(BootstrapInputNumber<>).MakeGenericType(fieldType);
        }
        else if (fieldType.IsDateTime())
        {
            ret = typeof(DateTimePicker<>).MakeGenericType(fieldType);
        }
        else if (fieldType.IsBoolean())
        {
            ret = typeof(Switch);
        }
        else if (fieldType == typeof(string))
        {
            ret = hasRows ? typeof(Textarea) : typeof(BootstrapInput<>).MakeGenericType(typeof(string));
        }
        return ret ?? typeof(BootstrapInput<>).MakeGenericType(fieldType);
    }

    /// <summary>
    /// 通过指定数据类型判断是否可使用 CheckboxList 进行渲染
    /// </summary>
    /// <param name="fieldType"></param>
    /// <param name="componentType">组件类型</param>
    /// <returns></returns>
    private static bool IsCheckboxList(Type fieldType, Type? componentType = null)
    {
        var ret = false;
        if (componentType != null)
        {
            ret = componentType.IsGenericType && componentType.GetGenericTypeDefinition() == typeof(CheckboxList<>);
        }
        if (!ret)
        {
            var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
            ret = type.IsAssignableTo(typeof(IEnumerable<string>));
        }
        return ret;
    }

    private static bool IsValidComponent(Type componentType) => Array.Find(componentType.GetProperties(), p => p.Name == nameof(IEditorItem.SkipValidate)) != null;

    /// <summary>
    /// 通过模型与指定数据类型生成组件参数集合
    /// </summary>
    /// <param name="fieldType">待编辑数据类型</param>
    /// <param name="model">上下文模型</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="item">IEditorItem 实例</param>
    /// <returns></returns>
    private static Dictionary<string, object> CreateMultipleAttributes(Type fieldType, object model, string fieldName, IEditorItem item)
    {
        var ret = new Dictionary<string, object>();
        var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
        if (type.Name == nameof(String))
        {
            var ph = item.PlaceHolder ?? GetPlaceHolder(model, fieldName);
            if (ph != null)
            {
                ret.Add("placeholder", ph);
            }
            if (item.Rows != 0)
            {
                ret.Add("rows", item.Rows);
            }
        }
        else if (type.IsNumber())
        {
            if (!string.IsNullOrEmpty(item.Step))
            {
                ret.Add("Step", item.Step);
            }
        }
        return ret;

    }

    /// <summary>
    /// 创建 <see cref="Func{T, TResult}"/> 委托方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TType"></typeparam>
    /// <param name="model"></param>
    /// <param name="col"></param>
    /// <param name="callback"></param>
    /// <returns></returns>
    public static Func<TType, Task> CreateOnValueChangedCallback<TModel, TType>(TModel model, ITableColumn col, Func<TModel, ITableColumn, object?, Task> callback) => v => callback(model, col, v);

    /// <summary>
    /// 创建 OnValueChanged 回调委托
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="fieldType"></param>
    /// <returns></returns>
    public static Expression<Func<TModel, ITableColumn, Func<TModel, ITableColumn, object?, Task>, object>> CreateOnValueChanged<TModel>(Type fieldType)
    {
        var method = typeof(Utility).GetMethod(nameof(CreateOnValueChangedCallback), BindingFlags.Static | BindingFlags.Public)!.MakeGenericMethod(typeof(TModel), fieldType);
        var exp_p1 = Expression.Parameter(typeof(TModel));
        var exp_p2 = Expression.Parameter(typeof(ITableColumn));
        var exp_p3 = Expression.Parameter(typeof(Func<,,,>).MakeGenericType(typeof(TModel), typeof(ITableColumn), typeof(object), typeof(Task)));
        var body = Expression.Call(null, method, exp_p1, exp_p2, exp_p3);

        return Expression.Lambda<Func<TModel, ITableColumn, Func<TModel, ITableColumn, object?, Task>, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2, exp_p3);
    }

    /// <summary>
    /// 创建 OnValueChanged 回调委托
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="fieldType"></param>
    /// <returns></returns>
    public static Func<TModel, ITableColumn, Func<TModel, ITableColumn, object?, Task>, object> GetOnValueChangedInvoke<TModel>(Type fieldType) => CacheManager.GetOnValueChangedInvoke<TModel>(fieldType);
    #endregion

    #region Format
    /// <summary>
    /// 任意类型格式化方法
    /// </summary>
    /// <param name="source"></param>
    /// <param name="format"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static string Format(object? source, string format, IFormatProvider? provider = null)
    {
        var ret = string.Empty;
        if (source != null)
        {
            var invoker = CacheManager.GetFormatInvoker(source.GetType());
            ret = invoker(source, format, provider);
        }
        return ret;
    }

    /// <summary>
    /// 任意类型格式化方法
    /// </summary>
    /// <param name="source"></param>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static string Format(object? source, IFormatProvider provider)
    {
        var ret = string.Empty;
        if (source != null)
        {
            var invoker = CacheManager.GetFormatProviderInvoker(source.GetType());
            ret = invoker(source, provider);
        }
        return ret;
    }
    #endregion

    /// <summary>
    /// 转换泛型类型为字符串方法
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string? ConvertValueToString<TValue>(TValue value)
    {
        var ret = "";
        var typeValue = typeof(TValue);
        if (typeValue == typeof(string))
        {
            ret = value!.ToString();
        }
        else if (typeValue.IsGenericType || typeValue.IsArray)
        {
            var t = typeValue.IsGenericType ? typeValue.GenericTypeArguments[0] : typeValue.GetElementType();
            if (t != null)
            {
                var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
                if (instance != null)
                {
                    var mi = instance.GetType().GetMethod(nameof(List<string>.AddRange));
                    if (mi != null)
                    {
                        mi.Invoke(instance, [value]);
                        var invoker = CacheManager.CreateConverterInvoker(t);
                        var v = invoker.Invoke(instance);
                        ret = string.Join(",", v);
                    }
                }
            }
        }
        return ret;
    }

    /// <summary>
    /// 获得 ValueChanged 回调委托
    /// </summary>
    /// <param name="component"></param>
    /// <param name="model"></param>
    /// <param name="fieldName"></param>
    /// <param name="fieldType"></param>
    /// <returns></returns>
    public static object GenerateValueChanged(ComponentBase component, object model, string fieldName, Type fieldType)
    {
        var valueChangedInvoker = CreateLambda(fieldType).Compile();
        return valueChangedInvoker(component, model, fieldName);

        static Expression<Func<ComponentBase, object, string, object>> CreateLambda(Type fieldType)
        {
            var exp_p1 = Expression.Parameter(typeof(ComponentBase));
            var exp_p2 = Expression.Parameter(typeof(object));
            var exp_p3 = Expression.Parameter(typeof(string));
            var method = typeof(Utility).GetMethod(nameof(CreateCallback), BindingFlags.Static | BindingFlags.Public)!.MakeGenericMethod(fieldType);
            var body = Expression.Call(null, method, exp_p1, exp_p2, exp_p3);

            return Expression.Lambda<Func<ComponentBase, object, string, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2, exp_p3);
        }
    }

    /// <summary>
    /// 创建 <see cref="EventCallback{TValue}"/> 方法
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <param name="component"></param>
    /// <param name="model"></param>
    /// <param name="fieldName"></param>
    /// <returns></returns>
    public static EventCallback<TType> CreateCallback<TType>(ComponentBase component, object model, string fieldName) => EventCallback.Factory.Create<TType>(component, t => CacheManager.SetPropertyValue(model, fieldName, t));

    /// <summary>
    /// 获得指定泛型的 IEditorItem 集合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<IEditorItem> GenerateEditorItems<TModel>(IEnumerable<ITableColumn>? source = null) => GetTableColumns<TModel>(source);

    /// <summary>
    /// 通过指定类型创建 IStringLocalizer 实例
    /// </summary>
    /// <typeparam name="TType"></typeparam>
    /// <returns></returns>
    public static IStringLocalizer? CreateLocalizer<TType>() => CreateLocalizer(typeof(TType));

    /// <summary>
    /// 通过指定类型创建 IStringLocalizer 实例
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static IStringLocalizer? CreateLocalizer(Type type) => CacheManager.CreateLocalizerByType(type);
}
