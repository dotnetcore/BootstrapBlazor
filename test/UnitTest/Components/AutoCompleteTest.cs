// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
}
