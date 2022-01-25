// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Bunit;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using UnitTest.Core;
using Xunit;

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
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "ArrowDown" });
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
    public void OnBlur_Test()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.OnCustomFilter, new Func<string, Task<IEnumerable<string>>>(_ => Task.FromResult(items)));
        });

        cut.Find(".form-control").KeyUp(new KeyboardEventArgs() { Key = "t" });
        cut.Find(".form-control").Blur();
    }
}
