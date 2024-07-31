﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// 显示组件基类
/// </summary>
public abstract class DisplayBase<TValue> : BootstrapModuleComponentBase
{
    /// <summary>
    /// 是否显示 标签
    /// </summary>
    protected bool IsShowLabel { get; set; }

    /// <summary>
    /// Gets the <see cref="FieldIdentifier"/> for the bound value.
    /// </summary>
    protected FieldIdentifier? FieldIdentifier { get; set; }

    /// <summary>
    /// 获得/设置 泛型参数 TValue 可为空类型 Type 实例，为空时表示类型不可为空
    /// </summary>
    protected Type? NullableUnderlyingType { get; set; }

    /// <summary>
    /// 获得/设置 泛型参数 TValue 可为空类型 Type 实例
    /// </summary>
    [NotNull]
    protected Type? ValueType { get; set; }

    /// <summary>
    /// Gets or sets the value of the input. This should be used with two-way binding.
    /// </summary>
    /// <example>
    /// @bind-Value="model.PropertyName"
    /// </example>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    /// Gets or sets a callback that updates the bound value.
    /// </summary>
    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets an expression that identifies the bound value.
    /// </summary>
    [Parameter]
    public Expression<Func<TValue>>? ValueExpression { get; set; }

    /// <summary>
    /// 获得/设置 是否显示前置标签 默认值为 null 为空时默认不显示标签
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Tooltip 多用于文字过长导致裁减时使用 默认 null
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    /// 获得/设置 显示名称
    /// </summary>
    [Parameter]
    public string? DisplayText { get; set; }

    /// <summary>
    /// 获得 ValidateForm 实例
    /// </summary>
    [CascadingParameter]
    protected ValidateForm? ValidateForm { get; set; }

    /// <summary>
    /// 获得 IShowLabel 实例
    /// </summary>
    [CascadingParameter(Name = "EditorForm")]
    protected IShowLabel? EditorForm { get; set; }

    /// <summary>
    /// 获得 InputGroup 实例
    /// </summary>
    [CascadingParameter]
    protected BootstrapInputGroup? InputGroup { get; set; }

    /// <summary>
    /// 获得 IFilter 实例
    /// </summary>
    [CascadingParameter]
    protected IFilter? Filter { get; set; }

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        NullableUnderlyingType = Nullable.GetUnderlyingType(typeof(TValue));
        ValueType ??= NullableUnderlyingType ?? typeof(TValue);

        if (ValueExpression != null)
        {
            FieldIdentifier = Microsoft.AspNetCore.Components.Forms.FieldIdentifier.Create(ValueExpression);
        }

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(ParameterView.Empty);
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Filter != null || InputGroup != null)
        {
            IsShowLabel = false;
        }
        else
        {
            IsShowLabel = ShowLabel ?? EditorForm?.ShowLabel ?? ValidateForm?.ShowLabel ?? false;

            if (FieldIdentifier.HasValue)
            {
                DisplayText ??= FieldIdentifier.Value.GetDisplayName();
            }
        }

        ShowLabelTooltip ??= EditorForm?.ShowLabelTooltip ?? ValidateForm?.ShowLabelTooltip ?? false;
    }

    /// <summary>
    /// 将 Value 格式化为 String 方法
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    protected virtual string? FormatValueAsString(TValue value)
    {
        string? ret;
        if (value is SelectedItem item)
        {
            ret = item.Value;
        }
        else
        {
            ret = value?.ToString();
        }
        return ret;
    }
}
