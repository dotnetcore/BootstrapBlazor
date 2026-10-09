// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace UnitTest.Components;

public class TableDrawerTest : TableDialogTestBase
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task KeepAdding_Ok(bool closeByBackdrop)
    {
        var addCount = 0;
        var savedModels = new List<Foo>();
        var cancelCount = 0;
        var closeCount = 0;
        var saveSuccess = true;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.EditMode, EditMode.Drawer);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.OnAddAsync, () => Task.FromResult(new Foo { Name = $"test-{++addCount}" }));
                pb.Add(a => a.OnSaveAsync, (foo, type) =>
                {
                    Assert.Equal(ItemChangedType.Add, type);
                    if (saveSuccess)
                    {
                        savedModels.Add(foo);
                    }
                    return Task.FromResult(saveSuccess);
                });
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    cancelCount++;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.EditDialogCloseAsync, (foo, saved) =>
                {
                    Assert.False(saved);
                    Assert.Equal("test-3", foo.Name);
                    closeCount++;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<Foo>>().Instance.AddAsync());
        var drawer = cut.FindComponent<Drawer>().Instance;
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var firstForm = editor.FindComponent<ValidateForm>().Instance;

        await cut.InvokeAsync(() => editor.Find("form").Submit());
        editor.WaitForAssertion(() => Assert.Equal("test-2", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name));
        Assert.Equal(2, addCount);
        Assert.Single(savedModels);
        Assert.True(drawer.IsOpen);
        Assert.Same(drawer, cut.FindComponent<Drawer>().Instance);
        Assert.NotSame(firstForm, editor.FindComponent<ValidateForm>().Instance);
        Assert.Equal("test-2", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);
        cut.FindComponent<DrawerContainer>().Render();
        Assert.Equal("test-2", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);

        await cut.InvokeAsync(() => editor.Find("form").Submit());
        editor.WaitForAssertion(() => Assert.Equal("test-3", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name));
        Assert.Equal(new[] { "test-1", "test-2" }, savedModels.Select(foo => foo.Name));
        Assert.Equal(3, addCount);
        Assert.Equal("test-3", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);
        Assert.Contains("test-1", cut.Find("tbody").TextContent);
        Assert.Contains("test-2", cut.Find("tbody").TextContent);
        Assert.Equal(0, cancelCount);
        Assert.Equal(0, closeCount);

        saveSuccess = false;
        var currentForm = editor.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => editor.Find("form").Submit());
        editor.WaitForAssertion(() => Assert.False(editor.Find("button[type=submit]").HasAttribute("disabled")));
        Assert.Same(currentForm, editor.FindComponent<ValidateForm>().Instance);
        Assert.Equal(3, addCount);
        Assert.True(drawer.IsOpen);

        if (closeByBackdrop)
        {
            await cut.InvokeAsync(drawer.OnContainerClick);
        }
        else
        {
            await cut.InvokeAsync(() => cut.Find("form .btn-secondary").Click());
        }
        Assert.Equal(1, cancelCount);
        Assert.Equal(1, closeCount);
        Assert.False(drawer.IsOpen);
    }

    [Fact]
    public async Task EditAsync_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.CloseConfirmTitle, "close-confirm-title");
                pb.Add(a => a.CloseConfirmContent, "close-confirm-content");
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.Drawer);
                pb.Add(a => a.OnBeforeShowDrawer, new Func<DrawerOption, Task>(op =>
                {
                    op.ShowBackdrop = true;
                    return Task.CompletedTask;
                }));
                pb.Add(a => a.Items, items);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
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

        // 编辑弹窗逻辑
        var form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());

        // 内置数据服务取消回调
        await cut.InvokeAsync(() => table.Instance.EditAsync());

        // 获得关闭按钮直接关闭抽屉
        var closeButton = cut.Find(".bb-editor-footer .btn-secondary");
        await cut.InvokeAsync(() => closeButton.Click());

        // 自定义数据服务取消回调测试
        table.Render(pb =>
        {
            pb.Add(a => a.DataService, new MockEFCoreDataService(localizer));
        });
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        closeButton = cut.Find(".bb-editor-footer .btn-secondary");
        await cut.InvokeAsync(() => closeButton.Click());

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        closeButton = cut.Find(".bb-editor-footer .btn-secondary");
        await cut.InvokeAsync(() => closeButton.Click());

        // 自定义数据服务取消回调测试
        table.Render(pb =>
        {
            pb.Add(a => a.EditDialogFullScreenSize, FullScreenSize.Always);
        });
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        closeButton = cut.Find(".bb-editor-footer .btn-secondary");
        await cut.InvokeAsync(() => closeButton.Click());

        var closed = false;
        // 测试 CloseCallback
        table.Render(pb =>
        {
            pb.Add(a => a.EditDialogCloseAsync, (model, result) =>
            {
                closed = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        closeButton = cut.Find(".bb-editor-footer .btn-secondary");
        await cut.InvokeAsync(() => closeButton.Click());
        Assert.True(closed);

        // 保存失败，不关闭抽屉
        closed = false;
        table.Render(pb =>
        {
            pb.Add(a => a.OnSaveAsync, (foo, itemType) => Task.FromResult(false));
        });
        checkbox = cut.FindComponents<Checkbox<Foo>>()[1];
        await cut.InvokeAsync(checkbox.Instance.OnToggleClick);
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.False(closed);

        // IsTracking mode
        table.Render(pb =>
        {
            pb.Add(a => a.IsTracking, true);
            pb.Add(a => a.OnSaveAsync, (foo, itemType) => Task.FromResult(true));
        });
        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        // 编辑弹窗逻辑
        var input = cut.Find("form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());

        var itemsChanged = false;
        // 更新插入模式
        table.Render(pb =>
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
        input = cut.Find("form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.True(itemsChanged);

        // 设置双向绑定 Items 后再测试 Add Save
        table.Render(pb =>
        {
            pb.Add(a => a.IsTracking, false);
            pb.Add(a => a.OnSaveAsync, null);
            pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows => items = rows.ToList()));
        });

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find("form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.Equal(3, items.Count);

        table.Render(pb =>
        {
            pb.Add(a => a.InsertRowMode, InsertRowMode.Last);
        });

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find("form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());
        Assert.Equal(3, items.Count);

        // 数据源是 OnQueryAsync 提供
        table.Render(pb =>
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
        input = cut.Find("form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find("form");
        await cut.InvokeAsync(() => form.Submit());

        // 数据为三行
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(3, rows.Count);

        table.Render(pb =>
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

        table.Render(pb =>
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
        table.Render(pb =>
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
    public void TableEditDrawerOption_Ok()
    {
        var option = new TableEditDrawerOption<Foo>();
        option.ShowLabel = false;

        Assert.False(option.ShowLabel);
    }
}
