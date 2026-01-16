// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">QueryColumn 组件</para>
/// <para lang="en">QueryColumn Component</para>
/// </summary>
public class QueryColumn<TType> : QueryGroup
{
    /// <summary>
    /// <para lang="zh">获得/设置 条件字段名称</para>
    /// <para lang="en">Get/Set Condition Field Name</para>
    /// </summary>
    [Parameter]
    public TType? Field { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 FieldExpression 表达式</para>
    /// <para lang="en">Get/Set FieldExpression</para>
    /// </summary>
    [Parameter]
    public Expression<Func<TType>>? FieldExpression { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 条件操作符号</para>
    /// <para lang="en">Get/Set Condition Operator</para>
    /// </summary>
    [Parameter]
    public FilterAction Operator { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 条件值</para>
    /// <para lang="en">Get/Set Condition Value</para>
    /// </summary>
    [Parameter]
    public object? Value { get; set; }

    private FieldIdentifier? _fieldIdentifier;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (FieldExpression != null)
        {
            _fieldIdentifier = FieldIdentifier.Create(FieldExpression);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _filter.FieldKey = _fieldIdentifier?.FieldName;
        _filter.FilterAction = Operator;
        _filter.FieldValue = Value;
    }
}
