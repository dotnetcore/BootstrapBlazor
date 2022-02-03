// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// CheckboxList 组件基类
/// </summary>
public partial class CheckboxList<TValue>
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? GetClassString(string defaultClass = "checkbox-list") => CssBuilder.Default("form-control")
        .AddClass(defaultClass)
        .AddClass("no-border", !ShowBorder && ValidCss != "is-invalid")
        .AddClass("is-vertical", IsVertical)
        .AddClass(CssClass)
        .AddClass("is-invalid", IsValid.HasValue && !IsValid.Value)
        .Build();

    /// <summary>
    /// 获得 组件内部 Checkbox 项目样式
    /// </summary>
    protected string? CheckboxItemClassString => CssBuilder.Default("checkbox-item")
        .AddClass(CheckboxItemClass)
        .Build();

    /// <summary>
    /// 获得/设置 数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 Checkbox 组件布局样式
    /// </summary>
    [Parameter]
    public string? CheckboxItemClass { get; set; }

    /// <summary>
    /// 获得/设置 是否显示边框 默认为 true
    /// </summary>
    [Parameter]
    public bool ShowBorder { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否为竖向排列 默认为 false
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 SelectedItemChanged 方法
    /// </summary>
    [Parameter]
    public Func<IEnumerable<SelectedItem>, TValue, Task>? OnSelectedChanged { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizerFactory? LocalizerFactory { get; set; }

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        EnsureParameterValid();

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(ParameterView.Empty);
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 处理 Required 标签
        if (EditContext != null && FieldIdentifier != null)
        {
            var pi = FieldIdentifier.Value.Model.GetType().GetPropertyByName(FieldIdentifier.Value.FieldName);
            if (pi != null)
            {
                var required = pi.GetCustomAttribute<RequiredAttribute>(true);
                if (required != null)
                {
                    Rules.Add(new RequiredValidator()
                    {
                        LocalizerFactory = LocalizerFactory,
                        ErrorMessage = required.ErrorMessage,
                        AllowEmptyString = required.AllowEmptyStrings
                    });
                }
            }
        }
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Items == null)
        {
            var t = typeof(TValue);
            if (t == typeof(string))
            {
                Items = Enumerable.Empty<SelectedItem>();
            }
            else
            {
                var innerType = t.GetGenericArguments()[0];
                Items = innerType.IsEnum ? innerType.ToSelectList() : Enumerable.Empty<SelectedItem>();
            }
        }

        InitValue();
    }

    private void InitValue()
    {
        // 通过 Value 对集合进行赋值
        if (Value != null)
        {
            var typeValue = typeof(TValue);
            IEnumerable? list = null;
            if (typeValue == typeof(string))
            {
                var values = CurrentValueAsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in Items)
                {
                    item.Active = values.Any(v => v.Equals(item.Value, StringComparison.OrdinalIgnoreCase));
                }
                list = values;
            }
            else if (typeValue.IsGenericType)
            {
                ProcessGenericItems(typeValue, list);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="typeValue"></param>
    /// <param name="list"></param>
    protected virtual void ProcessGenericItems(Type typeValue, IEnumerable? list)
    {
        var t = typeValue.GenericTypeArguments;
        var instance = Activator.CreateInstance(typeof(List<>).MakeGenericType(t));
        if (instance != null)
        {
            var mi = instance.GetType().GetMethod("AddRange");
            if (mi != null)
            {
                mi.Invoke(instance, new object[] { Value });
            }
            list = instance as IEnumerable;
            if (list != null)
            {
                foreach (var item in Items)
                {
                    item.Active = false;
                    foreach (var v in list)
                    {
                        item.Active = item.Value.Equals(v!.ToString(), StringComparison.OrdinalIgnoreCase);
                        if (item.Active)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Checkbox 组件选项状态改变时触发此方法
    /// </summary>
    /// <param name="item"></param>
    /// <param name="v"></param>
    private async Task OnStateChanged(SelectedItem item, bool v)
    {
        item.Active = v;

        var typeValue = typeof(TValue);
        if (typeValue == typeof(string))
        {
            CurrentValueAsString = string.Join(",", Items.Where(i => i.Active).Select(i => i.Value));
        }
        else if (typeValue.IsGenericType)
        {
            var t = typeValue.GenericTypeArguments;
            if (Activator.CreateInstance(typeof(List<>).MakeGenericType(t)) is IList instance)
            {
                foreach (var sl in Items.Where(i => i.Active))
                {
                    if (sl.Value.TryConvertTo(t[0], out var val))
                    {
                        instance.Add(val);
                    }
                }
                CurrentValue = (TValue)instance;
            }
        }

        if (OnSelectedChanged != null)
        {
            await OnSelectedChanged.Invoke(Items, Value);
        }
    }

    /// <summary>
    /// 泛型参数约束检查
    /// </summary>
    protected virtual void EnsureParameterValid()
    {
        var typeValue = typeof(TValue);
        if (typeValue.IsGenericType)
        {
            // 泛型参数
            if (!typeValue.IsAssignableTo(typeof(IEnumerable)))
            {
                throw new NotSupportedException();
            }
        }
        else if (typeValue != typeof(string))
        {
            throw new NotSupportedException();
        }
    }
}
