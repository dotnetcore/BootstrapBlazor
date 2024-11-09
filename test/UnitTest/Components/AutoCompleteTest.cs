﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Web;
using System.Reflection;

namespace UnitTest.Components;

public class AutoCompleteTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Parameter_Ok()
    {
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.NoDataTip, "test3");
            builder.Add(a => a.ShowNoDataTip, true);
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

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowUp"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowUp"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowUp"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowDown"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowDown"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowDown"));
    }

    [Fact]
    public void OnCustomFilter_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
        });

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
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

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
    }

    [Fact]
    public void MouseDown_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<string>>>(_ => Task.FromResult(items)));
        });

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.Find(".dropdown-item").MouseDown(new MouseEventArgs());
    }

    [Fact]
    public void ItemSelect_Test()
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

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowDown"));
        Assert.False(clicked);

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Enter"));
        Assert.True(clicked);

        clicked = false;
        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".dropdown-item");
            item.MouseDown();
        });
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

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Escape"));
        Assert.False(esc);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEsc, false);
        });

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Escape"));
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

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowDown"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Enter"));
        Assert.False(enter);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEnter, false);
        });

        cut.InvokeAsync(() => cut.Instance.OnKeyUp("t"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("ArrowDown"));
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("NumpadEnter"));
        Assert.True(enter);
    }

    [Fact]
    public async Task ShowDropdownListOnFocus_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
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
        // IsShown = true
        Assert.True(GetShownValue(cut.Instance));

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
        input = cut.Find("input");
        await cut.InvokeAsync(() => input.FocusAsync(new FocusEventArgs()));
        Assert.True(filter);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsPopover, true);
            pb.Add(a => a.OnFocusFilter, false);
        });
        input = cut.Find("input");
        await cut.InvokeAsync(() => input.FocusAsync(new FocusEventArgs()));
        // IsShown = true
        Assert.True(GetShownValue(cut.Instance));
    }

    private static bool GetShownValue(AutoComplete instance)
    {
        var fieldInfo = instance.GetType().GetField("_isShown", BindingFlags.NonPublic | BindingFlags.Instance);
        return fieldInfo?.GetValue(instance) is true;
    }

    [Fact]
    public async Task OnFocus_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.DisplayCount, 1);
        });

        // trigger focus
        await cut.InvokeAsync(async () =>
        {
            var input = cut.Find("input");
            await input.FocusAsync(new FocusEventArgs());
        });

        var filters = cut.FindAll(".dropdown-item");
        Assert.Single(filters);
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
        var items = new List<string>() { "test1", "test2" };
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

    [Fact]
    public async Task OnBlurAsync_Ok()
    {
        string? val = "";
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnBlurAsync, v =>
            {
                val = v;
                return Task.CompletedTask;
            });
        });

        // trigger blur
        var input = cut.Find("input");
        await cut.InvokeAsync(() =>
        {
            input.Input("123");
            input.Blur();
        });
        Assert.Equal("123", val);
    }
}
