// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class SelectObjectTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Value_Ok()
    {
        Product? v = null;
        string? url = null;
        var items = Enumerable.Range(1, 8).Select(i => new Product()
        {
            ImageUrl = $"./images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg"
        });
        var cut = Context.RenderComponent<SelectObject<Product>>(pb =>
        {
            pb.Add(a => a.Value, v);
            pb.Add(a => a.OnValueChanged, p =>
            {
                v = p;
                return Task.CompletedTask;
            });
            pb.Add(a => a.GetTextCallback, p => p?.ImageUrl);
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.OpenComponent<ListView<Product>>(0);
                pb.AddAttribute(1, "Items", items);
                pb.AddAttribute(2, "BodyTemplate", new RenderFragment<Product>(p => b => b.AddContent(0, p.ImageUrl)));
                pb.AddAttribute(3, "OnListViewItemClick", new Func<Product, Task>(async p =>
                {
                    context.SetValue(p);
                    await context.CloseAsync();

                    url = p.ImageUrl;
                }));
                pb.CloseComponent();
            });
        });

        var item = cut.Find(".listview-item");
        await cut.InvokeAsync(() => item.Click());
        Assert.NotNull(v);
        Assert.Equal(url, v.ImageUrl);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<SelectObject<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
            pb.Add(a => a.GetTextCallback, p => p);
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.AddContent(0, "test");
            });
        });
        cut.Contains("border-danger");
    }

    [Fact]
    public void ShowAppendArrow_Ok()
    {
        var cut = Context.RenderComponent<SelectObject<string>>(pb =>
        {
            pb.Add(a => a.ShowAppendArrow, false);
            pb.Add(a => a.GetTextCallback, p => p);
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.AddContent(0, "test");
            });
        });
        cut.DoesNotContain("form-select-append");
    }

    [Fact]
    public void DropdownMinWidth_Ok()
    {
        var cut = Context.RenderComponent<SelectObject<string>>(pb =>
        {
            pb.Add(a => a.DropdownMinWidth, 500);
            pb.Add(a => a.GetTextCallback, p => p);
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.AddContent(0, "test");
            });
        });
        Assert.Contains("data-bb-min-width=\"500\"", cut.Markup);
    }

    [Fact]
    public void GetTextCallback_Ok()
    {
        Assert.Throws<InvalidOperationException>(() => Context.RenderComponent<SelectObject<string>>(pb =>
        {
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.AddContent(0, "test");
            });
        }));
    }

    [Fact]
    public void Height_Ok()
    {
        var cut = Context.RenderComponent<SelectObject<string>>(pb =>
        {
            pb.Add(a => a.Height, 500);
            pb.Add(a => a.GetTextCallback, p => p);
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.AddContent(0, "test");
            });
        });
        Assert.Contains($"height: 500px;", cut.Markup);
    }

    [Fact]
    public void Template_Ok()
    {
        var items = Enumerable.Range(1, 8).Select(i => new Product()
        {
            ImageUrl = $"./images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg"
        }).ToList();
        var v = items[0];
        var cut = Context.RenderComponent<SelectObject<Product>>(pb =>
        {
            pb.Add(a => a.Value, v);
            pb.Add(a => a.GetTextCallback, p => p?.ImageUrl);
            pb.Add(a => a.Template, p => builder =>
            {
                builder.AddContent(0, $"Template-{p.ImageUrl}");
            });
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.OpenComponent<ListView<Product>>(0);
                pb.AddAttribute(1, "Items", items);
                pb.AddAttribute(2, "BodyTemplate", new RenderFragment<Product>(p => b => b.AddContent(0, p.ImageUrl)));
                pb.CloseComponent();
            });
        });

        Assert.Contains($"Template-{v.ImageUrl}", cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Value, null);
            pb.Add(a => a.PlaceHolder, "Template-PlaceHolder");
        });

        Assert.DoesNotContain($"Template-{items[0].ImageUrl}", cut.Markup);
        cut.Contains("Template-PlaceHolder");
    }

    [Fact]
    public async Task Validate_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var valid = false;
        var invalid = false;
        var model = Foo.Generate(localizer);
        var cut = Context.RenderComponent<ValidateForm>(builder =>
        {
            builder.Add(a => a.OnValidSubmit, context =>
            {
                valid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.OnInvalidSubmit, context =>
            {
                invalid = true;
                return Task.CompletedTask;
            });
            builder.Add(a => a.Model, model);
            builder.AddChildContent<SelectObject<string>>(pb =>
            {
                pb.Add(a => a.Value, model.Name);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(model, "Name", typeof(string)));
                pb.Add(a => a.OnValueChanged, v =>
                {
                    model.Name = v;
                    return Task.CompletedTask;
                });
                pb.Add(a => a.GetTextCallback, item => item);
                pb.Add(a => a.ChildContent, context => pb =>
                {
                    pb.OpenComponent<BootstrapInput<string>>(0);
                    pb.CloseComponent();
                });
            });
        });

        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(valid);

        model.Name = null;
        var table = cut.FindComponent<SelectObject<string>>();
        table.SetParametersAndRender();
        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(invalid);
    }

    class Product
    {
        public string ImageUrl { get; set; } = "";

        public string Description { get; set; } = "";
    }
}
