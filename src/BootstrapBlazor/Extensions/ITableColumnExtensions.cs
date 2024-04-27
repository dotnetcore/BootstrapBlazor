// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public static class IEditItemExtensions
{
    /// <summary>
    /// 集成 class 标签中设置的参数值
    /// </summary>
    /// <param name="dest"></param>
    /// <param name="source"></param>
    public static void InheritValue(this ITableColumn dest, AutoGenerateClassAttribute source)
    {
        if (source.Align != Alignment.None) dest.Align = source.Align;
        if (source.TextWrap) dest.TextWrap = source.TextWrap;
        if (source.Ignore) dest.Ignore = source.Ignore;
        if (source.Filterable) dest.Filterable = source.Filterable;
        if (source.Readonly) dest.Readonly = source.Readonly;
        if (source.Searchable) dest.Searchable = source.Searchable;
        if (source.ShowTips) dest.ShowTips = source.ShowTips;
        if (source.ShowCopyColumn) dest.ShowCopyColumn = source.ShowCopyColumn;
        if (source.Sortable) dest.Sortable = source.Sortable;
        if (source.TextEllipsis) dest.TextEllipsis = source.TextEllipsis;
    }

    /// <summary>
    /// 属性赋值方法
    /// </summary>
    /// <param name="dest"></param>
    /// <param name="source"></param>
    public static void CopyValue(this ITableColumn dest, IEditorItem source)
    {
        if (source.ComponentType != null) dest.ComponentType = source.ComponentType;
        if (source.ComponentParameters != null) dest.ComponentParameters = source.ComponentParameters;
        if (source.Ignore) dest.Ignore = source.Ignore;
        if (source.EditTemplate != null) dest.EditTemplate = source.EditTemplate;
        if (source.Items != null) dest.Items = source.Items;
        if (source.Lookup != null) dest.Lookup = source.Lookup;
        if (source.ShowSearchWhenSelect) dest.ShowSearchWhenSelect = source.ShowSearchWhenSelect;
        if (source.IsPopover) dest.IsPopover = source.IsPopover;
        if (source.LookupStringComparison != StringComparison.OrdinalIgnoreCase) dest.LookupStringComparison = source.LookupStringComparison;
        if (source.LookupServiceKey != null) dest.LookupServiceKey = source.LookupServiceKey;
        if (source.LookupServiceData != null) dest.LookupServiceData = source.LookupServiceData;
        if (source.Readonly) dest.Readonly = source.Readonly;
        if (source.Rows > 0) dest.Rows = source.Rows;
        if (source.SkipValidate) dest.SkipValidate = source.SkipValidate;
        if (!string.IsNullOrEmpty(source.Text)) dest.Text = source.Text;
        if (source.ValidateRules != null) dest.ValidateRules = source.ValidateRules;
        if (source.ShowLabelTooltip != null) dest.ShowLabelTooltip = source.ShowLabelTooltip;
        if (!string.IsNullOrEmpty(source.GroupName)) dest.GroupName = source.GroupName;
        if (source.GroupOrder != 0) dest.GroupOrder = source.GroupOrder;
        if (!string.IsNullOrEmpty(source.PlaceHolder)) dest.PlaceHolder = source.PlaceHolder;
        if (!string.IsNullOrEmpty(source.Step)) dest.Step = source.Step;
        if (source.Order != 0) dest.Order = source.Order;

        if (source is ITableColumn col)
        {
            col.CopyValue(dest);
        }
    }

    private static void CopyValue(this ITableColumn col, ITableColumn dest)
    {
        if (col.Align != Alignment.None) dest.Align = col.Align;
        if (col.TextWrap) dest.TextWrap = col.TextWrap;
        if (!string.IsNullOrEmpty(col.CssClass)) dest.CssClass = col.CssClass;
        if (col.DefaultSort) dest.DefaultSort = col.DefaultSort;
        if (col.DefaultSortOrder != SortOrder.Unset) dest.DefaultSortOrder = col.DefaultSortOrder;
        if (col.Filter != null) dest.Filter = col.Filter;
        if (col.Filterable) dest.Filterable = col.Filterable;
        if (col.FilterTemplate != null) dest.FilterTemplate = col.FilterTemplate;
        if (col.Fixed) dest.Fixed = col.Fixed;
        if (col.FormatString != null) dest.FormatString = col.FormatString;
        if (col.Formatter != null) dest.Formatter = col.Formatter;
        if (col.HeaderTemplate != null) dest.HeaderTemplate = col.HeaderTemplate;
        if (col.OnCellRender != null) dest.OnCellRender = col.OnCellRender;
        if (col.Searchable) dest.Searchable = col.Searchable;
        if (col.SearchTemplate != null) dest.SearchTemplate = col.SearchTemplate;
        if (col.ShownWithBreakPoint != BreakPoint.None) dest.ShownWithBreakPoint = col.ShownWithBreakPoint;
        if (col.ShowTips) dest.ShowTips = col.ShowTips;
        if (col.Sortable) dest.Sortable = col.Sortable;
        if (col.Template != null) dest.Template = col.Template;
        if (col.TextEllipsis) dest.TextEllipsis = col.TextEllipsis;
        if (!col.Visible) dest.Visible = col.Visible;
        if (col.Width != null) dest.Width = col.Width;
        if (col.ShowCopyColumn) dest.ShowCopyColumn = col.ShowCopyColumn;
        if (col.HeaderTextWrap) dest.HeaderTextWrap = col.HeaderTextWrap;
        if (!string.IsNullOrEmpty(col.HeaderTextTooltip)) dest.HeaderTextTooltip = col.HeaderTextTooltip;
        if (col.ShowHeaderTooltip) dest.ShowHeaderTooltip = col.ShowHeaderTooltip;
        if (col.HeaderTextEllipsis) dest.HeaderTextEllipsis = col.HeaderTextEllipsis;
        if (col.IsMarkupString) dest.IsMarkupString = col.IsMarkupString;
        if (!col.Visible) dest.Visible = col.Visible;
        if (col.IsVisibleWhenAdd.HasValue) dest.IsVisibleWhenAdd = col.IsVisibleWhenAdd;
        if (col.IsVisibleWhenEdit.HasValue) dest.IsVisibleWhenEdit = col.IsVisibleWhenEdit;
        if (col.IsReadonlyWhenAdd.HasValue) dest.IsReadonlyWhenAdd = col.IsReadonlyWhenAdd;
        if (col.IsReadonlyWhenEdit.HasValue) dest.IsReadonlyWhenEdit = col.IsReadonlyWhenEdit;
    }

    /// <summary>
    /// 将 ITableColumn 集合转化为 IFilterAction 集合
    /// </summary>
    /// <param name="columns"></param>
    /// <param name="searchText"></param>
    /// <returns></returns>
    public static List<IFilterAction> ToSearches(this IEnumerable<ITableColumn> columns, string? searchText)
    {
        var searches = new List<IFilterAction>();
        if (!string.IsNullOrEmpty(searchText))
        {
            foreach (var col in columns)
            {
                var type = Nullable.GetUnderlyingType(col.PropertyType) ?? col.PropertyType;
                if (type == typeof(bool) && bool.TryParse(searchText, out var @bool))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @bool, FilterAction.Equal));
                }
                else if (type.IsEnum && Enum.TryParse(type, searchText, true, out object? @enum))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @enum, FilterAction.Equal));
                }
                else if (type == typeof(string))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), searchText));
                }
                else if (type == typeof(int) && int.TryParse(searchText, out var @int))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @int, FilterAction.Equal));
                }
                else if (type == typeof(long) && long.TryParse(searchText, out var @long))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @long, FilterAction.Equal));
                }
                else if (type == typeof(short) && short.TryParse(searchText, out var @short))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @short, FilterAction.Equal));
                }
                else if (type == typeof(double) && double.TryParse(searchText, out var @double))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @double, FilterAction.Equal));
                }
                else if (type == typeof(float) && float.TryParse(searchText, out var @float))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @float, FilterAction.Equal));
                }
                else if (type == typeof(decimal) && decimal.TryParse(searchText, out var @decimal))
                {
                    searches.Add(new SearchFilterAction(col.GetFieldName(), @decimal, FilterAction.Equal));
                }
            }
        }
        return searches;
    }

    internal static RenderFragment RenderValue<TItem>(this ITableColumn col, TItem item) => async builder =>
    {
        var val = col.GetItemValue(item);
        if (col.Lookup != null && val != null)
        {
            // 转化 Lookup 数据源
            var lookupVal = col.Lookup.FirstOrDefault(l => l.Value.Equals(val.ToString(), col.LookupStringComparison));
            if (lookupVal != null)
            {
                builder.AddContent(10, col.RenderTooltip(lookupVal.Text));
            }
        }
        else if (val is bool v1)
        {
            builder.AddContent(20, v1.RenderSwitch());
        }
        else
        {
            string? content;
            if (col.Formatter != null)
            {
                // 格式化回调委托
                content = await col.Formatter(new TableColumnContext<TItem, object?>(item, val));
            }
            else if (!string.IsNullOrEmpty(col.FormatString))
            {
                // 格式化字符串
                content = Utility.Format(val, col.FormatString);
            }
            else if (col.PropertyType.IsDateTime())
            {
                content = Utility.Format(val, CultureInfo.CurrentUICulture.DateTimeFormat);
            }
            else if (val is IEnumerable<object> v)
            {
                content = string.Join(",", v);
            }
            else
            {
                content = val?.ToString();
            }
            builder.AddContent(30, col.RenderTooltip(content));
        }
    };

    private static RenderFragment RenderSwitch(this bool value) => builder =>
    {
        // 自动化处理 bool 值
        builder.OpenComponent(0, typeof(Switch));
        builder.AddAttribute(1, "Value", value);
        builder.AddAttribute(2, "IsDisabled", true);
        builder.CloseComponent();
    };

    internal static RenderFragment RenderColor<TItem>(this ITableColumn col, TItem item) => builder =>
    {
        var val = col.GetItemValue(item);
        var v = val?.ToString() ?? "#000";
        var style = $"background-color: {v};";
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", "is-color");
        builder.AddAttribute(2, "style", style);
        builder.CloseElement();
    };

    private static RenderFragment RenderTooltip(this ITableColumn col, string? text) => pb =>
    {
        if (col.ShowTips)
        {
            pb.OpenComponent<Tooltip>(0);
            pb.AddAttribute(1, nameof(Tooltip.Title), text);
            pb.AddAttribute(2, "class", "text-truncate d-block");
            if (col.IsMarkupString)
            {
                pb.AddAttribute(3, nameof(Tooltip.ChildContent), new RenderFragment(builder => builder.AddMarkupContent(0, text)));
                pb.AddAttribute(4, nameof(Tooltip.IsHtml), true);
            }
            else
            {
                pb.AddAttribute(3, nameof(Tooltip.ChildContent), new RenderFragment(builder => builder.AddContent(0, text)));
            }
            pb.CloseComponent();
        }
        else if (col.IsMarkupString)
        {
            pb.AddMarkupContent(3, text);
        }
        else
        {
            pb.AddContent(4, text);
        }
    };

    internal static object? GetItemValue<TItem>(this ITableColumn col, TItem item)
    {
        var fieldName = col.GetFieldName();
        object? ret;
        if (item is IDynamicObject dynamicObject)
        {
            ret = dynamicObject.GetValue(fieldName);
        }
        else
        {
            ret = Utility.GetPropertyValue<TItem, object?>(item, fieldName);

            if (ret != null)
            {
                var t = ret.GetType();
                if (t.IsEnum)
                {
                    // 如果是枚举这里返回 枚举的描述信息
                    var itemName = ret.ToString();
                    if (!string.IsNullOrEmpty(itemName))
                    {
                        ret = Utility.GetDisplayName(t, itemName);
                    }
                }
            }
        }
        return ret;
    }
}
