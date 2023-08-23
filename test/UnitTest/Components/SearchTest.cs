// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Web;

namespace UnitTest.Components;

public class SearchTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ShowClearButton_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder => builder.Add(s => s.ShowClearButton, true));
        cut.Contains("btn-secondary");
    }

    [Fact]
    public void ClearButtonIcon_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.ShowClearButton, true);
            builder.Add(s => s.ClearButtonIcon, "fa-fw fa-solid fa-trash");
        });
        Assert.Contains("fa-trash", cut.Markup);
    }

    [Fact]
    public void ClearButtonText_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.ShowClearButton, true);
            builder.Add(s => s.ClearButtonText, "Clear button");
        });
        Assert.Contains("Clear button", cut.Markup);
    }

    [Fact]
    public void ClearButtonColor_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.ShowClearButton, true);
            builder.Add(s => s.ClearButtonColor, Color.Secondary);
        });
        var ele = cut.Find(".btn-secondary");
        Assert.NotNull(ele);
    }

    [Fact]
    public void SearchButtonColor_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.SearchButtonColor, Color.Secondary);
        });
        var ele = cut.Find(".btn-secondary");
        Assert.NotNull(ele);
    }

    [Fact]
    public void SearchButtonIcon_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.SearchButtonIcon, "fa-fw fa-solid fa-magnifying-glass");
        });
        var ele = cut.Find(".fa-magnifying-glass");
        Assert.NotNull(ele);
    }

    [Fact]
    public void SearchButtonLoadingIcon_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.SearchButtonLoadingIcon, "fa-fw fa-spin fa-solid fa-spinner");
        });
    }

    [Fact]
    public void IsAutoClearAfterSearch_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Value, "Test");
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.IsAutoClearAfterSearch, true);
        });
        cut.Find(".btn-primary").Click();
    }

    [Fact]
    public async Task IsOnInputTrigger_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Value, "");
            builder.Add(s => s.IsOnInputTrigger, true);
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.OnSearch, v =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        await cut.InvokeAsync(() => cut.Instance.OnKeyUp("1"));
        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => item.MouseDown());
        Assert.True(clicked);
    }

    [Fact]
    public void SearchButtonText_Ok()
    {
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.SearchButtonText, "Search Text");
        });
        Assert.Contains("Search Text", cut.Markup);
    }

    [Fact]
    public void OnSearch_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.OnSearch, v =>
            {
                ret = true;
                return Task.CompletedTask;
            });
        });
        cut.Find(".btn-primary").Click();
        Assert.True(ret);
    }

    [Fact]
    public void OnClickItem_Ok()
    {
        var clicked = false;
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Value, "1");
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.OnSearch, v =>
            {
                clicked = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Find(".btn-primary").Click());
        Assert.True(clicked);
    }

    [Fact]
    public void OnClearClick_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Value, "1");
            builder.Add(s => s.ShowClearButton, true);
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.OnClear, v =>
            {
                ret = true;
                return Task.CompletedTask;
            });
        });
        cut.Find(".btn-secondary").Click();
        Assert.True(ret);
    }

    [Fact]
    public void OnEnterAsync_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Value, "1");
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.OnEnterAsync, v =>
            {
                ret = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Enter"));
        Assert.True(ret);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsOnInputTrigger, true);
        });
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Enter"));
    }

    [Fact]
    public void OnEscAsync_Ok()
    {
        var ret = false;
        var cut = Context.RenderComponent<Search>(builder =>
        {
            builder.Add(s => s.Value, "1");
            builder.Add(s => s.Items, new string[] { "1", "12", "123", "1234" });
            builder.Add(s => s.OnEscAsync, v =>
            {
                ret = true;
                return Task.CompletedTask;
            });
        });
        cut.InvokeAsync(() => cut.Instance.OnKeyUp("Escape"));
        Assert.True(ret);
    }

    [Fact]
    public void ValidateForm_Ok()
    {
        IEnumerable<string> items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, new Foo());
            pb.AddChildContent<Search>(pb =>
            {
                pb.Add(a => a.Items, items);
            });
        });

        // Trigger js invoke
        var comp = cut.FindComponent<Search>().Instance;
        comp.TriggerOnChange("v");

        Assert.Equal("v", comp.Value);
    }
}
