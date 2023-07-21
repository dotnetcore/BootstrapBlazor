// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class AutoCompleteTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Parameter_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, new String[] { "test1", "test2" });
            builder.Add(a => a.NoDataTip, "test3");
            builder.Add(a => a.DisplayCount, 10);
            builder.Add(a => a.IsLikeMatch, true);
            builder.Add(a => a.IgnoreCase, false);
            builder.Add(a => a.Debounce, 2000);
            builder.Add(a => a.ShowLabel, true);
            builder.Add(a => a.DisplayText, "test");
        });
        Assert.Contains("<div class=\"auto-complete\"", cut.Markup);
    }

    [Fact]
    public void KeyUp_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<string>>>(_ => Task.FromResult(items)));
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
    public void OnCustomFilter_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
    }

    [Fact]
    public void IsLikeMatch_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.IsLikeMatch, true);
            builder.Add(a => a.IgnoreCase, false);
            builder.Add(a => a.DisplayCount, 2);
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
    }

    [Fact]
    public void MouseDown_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<string>>>(_ => Task.FromResult(items)));
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".dropdown-item").MouseDown(new MouseEventArgs());
    }

    [Fact]
    public async Task ItemSelect_Test()
    {
        var clicked = false;
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.OnSelectedItemChanged, new Func<string, Task>(str =>
            {
                clicked = true;
                return Task.CompletedTask;
            }));
        });

        var input = cut.Find(".form-control");
        await cut.InvokeAsync(() => input.KeyUp(new KeyboardEventArgs() { Key = "t" }));
        await cut.InvokeAsync(() => input.KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" }));
        Assert.False(clicked);

        await cut.InvokeAsync(() => input.KeyUp(new KeyboardEventArgs() { Key = "Enter" }));
        Assert.True(clicked);

        clicked = false;
        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => item.MouseDown(new MouseEventArgs()));
        Assert.True(clicked);
    }

    [Fact]
    public void Esc_Test()
    {
        var esc = false;
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<string>>>(_ => Task.FromResult(items)));
            builder.Add(a => a.SkipEsc, true);
            builder.Add(a => a.OnEscAsync, new Func<string, Task>(v =>
            {
                esc = true;
                return Task.CompletedTask;
            }));
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Escape" });
        Assert.False(esc);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEsc, false);
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Escape" });
        Assert.True(esc);
    }

    [Fact]
    public void Enter_Test()
    {
        var enter = false;
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<string>>>(_ => Task.FromResult(items)));
            builder.Add(a => a.SkipEnter, true);
            builder.Add(a => a.OnEnterAsync, new Func<string, Task>(v =>
            {
                enter = true;
                return Task.CompletedTask;
            }));
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Enter" });
        Assert.False(enter);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEnter, false);
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "Enter" });
        Assert.True(enter);
    }

    [Fact]
    public void ShowDropdownListOnFocus_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowDropdownListOnFocus, false);
        });

        // 获得焦点时不会自动弹出下拉框
        cut.InvokeAsync(() =>
        {
            var input = cut.Find("input");
            input.FocusAsync(new FocusEventArgs());
            var menu = cut.Find("ul");
            Assert.Equal("dropdown-menu", menu.ClassList.ToString());
        });

        // 获得焦点时自动弹出下拉框
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDropdownListOnFocus, true);
        });
        cut.InvokeAsync(() =>
        {
            var input = cut.Find("input");
            input.FocusAsync(new FocusEventArgs());
            var menu = cut.Find("ul");
            Assert.Equal("dropdown-menu show", menu.ClassList.ToString());
        });

        var filter = false;
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.OnFocusFilter, true);
            pb.Add(a => a.OnCustomFilter, v =>
            {
                filter = true;
                return Task.FromResult<IEnumerable<string>>(new List<string>() { "12", "34" });
            });
        });

        // trigger focus
        cut.InvokeAsync(() =>
        {
            var input = cut.Find("input");
            input.FocusAsync(new FocusEventArgs());
            Assert.True(filter);
        });
    }

    [Fact]
    public async Task ItemTemplate_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, $"Template-{item}");
                builder.CloseElement();
            });
        });

        var input = cut.Find("input");
        await cut.InvokeAsync(() => input.FocusAsync(new FocusEventArgs()));

        Assert.Contains("Template-test1", cut.Markup);
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, new Foo());
            pb.AddChildContent<AutoComplete>(pb =>
            {
                pb.Add(a => a.Items, items);
            });
        });

        // Trigger js invoke
        var comp = cut.FindComponent<AutoComplete>().Instance;
        comp.TriggerOnChange("v");

        Assert.Equal("v", comp.Value);
    }

    [Fact]
    public void IsPopover_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.IsPopover, true);
            pb.Add(a => a.Placement, Placement.Auto);
            pb.Add(a => a.CustomClass, "ac-pop-test");
            pb.Add(a => a.ShowShadow, true);
        });

        // data-bs-toggle="@ToggleString" data-bs-placement="@PlacementString" data-bs-offset="@OffsetString" data-bs-custom-class="@CustomClassString"
        cut.Contains("data-bs-toggle=\"bb.dropdown\"");
        cut.DoesNotContain("data-bs-placement");
        cut.Contains("data-bs-custom-class=\"ac-pop-test shadow\"");
    }
}
