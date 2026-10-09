// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace UnitTest.Components;

public class TableDialogTest : TableDialogTestBase
{
    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task KeepAdding_ClosingEditorRejectsPendingValidation(EditMode mode)
    {
        var closeJs = mode == EditMode.Drawer
            ? Context.JSInterop.Setup<bool>("execute", invocation =>
                invocation.Arguments.Count == 2 && invocation.Arguments[1] is false)
            : null;
        var model = new DelayedValidationModel();
        var addCount = 0;
        var saveCount = 0;
        var cancelCount = 0;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DelayedValidationModel>>(pb =>
            {
                pb.Add(a => a.Items, new List<DelayedValidationModel>());
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.ShowCloseConfirm, false);
                pb.Add(a => a.EditTemplate, item => builder => builder.AddContent(0, item.Name));
                pb.Add(a => a.OnAddAsync, () =>
                {
                    addCount++;
                    return Task.FromResult(addCount == 1 ? model : new DelayedValidationModel());
                });
                pb.Add(a => a.OnSaveAsync, (item, changedType) =>
                {
                    saveCount++;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    cancelCount++;
                    return Task.CompletedTask;
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<DelayedValidationModel>>().Instance.AddAsync());
        var form = cut.FindComponent<EditDialog<DelayedValidationModel>>().FindComponent<EditForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnSubmit.InvokeAsync(form.EditContext!));
        var closing = Task.CompletedTask;
        try
        {
            await model.Started.Task.WaitAsync(TimeSpan.FromSeconds(5), Xunit.TestContext.Current.CancellationToken);
            if (mode == EditMode.Drawer)
            {
                var drawer = cut.FindComponent<Drawer>().Instance;
                closing = cut.InvokeAsync(drawer.Close);
                cut.WaitForAssertion(() => Assert.False(drawer.IsOpen));
                Assert.False(closing.IsCompleted);
            }
            else
            {
                await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.Close);
            }
            Assert.Single(cut.FindComponents<EditDialog<DelayedValidationModel>>());
            Assert.Equal(0, cancelCount);
            model.Continue.TrySetResult();
            await submit;
            Assert.Equal(0, saveCount);
            Assert.Equal(1, addCount);
        }
        finally
        {
            model.Continue.TrySetResult();
            closeJs?.SetResult(false);
        }
        await closing;
        if (mode == EditMode.Popup)
        {
            await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback);
        }
        Assert.Empty(cut.FindComponents<EditDialog<DelayedValidationModel>>());
        Assert.Equal(1, cancelCount);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task KeepAdding_DeniedCloseStillAllowsSave(EditMode mode)
    {
        var addCount = 0;
        var saveCount = 0;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.ShowCloseConfirm, false);
                pb.Add(a => a.OnAddAsync, () => Task.FromResult(new Foo { Name = $"item-{++addCount}" }));
                pb.Add(a => a.OnSaveAsync, (item, changedType) =>
                {
                    saveCount++;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnBeforeShowDrawer, option =>
                {
                    option.OnClosingAsync = () => Task.FromResult(false);
                    return Task.CompletedTask;
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<Foo>>().Instance.AddAsync());
        if (mode == EditMode.Drawer)
        {
            var drawer = cut.FindComponent<Drawer>().Instance;
            await cut.InvokeAsync(drawer.Close);
            Assert.True(drawer.IsOpen);
        }
        else
        {
            var modal = cut.FindComponent<Modal>().Instance;
            Func<Task<bool>> denyClose = () => Task.FromResult(false);
            modal.RegisterOnClosingCallback(denyClose);
            try
            {
                Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
            }
            finally
            {
                modal.UnRegisterOnClosingCallback(denyClose);
            }
        }
        var editor = cut.FindComponent<EditDialog<Foo>>();
        Assert.True(await cut.InvokeAsync(editor.Instance.CanCloseAsync));
        var form = editor.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.Equal(1, saveCount);
        Assert.Equal(2, addCount);
        Assert.Equal("item-2", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task KeepAdding_CloseThenReopenAllowsSave(EditMode mode)
    {
        var addCount = 0;
        var saveCount = 0;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.ShowCloseConfirm, false);
                pb.Add(a => a.OnAddAsync, () => Task.FromResult(new Foo { Name = $"item-{++addCount}" }));
                pb.Add(a => a.OnSaveAsync, (item, changedType) =>
                {
                    saveCount++;
                    return Task.FromResult(true);
                });
            });
        });
        var table = cut.FindComponent<Table<Foo>>().Instance;
        await cut.InvokeAsync(table.AddAsync);
        if (mode == EditMode.Drawer)
        {
            await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
        }
        else
        {
            var modal = cut.FindComponent<Modal>().Instance;
            await cut.InvokeAsync(modal.Close);
            await cut.InvokeAsync(modal.CloseCallback);
        }
        Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
        await cut.InvokeAsync(table.AddAsync);
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.Equal(1, saveCount);
        Assert.Equal(3, addCount);
        Assert.Equal("item-3", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task KeepAdding_ClosedEditorRejectsPendingValidation(EditMode mode)
    {
        var model = new DelayedValidationModel();
        var addCount = 0;
        var saveCount = 0;
        var cancelCount = 0;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<DelayedValidationModel>>(pb =>
            {
                pb.Add(a => a.Items, new List<DelayedValidationModel>());
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.ShowCloseConfirm, false);
                pb.Add(a => a.EditTemplate, item => builder => builder.AddContent(0, item.Name));
                pb.Add(a => a.OnAddAsync, () =>
                {
                    addCount++;
                    return Task.FromResult(addCount == 1 ? model : new DelayedValidationModel());
                });
                pb.Add(a => a.OnSaveAsync, (item, changedType) =>
                {
                    saveCount++;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    cancelCount++;
                    return Task.CompletedTask;
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<DelayedValidationModel>>().Instance.AddAsync());
        var form = cut.FindComponent<EditDialog<DelayedValidationModel>>().FindComponent<EditForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnSubmit.InvokeAsync(form.EditContext!));
        try
        {
            await model.Started.Task.WaitAsync(TimeSpan.FromSeconds(5), Xunit.TestContext.Current.CancellationToken);
            if (mode == EditMode.Drawer)
            {
                await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
            }
            else
            {
                await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback);
            }
            Assert.Empty(cut.FindComponents<EditDialog<DelayedValidationModel>>());
            Assert.Equal(1, cancelCount);
        }
        finally
        {
            model.Continue.TrySetResult();
        }
        await submit;
        Assert.Equal(0, saveCount);
        Assert.Equal(1, addCount);
    }

    public sealed class DelayedValidationModel : IAsyncValidatableObject
    {
        public string Name { get; set; } = "first";
        public TaskCompletionSource Started { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);
        public TaskCompletionSource Continue { get; } = new(TaskCreationOptions.RunContinuationsAsynchronously);

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => [];

        public async IAsyncEnumerable<ValidationResult> ValidateAsync(
            ValidationContext validationContext,
            [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            Started.TrySetResult();
            await Continue.Task.WaitAsync(cancellationToken);
            yield break;
        }
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task KeepAdding_InitializingBlocksClose(EditMode mode)
    {
        var nextModel = new TaskCompletionSource<Foo>(TaskCreationOptions.RunContinuationsAsynchronously);
        var addCount = 0;
        var cancelCount = 0;
        var closingCount = 0;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.Items, new List<Foo>());
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.OnAddAsync, () => ++addCount == 1
                    ? Task.FromResult(new Foo { Name = "first" })
                    : nextModel.Task);
                pb.Add(a => a.OnSaveAsync, (foo, type) => Task.FromResult(true));
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    cancelCount++;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.OnBeforeShowDrawer, option =>
                {
                    option.OnClosingAsync = () =>
                    {
                        closingCount++;
                        return Task.FromResult(true);
                    };
                    return Task.CompletedTask;
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<Foo>>().Instance.AddAsync());
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        editor.WaitForAssertion(() => Assert.Equal(2, addCount));
        Assert.False(await cut.InvokeAsync(editor.Instance.CanCloseAsync));
        if (mode == EditMode.Drawer)
        {
            await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
            Assert.True(cut.FindComponent<Drawer>().Instance.IsOpen);
            Assert.Equal(0, closingCount);
        }
        else
        {
            Assert.False(await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.BeforeCloseCallback));
            Assert.Single(cut.FindComponents<ModalDialog>());
        }
        Assert.Equal(0, cancelCount);

        nextModel.SetResult(new Foo { Name = "next" });
        await submit;
        Assert.Equal("next", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);
        Assert.True(await cut.InvokeAsync(editor.Instance.CanCloseAsync));
        if (mode == EditMode.Drawer)
        {
            await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
            Assert.Equal(1, closingCount);
        }
        else
        {
            await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback);
        }
        Assert.Equal(1, cancelCount);
    }

    [Theory]
    [InlineData(EditMode.Popup, false, false)]
    [InlineData(EditMode.Drawer, false, false)]
    [InlineData(EditMode.Popup, true, false)]
    [InlineData(EditMode.Drawer, true, false)]
    [InlineData(EditMode.Popup, false, true)]
    [InlineData(EditMode.Drawer, false, true)]
    public async Task KeepAdding_InitializationFailureCleansUpOnClose(EditMode mode, bool failCleanup, bool failCancelCallback)
    {
        var dataService = new FailingNextModelDataService(failCleanup);
        var cancelCount = 0;
        var closeCount = 0;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.DataService, dataService);
                pb.Add(a => a.CreateItemCallback, () => new Foo { Name = "new" });
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    cancelCount++;
                    return failCancelCallback
                        ? Task.FromException(new InvalidOperationException("Cancel callback failed"))
                        : Task.CompletedTask;
                });
                pb.Add(a => a.EditDialogCloseAsync, (foo, saved) =>
                {
                    Assert.False(saved);
                    closeCount++;
                    return Task.CompletedTask;
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<Foo>>().Instance.AddAsync());
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.Equal(1, dataService.SaveCount);
        Assert.Equal(2, dataService.AddCount);
        Assert.Equal(0, dataService.CancelCount);
        Assert.Equal(0, cancelCount);
        Assert.NotEmpty(editor.Find("[role=alert]").TextContent);
        Assert.True(editor.FindComponents<Button>().Single(button => button.Instance.ButtonType == ButtonType.Submit).Instance.IsDisabled);
        Assert.True(await cut.InvokeAsync(editor.Instance.CanCloseAsync));

        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        Assert.Equal(1, dataService.SaveCount);
        Func<Task> close = mode == EditMode.Drawer
            ? () => cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close)
            : () => cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback);
        if (failCleanup || failCancelCallback)
        {
            await Assert.ThrowsAsync<InvalidOperationException>(close);
        }
        else
        {
            await close();
        }
        Assert.Equal(1, dataService.CancelCount);
        Assert.Equal(1, cancelCount);
        Assert.Equal(1, closeCount);
    }

    private sealed class FailingNextModelDataService(bool failCleanup) : IDataService<Foo>, IEntityFrameworkCoreDataService
    {
        public int AddCount { get; private set; }
        public int SaveCount { get; private set; }
        public int CancelCount { get; private set; }

        public Task<bool> AddAsync(Foo model) => ++AddCount == 1
            ? Task.FromResult(true)
            : Task.FromException<bool>(new InvalidOperationException("Next model initialization failed"));

        public Task<bool> SaveAsync(Foo model, ItemChangedType changedType)
        {
            SaveCount++;
            return Task.FromResult(true);
        }

        public Task CancelAsync()
        {
            CancelCount++;
            return failCleanup
                ? Task.FromException(new InvalidOperationException("Cancellation failed"))
                : Task.CompletedTask;
        }

        public Task<QueryData<Foo>> QueryAsync(QueryPageOptions option) => Task.FromResult(new QueryData<Foo>
        {
            Items = [],
            TotalCount = 0
        });

        public Task<bool> DeleteAsync(IEnumerable<Foo> models) => Task.FromResult(true);

        public Task EditAsync(object model) => Task.CompletedTask;
    }

    [Fact]
    public async Task KeepAdding_Ok()
    {
        var addCount = 0;
        var saveCount = 0;
        var cancelCount = 0;
        bool? closeSaved = null;
        string? closeModelName = null;
        var items = new List<Foo>();
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.OnAddAsync, () => Task.FromResult(new Foo { Name = $"test-{++addCount}" }));
                pb.Add(a => a.OnSaveAsync, (foo, itemType) =>
                {
                    saveCount++;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    cancelCount++;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.EditDialogCloseAsync, (foo, saved) =>
                {
                    closeModelName = foo.Name;
                    closeSaved = saved;
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

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        var editor = cut.FindComponent<EditDialog<Foo>>();
        var firstForm = editor.FindComponent<ValidateForm>().Instance;
        Assert.Equal("test-1", ((Foo)firstForm.Model!).Name);

        await cut.InvokeAsync(() => editor.Find("form").Submit());

        editor.WaitForAssertion(() =>
        {
            Assert.Equal(2, addCount);
            Assert.Equal(1, saveCount);
            Assert.Single(cut.FindComponents<ModalDialog>());
            Assert.Equal("test-2", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);
            Assert.NotSame(firstForm, editor.FindComponent<ValidateForm>().Instance);
            Assert.Contains("test-1", cut.Find("tbody").TextContent);
        });
        cut.FindComponent<Dialog>().Render();
        Assert.Equal("test-2", ((Foo)editor.FindComponent<ValidateForm>().Instance.Model!).Name);

        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(1, cancelCount);
        Assert.False(closeSaved);
        Assert.Equal("test-2", closeModelName);
    }

    [Fact]
    public async Task EditAsync_Ok()
    {
        var options = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        options.CurrentValue.ToastDelay = 0;
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
                pb.Add(a => a.EditDialogLabelWidth, 200);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.CloseConfirmTitle, "close-confirm-title");
                pb.Add(a => a.CloseConfirmContent, "close-confirm-content");
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
        cut.Contains("--bb-row-label-width: 200px;");

        // 编辑弹窗逻辑
        var form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 内置数据服务取消回调
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 自定义数据服务取消回调测试
        table.Render(pb =>
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
        table.Render(pb =>
        {
            pb.Add(a => a.EditDialogFullScreenSize, FullScreenSize.Always);
        });
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        Assert.Contains(" modal-fullscreen ", cut.Markup);
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

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
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(closed);

        // IsTracking mode
        table.Render(pb =>
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
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
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
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(3, items.Count);

        table.Render(pb =>
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
        input = cut.Find(".modal-body form input.form-control");
        await cut.InvokeAsync(() => input.Change("Test_Name"));

        form = cut.Find(".modal-body form");
        await cut.InvokeAsync(() => form.Submit());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

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

        await cut.WaitForAssertionAsync(() => cut.Find(".fa-magnifying-glass"));
        var queryButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => queryButton.Click());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        table.Render(pb =>
        {
            pb.Add(a => a.GetAdvancedSearchFilterCallback, new Func<PropertyInfo, Foo, List<SearchFilterAction>?>((p, model) =>
            {
                return null;
            }));
        });

        searchButton = cut.Find(".fa-magnifying-glass-plus");
        await cut.InvokeAsync(() => searchButton.Click());

        await cut.WaitForAssertionAsync(() => cut.Find(".fa-magnifying-glass"));
        queryButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => queryButton.Click());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

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

        await cut.WaitForAssertionAsync(() => cut.Find(".fa-magnifying-glass"));
        queryButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => queryButton.Click());
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 开启 UseSearchForm 优先级最高
        FilterKeyValueAction? filter = null;
        table.Render(pb =>
        {
            pb.Add(a => a.UseSearchForm, true);
            pb.Add(a => a.SearchItems, new List<ISearchItem>()
            {
                new SearchItem("Name", typeof(string), "Name"),
                new SearchItem("Address", typeof(string), "Address")
                {
                    Metadata = new StringSearchMetadata() { PlaceHolder = "Address-Placeholder" }
                }
            });
            pb.Add(a => a.OnQueryAsync, options =>
            {
                filter = options.ToFilter();
                return Task.FromResult(new QueryData<Foo>()
                {
                    Items = items,
                    TotalCount = items.Count,
                    IsAdvanceSearch = true,
                    IsSearch = true,
                    IsFiltered = true,
                    IsSorted = true
                });
            });
        });
        // 弹出高级搜索弹窗内部使用 SearchForm 组件，测试 SearchForm 组件的功能
        searchButton = cut.Find(".fa-magnifying-glass-plus");
        await cut.InvokeAsync(() => searchButton.Click());
        await cut.WaitForAssertionAsync(() => cut.Find(".fa-magnifying-glass"));

        // 查找高级搜索弹窗组件
        var searchDialog = cut.FindComponent<SearchDialog<Foo>>();
        Assert.NotNull(searchDialog);
        searchDialog.Contains("Address-Placeholder");

        // 更新搜索条件值
        var searchItem = searchDialog.FindComponent<BootstrapInput<string>>();
        Assert.NotNull(searchItem.Instance.OnValueChanged);
        await cut.InvokeAsync(() => searchItem.Instance.OnValueChanged("Test_Name"));

        // 测试点击搜索按钮
        searchButton = cut.Find(".fa-magnifying-glass");
        await cut.InvokeAsync(() => searchButton.Click());
        // 关闭弹窗
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.NotNull(filter);
        Assert.Single(filter.Filters);

        // 测试点击重置按钮
        searchButton = cut.Find(".fa-magnifying-glass-plus");
        await cut.InvokeAsync(() => searchButton.Click());
        await cut.WaitForAssertionAsync(() => cut.Find(".fa-magnifying-glass"));

        var resetButton = cut.Find(".fa-trash-can");
        await cut.InvokeAsync(() => resetButton.Click());
        // 关闭弹窗
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.NotNull(filter);
        Assert.Empty(filter.Filters.SelectMany(i => i.Filters).SelectMany(i => i.Filters));
    }

    [Fact]
    public async Task EditDialog_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var dialogService = Context.Services.GetRequiredService<DialogService>();
        var items = Foo.GenerateFoo(localizer, 2);
        Dialog dialog = default!;
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
        table.Render(pb =>
        {
            pb.Add(a => a.ShowCloseConfirm, true);
        });
        var add = cut.Find(".table-toolbar button");
        await cut.InvokeAsync(() => add.Click());

        // 检查 dialog 是否显示
        var editDialog = cut.FindComponents<Dialog>().FirstOrDefault(i => i.Instance == dialog);
        Assert.NotNull(editDialog);

        // 更新变化值
        IRenderedComponent<ValidateForm> renderedComponent = cut.FindComponent<ValidateForm>();
        var editForm = renderedComponent;
        editForm.Instance.OnFieldValueChanged("Name", "Test_Name");

        var modal = cut.FindComponent<Modal>();
        // 弹出确认窗
        _ = Task.Run(async () => await cut.InvokeAsync(() => modal.Instance.BeforeCloseCallback()));

        // 模拟点击确认按钮
        cut.WaitForAssertion(() => cut.Find(".swal2-actions"));
        var closeButton = cut.Find(".swal2-actions .btn-danger");
        await cut.InvokeAsync(() => closeButton.Click());

        // 关闭 Swal 确认弹窗
        var count = cut.FindComponents<Modal>().Count;
        var swalModal = cut.FindComponents<Modal>()[count - 1];
        await cut.InvokeAsync(() => swalModal.Instance.CloseCallback());
    }

    [Fact]
    public async Task Required_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
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
