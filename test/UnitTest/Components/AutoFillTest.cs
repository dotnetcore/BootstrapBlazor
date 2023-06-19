// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

public class AutoFillTest : BootstrapBlazorTestBase
{
    private IStringLocalizer<Foo> Localizer { get; }

    private Foo Model { get; }

    private List<Foo> Items { get; }

    public AutoFillTest()
    {
        Localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        Model = Foo.Generate(Localizer);
        Items = Foo.GenerateFoo(Localizer, 2);
    }

    [Fact]
    public void ShowLabel_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.ShowLabel, true);
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.DisplayText, "AutoFillDisplayText");
        });
        Assert.Contains("AutoFillDisplayText", cut.Markup);
    }

    [Fact]
    public void NullItems_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>();
        Assert.Contains("dropdown-menu", cut.Markup);
    }

    [Fact]
    public void OnCustomFilter_Ok()
    {
        var filtered = false;
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<Foo>>>(key =>
            {
                filtered = true;
                var items = Foo.GenerateFoo(Localizer, 3);
                return Task.FromResult(items.AsEnumerable());
            }));
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        Assert.True(filtered);
    }

    [Fact]
    public void Escape_Ok()
    {
        var escTrigger = false;
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.SkipEsc, true);
            pb.Add(a => a.OnEscAsync, new Func<Foo, Task>(foo =>
           {
               escTrigger = true;
               return Task.CompletedTask;
           }));
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Escape" });
        Assert.False(escTrigger);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEsc, false);
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Escape" });
        Assert.True(escTrigger);
    }

    [Fact]
    public void Enter_Ok()
    {
        var enterTrigger = false;
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.SkipEnter, true);
            pb.Add(a => a.OnEnterAsync, new Func<Foo, Task>(foo =>
            {
                enterTrigger = true;
                return Task.CompletedTask;
            }));
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Enter" });
        Assert.False(enterTrigger);

        Foo? selectedItem = null;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEnter, false);
            pb.Add(a => a.OnSelectedItemChanged, new Func<Foo, Task>(foo =>
            {
                selectedItem = foo;
                return Task.CompletedTask;
            }));
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Enter" });
        Assert.True(enterTrigger);
        Assert.NotNull(selectedItem);
    }

    [Fact]
    public void OnSelectedItemChanged_Ok()
    {
        Foo? selectedItem = null;
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.OnSelectedItemChanged, new Func<Foo, Task>(foo =>
            {
                selectedItem = foo;
                return Task.CompletedTask;
            }));
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".dropdown-item").MouseDown(new MouseEventArgs());
        Assert.NotNull(selectedItem);
    }

    [Fact]
    public void KeyUp_Test()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowUp" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowUp" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowUp" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
    }

    [Fact]
    public void DisplayCount_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.IsLikeMatch, true);
            pb.Add(a => a.IgnoreCase, false);
            pb.Add(a => a.DisplayCount, 2);
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Z" });
        Assert.Equal(2, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public void OnGetDisplayText_Null()
    {
        var v = new AutoFillNullStringMock();
        var cut = Context.RenderComponent<AutoFill<AutoFillNullStringMock?>>(pb =>
        {
            pb.Add(a => a.IgnoreCase, true);
            pb.Add(a => a.Value, v);
            pb.Add(a => a.Items, new List<AutoFillNullStringMock>
            {
                new AutoFillNullStringMock() { Value = "1" },
                new AutoFillNullStringMock() { Value = "2" },
            });
            pb.Add(a => a.Template, v => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, v!.Value);
                builder.CloseElement();
            });
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "1" });
        cut.Find(".dropdown-item").MouseDown(new MouseEventArgs());
        Assert.Equal("1", cut.Instance.Value!.Value);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, new List<AutoFillNullStringMock?>
            {
                null,
                new AutoFillNullStringMock() { Value = "2" },
            });
            pb.Add(a => a.Template, (RenderFragment<AutoFillNullStringMock?>?)null);
        });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "2" });
        Assert.Equal(2, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public void OnGetDisplayText_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.OnGetDisplayText, foo => foo.Name ?? "");
        });
        var input = cut.Find("input");
        Assert.Equal("张三 1000", input.Attributes["value"]?.Value);
    }

    [Fact]
    public void Debounce_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.Debounce, 200);
        });
    }

    [Fact]
    public async Task ShowDropdownListOnFocus_Ok()
    {
        var cut = Context.RenderComponent<AutoFill<Foo>>(pb =>
        {
            pb.Add(a => a.Value, Model);
            pb.Add(a => a.Items, Items);
            pb.Add(a => a.ShowDropdownListOnFocus, false);
        });

        // 获得焦点时不会自动弹出下拉框
        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.FocusAsync(new FocusEventArgs()));

        var menu = cut.Find("ul");
        Assert.Equal("dropdown-menu", menu.ClassList.ToString());

        // 获得焦点时自动弹出下拉框
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDropdownListOnFocus, true);
        });
        input = cut.Find("input");
        await cut.InvokeAsync(() => input.FocusAsync(new FocusEventArgs()));
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        var v = "";
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, new Foo());
            pb.AddChildContent<AutoFill<string>>(pb =>
            {
                pb.Add(a => a.Items, items);
                pb.Add(a => a.OnCustomFilter, key =>
                {
                    v = key;
                    return Task.FromResult(items);
                });
            });
        });

        // Trigger js invoke
        var comp = cut.FindComponent<AutoFill<string>>().Instance;
        comp.TriggerOnChange("v");
        var input = cut.Find("input");
        cut.InvokeAsync(() => input.KeyUp("Enter"));
        Assert.Equal("v", v);
    }

    class AutoFillNullStringMock
    {
        [NotNull]
        public string? Value { get; set; }

        public override string? ToString()
        {
            return null;
        }
    }
}
