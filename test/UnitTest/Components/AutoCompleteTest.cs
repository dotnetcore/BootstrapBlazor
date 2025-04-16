// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class AutoCompleteTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.IsSelectAllTextOnFocus, true);
            pb.Add(a => a.IsSelectAllTextOnEnter, true);
        });
        Assert.Contains("<div class=\"auto-complete\"", cut.Markup);
        Assert.Contains("data-bb-trigger-delete=\"true\"", cut.Markup);

        var menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowNoDataTip, false);
        });
        menus = cut.FindAll(".dropdown-item");
        Assert.Empty(menus);

        var items = new List<string>() { "test1", "test2" };
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public void Value_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, new List<string>() { "test1", "test12", "test123", "test1234" });
            pb.Add(a => a.Value, "test12");
            pb.Add(a => a.DisplayCount, 2);
        });
        var menus = cut.FindAll(".dropdown-item");

        // 由于 Value = test12
        // 并且设置了 DisplayCount = 2
        // 候选项只有 2 个
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public void Debounce_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>();
        Assert.DoesNotContain("data-bb-debounce", cut.Markup);

        cut.SetParametersAndRender(pb => pb.Add(a => a.Debounce, 100));
        cut.Contains("data-bb-debounce=\"100\"");
    }

    [Fact]
    public async Task OnCustomFilter_Test()
    {
        var items = new List<string> { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.OnCustomFilter, _ => Task.FromResult<IEnumerable<string>>(["test2", "test3", "test4"]));
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Equal(3, menus.Count);
    }

    [Fact]
    public async Task IsLikeMatch_Test()
    {
        var items = new List<string>() { "task1", "Task2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
        });

        // 正常匹配 无结果显示 NoDataTip
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("a"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        // 模糊匹配
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsLikeMatch, true);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("as"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("k1"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter(""));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public async Task IgnoreCase_Ok()
    {
        var items = new List<string>() { "task1", "Task2" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.IgnoreCase, false);
        });

        // 大小写敏感
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Single(menus);

        // 忽略大小写
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IgnoreCase, true);
        });
        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public async Task DisplayCount_Ok()
    {
        var items = new List<string>() { "task1", "Task2", "task3", "Task4" };
        var cut = Context.RenderComponent<AutoComplete>(builder =>
        {
            builder.Add(a => a.Items, items);
            builder.Add(a => a.DisplayCount, 2);
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Equal(2, menus.Count);
    }

    [Fact]
    public async Task OnCustomFilter_Ok()
    {
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnCustomFilter, _ => Task.FromResult<IEnumerable<string>>(["test3", "test4", "test5"]));
        });

        await cut.InvokeAsync(() => cut.Instance.TriggerFilter("t"));
        var menus = cut.FindAll(".dropdown-item");
        Assert.Equal(3, menus.Count);
    }

    [Fact]
    public void ShowDropdownListOnFocus_Ok()
    {
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
        });
        cut.Contains("data-bb-auto-dropdown-focus=\"true\"");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowDropdownListOnFocus, false);
        });
        cut.DoesNotContain("data-bb-auto-dropdown-focus");
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var items = new List<string>() { "test1", "test2" };
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.AddContent(0, $"Template-{item}");
            });
        });

        Assert.Contains("Template-test1", cut.Markup);
        Assert.Contains("Template-test2", cut.Markup);
    }

    [Fact]
    public async Task OnSelectedItemChanged_Ok()
    {
        var items = new List<string>() { "test1", "test2" };
        var selectedItem = "";
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.Items, items);
            pb.Add(a => a.OnSelectedItemChanged, v => { selectedItem = v; return Task.CompletedTask; });
        });

        var item = cut.Find(".dropdown-item");
        await cut.InvokeAsync(() => item.Click());
        Assert.Equal("test1", selectedItem);
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
        Assert.Contains("form-label", cut.Markup);
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
            pb.Add(a => a.Value, "test1");
            pb.Add(a => a.OnBlurAsync, v =>
            {
                val = v;
                return Task.CompletedTask;
            });
        });

        // trigger blur
        await cut.InvokeAsync(() => cut.Instance.TriggerBlur());
        Assert.Equal("test1", val);
    }

    [Fact]
    public void SkipEnter_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.SkipEnter, false);
        });
        cut.DoesNotContain("data-bb-skip-enter");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEnter, true);
        });
        cut.Contains("data-bb-skip-enter=\"true\"");
    }

    [Fact]
    public void SkipEsc_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.SkipEsc, false);
        });
        cut.DoesNotContain("data-bb-skip-esc");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.SkipEsc, true);
        });
        cut.Contains("data-bb-skip-esc=\"true\"");
    }

    [Fact]
    public void ScrollIntoViewBehavior_Ok()
    {
        var cut = Context.RenderComponent<AutoComplete>(pb =>
        {
            pb.Add(a => a.ScrollIntoViewBehavior, ScrollIntoViewBehavior.Smooth);
        });
        cut.DoesNotContain("data-bb-scroll-behavior");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ScrollIntoViewBehavior, ScrollIntoViewBehavior.Auto);
        });
        cut.Contains("data-bb-scroll-behavior=\"auto\"");
    }

    [Fact]
    public void Trigger_Ok()
    {
        var cut = Context.RenderComponent<MockPopoverCompleteBase>();
        cut.Instance.TriggerFilter("test");
    }

    class MockPopoverCompleteBase : PopoverCompleteBase<string>
    {
        public override Task TriggerFilter(string val)
        {
            return base.TriggerFilter(val);
        }
    }
}
