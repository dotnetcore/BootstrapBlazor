﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class SelectTableTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>();
        });
        var rows = cut.FindAll("tbody > tr");
        Assert.Empty(rows);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Color, Color.Danger);
            });
        });
        cut.Contains("border-danger");
    }

    [Fact]
    public void ShowAppendArrow_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.ShowAppendArrow, false);
            });
        });
        cut.DoesNotContain("form-select-append");
    }

    [Fact]
    public void Template_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.Template, foo => builder =>
                {
                    builder.AddContent(0, $"Template-{foo.Name}");
                });
            });
        });
        var rows = cut.FindAll("tbody > tr");
        Assert.Equal(4, rows.Count);

        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, items[0]);
        });
        Assert.Contains($"Template-{items[0].Name}", cut.Markup);
    }

    [Fact]
    public void GetTextCallback_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.GetTextCallback, foo => foo.Name);
            });
        });
        Assert.Contains($"value=\"{items[0].Name}\"", cut.Markup);

        var table = cut.FindComponent<SelectTable<Foo>>();
        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetTextCallback, foo => null);
        });
        Assert.Contains("value=\"BootstrapBlazor.Server.Data.Foo\"", cut.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.GetTextCallback, null);
        });
        Assert.Contains("value=\"BootstrapBlazor.Server.Data.Foo\"", cut.Markup);

        table.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, null);
        });
        Assert.DoesNotContain("value=\"\"", cut.Markup);
    }

    [Fact]
    public void Height_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.Height, 100);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });
        Assert.Contains($"height: 100px;", cut.Markup);
    }

    [Fact]
    public async Task Value_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 4);
        Foo? v = null;
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<SelectTable<Foo>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.Value, items[0]);
                pb.Add(a => a.OnValueChanged, foo =>
                {
                    v = foo;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();

                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Address");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Address", typeof(string)));
                    builder.CloseComponent();
                });
            });
        });

        var rows = cut.FindAll("tbody > tr");
        await cut.InvokeAsync(() =>
        {
            rows[1].Click();
        });
        Assert.Equal(items[1].Name, v?.Name);
    }
}
