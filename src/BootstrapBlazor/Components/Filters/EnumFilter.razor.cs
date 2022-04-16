// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 枚举类型过滤组件
/// </summary>
public partial class EnumFilter
{
    private string? Value { get; set; }

    private IEnumerable<SelectedItem> Items { get; set; } = Enumerable.Empty<SelectedItem>();

    /// <summary>
    /// 内部使用
    /// </summary>
    [NotNull]
    private Type? EnumType { get; set; }

    /// <summary>
    /// 获得/设置 相关枚举类型
    /// </summary>
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [Parameter]
    [NotNull]
    public Type? Type { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Type == null) throw new InvalidOperationException("the Parameter Type must be set.");

        if (TableFilter != null)
        {
            TableFilter.ShowMoreButton = false;
        }

        EnumType = Nullable.GetUnderlyingType(Type) ?? Type;
        Items = EnumType.ToSelectList(new SelectedItem("", Localizer["EnumFilter.AllText"].Value));
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Reset()
    {
        Value = "";
        StateHasChanged();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override IEnumerable<FilterKeyValueAction> GetFilterConditions()
    {
        var filters = new List<FilterKeyValueAction>();
        if (!string.IsNullOrEmpty(Value) && Enum.TryParse(EnumType, Value, out var val))
        {
            filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = val,
                FilterAction = FilterAction.Equal
            });
        }
        return filters;
    }

    /// <summary>
    /// Override existing filter conditions
    /// </summary>
    public void SetFilterConditions(IEnumerable<FilterKeyValueAction> conditions)
    {
        if (conditions.Any())
        {
            var type = Nullable.GetUnderlyingType(Type) ?? Type;
            FilterKeyValueAction first = conditions.First();
            if (first.FieldValue != null && first.FieldValue.GetType() == type)
            {
                Value = first.FieldValue.ToString();
            }
            else
            {
                Value = "";
            }
        }
    }
}
