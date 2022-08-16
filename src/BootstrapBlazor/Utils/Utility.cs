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
    public static TValue GetKeyValue<TModel, TValue>(TModel model, Type? customAttribute = null) => CacheManager.GetKeyValue<TModel, TValue>(model, customAttribute);

    /// <summary>
    /// 
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
    /// 
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
    public static IEnumerable<LocalizedString> GetJsonStringByTypeName(JsonLocalizationOptions option, Assembly assembly, string typeName, string? cultureName = null, bool forceLoad = false) => CacheManager.GetJsonStringByTypeName(option, assembly, typeName, cultureName, forceLoad) ?? Enumerable.Empty<LocalizedString>();

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
            var pinfo = v.GetType().GetPropertyByName(pi.Name);
            if (pinfo != null)
            {
                pi.SetValue(source, pinfo.GetValue(v));
            }
        }
    }

    /// <summary>
    /// 泛型 Clone 方法
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="item"></param>
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
                    var instance = Activator.CreateInstance(type);
                    if (instance != null)
                    {
                        ret = (TModel)instance;
                        if (ret != null)
                        {
                            var valType = ret.GetType();

                            // 20200608 tian_teng@outlook.com 支持字段和只读属性
                            foreach (var f in type.GetFields())
                            {
                                var v = f.GetValue(item);
                                var field = valType.GetField(f.Name);
                                if (field != null)
                                {
                                    field.SetValue(ret, v);
                                }
                            };
                            foreach (var p in type.GetRuntimeProperties())
                            {
                                if (p.CanWrite)
                                {
                                    var v = p.GetValue(item);
                                    var property = valType.GetRuntimeProperties().FirstOrDefault(i => i.Name == p.Name && i.PropertyType == p.PropertyType);
                                    if (property != null)
                                    {
                                        property.SetValue(ret, v);
                                    }
                                }
                            };
                        }
                    }
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
        if (valType != null)
        {
            type.GetFields().ToList().ForEach(f =>
            {
                var v = f.GetValue(source);
                valType.GetField(f.Name)!.SetValue(destination, v);
            });
            type.GetRuntimeProperties().ToList().ForEach(p =>
            {
                if (p.CanWrite)
                {
                    var v = p.GetValue(source);
                    valType.GetProperty(p.Name)!.SetValue(destination, v);
                }
            });
        }
    }

    #region GenerateColumns
    /// <summary>
    /// 通过指定 Model 获得 IEditorItem 集合方法
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static IEnumerable<ITableColumn> GenerateColumns<TModel>(Func<ITableColumn, bool> predicate) => InternalTableColumn.GetProperties<TModel>().Where(predicate);

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
            builder.AddAttribute(1, nameof(Switch.Value), fieldValue);
            builder.AddAttribute(2, nameof(Switch.IsDisabled), true);
            builder.AddAttribute(3, nameof(Switch.DisplayText), displayName);
            builder.AddAttribute(4, nameof(Switch.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.CloseComponent();
        }
        else if (item.ComponentType == typeof(Textarea))
        {
            builder.OpenComponent(0, typeof(Textarea));
            builder.AddAttribute(1, nameof(Textarea.DisplayText), displayName);
            builder.AddAttribute(2, nameof(Textarea.Value), fieldValue);
            builder.AddAttribute(3, nameof(Textarea.ShowLabelTooltip), item.ShowLabelTooltip);
            builder.AddAttribute(4, "readonly", true);
            if (item.Rows > 0)
            {
                builder.AddAttribute(5, "rows", item.Rows);
            }
            builder.CloseComponent();
        }
        else
        {
            builder.OpenComponent(0, typeof(Display<>).MakeGenericType(fieldType));
            builder.AddAttribute(1, nameof(Display<string>.DisplayText), displayName);
            builder.AddAttribute(2, nameof(Display<string>.Value), fieldValue);
            builder.AddAttribute(3, nameof(Display<string>.LookupServiceKey), item.LookupServiceKey);
            builder.AddAttribute(4, nameof(Display<string>.ShowLabelTooltip), item.ShowLabelTooltip);
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
        var lookup = item.Lookup ?? lookUpService?.GetItemsByKey(item.LookupServiceKey);
        var componentType = item.ComponentType ?? GenerateComponentType(fieldType, item.Rows != 0, lookup);
        builder.OpenComponent(0, componentType);
        if (componentType.IsSubclassOf(typeof(ValidateBase<>).MakeGenericType(fieldType)))
        {
            builder.AddAttribute(1, nameof(ValidateBase<string>.DisplayText), displayName);
            builder.AddAttribute(2, nameof(ValidateBase<string>.Value), fieldValue);
            builder.AddAttribute(3, nameof(ValidateBase<string>.ValueChanged), fieldValueChanged);
            builder.AddAttribute(4, nameof(ValidateBase<string>.ValueExpression), valueExpression);

            if (!item.CanWrite(model.GetType(), changedType, isSearch))
            {
                builder.AddAttribute(5, nameof(ValidateBase<string>.IsDisabled), true);
            }

            if (item.ValidateRules != null)
            {
                builder.AddAttribute(6, nameof(ValidateBase<string>.ValidateRules), item.ValidateRules);
            }

            if (item.ShowLabelTooltip != null)
            {
                builder.AddAttribute(7, nameof(ValidateBase<string>.ShowLabelTooltip), item.ShowLabelTooltip);
            }
        }

        if (componentType == typeof(NullSwitch) && TryGetProperty(model.GetType(), fieldName, out var propertyInfo))
        {
            // 读取默认值
            var defaultValueAttr = propertyInfo.GetCustomAttribute<DefaultValueAttribute>();
            if (defaultValueAttr != null)
            {
                var dv = defaultValueAttr.Value is bool v && v;
                builder.AddAttribute(8, nameof(NullSwitch.DefaultValueWhenNull), dv);
            }
        }

        if (IsCheckboxList(fieldType, componentType) && item.Items != null)
        {
            builder.AddAttribute(9, nameof(CheckboxList<IEnumerable<string>>.Items), item.Items.Clone());
        }

        // Nullabl<bool?>
        if (item.ComponentType == typeof(Select<bool?>) && fieldType == typeof(bool?) && lookup == null && item.Items == null)
        {
            builder.AddAttribute(10, nameof(Select<bool?>.Items), GetNullableBoolItems(model, fieldName));
        }

        // Lookup
        if (lookup != null && item.Items == null)
        {
            builder.AddAttribute(11, nameof(Select<SelectedItem>.ShowSearch), true);
            builder.AddAttribute(12, nameof(Select<SelectedItem>.Items), lookup.Clone());
            builder.AddAttribute(13, nameof(Select<SelectedItem>.StringComparison), item.LookupStringComparison);
        }

        // 增加非枚举类,手动设定 ComponentType 为 Select 并且 Data 有值 自动生成下拉框
        if (item.Items != null && item.ComponentType == typeof(Select<>).MakeGenericType(fieldType))
        {
            builder.AddAttribute(14, nameof(Select<SelectedItem>.Items), item.Items.Clone());
        }

        // 设置 SkipValidate 参数
        if (IsValidatableComponent(componentType))
        {
            builder.AddAttribute(15, nameof(IEditorItem.SkipValidate), item.SkipValidate);
        }

        builder.AddMultipleAttributes(16, CreateMultipleAttributes(fieldType, model, fieldName, item));

        if (item.ComponentParameters != null)
        {
            builder.AddMultipleAttributes(17, item.ComponentParameters);
        }
        builder.CloseComponent();
    }

    private static List<SelectedItem> Clone(this IEnumerable<SelectedItem> source) => source.Select(d => new SelectedItem(d.Value, d.Text)
    {
        Active = d.Active,
        IsDisabled = d.IsDisabled,
        GroupName = d.GroupName
    }).ToList();

    private static object? GenerateValue(object model, string fieldName) => Utility.GetPropertyValue<object, object?>(model, fieldName);

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
                if (body == null)
                {
                    body = Expression.Property(Expression.Convert(Expression.Constant(model), type), p);
                }
                else
                {
                    body = Expression.Property(body, p);
                }
            }
            var tDelegate = typeof(Func<>).MakeGenericType(fieldType);
            return Expression.Lambda(tDelegate, body!);
        }
    }

    /// <summary>
    /// 通过指定类型生成组件类型
    /// </summary>
    /// <param name="fieldType"></param>
    /// <param name="hasRows">是否为 Textarea 组件</param>
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
        else
        {
            switch (type.Name)
            {
                case nameof(Boolean):
                    ret = typeof(Switch);
                    break;
                case nameof(DateTime):
                    ret = typeof(DateTimePicker<>).MakeGenericType(fieldType);
                    break;
                case nameof(Int16):
                case nameof(Int32):
                case nameof(Int64):
                case nameof(Single):
                case nameof(Double):
                case nameof(Decimal):
                    ret = typeof(BootstrapInputNumber<>).MakeGenericType(fieldType);
                    break;
                case nameof(String):
                    if (hasRows)
                    {
                        ret = typeof(Textarea);
                    }
                    else
                    {
                        ret = typeof(BootstrapInput<>).MakeGenericType(typeof(string));
                    }
                    break;
            }
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

    private static bool IsValidatableComponent(Type componentType) => componentType.GetProperties().FirstOrDefault(p => p.Name == nameof(IEditorItem.SkipValidate)) != null;

    /// <summary>
    /// 通过模型与指定数据类型生成组件参数集合
    /// </summary>
    /// <param name="fieldType">待编辑数据类型</param>
    /// <param name="model">上下文模型</param>
    /// <param name="fieldName">字段名称</param>
    /// <param name="item">IEditorItem 实例</param>
    /// <returns></returns>
    private static IEnumerable<KeyValuePair<string, object>> CreateMultipleAttributes(Type fieldType, object model, string fieldName, IEditorItem item)
    {
        var ret = new List<KeyValuePair<string, object>>();
        var type = Nullable.GetUnderlyingType(fieldType) ?? fieldType;
        switch (type.Name)
        {
            case nameof(String):
                var ph = item.PlaceHolder ?? Utility.GetPlaceHolder(model, fieldName);
                if (ph != null)
                {
                    ret.Add(new("placeholder", ph));
                }
                if (item.Rows != 0)
                {
                    ret.Add(new("rows", item.Rows));
                }
                break;
            case nameof(Int16):
            case nameof(Int32):
            case nameof(Int64):
            case nameof(Single):
            case nameof(Double):
            case nameof(Decimal):
                if (item.Step != null)
                {
                    var step = item.Step.ToString();
                    if (!string.IsNullOrEmpty(step))
                    {
                        ret.Add(new("Step", step));
                    }
                }
                break;
            default:
                break;
        }
        return ret;
    }

    private static Func<TType, Task> CreateOnValueChangedCallback<TModel, TType>(TModel model, ITableColumn col, Func<TModel, ITableColumn, object?, Task> callback) => new(v => callback(model, col, v));

    /// <summary>
    /// 创建 OnValueChanged 回调委托
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <returns></returns>
    public static Expression<Func<TModel, ITableColumn, Func<TModel, ITableColumn, object?, Task>, object>> CreateOnValueChanged<TModel>(Type fieldType)
    {
        var method = typeof(Utility).GetMethod(nameof(CreateOnValueChangedCallback), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(typeof(TModel), fieldType);
        var exp_p1 = Expression.Parameter(typeof(TModel));
        var exp_p2 = Expression.Parameter(typeof(ITableColumn));
        var exp_p3 = Expression.Parameter(typeof(Func<,,,>).MakeGenericType(typeof(TModel), typeof(ITableColumn), typeof(object), typeof(Task)));
        var body = Expression.Call(null, method, exp_p1, exp_p2, exp_p3);

        return Expression.Lambda<Func<TModel, ITableColumn, Func<TModel, ITableColumn, object?, Task>, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2, exp_p3);
    }
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
    /// 
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
            var t = typeValue.IsGenericType ? typeValue.GenericTypeArguments[0] : typeValue.GetElementType()!;
            var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t))!;
            var mi = instance.GetType().GetMethod("AddRange");
            if (mi != null)
            {
                mi.Invoke(instance, new object[] { value! });
            }

            var invoker = CacheManager.CreateConverterInvoker(t);
            var v = invoker.Invoke(instance);
            ret = string.Join(",", v);
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
    public static object? GenerateValueChanged(ComponentBase component, object model, string fieldName, Type fieldType)
    {
        var valueChangedInvoker = CreateLambda(fieldType).Compile();
        return valueChangedInvoker(component, model, fieldName);

        static Expression<Func<ComponentBase, object, string, object>> CreateLambda(Type fieldType)
        {
            var exp_p1 = Expression.Parameter(typeof(ComponentBase));
            var exp_p2 = Expression.Parameter(typeof(object));
            var exp_p3 = Expression.Parameter(typeof(string));
            var method = typeof(Utility).GetMethod(nameof(CreateCallback), BindingFlags.Static | BindingFlags.NonPublic)!.MakeGenericMethod(fieldType);
            var body = Expression.Call(null, method, exp_p1, exp_p2, exp_p3);

            return Expression.Lambda<Func<ComponentBase, object, string, object>>(Expression.Convert(body, typeof(object)), exp_p1, exp_p2, exp_p3);
        }
    }

    private static EventCallback<TType> CreateCallback<TType>(ComponentBase component, object model, string fieldName) => EventCallback.Factory.Create<TType>(component, t => CacheManager.SetPropertyValue(model, fieldName, t));

    /// <summary>
    /// 获得指定泛型的 IEditorItem 集合
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IEnumerable<IEditorItem> GenerateEditorItems<TModel>(IEnumerable<ITableColumn>? source = null) => InternalTableColumn.GetProperties<TModel>(source);

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
