// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class TableColumnTest
{
    [Fact]
    public void InternalTableColumn_Ok()
    {
        var typeName = "BootstrapBlazor.Components.InternalTableColumn, BootstrapBlazor";

        var type = Type.GetType(typeName);
        var instance = Activator.CreateInstance(type!, "Name", typeof(string), "Name");
        SetValue("Sortable", true);
        SetValue("DefaultSort", true);
        SetValue("DefaultSortOrder", SortOrder.Asc);
        SetValue("Filterable", true);
        SetValue("Searchable", true);
        SetValue("Width", 100);
        SetValue("Fixed", true);
        SetValue("TextWrap", true);
        SetValue("TextEllipsis", true);
        SetValue("SkipValidate", true);
        SetValue("IsReadonlyWhenAdd", true);
        SetValue("IsReadonlyWhenEdit", true);
        SetValue("ShowLabelTooltip", true);
        SetValue("CssClass", "css-test");
        SetValue("ShownWithBreakPoint", BreakPoint.Small);
        SetValue("Template", new RenderFragment<object?>(context => builder =>
        {
            builder.AddContent(0, context);
        }));
        SetValue("SearchTemplate", new RenderFragment<object?>(context => builder =>
        {
            builder.AddContent(0, context);
        }));
        SetValue("FilterTemplate", new RenderFragment(builder =>
        {
            builder.AddContent(0, "test");
        }));
        SetValue("HeaderTemplate", new RenderFragment<ITableColumn>(col => builder =>
        {
            builder.AddContent(0, col.GetFieldName());
        }));
        SetValue("Filter", new TableFilter());
        SetValue("FormatString", "test");
        SetValue("Formatter", new Func<object?, Task<string>>(val =>
        {
            return Task.FromResult("Test");
        }));
        SetValue("Align", Alignment.Left);
        SetValue("ShowTips", true);
        SetValue("Readonly", true);
        SetValue("Step", 1);
        SetValue("Rows", 1);
        SetValue("ComponentType", typeof(string));
        SetValue("Order", 1);
        SetValue("Lookup", new SelectedItem[] { new("test", "Test") });
        SetValue("ShowSearchWhenSelect", true);
        SetValue("IsPopover", true);
        SetValue("LookupStringComparison", StringComparison.Ordinal);
        SetValue("LookupServiceKey", "Test");
        SetValue("OnCellRender", new Action<TableCellArgs>(args => { }));
        SetValue("ValidateRules", new List<IValidator> { new RequiredValidator() });
        SetValue("GroupName", "Test");
        SetValue("GroupOrder", 1);
        SetValue("ShowCopyColumn", true);
        SetValue("HeaderTextWrap", true);
        SetValue("ShowHeaderTooltip", true);
        SetValue("HeaderTextTooltip", "Test");
        SetValue("HeaderTextEllipsis", true);

        void SetValue(string properyName, object? val) => type!.GetProperty(properyName)!.SetValue(instance, val);
    }
}
