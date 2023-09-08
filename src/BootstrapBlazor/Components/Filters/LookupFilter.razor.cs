// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 枚举类型过滤组件
/// </summary>
public partial class LookupFilter
{
    private string? Value { get; set; }

    /// <summary>
    /// 获得/设置 相关枚举类型
    /// </summary>
#if NET6_0_OR_GREATER
    [EditorRequired]
#endif
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

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
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Lookup == null)
        {
            throw new InvalidOperationException("the Parameter Lookup must be set.");
        }

        if (Type == null)
        {
            throw new InvalidOperationException("the Parameter Type must be set.");
        }

        if (TableFilter != null)
        {
            TableFilter.ShowMoreButton = false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        if (Items == null)
        {
            var items = new List<SelectedItem>
            {
                new SelectedItem("", Localizer["EnumFilter.AllText"].Value)
            };
            items.AddRange(Lookup);
            Items = items;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override void Reset()
    {
        Value = "";
        StateHasChanged();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override FilterKeyValueAction GetFilterConditions()
    {
        var filter = new FilterKeyValueAction() { Filters = new() };
        if (!string.IsNullOrEmpty(Value))
        {
            var type = Nullable.GetUnderlyingType(Type) ?? Type;
            var val = Convert.ChangeType(Value, type);
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FieldKey = FieldKey,
                FieldValue = val,
                FilterAction = FilterAction.Equal
            });
        }
        return filter;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override async Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        var type = Nullable.GetUnderlyingType(Type) ?? Type;
        if (first.FieldValue != null && first.FieldValue.GetType() == type)
        {
            Value = first.FieldValue.ToString();
        }
        else
        {
            Value = "";
        }
        await base.SetFilterConditionsAsync(filter);
    }
}
