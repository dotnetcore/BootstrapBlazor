// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using TestContext = Xunit.TestContext;

namespace UnitTest.Components;

public class EditDialogLifecycleTest : BootstrapBlazorTestBase
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task BoundDrawerClose_RejectsOldValidation(bool reopen)
    {
        var model = new TableDialogTest.DelayedValidationModel();
        var saved = 0;
        var closed = 0;
        var changed = 0;
        var cut = Context.Render<Drawer>(pb =>
        {
            pb.Add(a => a.IsOpen, true);
            pb.Add(a => a.OnCloseAsync, () =>
            {
                closed++;
                return Task.CompletedTask;
            });
            pb.Add(a => a.IsOpenChanged, value => changed++);
            pb.AddChildContent<EditDialog<TableDialogTest.DelayedValidationModel>>(editor =>
            {
                editor.Add(a => a.Model, model);
                editor.Add(a => a.ShowCloseConfirm, false);
                editor.Add(a => a.KeepOpenAfterSave, true);
                editor.Add(a => a.BodyTemplate, item => builder => builder.AddContent(0, item.Name));
                editor.Add(a => a.OnSaveAsync, context =>
                {
                    saved++;
                    return Task.FromResult(false);
                });
            });
        });
        var form = cut.FindComponent<EditForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnSubmit.InvokeAsync(form.EditContext!));
        try
        {
            await model.Started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            cut.Render(pb => pb.Add(a => a.IsOpen, false));
            if (reopen)
            {
                cut.Render(pb => pb.Add(a => a.IsOpen, true));
            }
            model.Continue.TrySetResult();
            await submit;
            Assert.Equal(0, saved);
            Assert.Equal(0, closed);
            Assert.Equal(0, changed);
            if (reopen)
            {
                var currentForm = cut.FindComponent<ValidateForm>().Instance;
                await cut.InvokeAsync(() => currentForm.OnValidSubmit!(new EditContext(model)));
                Assert.Equal(1, saved);
            }
        }
        finally
        {
            model.Continue.TrySetResult();
            await submit;
        }
    }

    [Fact]
    public async Task PopupHideFailure_AllowsRetryInsteadOfPermanentClosing()
    {
        var failure = new InvalidOperationException("Hide failed");
        var hide = Context.JSInterop.SetupVoid("execute", invocation =>
            invocation.Arguments.Count == 2 && invocation.Arguments[1] is string method && method == "hide");
        hide.SetException(failure);
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        var modal = cut.FindComponent<Modal>().Instance;
        Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(modal.Close)));
        Assert.Single(cut.FindComponents<EditDialog<Foo>>());
        hide.SetVoidResult();
        await cut.InvokeAsync(modal.Close);
        await cut.InvokeAsync(modal.CloseCallback);
        Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
    }

    [Fact]
    public async Task BoundDrawerReopen_DoesNotAcceptPreviousCloseCheck()
    {
        var first = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var second = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var checks = 0;
        var closed = 0;
        var cut = Context.Render<Drawer>(pb =>
        {
            pb.Add(a => a.IsOpen, true);
            pb.Add(a => a.OnClosingAsync, () => ++checks == 1 ? first.Task : second.Task);
            pb.Add(a => a.OnCloseAsync, () =>
            {
                closed++;
                return Task.CompletedTask;
            });
        });
        var oldClose = cut.InvokeAsync(cut.Instance.Close);
        cut.WaitForAssertion(() => Assert.Equal(1, checks));
        cut.Render(pb => pb.Add(a => a.IsOpen, false));
        cut.Render(pb => pb.Add(a => a.IsOpen, true));
        var currentClose = cut.InvokeAsync(cut.Instance.Close);
        try
        {
            cut.WaitForAssertion(() => Assert.Equal(2, checks));
            first.TrySetResult(true);
            await oldClose;
            Assert.True(cut.Instance.IsOpen);
            Assert.Equal(0, closed);
            await cut.InvokeAsync(cut.Instance.Close);
            Assert.Equal(2, checks);
            second.TrySetResult(true);
            await currentClose;
            Assert.False(cut.Instance.IsOpen);
            Assert.Equal(1, closed);
        }
        finally
        {
            first.TrySetResult(false);
            second.TrySetResult(false);
            await Task.WhenAll(oldClose, currentClose);
        }
    }

    [Fact]
    public async Task PopupLateHideFailure_DoesNotRecoverNewClosingTarget()
    {
        var hide = Context.JSInterop.SetupVoid("execute", invocation =>
            invocation.Arguments.Count == 2 && invocation.Arguments[1] is string method && method == "hide");
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        var modal = cut.FindComponent<Modal>().Instance;
        var closing = cut.InvokeAsync(modal.Close);
        try
        {
            cut.WaitForAssertion(() => Assert.Single(hide.Invocations));
            var next = new Foo();
            await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
            {
                Model = next, ShowConfirmCloseSwal = false
            }));
            await cut.InvokeAsync(modal.CloseCallback);
            Assert.Same(next, cut.FindComponent<EditDialog<Foo>>().Instance.Model);
            Assert.True(await cut.InvokeAsync(() => modal.BeforeCloseWithRequestCallback("next")));
            hide.SetException(new InvalidOperationException("Late hide failure"));
            await Assert.ThrowsAsync<InvalidOperationException>(() => closing);
            Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
            await cut.InvokeAsync(modal.CloseCallback);
            Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
        }
        finally
        {
            hide.SetVoidResult();
        }
    }

    [Fact]
    public async Task BrowserHideFailure_OnlyRecoversMatchingUnclosedRequest()
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        var modal = cut.FindComponent<Modal>().Instance;
        Assert.True(await cut.InvokeAsync(() => modal.BeforeCloseWithRequestCallback("first")));
        await cut.InvokeAsync(() => modal.CloseFailedCallback("unrelated"));
        Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
        await cut.InvokeAsync(() => modal.CloseFailedCallback("first"));
        Assert.True(await cut.InvokeAsync(() => modal.BeforeCloseWithRequestCallback("retry")));
        await cut.InvokeAsync(modal.CloseCallback);
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        Assert.True(await cut.InvokeAsync(() => modal.BeforeCloseWithRequestCallback("next")));
        await cut.InvokeAsync(() => modal.CloseFailedCallback("retry"));
        Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
        await cut.InvokeAsync(modal.CloseCallback);
        Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
    }

    [Fact]
    public async Task PopupLateHideFailure_DoesNotRecoverRetryInSamePresentation()
    {
        var hide = Context.JSInterop.SetupVoid("execute", invocation =>
            invocation.Arguments.Count == 2 && invocation.Arguments[1] is string method && method == "hide");
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        var modal = cut.FindComponent<Modal>().Instance;
        var closing = cut.InvokeAsync(modal.Close);
        try
        {
            cut.WaitForAssertion(() => Assert.Single(hide.Invocations));
            var field = typeof(Modal).GetField("_closingWindow", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var window = Assert.IsAssignableFrom<System.Runtime.CompilerServices.ITuple>(field!.GetValue(modal));
            var request = Assert.IsType<string>(window[3]);
            await cut.InvokeAsync(() => modal.CloseFailedCallback(request));
            Assert.True(await cut.InvokeAsync(() => modal.BeforeCloseWithRequestCallback("retry")));
            hide.SetException(new InvalidOperationException("Late hide failure"));
            await Assert.ThrowsAsync<InvalidOperationException>(() => closing);
            Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
            await cut.InvokeAsync(modal.CloseCallback);
        }
        finally
        {
            hide.SetVoidResult();
        }
    }

    [Theory]
    [InlineData(EditMode.Popup, false)]
    [InlineData(EditMode.Drawer, false)]
    [InlineData(EditMode.Popup, true)]
    [InlineData(EditMode.Drawer, true)]
    public async Task ItemsAfterSaveFailure_MarksOnlyExternalPersistence(EditMode mode, bool externalSave)
    {
        var fail = true;
        var saved = 0;
        var closedSaved = false;
        var model = new Foo { Name = "record" };
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(table =>
            {
                table.Add(a => a.Items, new List<Foo>());
                table.Add(a => a.IsTracking, false);
                table.Add(a => a.EditMode, mode);
                table.Add(a => a.ShowCloseConfirm, false);
                table.Add(a => a.KeepAdding, true);
                table.Add(a => a.OnAddAsync, () => Task.FromResult(model));
                if (externalSave)
                {
                    table.Add(a => a.OnSaveAsync, (item, type) =>
                    {
                        saved++;
                        return Task.FromResult(true);
                    });
                }
                table.Add(a => a.OnAfterSaveAsync, item =>
                    fail ? Task.FromException(new InvalidOperationException("After-save failed")) : Task.CompletedTask);
                table.Add(a => a.EditDialogCloseAsync, (item, wasSaved) =>
                {
                    closedSaved = wasSaved;
                    return Task.CompletedTask;
                });
            });
        });
        var table = cut.FindComponent<Table<Foo>>().Instance;
        await cut.InvokeAsync(table.AddAsync);
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model))));
        Assert.Empty(table.Items!);
        Assert.Equal(externalSave, editor.Find("button[type='submit']").HasAttribute("disabled"));
        fail = false;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        if (externalSave)
        {
            Assert.Equal(1, saved);
            Assert.Empty(table.Items!);
        }
        else
        {
            Assert.Single(table.Items!);
        }
        if (mode == EditMode.Drawer)
        {
            await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
        }
        else
        {
            await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback);
        }
        Assert.Equal(externalSave, closedSaved);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task ItemsChangedFailureAfterMutation_BlocksRetry(EditMode mode)
    {
        var changed = 0;
        var model = new Foo();
        var cut = Context.Render<BootstrapBlazorRoot>(pb => pb.AddChildContent<Table<Foo>>(table =>
        {
            table.Add(a => a.Items, new List<Foo>());
            table.Add(a => a.IsTracking, false);
            table.Add(a => a.EditMode, mode);
            table.Add(a => a.ShowCloseConfirm, false);
            table.Add(a => a.KeepAdding, true);
            table.Add(a => a.OnAddAsync, () => Task.FromResult(model));
            table.Add(a => a.ItemsChanged, items =>
            {
                changed++;
                throw new InvalidOperationException("Items changed failed");
            });
        }));
        var table = cut.FindComponent<Table<Foo>>().Instance;
        await cut.InvokeAsync(table.AddAsync);
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model))));
        Assert.Single(table.Rows);
        Assert.True(editor.Find("button[type='submit']").HasAttribute("disabled"));
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        Assert.Equal(1, changed);
        Assert.Single(table.Rows);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task ItemsUpdateHookFailure_AllowsRetryBeforeReplacingRow(EditMode mode)
    {
        var fail = true;
        var original = new Foo { Name = "original" };
        var cut = Context.Render<BootstrapBlazorRoot>(pb => pb.AddChildContent<Table<Foo>>(table =>
        {
            table.Add(a => a.Items, new[] { original });
            table.Add(a => a.IsTracking, false);
            table.Add(a => a.EditMode, mode);
            table.Add(a => a.ShowCloseConfirm, false);
            table.Add(a => a.ModelEqualityComparer, (first, second) => first.Id == second.Id);
            table.Add(a => a.OnAfterSaveAsync, item =>
                fail ? Task.FromException(new InvalidOperationException("Update hook failed")) : Task.CompletedTask);
        }));
        var table = cut.FindComponent<Table<Foo>>().Instance;
        table.SelectedRows.Add(original);
        await cut.InvokeAsync(table.EditAsync);
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        var edited = (Foo)form.Model!;
        edited.Name = "updated";
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(edited))));
        Assert.Same(original, Assert.Single(table.Items!));
        Assert.Equal("original", original.Name);
        Assert.False(editor.Find("button[type='submit']").HasAttribute("disabled"));
        fail = false;
        await cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(edited)));
        Assert.Same(edited, Assert.Single(table.Items!));
        Assert.Equal("updated", table.Items!.Single().Name);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task ClosingRerender_PreservesSubmissionBlock(EditMode mode)
    {
        var closeJs = mode == EditMode.Drawer
            ? Context.JSInterop.Setup<bool>("execute", invocation =>
                invocation.Arguments.Count == 2 && invocation.Arguments[1] is false)
            : null;
        var model = new TableDialogTest.DelayedValidationModel();
        var saved = 0;
        var created = 0;
        ITableEditDialogOption<TableDialogTest.DelayedValidationModel> option = mode == EditMode.Drawer
            ? new TableEditDrawerOption<TableDialogTest.DelayedValidationModel>()
            : new EditDialogOption<TableDialogTest.DelayedValidationModel>();
        option.Model = model;
        option.ShowConfirmCloseSwal = false;
        option.KeepOpenAfterSave = true;
        option.DialogBodyTemplate = item => builder => builder.AddContent(0, item.Name);
        option.OnEditAsync = context =>
        {
            saved++;
            return Task.FromResult(true);
        };
        option.CreateNextModelAsync = () =>
        {
            created++;
            return Task.FromResult(new TableDialogTest.DelayedValidationModel());
        };
        var cut = Context.Render<BootstrapBlazorRoot>();
        if (option is TableEditDrawerOption<TableDialogTest.DelayedValidationModel> drawerOption)
        {
            var service = Context.Services.GetRequiredService<DrawerService>();
            await cut.InvokeAsync(() => service.ShowEditDrawer(drawerOption, new DrawerOption()));
        }
        else if (option is EditDialogOption<TableDialogTest.DelayedValidationModel> dialogOption)
        {
            var service = Context.Services.GetRequiredService<DialogService>();
            await cut.InvokeAsync(() => service.ShowEditDialog(dialogOption));
        }
        var form = cut.FindComponent<EditForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnSubmit.InvokeAsync(form.EditContext!));
        var closing = Task.CompletedTask;
        try
        {
            await model.Started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            if (mode == EditMode.Drawer)
            {
                var drawer = cut.FindComponent<Drawer>().Instance;
                closing = cut.InvokeAsync(drawer.Close);
                cut.WaitForAssertion(() => Assert.False(drawer.IsOpen));
                Assert.False(closing.IsCompleted);
                cut.FindComponent<DrawerContainer>().Render();
                Assert.False(drawer.IsOpen);
            }
            else
            {
                await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.Close);
                cut.FindComponent<Dialog>().Render();
            }
            model.Continue.TrySetResult();
            await submit;
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
        Assert.Equal(new[] { 0, 0 }, new[] { saved, created });
    }

    [Fact]
    public async Task PopupRerender_PreservesRegisteredBusyGuard()
    {
        var saving = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var model = new Foo();
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = model,
            ShowConfirmCloseSwal = false,
            OnEditAsync = context =>
            {
                started.TrySetResult();
                return saving.Task;
            }
        }));
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            cut.FindComponent<Dialog>().Render();
            Assert.False(await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.BeforeCloseCallback));
        }
        finally
        {
            saving.TrySetResult(false);
            await submit;
        }
    }

    [Fact]
    public async Task PopupOptionGuard_CanDenyCloseAlongsideEditorGuard()
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnClosingAsync = () => Task.FromResult(false)
        }));
        Assert.False(await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.BeforeCloseCallback));
    }

    [Fact]
    public async Task ModalGuards_AwaitAllAndHonorAnyVeto()
    {
        var gate = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var lastGuardCalls = 0;
        var cut = Context.Render<Modal>();
        cut.Instance.RegisterOnClosingCallback(async () =>
        {
            started.TrySetResult();
            return await gate.Task;
        });
        cut.Instance.RegisterOnClosingCallback(() =>
        {
            lastGuardCalls++;
            return Task.FromResult(true);
        });
        var checking = cut.InvokeAsync(cut.Instance.BeforeCloseCallback);
        await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
        Assert.False(checking.IsCompleted);
        Assert.False(await cut.InvokeAsync(cut.Instance.BeforeCloseCallback));
        gate.TrySetResult(false);
        Assert.False(await checking);
        Assert.Equal(1, lastGuardCalls);
    }

    [Fact]
    public async Task MultiPopup_ReturningToParentPreservesBusyGuard()
    {
        var saving = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var model = new Foo();
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = model,
            ShowConfirmCloseSwal = false,
            OnEditAsync = context =>
            {
                started.TrySetResult();
                return saving.Task;
            }
        }));
        var parentEditor = cut.FindComponent<EditDialog<Foo>>();
        var form = parentEditor.FindComponent<ValidateForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
            {
                Model = new Foo(),
                ShowConfirmCloseSwal = false
            }));
            var modal = cut.FindComponent<Modal>().Instance;
            Assert.True(await cut.InvokeAsync(modal.BeforeCloseCallback));
            await cut.InvokeAsync(modal.CloseCallback);
            Assert.Single(cut.FindComponents<EditDialog<Foo>>());
            Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
        }
        finally
        {
            saving.TrySetResult(false);
            await submit;
        }
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task CloseException_StillRemovesCurrentEditor(EditMode mode)
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var failure = new InvalidOperationException("Close cleanup failed");
        if (mode == EditMode.Popup)
        {
            var service = Context.Services.GetRequiredService<DialogService>();
            await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
            {
                Model = new Foo(),
                ShowConfirmCloseSwal = false,
                OnCloseAsync = () => Task.FromException(failure)
            }));
            Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() =>
                cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback)));
        }
        else
        {
            var service = Context.Services.GetRequiredService<DrawerService>();
            await cut.InvokeAsync(() => service.ShowEditDrawer(new TableEditDrawerOption<Foo>
            {
                Model = new Foo(),
                ShowConfirmCloseSwal = false,
                OnCloseAsync = () => Task.FromException(failure)
            }, new DrawerOption()));
            Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() =>
                cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close)));
        }
        Assert.Empty(cut.FindComponents<EditDialog<Foo>>());
    }

    [Fact]
    public async Task ConcurrentDrawerClose_RunsCleanupOnlyOnce()
    {
        var closeJs = Context.JSInterop.Setup<bool>("execute", invocation =>
            invocation.Arguments.Count == 2 && invocation.Arguments[1] is false);
        var editedClosed = 0;
        var userClosed = 0;
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DrawerService>();
        var option = new DrawerOption
        {
            OnCloseAsync = () =>
            {
                userClosed++;
                return Task.CompletedTask;
            }
        };
        await cut.InvokeAsync(() => service.ShowEditDrawer(new TableEditDrawerOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnCloseAsync = () =>
            {
                editedClosed++;
                return Task.CompletedTask;
            }
        }, option));
        var firstClose = cut.InvokeAsync(option.CloseAsync);
        cut.WaitForAssertion(() => Assert.False(cut.FindComponent<Drawer>().Instance.IsOpen));
        var secondClose = cut.InvokeAsync(option.CloseAsync);
        closeJs.SetResult(false);
        await Task.WhenAll(firstClose, secondClose);
        Assert.Equal(new[] { 1, 1 }, new[] { editedClosed, userClosed });
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task DrawerCloseCallback_DoesNotRemoveNewlyOpenedDrawer(bool reuseOption)
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DrawerService>();
        var original = new DrawerOption();
        var next = reuseOption ? original : new DrawerOption();
        original.OnCloseAsync = () =>
        {
            next.Width = "700px";
            return service.Show(next);
        };
        await cut.InvokeAsync(() => service.Show(original));
        var firstDrawer = cut.FindComponent<Drawer>().Instance;
        await cut.InvokeAsync(original.CloseAsync);
        Assert.Single(cut.FindComponents<Drawer>());
        Assert.Equal("700px", cut.FindComponent<Drawer>().Instance.Width);
        Assert.NotSame(firstDrawer, cut.FindComponent<Drawer>().Instance);
        Assert.False(firstDrawer.IsOpen);
    }

    [Theory]
    [InlineData(EditMode.Popup, false)]
    [InlineData(EditMode.Drawer, false)]
    [InlineData(EditMode.Popup, true)]
    [InlineData(EditMode.Drawer, true)]
    public async Task PostSaveFailure_BlocksRetryAndPreservesSavedClose(EditMode mode, bool failHook)
    {
        var saved = 0;
        var created = 0;
        var canceled = 0;
        var closedSaved = false;
        var failure = new InvalidOperationException("Post-save processing failed");
        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, mode);
                pb.Add(a => a.KeepAdding, true);
                pb.Add(a => a.ShowCloseConfirm, false);
                pb.Add(a => a.OnAddAsync, () =>
                {
                    created++;
                    return Task.FromResult(new Foo());
                });
                pb.Add(a => a.OnSaveAsync, (item, changedType) =>
                {
                    saved++;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnAfterSaveAsync, item => failHook ? Task.FromException(failure) : Task.CompletedTask);
                pb.Add(a => a.OnQueryAsync, options => !failHook && saved > 0
                    ? Task.FromException<QueryData<Foo>>(failure)
                    : Task.FromResult(new QueryData<Foo> { Items = [], TotalCount = 0 }));
                pb.Add(a => a.OnAfterCancelSaveAsync, () =>
                {
                    canceled++;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.EditDialogCloseAsync, (item, wasSaved) =>
                {
                    closedSaved = wasSaved;
                    return Task.CompletedTask;
                });
            });
        });
        await cut.InvokeAsync(() => cut.FindComponent<Table<Foo>>().Instance.AddAsync());
        var editor = cut.FindComponent<EditDialog<Foo>>();
        var form = editor.FindComponent<ValidateForm>().Instance;
        var context = new EditContext(form.Model!);
        Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() =>
            cut.InvokeAsync(() => form.OnValidSubmit!(context))));
        Assert.NotEmpty(editor.Find("[role='alert']").TextContent);
        Assert.True(editor.Find("button[type='submit']").HasAttribute("disabled"));
        await cut.InvokeAsync(() => form.OnValidSubmit!(context));
        Assert.Equal(1, saved);
        Assert.Equal(1, created);
        if (mode == EditMode.Drawer)
        {
            await cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
        }
        else
        {
            await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.CloseCallback);
        }
        Assert.True(closedSaved);
        Assert.Equal(0, canceled);
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task ClosingCheck_RechecksBusyStateAfterAwait(EditMode mode)
    {
        var checking = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var startedChecking = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var saving = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var startedSaving = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var model = new Foo();
        Func<Task<bool>> check = () =>
        {
            startedChecking.TrySetResult();
            return checking.Task;
        };
        Func<EditContext, Task<bool>> save = context =>
        {
            startedSaving.TrySetResult();
            return saving.Task;
        };
        var cut = Context.Render<BootstrapBlazorRoot>();
        if (mode == EditMode.Popup)
        {
            var service = Context.Services.GetRequiredService<DialogService>();
            await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
            {
                Model = model, ShowConfirmCloseSwal = false, OnEditAsync = save, OnClosingAsync = check
            }));
        }
        else
        {
            var service = Context.Services.GetRequiredService<DrawerService>();
            await cut.InvokeAsync(() => service.ShowEditDrawer(new TableEditDrawerOption<Foo>
            {
                Model = model, ShowConfirmCloseSwal = false, OnEditAsync = save
            }, new DrawerOption { OnClosingAsync = check }));
        }
        var closing = mode == EditMode.Popup
            ? cut.InvokeAsync(cut.FindComponent<Modal>().Instance.Close)
            : cut.InvokeAsync(cut.FindComponent<Drawer>().Instance.Close);
        await startedChecking.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
        var form = cut.FindComponent<ValidateForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(model)));
        try
        {
            await startedSaving.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            checking.TrySetResult(true);
            await closing;
            Assert.Single(cut.FindComponents<EditDialog<Foo>>());
            if (mode == EditMode.Drawer)
            {
                Assert.True(cut.FindComponent<Drawer>().Instance.IsOpen);
            }
            else
            {
                Assert.False(await cut.InvokeAsync(cut.FindComponent<Modal>().Instance.BeforeCloseCallback));
            }
        }
        finally
        {
            checking.TrySetResult(false);
            saving.TrySetResult(false);
            await submit;
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task PopupCloseCallback_PreservesNewPresentationAndParentGuard(bool failClose)
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        var next = new Foo { Name = "next" };
        var failure = new InvalidOperationException("Close failed");
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnClosingAsync = () => Task.FromResult(false)
        }));
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnCloseAsync = async () =>
            {
                await service.ShowEditDialog(new EditDialogOption<Foo>
                {
                    Model = next,
                    ShowConfirmCloseSwal = false,
                    OnClosingAsync = () => Task.FromResult(false)
                });
                if (failClose)
                {
                    throw failure;
                }
            }
        }));
        var modal = cut.FindComponent<Modal>().Instance;
        Assert.True(await cut.InvokeAsync(modal.BeforeCloseCallback));
        if (failClose)
        {
            Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(modal.CloseCallback)));
        }
        else
        {
            await cut.InvokeAsync(modal.CloseCallback);
        }
        Assert.Equal(2, cut.FindComponents<EditDialog<Foo>>().Count);
        Assert.Contains(cut.FindComponents<EditDialog<Foo>>(), editor => ReferenceEquals(editor.Instance.Model, next));
        Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
        await cut.InvokeAsync(modal.CloseCallback);
        Assert.Single(cut.FindComponents<EditDialog<Foo>>());
        Assert.False(await cut.InvokeAsync(modal.BeforeCloseCallback));
    }

    [Fact]
    public async Task PopupPendingCheck_DoesNotBlockOrCloseChildPresentation()
    {
        var checking = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(),
            ShowConfirmCloseSwal = false,
            OnClosingAsync = () =>
            {
                started.TrySetResult();
                return checking.Task;
            }
        }));
        var modal = cut.FindComponent<Modal>().Instance;
        var parentChecking = cut.InvokeAsync(modal.BeforeCloseCallback);
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
            {
                Model = new Foo(), ShowConfirmCloseSwal = false
            }));
            Assert.True(await cut.InvokeAsync(modal.BeforeCloseCallback));
            checking.TrySetResult(true);
            Assert.False(await parentChecking);
            await cut.InvokeAsync(modal.CloseCallback);
            Assert.Single(cut.FindComponents<EditDialog<Foo>>());
        }
        finally
        {
            checking.TrySetResult(false);
            await parentChecking;
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task DrawerPendingCheck_CannotCloseReplacement(bool reuseOption)
    {
        var checking = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var closed = 0;
        var original = new DrawerOption
        {
            OnClosingAsync = () =>
            {
                started.TrySetResult();
                return checking.Task;
            },
            OnCloseAsync = () =>
            {
                closed++;
                return Task.CompletedTask;
            }
        };
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DrawerService>();
        await cut.InvokeAsync(() => service.Show(original));
        var first = cut.FindComponent<Drawer>().Instance;
        var closing = cut.InvokeAsync(first.Close);
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            var next = reuseOption ? original : new DrawerOption();
            next.OnClosingAsync = () => Task.FromResult(true);
            await cut.InvokeAsync(() => service.Show(next));
            checking.TrySetResult(true);
            await closing;
            Assert.Equal(0, closed);
            var replacement = cut.FindComponent<Drawer>().Instance;
            Assert.NotSame(first, replacement);
            Assert.True(replacement.IsOpen);
            await cut.InvokeAsync(next.CloseAsync);
            Assert.Empty(cut.FindComponents<Drawer>());
        }
        finally
        {
            checking.TrySetResult(false);
            await closing;
        }
    }

    [Fact]
    public async Task OrdinaryDrawer_UpdateOpenOptionPreservesComponent()
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DrawerService>();
        var option = new DrawerOption { Width = "400px" };
        await cut.InvokeAsync(() => service.Show(option));
        var drawer = cut.FindComponent<Drawer>().Instance;
        option.Width = "700px";
        await cut.InvokeAsync(() => service.Show(option));
        Assert.Same(drawer, cut.FindComponent<Drawer>().Instance);
        Assert.Equal("700px", drawer.Width);
        await cut.InvokeAsync(option.CloseAsync);
        Assert.Empty(cut.FindComponents<Drawer>());
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task ReopeningStaticHost_RejectsPreviousPresentationValidation(EditMode mode)
    {
        var model = new TableDialogTest.DelayedValidationModel();
        var saved = 0;
        Modal? modal = null;
        Drawer? drawer = null;
        RenderFragment editor = builder =>
        {
            builder.OpenComponent<EditDialog<TableDialogTest.DelayedValidationModel>>(0);
            builder.AddAttribute(1, nameof(EditDialog<Foo>.Model), model);
            builder.AddAttribute(2, nameof(EditDialog<Foo>.ShowCloseConfirm), false);
            builder.AddAttribute(3, nameof(EditDialog<Foo>.OnSaveAsync), new Func<EditContext, Task<bool>>(context =>
            {
                saved++;
                return Task.FromResult(false);
            }));
            builder.CloseComponent();
        };
        var cut = Context.Render<BootstrapBlazorRoot>(pb => pb.AddChildContent(builder =>
        {
            if (mode == EditMode.Popup)
            {
                builder.OpenComponent<Modal>(0);
                builder.AddAttribute(1, nameof(Modal.ChildContent), new RenderFragment(builder =>
                {
                    builder.OpenComponent<ModalDialog>(0);
                    builder.AddAttribute(1, nameof(ModalDialog.BodyTemplate), editor);
                    builder.CloseComponent();
                }));
                builder.AddComponentReferenceCapture(2, component => modal = (Modal)component);
            }
            else
            {
                builder.OpenComponent<Drawer>(0);
                builder.AddAttribute(1, nameof(Drawer.IsOpen), true);
                builder.AddAttribute(2, nameof(Drawer.ChildContent), editor);
                builder.AddComponentReferenceCapture(3, component => drawer = (Drawer)component);
            }
            builder.CloseComponent();
        }));
        var firstEditor = cut.FindComponent<EditDialog<TableDialogTest.DelayedValidationModel>>().Instance;
        var firstForm = cut.FindComponent<EditForm>().Instance;
        var submit = cut.InvokeAsync(() => firstForm.OnSubmit.InvokeAsync(firstForm.EditContext!));
        try
        {
            await model.Started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            if (mode == EditMode.Popup)
            {
                await cut.InvokeAsync(modal!.Close);
                await cut.InvokeAsync(modal.CloseCallback);
                await cut.InvokeAsync(modal.Show);
            }
            else
            {
                await cut.InvokeAsync(drawer!.Close);
                cut.FindComponents<Drawer>().Single(component => ReferenceEquals(component.Instance, drawer))
                    .Render(pb => pb.Add(a => a.IsOpen, true));
            }
            Assert.Same(firstEditor, cut.FindComponent<EditDialog<TableDialogTest.DelayedValidationModel>>().Instance);
            model.Continue.TrySetResult();
            await submit;
            Assert.Equal(0, saved);
            var currentForm = cut.FindComponent<ValidateForm>().Instance;
            await cut.InvokeAsync(() => currentForm.OnValidSubmit!(new EditContext(model)));
            Assert.Equal(1, saved);
        }
        finally
        {
            model.Continue.TrySetResult();
            await submit;
        }
    }

    [Fact]
    public async Task LoadingCleanupFailureAfterSave_BlocksRetryAndPreservesException()
    {
        var failure = new InvalidOperationException("Loading cleanup failed");
        var loading = Context.JSInterop.SetupVoid("execute", invocation =>
            invocation.Arguments.Count == 2 && invocation.Arguments[1] is false);
        loading.SetException(failure);
        var saved = 0;
        var model = new Foo();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, model);
            pb.Add(a => a.ShowLoading, true);
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.OnSaveAsync, context =>
            {
                saved++;
                return Task.FromResult(true);
            });
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        var context = new EditContext(model);
        Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() =>
            cut.InvokeAsync(() => form.OnValidSubmit!(context))));
        Assert.NotEmpty(cut.Find("[role='alert']").TextContent);
        await cut.InvokeAsync(() => form.OnValidSubmit!(context));
        Assert.Equal(1, saved);
        Assert.True(await cut.InvokeAsync(cut.Instance.CanCloseAsync));
    }

    [Fact]
    public async Task DrawerAnimationFailure_StillRunsCleanup()
    {
        var failure = new InvalidOperationException("Hide failed");
        var closeJs = Context.JSInterop.Setup<bool>("execute", invocation =>
            invocation.Arguments.Count == 2 && invocation.Arguments[1] is false);
        closeJs.SetException(failure);
        var closed = 0;
        var cut = Context.Render<BootstrapBlazorRoot>();
        var service = Context.Services.GetRequiredService<DrawerService>();
        var option = new DrawerOption
        {
            OnCloseAsync = () =>
            {
                closed++;
                return Task.CompletedTask;
            }
        };
        await cut.InvokeAsync(() => service.Show(option));
        Assert.Same(failure, await Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(option.CloseAsync)));
        Assert.Equal(1, closed);
        Assert.Empty(cut.FindComponents<Drawer>());
    }

    [Theory]
    [InlineData(EditMode.Popup)]
    [InlineData(EditMode.Drawer)]
    public async Task DisposalWhileSaving_DoesNotInitializeAnotherModel(EditMode mode)
    {
        var saving = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var created = 0;
        var cut = Context.Render<BootstrapBlazorRoot>();
        ITableEditDialogOption<Foo> option = mode == EditMode.Popup
            ? new EditDialogOption<Foo>()
            : new TableEditDrawerOption<Foo>();
        option.Model = new Foo();
        option.ShowConfirmCloseSwal = false;
        option.KeepOpenAfterSave = true;
        option.OnEditAsync = context =>
        {
            started.TrySetResult();
            return saving.Task;
        };
        option.CreateNextModelAsync = () =>
        {
            created++;
            return Task.FromResult(new Foo());
        };
        if (option is EditDialogOption<Foo> dialogOption)
        {
            var service = Context.Services.GetRequiredService<DialogService>();
            await cut.InvokeAsync(() => service.ShowEditDialog(dialogOption));
        }
        else if (option is TableEditDrawerOption<Foo> drawerOption)
        {
            var service = Context.Services.GetRequiredService<DrawerService>();
            await cut.InvokeAsync(() => service.ShowEditDrawer(drawerOption, new DrawerOption()));
        }
        var form = cut.FindComponent<ValidateForm>().Instance;
        var editor = cut.FindComponent<EditDialog<Foo>>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(form.Model!)));
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            await cut.InvokeAsync(() => ((IAsyncDisposable)editor).DisposeAsync().AsTask());
            saving.TrySetResult(true);
            await submit;
            Assert.Equal(0, created);
        }
        finally
        {
            saving.TrySetResult(false);
            await submit;
        }
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task ParameterChange_DoesNotAcceptPreviousSaveContinuation(bool initializing)
    {
        var saving = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        var creating = new TaskCompletionSource<Foo>(TaskCreationOptions.RunContinuationsAsynchronously);
        var started = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
        var initial = new Foo();
        var external = new Foo();
        var created = 0;
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, initial);
            pb.Add(a => a.KeepOpenAfterSave, true);
            pb.Add(a => a.OnSaveAsync, context =>
            {
                if (!initializing) started.TrySetResult();
                return initializing ? Task.FromResult(true) : saving.Task;
            });
            pb.Add(a => a.CreateNextModelAsync, () =>
            {
                created++;
                started.TrySetResult();
                return creating.Task;
            });
        });
        var form = cut.FindComponent<ValidateForm>().Instance;
        var submit = cut.InvokeAsync(() => form.OnValidSubmit!(new EditContext(initial)));
        try
        {
            await started.Task.WaitAsync(TimeSpan.FromSeconds(5), TestContext.Current.CancellationToken);
            cut.Render(pb => pb.Add(a => a.Model, external));
            saving.TrySetResult(true);
            creating.TrySetResult(new Foo());
            await submit;
            Assert.Same(external, cut.FindComponent<ValidateForm>().Instance.Model);
            Assert.Equal(initializing ? 1 : 0, created);
        }
        finally
        {
            saving.TrySetResult(false);
            creating.TrySetResult(new Foo());
            await submit;
        }
    }

    [Fact]
    public async Task ClosingEmptyModal_DoesNotBlockNextPresentation()
    {
        var cut = Context.Render<BootstrapBlazorRoot>();
        var modal = cut.FindComponent<Modal>().Instance;
        await cut.InvokeAsync(modal.Close);
        var service = Context.Services.GetRequiredService<DialogService>();
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        Assert.True(await cut.InvokeAsync(modal.BeforeCloseCallback));
        await cut.InvokeAsync(modal.CloseCallback);
        await cut.InvokeAsync(modal.Close);
        await cut.InvokeAsync(() => service.ShowEditDialog(new EditDialogOption<Foo>
        {
            Model = new Foo(), ShowConfirmCloseSwal = false
        }));
        Assert.True(await cut.InvokeAsync(modal.BeforeCloseCallback));
    }
}
