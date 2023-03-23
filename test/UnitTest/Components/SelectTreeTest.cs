// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Components;

public class SelectTreeTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.ShowIcon, true);
            builder.Add(p => p.ModelEqualityComparer, (s1, s2) => { return true; });
            builder.Add(p => p.OnExpandNodeAsync, (s) => { return Task.FromResult(new List<TreeViewItem<string>>().AsEnumerable()); });
            builder.Add(p => p.CustomKeyAttribute, typeof(string));
        });
        cut.Contains("select dropdown select-tree");
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.Color, Color.Primary);
        });

        Assert.Contains("border-primary", cut.Markup);
    }

    [Fact]
    public void Edit_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.IsEdit, true);
        });
        var input = cut.Find(".dropdown-toggle input");
        cut.InvokeAsync(() => input.Change("123"));
        Assert.Equal("123", cut.Instance.Value);
    }

    [Fact]
    public void IsShowLabel_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.ShowLabel, true);
            builder.Add(p => p.DisplayText, "test-showLabel");
        });

        Assert.Contains("test-showLabel", cut.Markup);
    }

    [Fact]
    public async Task Value_Null()
    {
        var model = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(c => c.Model, model);
            builder.AddChildContent<SelectTree<string>>(c =>
            {
                c.Add(p => p.Items, BindItems);
                c.Add(p => p.Value, model.Name);
                c.Add(p => p.ValueChanged, EventCallback.Factory.Create<string>(this, s => model.Name = s));
                c.Add(p => p.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });
        await cut.InvokeAsync(() => cut.Find("form").Submit());
        cut.Contains("class=\"form-select form-control border-success modified valid is-valid\"");
        cut.Contains("value=\"Test1\"");
    }

    [Fact]
    public async Task Valid_Ok()
    {
        var model = new Foo() { Name = "Test" };
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(c => c.Model, model);
            builder.AddChildContent<SelectTree<string>>(c =>
            {
                c.Add(p => p.Items, BindItems);
                c.Add(p => p.Value, model.Name);
                c.Add(p => p.ValueChanged, EventCallback.Factory.Create<string>(this, s => model.Name = s));
                c.Add(p => p.ValueExpression, Utility.GenerateValueExpression(model, nameof(model.Name), typeof(string)));
            });
        });
        await cut.InvokeAsync(() => cut.Find("form").Submit());
        cut.Contains("border-success valid is-valid");
    }

    [Fact]
    public void PlaceHolder_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.PlaceHolder, "Please input value");
        });
        cut.Contains("Please input value");
    }

    [Fact]
    public void ItemChanged_Ok()
    {
        var changed = 0;
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.OnSelectedItemChanged, v =>
            {
                changed++;
                return Task.CompletedTask;
            });
        });
        Assert.Equal(1, changed);

        // 选择第一个候选项
        var node = cut.Find(".tree-node");
        cut.InvokeAsync(() => node.Click());
        Assert.NotEqual(2, changed);

        node = cut.FindAll(".tree-node").Skip(1).Take(1).First();
        cut.InvokeAsync(() => node.Click());
        Assert.Equal(2, changed);
    }

    [Fact]
    public void StringComparison_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.StringComparison, StringComparison.CurrentCultureIgnoreCase);
        });

        Assert.Equal(StringComparison.CurrentCultureIgnoreCase, cut.Instance.StringComparison);
    }

    [Fact]
    public async Task OnSelectedItemChanged_Ok()
    {
        var res = false;
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
            builder.Add(p => p.OnSelectedItemChanged, new Func<string, Task>(s =>
            {
                res = true;
                return Task.CompletedTask;
            }));
        });
        await cut.InvokeAsync(() => cut.Find(".tree-node").Click());

        Assert.True(res);
    }

    [Fact]
    public void RetrieveId_Ok()
    {
        var cut = Context.RenderComponent<MockSelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
        });
        Assert.True(cut.Instance.TestRetrieveId());
    }

    [Fact]
    public void IsPopover_Ok()
    {
        var cut = Context.RenderComponent<SelectTree<string>>(builder =>
        {
            builder.Add(p => p.Items, BindItems);
        });
        cut.Contains("data-bs-toggle=\"dropdown\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsPopover, true);
        });
        cut.DoesNotContain("data-bs-toggle=\"dropdown\"");
    }

    private List<TreeViewItem<string>> BindItems { get; } = new List<TreeViewItem<string>>()
    {
        new TreeViewItem<string>("Test1")
        {
            Text ="Test1",
            Icon = "fa-solid fa-folder",
            ExpandIcon = "fa-solid fa-folder-open",
            CheckedState =CheckboxState.Checked,
            IsActive = true,
            Items = new List<TreeViewItem<string>>()
            {
                new TreeViewItem<string>("Test1-1")
                {
                    Text ="Test1-1",
                    Icon = "fa-solid fa-folder",
                    ExpandIcon = "fa-solid fa-folder-open",
                    Items = new List<TreeViewItem<string>>()
                    {
                        new TreeViewItem<string>("Test1-1-1") { Text = "Test1-1-1", Icon = "fa-solid fa-file" },
                        new TreeViewItem<string>("Test1-1-2") { Text = "Test1-1-2", Icon = "fa-solid fa-file" }
                    }
                }
            }
        }
    };

    private class MockSelectTree<TValue> : SelectTree<TValue>
    {
        public bool TestRetrieveId()
        {
            return base.RetrieveId() == $"{Id}_input";
        }
    }
}
