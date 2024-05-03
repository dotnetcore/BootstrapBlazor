// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class DialogTest : DialogTestBase
{
    [Fact]
    public void Show_Ok()
    {
        #region Show
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<MockDialogTest>();
        });
        var dialog = cut.FindComponent<MockDialogTest>().Instance.DialogService;

        var closed = false;
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            BodyTemplate = builder => builder.AddContent(0, "Test-BodyTemplate"),
            HeaderTemplate = builder => builder.AddContent(0, "Test-HeaderTemplate"),
            FooterTemplate = builder => builder.AddContent(0, "Test-FooterTemplate"),
            Class = "test-class",
            ShowMaximizeButton = true,
            IsBackdrop = false,
            ShowResize = true,
            ShowExportPdfButton = true,
            ShowExportPdfButtonInHeader = true,
            ExportPdfButtonOptions = new(),
            OnCloseAsync = () =>
            {
                closed = true;
                return Task.CompletedTask;
            }
        }));
        Assert.Contains("<svg", cut.Markup);
        Assert.Contains("data-bs-backdrop=\"static\"", cut.Markup);

        // 全屏按钮
        Assert.Contains("btn-maximize", cut.Markup);

        // 代码覆盖模板单元测试
        Assert.Contains("Test-BodyTemplate", cut.Markup);
        Assert.Contains("Test-HeaderTemplate", cut.Markup);
        Assert.Contains("Test-FooterTemplate", cut.Markup);
        Assert.Contains("test-class", cut.Markup);

        // 测试关闭逻辑
        var modal = cut.FindComponent<Modal>();
        cut.InvokeAsync(async () =>
        {
            await modal.Instance.Close();
            await modal.Instance.CloseCallback();
        });
        Assert.True(closed);

        // 测试 HeaderToolbarTemplate
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            IsBackdrop = true,
            HeaderToolbarTemplate = builder => builder.AddContent(0, "Test-HeaderToolbarTemplate"),
        }));
        Assert.DoesNotContain("data-bs-backdrop", cut.FindComponent<Modal>().Markup);
        Assert.Contains("Test-HeaderToolbarTemplate", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 Component 赋值逻辑
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            Component = BootstrapDynamicComponent.CreateComponent<Button>(),
            BodyTemplate = null
        }));
        Assert.Contains("class=\"btn btn-primary\"", cut.Markup);
        modal = cut.FindComponent<Modal>();
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 Component 与 BodyTemplate 均为 null 逻辑
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            Component = null,
            BodyTemplate = null
        }));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShownCallbackAsync
        var shown = false;
        var option1 = new DialogOption
        {
            BodyTemplate = builder => builder.AddContent(0, "Test-BodyTemplate"),
            OnShownAsync = () =>
            {
                shown = true;
                return Task.CompletedTask;
            }
        };
        cut.InvokeAsync(() => dialog.Show(option1));
        modal = cut.FindComponent<Modal>();
        cut.InvokeAsync(() => modal.Instance.ShownCallback());
        Assert.True(shown);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowSearchDialog
        // 无按钮回调赋值
        var option = new SearchDialogOption<Foo>()
        {
            Title = "Test-SearchDialogTitle",
            Model = new Foo(),
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            LabelAlign = Alignment.Left,
            ResetButtonText = null,
            QueryButtonText = null,
            ShowLabel = true,
            Items = null
        };

        cut.InvokeAsync(() => dialog.ShowSearchDialog(option));

        // 重置按钮委托为空 null
        var button = cut.FindComponents<Button>().First(b => b.Instance.Text == "重置");
        cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 搜索按钮委托为空
        cut.InvokeAsync(() => dialog.ShowSearchDialog(option));
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "查询");
        cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 重置按钮
        var reset = false;
        option.OnResetSearchClick = () =>
        {
            reset = true;
            return Task.CompletedTask;
        };
        cut.InvokeAsync(() => dialog.ShowSearchDialog(option));
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "重置");
        cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(reset);

        // 搜索按钮
        var search = false;
        option.DialogBodyTemplate = foo => builder => builder.AddContent(0, foo.Name);
        option.OnSearchClick = () =>
        {
            search = true;
            return Task.CompletedTask;
        };
        cut.InvokeAsync(() => dialog.ShowSearchDialog(option));
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "查询");
        cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(search);
        #endregion

        #region ShowEditDialog
        // 无按钮回调赋值
        var editOption = new EditDialogOption<Foo>()
        {
            Model = new Foo(),
            ItemsPerRow = 2,
            RowType = RowType.Inline,
            ItemChangedType = ItemChangedType.Add,
            LabelAlign = Alignment.Left,
            ShowLabel = true
        };
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 设置关闭回调
        closed = false;
        editOption.OnCloseAsync = () =>
        {
            closed = true;
            return Task.CompletedTask;
        };
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        // 点击关闭按钮
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "关闭");
        cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(closed);

        // 设置保存回调
        var saved = false;
        editOption.ShowLoading = true;
        editOption.OnEditAsync = context =>
        {
            saved = true;
            return Task.FromResult(true);
        };

        var model = new Foo() { Name = "Test" };
        var parameters = new Dictionary<string, object?>()
        {
            ["Field"] = "Name",
            ["FieldExpression"] = model.GenerateValueExpression()
        };
        var item = new EditorItem<Foo, string>();
        cut.InvokeAsync(() => item.SetParametersAsync(ParameterView.FromDictionary(parameters)));
        editOption.Items = new IEditorItem[]
        {
            item
        };
        editOption.Model = model;
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        var form = cut.Find("form");
        form.Submit();
        Assert.True(saved);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // edit dialog is tracking true
        editOption.IsTracking = true;
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        button = cut.FindComponents<Button>().FirstOrDefault(b => b.Instance.Text == "关闭");
        Assert.Null(button);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // edit dialog is tracking false
        editOption.IsTracking = false;
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        button = cut.FindComponents<Button>().FirstOrDefault(b => b.Instance.Text == "关闭");
        Assert.NotNull(button);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // Edit Dialog FooterTemplate
        editOption.DialogFooterTemplate = modal => builder => builder.AddContent(0, "footer-template");
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        cut.Contains("footer-template");
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // body template is not null
        editOption.DialogBodyTemplate = modal => builder => builder.AddContent(0, "body-template");
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        cut.Contains("body-template");
        cut.Contains("footer-template");
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 测试 DialogBodyTemplate
        editOption.DialogBodyTemplate = foo => builder => builder.AddContent(0, "test");
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        form.Submit();
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // DisableAutoSubmitFormByEnter
        editOption.DisableAutoSubmitFormByEnter = true;
        cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
        cut.Contains("data-bb-dissubmit=\"true\"");
        form.Submit();
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // Modal is Null
        editOption.Model = null;
        var t = Assert.ThrowsAsync<InvalidOperationException>(() => cut.InvokeAsync(() => dialog.ShowEditDialog(editOption)));
        cut.InvokeAsync(() => cut.Find(".btn-close").Click());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowModal
        var result = false;
        var resultOption = new ResultDialogOption()
        {
            ShowYesButton = true,
            ButtonYesText = "Test-Yes",
            ButtonYesIcon = "test test-yes-icon",
            ButtonYesColor = Color.Primary,
            ShowNoButton = true,
            ButtonNoText = "Test-No",
            ButtonNoIcon = "test test-no-icon",
            ButtonNoColor = Color.Danger,
            ShowCloseButton = true,
            ButtonCloseText = "Test-Close",
            ButtonCloseIcon = "test test-close-icon",
            ButtonCloseColor = Color.Secondary,
            ComponentParameters = new Dictionary<string, object>()
            {
                [nameof(MockModalDialog.Value)] = result,
                [nameof(MockModalDialog.ValueChanged)] = EventCallback.Factory.Create<bool>(this, b => result = b)
            }
        };

        // 点击的是 Yes 按钮
        cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "Test-Yes");
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(result);

        // 点击的是 No 按钮
        result = true;
        resultOption = new ResultDialogOption()
        {
            ComponentParameters = new Dictionary<string, object>()
            {
                [nameof(MockModalDialog.Value)] = result,
                [nameof(MockModalDialog.ValueChanged)] = EventCallback.Factory.Create<bool>(this, b => result = b)
            }
        };
        cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "取消");
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.False(result);

        // 点击关闭按钮
        resultOption = new ResultDialogOption()
        {
            ShowCloseButton = true,
            ComponentParameters = new Dictionary<string, object>()
            {
                [nameof(MockModalDialog.Value)] = result,
                [nameof(MockModalDialog.ValueChanged)] = EventCallback.Factory.Create<bool>(this, b => result = b)
            },
            OnCloseAsync = () => Task.CompletedTask
        };
        cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
        button = cut.FindComponents<Button>().First(b => b.Instance.Text == "关闭");
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 点击右上角关闭按钮
        cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
        var btnElement = cut.Find(".btn-close");
        cut.InvokeAsync(() => btnElement.Click());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 点击 FooterTemplate 中的 关闭 按钮
        cut.InvokeAsync(() => dialog.ShowModal<MockModalDialogClosingFalse>(resultOption));
        button = cut.FindComponents<Button>().Last(b => b.Instance.Text == "关闭");
        cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region 弹窗中的弹窗测试
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            // 弹窗中按钮
            BodyTemplate = BootstrapDynamicComponent.CreateComponent<Button>(new Dictionary<string, object?>()
            {
                [nameof(Button.OnClickWithoutRender)] = () =>
                {
                    // 继续弹窗
                    dialog.Show(new DialogOption()
                    {
                        BodyTemplate = builder =>
                        {
                            builder.AddContent(0, "Test");
                        }
                    });
                    return Task.CompletedTask;
                }
            }).Render()
        }));

        // 弹出第二个弹窗
        var buttonInDialog = cut.Find(".btn-primary");
        buttonInDialog.Click();
        Assert.Equal(2, cut.FindComponents<ModalDialog>().Count);

        // 关闭第二个弹窗
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Single(cut.FindComponents<ModalDialog>());

        // 关闭第一个弹窗
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Empty(cut.FindComponents<ModalDialog>());
        #endregion

        #region 全屏弹窗
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            FullScreenSize = FullScreenSize.Large
        }));
        Assert.Contains("modal-fullscreen-lg-down", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region IsCenter
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            IsCentered = true
        }));
        Assert.Contains("modal-dialog-centered", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            IsCentered = false
        }));
        Assert.DoesNotContain("modal-dialog-centered", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region IsKeyboard
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            IsKeyboard = true
        }));
        Assert.Contains("data-bs-keyboard=\"true\"", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            IsKeyboard = false
        }));
        Assert.DoesNotContain("data-bs-keyboard\"false\"", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowHeaderCloseButton
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowHeaderCloseButton = true
        }));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowHeaderCloseButton = false
        }));
        Assert.DoesNotContain("btn-close", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowPrintButton
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowPrintButton = true
        }));
        Assert.Contains("btn-print", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowPrintButton = false
        }));
        Assert.DoesNotContain("btn-print", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowPrintButton = true,
            ShowPrintButtonInHeader = true,
            PrintButtonText = "Print-Test"
        }));
        Assert.Contains("btn-print", cut.Markup);
        Assert.Contains("Print-Test", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowSaveButton
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowSaveButton = true,
            SaveButtonText = "Save-Test",
            CloseButtonText = "Close-Test",
            BodyContext = "Test"
        }));
        Assert.Contains("Save-Test", cut.Markup);
        Assert.Contains("Close-Test", cut.Markup);
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region OnSaveAsync
        var save = false;
        cut.InvokeAsync(() => dialog.Show(new DialogOption()
        {
            ShowSaveButton = true,
            IsAutoCloseAfterSave = true,
            IsDraggable = false,
            OnSaveAsync = () =>
            {
                save = true;
                return Task.FromResult(save);
            }
        }));
        var btnClose = cut.FindComponents<Button>().First(i => i.Instance.Icon == "fa-solid fa-floppy-disk");
        cut.InvokeAsync(() => btnClose.Instance.OnClickWithoutRender!.Invoke());
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.True(save);
        #endregion

        #region ShowSaveDialog
        cut.InvokeAsync(() => dialog.ShowSaveDialog<MockDialogTest>("Title", () => Task.FromResult(true), p => { }, op => op.Class = "test"));
        cut.InvokeAsync(() => dialog.ShowSaveDialog<MockDialogTest>("Title"));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowValidateFormDialog
        cut.InvokeAsync(() => dialog.ShowValidateFormDialog<MockValidateFormDialog>("ValidateFormDialog"));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());

        Dictionary<string, object?> parameterFactory(DialogOption op) => new();
        void ConfigureOption(DialogOption op) => op.Class = "ValidateFormDialog-Class";
        cut.InvokeAsync(() => dialog.ShowValidateFormDialog<MockValidateFormDialog>("ValidateFormDialog", parameterFactory, ConfigureOption));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion

        #region ShowCloseDialog
        cut.InvokeAsync(() => dialog.ShowCloseDialog<MockValidateFormDialog>("CloseDialog", null, ConfigureOption));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        cut.InvokeAsync(() => dialog.ShowCloseDialog<MockValidateFormDialog>("CloseDialog"));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        cut.InvokeAsync(() => dialog.ShowCloseDialog<MockValidateFormDialog>("CloseDialog", parameter =>
        {
            parameter.Add("Class", "test");
        }));
        cut.InvokeAsync(() => modal.Instance.CloseCallback());
        #endregion
    }

    private class MockValidateFormDialog : ComponentBase
    {
        [Parameter]
        public string? Class { get; set; }
    }

    private class MockDialogTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public DialogService? DialogService { get; set; }
    }

    private class MockModalDialog : ComponentBase, IResultDialog
    {
        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        public async Task OnClose(DialogResult result)
        {
            if (result == DialogResult.Yes)
            {
                Value = true;
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }
            }
            else
            {
                Value = false;
                if (ValueChanged.HasDelegate)
                {
                    await ValueChanged.InvokeAsync(Value);
                }
            }
        }
    }

    private class MockModalDialogClosingFalse : MockModalDialog, IResultDialog
    {
        public Task<bool> OnClosing(DialogResult result) => Task.FromResult(false);
    }
}
