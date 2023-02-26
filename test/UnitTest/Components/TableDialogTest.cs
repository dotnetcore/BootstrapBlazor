// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using AngleSharp.Dom;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class TableDialogTest : TableDialogTestBase
{
    [Fact]
    public void EditAsync_Ok()
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
                pb.Add(a => a.EditDialogCloseButtonText, "test-close");
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
                });
                pb.Add(a => a.OnSaveAsync, (foo, itemType) => Task.FromResult(true));
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        // 选一个
        var input = cut.Find("tbody tr input");
        cut.InvokeAsync(() => input.Click());
        cut.InvokeAsync(() => table.Instance.EditAsync());

        cut.Contains("test-save");
        cut.Contains("test-close");

        cut.Contains("modal-lg");
        cut.DoesNotContain("btn-maximize");
        cut.Contains("is-draggable");

        // 编辑弹窗逻辑
        var form = cut.Find(".modal-body form");
        cut.InvokeAsync(() => form.Submit());
        var modal = cut.FindComponent<Modal>();
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 内置数据服务取消回调
        cut.InvokeAsync(() => table.Instance.EditAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 自定义数据服务取消回调测试
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DataService, new MockEFCoreDataService(localizer));
        });
        cut.InvokeAsync(() => table.Instance.EditAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // Add 弹窗
        cut.InvokeAsync(() => table.Instance.AddAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 自定义数据服务取消回调测试
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.EditDialogFullScreenSize, FullScreenSize.Always);
        });
        cut.InvokeAsync(() => table.Instance.AddAsync());
        Assert.Contains(" modal-fullscreen ", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

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
        cut.InvokeAsync(() => table.Instance.AddAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(closed);

        // IsTracking mode
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsTracking, true);
        });
        // Add 弹窗
        cut.InvokeAsync(() => table.Instance.AddAsync());

        // 编辑弹窗逻辑
        input = cut.Find(".modal-body form input.form-control");
        cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        cut.InvokeAsync(() => form.Submit());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

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
        cut.InvokeAsync(() => table.Instance.AddAsync());
        cut.Contains("test_edit_footer");

        // 编辑弹窗逻辑
        input = cut.Find(".modal-body form input.form-control");
        cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        cut.InvokeAsync(() => form.Submit());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(itemsChanged);

        // 设置双向绑定 Items 后再测试 Add Save
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsTracking, false);
            pb.Add(a => a.OnSaveAsync, null);
            pb.Add(a => a.ItemsChanged, EventCallback.Factory.Create<IEnumerable<Foo>>(this, rows => items = rows.ToList()));
        });
        // Add 弹窗
        cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find(".modal-body form input.form-control");
        cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        cut.InvokeAsync(() => form.Submit());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(3, items.Count);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.InsertRowMode, InsertRowMode.Last);
        });
        // Add 弹窗
        cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find(".modal-body form input.form-control");
        cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        cut.InvokeAsync(() => form.Submit());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
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
        cut.InvokeAsync(() => table.Instance.AddAsync());
        input = cut.Find(".modal-body form input.form-control");
        cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        cut.InvokeAsync(() => form.Submit());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 数据为三行
        var rows = cut.FindAll("tbody tr");
        Assert.Equal(3, rows.Count);
    }

    private class MockEFCoreDataService : IDataService<Foo>, IEntityFrameworkCoreDataService
    {
        IStringLocalizer<Foo> Localizer { get; set; }

        public MockEFCoreDataService(IStringLocalizer<Foo> localizer) => Localizer = localizer;

        public Task<bool> AddAsync(Foo model) => Task.FromResult(true);

        public Task<bool> DeleteAsync(IEnumerable<Foo> models) => Task.FromResult(true);

        public Task<QueryData<Foo>> QueryAsync(QueryPageOptions option)
        {
            var foos = Foo.GenerateFoo(Localizer, 2);
            var ret = new QueryData<Foo>()
            {
                Items = foos,
                TotalCount = 2,
                IsAdvanceSearch = true,
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true
            };
            return Task.FromResult(ret);
        }

        public Task<bool> SaveAsync(Foo model, ItemChangedType changedType) => Task.FromResult(true);

        public Task CancelAsync() => Task.CompletedTask;

        public Task EditAsync(object model) => Task.CompletedTask;
    }
}
