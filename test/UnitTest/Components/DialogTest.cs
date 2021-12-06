// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Bunit;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using UnitTest.Core;
using UnitTest.Extensions;
using Xunit;

namespace UnitTest.Components
{
    public class DialogTest : BootstrapBlazorTestBase
    {
        [Fact]
        public void Show_Ok()
        {
            var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
            {
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
                OnCloseAsync = () =>
                {
                    closed = true;
                    return Task.CompletedTask;
                }
            }));

            // 代码覆盖模板单元测试
            Assert.Contains("Test-BodyTemplate", cut.Markup);
            Assert.Contains("Test-HeaderTemplate", cut.Markup);
            Assert.Contains("Test-FooterTemplate", cut.Markup);
            Assert.Contains("test-class", cut.Markup);

            // 测试关闭逻辑
            var modal = cut.FindComponent<Modal>();
            cut.InvokeAsync(() => modal.Instance.Close());
            Assert.True(closed);

            // 测试 Component 赋值逻辑
            cut.InvokeAsync(() => dialog.Show(new DialogOption()
            {
                Component = BootstrapDynamicComponent.CreateComponent<Button>(),
                BodyTemplate = null
            }));
            Assert.Contains("class=\"btn btn-primary\"", cut.Markup);
            modal = cut.FindComponent<Modal>();
            cut.InvokeAsync(() => modal.Instance.Close());

            // 测试 Component 与 BodyTemplate 均为 null 逻辑
            cut.InvokeAsync(() => dialog.Show(new DialogOption()
            {
                Component = null,
                BodyTemplate = null
            }));
            cut.InvokeAsync(() => modal.Instance.Close());

            #region ShowSearchDialog
            // 无按钮回调赋值
            var option = new SearchDialogOption<Foo>()
            {
                Title = "Test-SearchDialogTitle",
                Model = new Foo(),
                ItemsPerRow = 2,
                RowType = RowType.Inline
            };
            cut.InvokeAsync(() => dialog.ShowSearchDialog(option));
            cut.InvokeAsync(() => modal.Instance.Close());

            // 重置按钮
            var reset = false;
            option.OnResetSearchClick = () =>
            {
                reset = true;
                return Task.CompletedTask;
            };
            cut.InvokeAsync(() => dialog.ShowSearchDialog(option));
            var button = cut.FindComponents<Button>().First(b => b.Instance.Text == "重置");
            cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
            Assert.True(reset);
            cut.InvokeAsync(() => modal.Instance.Close());

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
            Assert.True(search);
            #endregion

            #region ShowEditDialog
            // 无按钮回调赋值
            var editOption = new EditDialogOption<Foo>()
            {
                Model = new Foo(),
                ItemsPerRow = 2,
                RowType = RowType.Inline
            };
            cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
            cut.InvokeAsync(() => modal.Instance.Close());

            // 设置关闭回调
            closed = false;
            editOption.OnCloseAsync = () =>
            {
                closed = true;
                return Task.CompletedTask;
            };
            cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
            button = cut.FindComponents<Button>().First(b => b.Instance.Text == "关闭");
            cut.InvokeAsync(() => button.Instance.OnClickWithoutRender!.Invoke());
            Assert.True(closed);

            // 设置保存回调
            var saved = false;
            editOption.ShowLoading = true;
            editOption.OnSaveAsync = context =>
            {
                saved = true;
                return Task.FromResult(true);
            };

            var model = new Foo() { Name = "Test" };
            var parameters = new Dictionary<string, object>()
            {
                ["Field"] = "Name",
                ["FieldExpression"] = model.GenerateValueExpression()
            };
#if NET5_0
            var item = new EditorItem<string>();
#else
            var item = new EditorItem<Foo, string>();
#endif
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

            // 测试 DialogBodyTemplate
            editOption.DialogBodyTemplate = foo => builder => builder.AddContent(0, "test");
            cut.InvokeAsync(() => dialog.ShowEditDialog(editOption));
            form.Submit();
            #endregion

            #region ShowModal
            var result = false;
            var resultOption = new ResultDialogOption()
            {
                ComponentParamters = new Dictionary<string, object>()
                {
                    [nameof(MockModalDialog.Value)] = result,
                    [nameof(MockModalDialog.ValueChanged)] = EventCallback.Factory.Create<bool>(this, b => result = b)
                }
            };

            // 点击的是 Yes 按钮
            cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
            button = cut.FindComponents<Button>().First(b => b.Instance.Text == "确认");
            cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
            Assert.True(result);

            // 点击的是 No 按钮
            result = true;
            resultOption = new ResultDialogOption()
            {
                ComponentParamters = new Dictionary<string, object>()
                {
                    [nameof(MockModalDialog.Value)] = result,
                    [nameof(MockModalDialog.ValueChanged)] = EventCallback.Factory.Create<bool>(this, b => result = b)
                }
            };
            cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
            button = cut.FindComponents<Button>().First(b => b.Instance.Text == "取消");
            cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
            Assert.False(result);

            // 点击关闭按钮
            resultOption = new ResultDialogOption()
            {
                ShowCloseButton = true,
                ComponentParamters = new Dictionary<string, object>()
                {
                    [nameof(MockModalDialog.Value)] = result,
                    [nameof(MockModalDialog.ValueChanged)] = EventCallback.Factory.Create<bool>(this, b => result = b)
                },
                OnCloseAsync = () => Task.CompletedTask
            };
            cut.InvokeAsync(() => dialog.ShowModal<MockModalDialog>(resultOption));
            button = cut.FindComponents<Button>().First(b => b.Instance.Text == "关闭");
            cut.InvokeAsync(() => button.Instance.OnClick.InvokeAsync());
            #endregion
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
    }
}
