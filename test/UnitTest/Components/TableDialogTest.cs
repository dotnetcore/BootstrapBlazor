// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

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
        await cut.InvokeAsync(() => input.Click());
        await cut.InvokeAsync(() => table.Instance.EditAsync());

        cut.Contains("test-save");
        cut.Contains("test-close");

        cut.Contains("modal-lg");
        cut.DoesNotContain("btn-maximize");
        cut.Contains("is-draggable");

        // 编辑弹窗逻辑
        var form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());

        // 内置数据服务取消回调
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        var btnClose = cut.Find(".btn-close");
        await cut.InvokeAsync(() => btnClose.Click());

        // 自定义数据服务取消回调测试
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.DataService, new MockEFCoreDataService(localizer));
        });
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        btnClose = cut.Find(".btn-close");
        await cut.InvokeAsync(() => btnClose.Click());

        // Add 弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        btnClose = cut.Find(".btn-close");
        await cut.InvokeAsync(() => btnClose.Click());
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
