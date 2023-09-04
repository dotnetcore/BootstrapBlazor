// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Forms;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryColumn 组件
/// </summary>
public class QueryColumn<TType> : QueryGroup
{
    /// <summary>
    /// 获得/设置 条件字段名称
    /// </summary>
    [Parameter]
    public TType? Field { get; set; }

    /// <summary>
    /// 获得/设置 FieldExpression 表达式
    /// </summary>
    [Parameter]
    public Expression<Func<TType>>? FieldExpression { get; set; }

    /// <summary>
    /// 获得/设置 条件操作符号
    /// </summary>
    [Parameter]
    public FilterAction Operator { get; set; }

    /// <summary>
    /// 获得/设置 条件值
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
