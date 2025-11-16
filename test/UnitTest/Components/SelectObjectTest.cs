// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
        var cut = Context.Render<SelectObject<Product>>(pb =>
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

        var isClear = false;
        cut.Render(pb =>
        {
            pb.Add(a => a.IsClearable, true);
            pb.Add(a => a.OnClearAsync, () =>
            {
                isClear = true;
                return Task.CompletedTask;
            });
        });
        Assert.Contains("clear-icon", cut.Markup);

        var span = cut.Find(".clear-icon");
        await cut.InvokeAsync(() => span.Click());
        var input = cut.Find(".form-select");
        Assert.Null(input.GetAttribute("value"));
        Assert.True(isClear);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.Render<SelectObject<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
            pb.Add(a => a.GetTextCallback, p => p);
            pb.Add(a => a.ChildContent, context => pb =>
            {
                pb.AddContent(0, "test");
            });
            pb.Add(a => a.IsClearable, true);
        });
        cut.Contains("border-danger");

        var span = cut.Find(".clear-icon");
        Assert.True(span.ClassList.Contains("text-danger"));
    }

    [Fact]
    public void ShowAppendArrow_Ok()
    {
        var cut = Context.Render<SelectObject<string>>(pb =>
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
        var cut = Context.Render<SelectObject<string>>(pb =>
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
        Assert.Throws<InvalidOperationException>(() => Context.Render<SelectObject<string>>(pb =>
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
        var cut = Context.Render<SelectObject<string>>(pb =>
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
        var cut = Context.Render<SelectObject<Product>>(pb =>
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

        cut.Render(pb =>
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
        var cut = Context.Render<ValidateForm>(builder =>
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
                pb.Add(a => a.IsClearable, true);
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

        var span = cut.Find(".clear-icon");
        Assert.True(span.ClassList.Contains("text-success"));

        model.Name = null;
        var table = cut.FindComponent<SelectObject<string>>();
        table.Render();
        await cut.InvokeAsync(() =>
        {
            var form = cut.Find("form");
            form.Submit();
        });
        Assert.True(invalid);

        span = cut.Find(".clear-icon");
        Assert.True(span.ClassList.Contains("text-danger"));
    }

    class Product
    {
        public string ImageUrl { get; set; } = "";

        public string Description { get; set; } = "";
    }
}
