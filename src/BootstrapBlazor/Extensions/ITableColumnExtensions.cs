// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// IEditItem 扩展方法
/// </summary>
public static class IEditItemExtensions
{
    /// <summary>
    /// 继承 class 标签中设置的参数值
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
        if (!source.Visible) dest.Visible = source.Visible;
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
        if (source.Ignore.HasValue) dest.Ignore = source.Ignore;
        if (source.EditTemplate != null) dest.EditTemplate = source.EditTemplate;
        if (source.Items != null) dest.Items = source.Items;
        if (source.Lookup != null) dest.Lookup = source.Lookup;
        if (source.ShowSearchWhenSelect) dest.ShowSearchWhenSelect = source.ShowSearchWhenSelect;
        if (source.IsPopover) dest.IsPopover = source.IsPopover;
        if (source.LookupStringComparison != StringComparison.OrdinalIgnoreCase) dest.LookupStringComparison = source.LookupStringComparison;
        if (source.LookupServiceKey != null) dest.LookupServiceKey = source.LookupServiceKey;
        if (source.LookupServiceData != null) dest.LookupServiceData = source.LookupServiceData;
        if (source.LookupService != null) dest.LookupService = source.LookupService;
        if (source.Readonly.HasValue) dest.Readonly = source.Readonly;
        if (source.Rows > 0) dest.Rows = source.Rows;
        if (source.Cols > 0) dest.Cols = source.Cols;
        if (source.SkipValidate) dest.SkipValidate = source.SkipValidate;
        if (!string.IsNullOrEmpty(source.Text)) dest.Text = source.Text;
        if (source.ValidateRules != null) dest.ValidateRules = source.ValidateRules;
        if (source.ShowLabelTooltip != null) dest.ShowLabelTooltip = source.ShowLabelTooltip;
        if (!string.IsNullOrEmpty(source.GroupName)) dest.GroupName = source.GroupName;
        if (source.GroupOrder != 0) dest.GroupOrder = source.GroupOrder;
        if (!string.IsNullOrEmpty(source.PlaceHolder)) dest.PlaceHolder = source.PlaceHolder;
        if (!string.IsNullOrEmpty(source.Step)) dest.Step = source.Step;
        if (source.Order != 0) dest.Order = source.Order;
        if (source.Required.HasValue) dest.Required = source.Required;
        if (!string.IsNullOrEmpty(source.RequiredErrorMessage)) dest.RequiredErrorMessage = source.RequiredErrorMessage;

        if (source is ITableColumn col)
        {
            col.CopyValue(dest);
        }
    }

    private static void CopyValue(this ITableColumn col, ITableColumn dest)
    {
        if (col.Align.HasValue) dest.Align = col.Align;
        if (col.TextWrap.HasValue) dest.TextWrap = col.TextWrap;
        if (!string.IsNullOrEmpty(col.CssClass)) dest.CssClass = col.CssClass;
        if (col.DefaultSort) dest.DefaultSort = col.DefaultSort;
        if (col.DefaultSortOrder != SortOrder.Unset) dest.DefaultSortOrder = col.DefaultSortOrder;
        if (col.Filter != null) dest.Filter = col.Filter;
        if (col.Filterable.HasValue) dest.Filterable = col.Filterable;
        if (col.FilterTemplate != null) dest.FilterTemplate = col.FilterTemplate;
        if (col.Fixed) dest.Fixed = col.Fixed;
        if (col.FormatString != null) dest.FormatString = col.FormatString;
        if (col.Formatter != null) dest.Formatter = col.Formatter;
        if (col.HeaderTemplate != null) dest.HeaderTemplate = col.HeaderTemplate;
        if (col.OnCellRender != null) dest.OnCellRender = col.OnCellRender;
        if (col.Searchable.HasValue) dest.Searchable = col.Searchable;
        if (col.SearchTemplate != null) dest.SearchTemplate = col.SearchTemplate;
        if (col.ShownWithBreakPoint != BreakPoint.None) dest.ShownWithBreakPoint = col.ShownWithBreakPoint;
        if (col.ShowTips.HasValue) dest.ShowTips = col.ShowTips;
        if (col.Sortable.HasValue) dest.Sortable = col.Sortable;
        if (col.Template != null) dest.Template = col.Template;
        if (col.TextEllipsis.HasValue) dest.TextEllipsis = col.TextEllipsis;
        if (col.Width != null) dest.Width = col.Width;
        if (col.ShowCopyColumn.HasValue) dest.ShowCopyColumn = col.ShowCopyColumn;
        if (col.HeaderTextWrap) dest.HeaderTextWrap = col.HeaderTextWrap;
        if (!string.IsNullOrEmpty(col.HeaderTextTooltip)) dest.HeaderTextTooltip = col.HeaderTextTooltip;
        if (col.ShowHeaderTooltip) dest.ShowHeaderTooltip = col.ShowHeaderTooltip;
        if (col.HeaderTextEllipsis) dest.HeaderTextEllipsis = col.HeaderTextEllipsis;
        if (col.IsMarkupString) dest.IsMarkupString = col.IsMarkupString;
        if (col.Visible.HasValue) dest.Visible = col.Visible;
        if (col.IsVisibleWhenAdd.HasValue) dest.IsVisibleWhenAdd = col.IsVisibleWhenAdd;
        if (col.IsVisibleWhenEdit.HasValue) dest.IsVisibleWhenEdit = col.IsVisibleWhenEdit;
        if (col.IsReadonlyWhenAdd.HasValue) dest.IsReadonlyWhenAdd = col.IsReadonlyWhenAdd;
        if (col.IsReadonlyWhenEdit.HasValue) dest.IsReadonlyWhenEdit = col.IsReadonlyWhenEdit;
        if (col.GetTooltipTextCallback != null) dest.GetTooltipTextCallback = col.GetTooltipTextCallback;
        if (col.CustomSearch != null) dest.CustomSearch = col.CustomSearch;
        if (col.ToolboxTemplate != null) dest.ToolboxTemplate = col.ToolboxTemplate;
        if (col.IsRequiredWhenAdd.HasValue) dest.IsRequiredWhenAdd = col.IsRequiredWhenAdd;
        if (col.IsRequiredWhenEdit.HasValue) dest.IsRequiredWhenEdit = col.IsRequiredWhenEdit;
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
                if (col.CustomSearch != null)
                {
                    searches.Add(col.CustomSearch(col, searchText));
                    continue;
                }
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

    /// <summary>
    /// 当前单元格方法
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="col"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static RenderFragment RenderValue<TItem>(this ITableColumn col, TItem item) => builder =>
    {
        var val = col.GetItemValue(item);
        if (col.IsLookup() && val != null)
        {
            builder.AddContent(10, col.RenderLookupContent(val.ToString(), item));
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
                builder.OpenComponent<TableFormatContent>(40);
                builder.AddAttribute(45, nameof(TableFormatContent.Formatter), col.Formatter);
                builder.AddAttribute(46, nameof(TableFormatContent.Item), new TableColumnContext<TItem, object?>(item, val));
                builder.CloseComponent();
            }
            else
            {
                if (!string.IsNullOrEmpty(col.FormatString))
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
                builder.AddContent(30, col.RenderLookupContent(content, item));
            }
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

    private static RenderFragment RenderLookupContent<TItem>(this ITableColumn col, string? text, TItem item) => pb =>
    {
        if (col.GetShowTips())
        {
            pb.AddContent(10, col.RenderTooltip(text, item));
        }
        else
        {
            pb.AddContent(20, col.RenderContent(text));
        }
    };

    private static RenderFragment RenderTooltip<TItem>(this ITableColumn col, string? text, TItem item) => pb =>
    {
        pb.OpenComponent<Tooltip>(0);
        pb.SetKey(item);
        if (col.GetTooltipTextCallback != null)
        {
            pb.AddAttribute(10, nameof(Tooltip.GetTitleCallback), new Func<Task<string?>>(() => col.GetTooltipTextCallback(item)));
        }
        else
        {
            pb.AddAttribute(11, nameof(Tooltip.Title), text);
        }
        if (col.IsMarkupString)
        {
            pb.AddAttribute(12, nameof(Tooltip.IsHtml), true);
        }
        pb.AddAttribute(13, "class", "text-truncate d-block");
        pb.AddAttribute(14, nameof(Tooltip.ChildContent), col.RenderContent(text));
        pb.CloseComponent();
    };

    private static RenderFragment RenderContent(this ITableColumn col, string? text) => pb =>
    {
        if (col.IsLookup())
        {
            pb.OpenComponent<LookupContent>(100);
            pb.AddAttribute(101, nameof(LookupContent.LookupService), col.LookupService);
            pb.AddAttribute(102, nameof(LookupContent.LookupServiceKey), col.LookupServiceKey);
            pb.AddAttribute(103, nameof(LookupContent.LookupServiceData), col.LookupServiceData);
            pb.AddAttribute(104, nameof(LookupContent.LookupStringComparison), col.LookupStringComparison);
            pb.AddAttribute(105, nameof(LookupContent.Lookup), col.Lookup);
            pb.AddAttribute(106, nameof(LookupContent.Value), text);
            pb.CloseComponent();
        }
        else if (col.IsMarkupString)
        {
            pb.AddMarkupContent(110, text);
        }
        else
        {
            pb.AddContent(120, text);
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

    internal static bool GetSearchable(this ITableColumn col) => col.Searchable ?? false;

    internal static bool GetFilterable(this ITableColumn col) => col.Filterable ?? false;

    internal static bool GetSortable(this ITableColumn col) => col.Sortable ?? false;

    internal static bool GetTextWrap(this ITableColumn col) => col.TextWrap ?? false;

    internal static bool GetTextEllipsis(this ITableColumn col) => col.TextEllipsis ?? false;

    internal static bool GetVisible(this ITableColumn col) => col.Visible ?? true;

    internal static bool GetShowCopyColumn(this ITableColumn col) => col.ShowCopyColumn ?? false;

    internal static bool GetShowTips(this ITableColumn col) => col.ShowTips ?? false;

    internal static Alignment GetAlign(this ITableColumn col) => col.Align ?? Alignment.None;

    internal static int? GetColumnFixedWidth(this ITableColumn col, int width) => col.Fixed ? col.Width ?? width : col.Width;
}
