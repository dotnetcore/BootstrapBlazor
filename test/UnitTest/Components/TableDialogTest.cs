// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace UnitTest.Components;

public class TableDialogTest : TableDialogTestBase
{
    [Fact]
    public async Task EditAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.EditDialogIsDraggable, true);
                pb.Add(a => a.EditDialogShowMaximizeButton, false);
                pb.Add(a => a.EditDialogFullScreenSize, FullScreenSize.None);
                pb.Add(a => a.EditDialogSize, Size.Large);
                pb.Add(a => a.EditDialogSaveButtonText, "test-save");
                pb.Add(a => a.EditDialogSaveButtonIcon, "icon-test-save");
                pb.Add(a => a.EditDialogCloseButtonText, "test-close");
                pb.Add(a => a.EditDialogCloseButtonIcon, "icon-test-close");
                pb.Add(a => a.EditDialogItemsPerRow, 2);
                pb.Add(a => a.EditDialogRowType, RowType.Inline);
                pb.Add(a => a.EditDialogLabelAlign, Alignment.Center);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(3, "Ignore", true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableTemplateColumn<Foo>>(10);
                    builder.AddAttribute(11, "Template", new RenderFragment<TableColumnContext<Foo, object?>>(context => builder =>
                    {
                        builder.AddContent(0, $"template-{context.Row.Name}");
                    }));
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnSaveAsync, (foo, itemType) => Task.FromResult(true));
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        // 选一个
        var checkbox = cut.FindComponents<Checkbox<Foo>>()[1];
        await cut.InvokeAsync(checkbox.Instance.OnToggleClick);
        await cut.InvokeAsync(() => table.Instance.EditAsync());

        cut.Contains("test-save");
        cut.Contains("test-close");

        cut.Contains("modal-lg");
        cut.DoesNotContain("btn-maximize");
        cut.Contains("is-draggable");

        // 编辑弹窗逻辑
        var form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 内置数据服务取消回调
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 自定义数据服务取消回调测试
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DataService, new MockEFCoreDataService(localizer));
            pb.Add(a => a.BeforeShowEditDialogCallback, new Action<ITableEditDialogOption<Foo>>(o => o.DisableAutoSubmitFormByEnter = true));
        });
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 自定义数据服务取消回调测试
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.EditDialogFullScreenSize, FullScreenSize.Always);
        });
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        Assert.Contains(" modal-fullscreen ", cut.Markup);
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        var closed = false;
        // 测试 CloseCallback
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.EditDialogCloseAsync, (model, result) =>
            {
                closed = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(closed);

        // IsTracking mode
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsTracking, true);
        });
        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        // 编辑弹窗逻辑
        var input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        var itemsChanged = false;
        // 更新插入模式
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.InsertRowMode, InsertRowMode.First);
            pb.Add(a => a.ItemsChanged, foo =>
            {
                itemsChanged = true;
            });
            pb.Add(a => a.EditFooterTemplate, foo => builder => builder.AddContent(0, "test_edit_footer"));
        });

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        cut.Contains("test_edit_footer");

        // 编辑弹窗逻辑
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(itemsChanged);

        // 设置双向绑定 Items 后再测试 Add Save
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsTracking, false);
            pb.Add(a => a.OnSaveAsync, null);
            pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows => items = rows.ToList()));
        });
        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(3, items.Count);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.InsertRowMode, InsertRowMode.Last);
        });

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(3, items.Count);

        // 数据源是 OnQueryAsync 提供
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, null);
            pb.Add(a => a.OnQueryAsync, options => Task.FromResult(new QueryData<Foo>()
            {
                Items = items,
                TotalCount = items.Count,
                IsAdvanceSearch = true,
                IsSearch = true,
                IsFiltered = true,
                IsSorted = true
            }));
        });

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 数据为三行
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(3, rows.Count);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsExcel, false);
            pb.Add(a => a.ShowToolbar, true);
            pb.Add(a => a.ShowSearch, true);
            pb.Add(a => a.ShowSearchText, false);
            pb.Add(a => a.SearchDialogSize, Size.ExtraExtraLarge);
            pb.Add(a => a.SearchDialogIsDraggable, true);
            pb.Add(a => a.ScrollingDialogContent, true);
            pb.Add(a => a.SearchDialogShowMaximizeButton, true);
            pb.Add(a => a.SearchDialogItemsPerRow, 2);
            pb.Add(a => a.SearchDialogRowType, RowType.Inline);
            pb.Add(a => a.SearchDialogLabelAlign, Alignment.Right);
            pb.Add(a => a.ShowAdvancedSearch, true);
            pb.Add(a => a.RenderMode, TableRenderMode.Table);
            pb.Add(a => a.ShowUnsetGroupItemsOnTop, true);
            pb.Add(a => a.TableColumns, foo => builder =>
            {
                builder.OpenComponent<TableColumn<Foo, string>>(0);
                builder.AddAttribute(1, "Field", "Name");
                builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                builder.AddAttribute(3, "Searchable", true);
                builder.CloseComponent();
            });
        });

        var searchButton = cut.Find(".fa-magnifying-glass-plus");
        await cut.InvokeAsync(() => searchButton.Click());

        cut.WaitForAssertion(() => cut.Find(".fa-magnifying-glass"));
        var queryButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => queryButton.Click());

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetAdvancedSearchFilterCallback, new Func<PropertyInfo, Foo, List<SearchFilterAction>?>((p, model) =>
            {
                return null;
            }));
        });

        searchButton = cut.Find(".fa-magnifying-glass-plus");
        await cut.InvokeAsync(() => searchButton.Click());

        cut.WaitForAssertion(() => cut.Find(".fa-magnifying-glass"));
        queryButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => queryButton.Click());

        table = cut.FindComponent<Table<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetAdvancedSearchFilterCallback, new Func<PropertyInfo, Foo, List<SearchFilterAction>?>((p, model) =>
            {
                var v = p.GetValue(model);
                return
                [
                    new SearchFilterAction(p.Name, v, FilterAction.Equal)
                ];
            }));
        });

        searchButton = cut.Find(".fa-magnifying-glass-plus");
        await cut.InvokeAsync(() => searchButton.Click());

        cut.WaitForAssertion(() => cut.Find(".fa-magnifying-glass"));
        queryButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => queryButton.Click());
    }

    [Fact]
    public async Task EditDialog_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var dialogService = Context.Services.GetRequiredService<DialogService>();
        var items = Foo.GenerateFoo(localizer, 2);
        Dialog dialog = default!;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent(builder =>
            {
                builder.OpenComponent<Dialog>(0);
                builder.AddComponentReferenceCapture(1, obj => dialog = (Dialog)obj);
                builder.CloseComponent();

                builder.OpenComponent<Button>(2);
                builder.AddAttribute(3, "OnClick", EventCallback.Factory.Create<MouseEventArgs>(this, e => ShowDialog(dialogService, items, dialog)));
                builder.CloseComponent();
            });
        });

        // 点击按钮弹出 Dialog
        var button = cut.FindComponent<Button>();
        await cut.InvokeAsync(button.Instance.OnClick.InvokeAsync);

        // 点击表格新建按钮
        var table = cut.FindComponent<Table<Foo>>();
        var add = cut.Find(".table-toolbar button");
        await cut.InvokeAsync(() => add.Click());

        // 检查 dialog 是否显示
        var editDialog = cut.FindComponents<Dialog>().FirstOrDefault(i => i.Instance == dialog);
        Assert.NotNull(editDialog);
    }

    [Fact]
    public async Task Required_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.AddAttribute(3, "Required", true);
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.AddAttribute(3, "IsRequiredWhenAdd", true);
                    builder.AddAttribute(4, "IsRequiredWhenEdit", true);
                    builder.AddAttribute(4, "RequiredErrorMessage", "test error message");
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnSaveAsync, (foo, itemType) => Task.FromResult(true));
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        var modal = cut.FindComponent<Modal>();

        // 选一个
        var item = cut.FindComponent<Checkbox<Foo>>();
        await cut.InvokeAsync(item.Instance.OnToggleClick);
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        var form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
    }

    private static Task ShowDialog(DialogService dialogService, List<Foo> items, Dialog dialog) => dialogService.Show(new DialogOption()
    {
        Title = "test-dialog-table",
        Component = BootstrapDynamicComponent.CreateComponent<Table<Foo>>(new Dictionary<string, object?>()
            {
                {"RenderMode",  TableRenderMode.Table},
                {"Items", items},
                {"EditDialog", dialog},
                {"IsMultipleSelect", true},
                {"ShowToolbar", true },
                {"TableColumns", new RenderFragment<Foo>(foo => builder =>
                    {
                        builder.OpenComponent<TableColumn<Foo, string>>(0);
                        builder.AddAttribute(1, "Field", "Name");
                        builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                        builder.CloseComponent();
                    })
                }
            })
    });
}
