// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    [Parameter]
    [NotNull]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务实例
    /// </summary>
    [Parameter]
    [NotNull]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作，可以通过 <see cref="LookupServiceData"/> 传递自定义数据
    /// </summary>
    [Parameter]
    [NotNull]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值自定义数据，通过 <see cref="LookupServiceKey"/> 指定键值
    /// </summary>
    [Parameter]
    [NotNull]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// 获得/设置 字典数据源字符串比较规则 默认 StringComparison.OrdinalIgnoreCase 大小写不敏感 
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    /// <summary>
    /// 获得/设置 相关枚举类型
    /// </summary>
    [EditorRequired]
    [Parameter]
    [NotNull]
    public Type? Type { get; set; }

    /// <summary>
    /// 获得 是否为 ShowSearch 呈现模式 默认为 false
    /// </summary>
    [Parameter]
    public bool IsShowSearch { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<TableFilter>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

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
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        var items = new List<SelectedItem>
        {
            new("", Localizer["EnumFilter.AllText"].Value)
        };
        if (Lookup != null)
        {
            items.AddRange(Lookup);
        }
        else if (!string.IsNullOrEmpty(LookupServiceKey))
        {
            var lookupService = GetLookupService();
            var lookup = await lookupService.GetItemsAsync(LookupServiceKey, LookupServiceData);
            if (lookup != null)
            {
                items.AddRange(lookup);
            }
        }
        Items = items;
    }

    private ILookupService GetLookupService() => LookupService ?? InjectLookupService;

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
        var filter = new FilterKeyValueAction() { Filters = [] };
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
