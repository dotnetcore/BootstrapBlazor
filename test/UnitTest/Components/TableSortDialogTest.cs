// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Dom;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class TableSortDialogTest : TableSortDialogTestBase
{
    [Fact]
    public void AdvancedSort_Ok()
    {
        var sortList = new List<string>();
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowAdvancedSort, true);
                pb.Add(a => a.AdvancedSortDialogShowMaximizeButton, true);
                pb.Add(a => a.AdvancedSortDialogIsDraggable, true);
                pb.Add(a => a.AdvancedSortDialogSize, Size.Small);
                pb.Add(a => a.OnQueryAsync, op =>
                {
                    sortList.AddRange(op.AdvancedSortList);
                    return Task.FromResult(new QueryData<Foo>()
                    {
                        Items = Foo.GenerateFoo(localizer, 4),
                        TotalCount = 4
                    });
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Sortable", true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, int>>(0);
                    builder.AddAttribute(1, "Field", 1);
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Count", typeof(int)));
                    builder.AddAttribute(3, "Sortable", true);
                    builder.CloseComponent();
                });
            });
        });
        var button = cut.Find(".table-column-right > .btn");
        Assert.Equal("高级排序", button.Text());
        cut.InvokeAsync(() => button.Click());

        cut.WaitForAssertion(() => cut.Contains("dialog-advance-sort"));

        // 点击 Clear
        var btnClear = cut.Find(".table-advance-sort-toolbar .btn-danger");
        cut.InvokeAsync(() => btnClear.Click());

        // 点击 Add
        var btnAdd = cut.Find(".table-advance-sort-toolbar .btn");
        cut.InvokeAsync(() => btnAdd.Click());

        var fieldSelect = cut.FindComponent<Select<string>>();
        Assert.Equal(new string[] { "Name", "Count" }, fieldSelect.Instance.Items.Select(i => i.Value));

        var orderSelect = cut.FindComponent<Select<SortOrder>>();
        Assert.Equal(new string[] { "Asc", "Desc" }, orderSelect.Instance.Items.Select(i => i.Value));

        // 点击 Remove
        var btnRemove = cut.Find(".row .btn-danger");
        cut.InvokeAsync(() => btnRemove.Click());

        btnAdd = cut.Find(".table-advance-sort-toolbar .btn");
        cut.InvokeAsync(() => btnAdd.Click());

        fieldSelect = cut.FindComponent<Select<string>>();
        Assert.Equal(new string[] { "Name", "Count" }, fieldSelect.Instance.Items.Select(i => i.Value));

        orderSelect = cut.FindComponent<Select<SortOrder>>();
        Assert.Equal(new string[] { "Asc", "Desc" }, orderSelect.Instance.Items.Select(i => i.Value));

        // 关闭弹窗
        var btnClose = cut.Find(".modal-footer .btn-primary");
        cut.InvokeAsync(() => btnClose.Click());

        Assert.Equal("Name Asc", sortList[0]);
    }

    [Fact]
    public void AdvancedSortDialog_Ok()
    {
        var cut = Context.RenderComponent<TableAdvancedSortDialog>();
        cut.Contains("dialog-advance-sort");
    }
}
