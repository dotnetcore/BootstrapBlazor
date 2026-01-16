// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">显示组件基类</para>
///  <para lang="en">Display Base Component</para>
/// </summary>
public abstract class DisplayBase<TValue> : BootstrapModuleComponentBase
{
    /// <summary>
    ///  <para lang="zh">是否显示 标签</para>
    ///  <para lang="en">Whether to Show Label</para>
    /// </summary>
    protected bool IsShowLabel { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 the <see cref="FieldIdentifier"/> for the bound value.</para>
    ///  <para lang="en">Gets the <see cref="FieldIdentifier"/> for the bound value.</para>
    /// </summary>
    protected FieldIdentifier? FieldIdentifier { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 泛型参数 TValue 可为空类型 Type 实例，为空时表示类型不可为空</para>
    ///  <para lang="en">Get/Set Generic Parameter TValue Nullable Type Instance. Null means type is not nullable</para>
    /// </summary>
    protected Type? NullableUnderlyingType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 泛型参数 TValue 可为空类型 Type 实例</para>
    ///  <para lang="en">Get/Set Generic Parameter TValue Nullable Type Instance</para>
    /// </summary>
    [NotNull]
    protected Type? ValueType { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 the value of the input. This should be used with two-way binding.</para>
    ///  <para lang="en">Gets or sets the value of the input. This should be used with two-way binding.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    /// <example>
    /// @bind-Value="model.PropertyName"
    /// </example>
    [Parameter]
    [NotNull]
    public TValue? Value { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 a 回调 that updates the bound value.</para>
    ///  <para lang="en">Gets or sets a callback that updates the bound value.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<TValue?> ValueChanged { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 an expression that identifies the bound value.</para>
    ///  <para lang="en">Gets or sets an expression that identifies the bound value.</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Expression<Func<TValue?>>? ValueExpression { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示前置标签 默认值为 null 为空时默认不显示标签</para>
    ///  <para lang="en">Get/Set Whether to Show Label. Default is null, not show label when null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowLabel { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否显示 Tooltip 多用于文字过长导致裁减时使用 默认 null</para>
    ///  <para lang="en">Get/Set Whether to Show Tooltip. Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool? ShowLabelTooltip { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 显示名称</para>
    ///  <para lang="en">Get/Set Display Text</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? DisplayText { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 ValidateForm 实例</para>
    ///  <para lang="en">Get ValidateForm Instance</para>
    /// </summary>
    [CascadingParameter]
    protected ValidateForm? ValidateForm { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 IShowLabel 实例</para>
    ///  <para lang="en">Get IShowLabel Instance</para>
    /// </summary>
    [CascadingParameter(Name = "EditorForm")]
    protected IShowLabel? EditorForm { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 InputGroup 实例</para>
    ///  <para lang="en">Get InputGroup Instance</para>
    /// </summary>
    [CascadingParameter]
    protected BootstrapInputGroup? InputGroup { get; set; }

    /// <summary>
    ///  <para lang="zh">获得 IFilter 实例</para>
    ///  <para lang="en">Get IFilter Instance</para>
    /// </summary>
    [CascadingParameter]
    protected IFilter? Filter { get; set; }

    /// <summary>
    ///  <para lang="zh">SetParametersAsync 方法</para>
    ///  <para lang="en">SetParametersAsync Method</para>
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
    ///  <para lang="zh">OnParametersSet 方法</para>
    ///  <para lang="en">OnParametersSet Method</para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        // 显式设置显示标签时一定显示
        // Show label when explicitly set
        var showLabel = ShowLabel;

        if (Filter != null)
        {
            IsShowLabel = false;
        }
        else if (InputGroup == null)
        {
            // 如果被 InputGroup 包裹不显示 Label
            // 组件自身未设置 ShowLabel 取 EditorForm/ValidateForm 级联值
            // If wrapped by InputGroup, do not show Label
            // If component itself does not set ShowLabel, take EditorForm/ValidateForm cascading value
            if (ShowLabel == null && (EditorForm != null || ValidateForm != null))
            {
                showLabel = EditorForm?.ShowLabel ?? ValidateForm?.ShowLabel ?? true;
            }

            IsShowLabel = showLabel ?? false;
        }
        else
        {
            IsShowLabel = showLabel ?? EditorForm?.ShowLabel ?? ValidateForm?.ShowLabel ?? false;
        }

        // 设置显示标签时未提供 DisplayText 通过双向绑定获取 DisplayName
        // When ShowLabel is set but DisplayText is not provided, get DisplayName through two-way binding
        if (IsShowLabel)
        {
            DisplayText ??= FieldIdentifier?.GetDisplayName();
        }

        // 设置是否显示标签工具栏
        // Set whether to show label tooltip
        ShowLabelTooltip ??= EditorForm?.ShowLabelTooltip ?? ValidateForm?.ShowLabelTooltip;
    }

    /// <summary>
    ///  <para lang="zh">将 Value 格式化为 String 方法</para>
    ///  <para lang="en">Format Value to String Method</para>
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    protected virtual string? FormatValueAsString(TValue? value)
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
