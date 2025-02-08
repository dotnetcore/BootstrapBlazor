// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DropdownTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task ShowSplit_OK()
    {
        var clicked = false;
        var clickedWithoutRender = false;
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>(pb =>
        {
            pb.Add(a => a.ShowSplit, true);
            pb.Add(a => a.OnClick, () =>
            {
                clicked = true;
            });
            pb.Add(a => a.OnClickWithoutRender, () =>
            {
                clickedWithoutRender = true;
                return Task.CompletedTask;
            });
        });
        Assert.Contains(" dropdown-toggle-split", cut.Markup);

        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
        Assert.True(clicked);
        Assert.True(clickedWithoutRender);
    }

    [Fact]
    public async Task IsAsync_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>(pb =>
        {
            pb.Add(a => a.ShowSplit, true);
            pb.Add(a => a.IsAsync, true);
            pb.Add(a => a.IsKeepDisabled, false);
            pb.Add(a => a.Icon, "fa-solid fa-test-icon");
            pb.Add(a => a.OnClickWithoutRender, () =>
            {
                return Task.CompletedTask;
            });
        });
        cut.Contains("<i class=\"fa-solid fa-test-icon\"></i>");
        var button = cut.Find("button");
        await cut.InvokeAsync(() => button.Click());
    }

    [Fact]
    public void ShowSize_OK()
    {
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>(pb =>
        {
            pb.Add(a => a.Size, BootstrapBlazor.Components.Size.ExtraLarge);
        });
        Assert.Contains("btn-xl", cut.Markup);
    }

    [Theory]
    [InlineData(Direction.Dropleft)]
    [InlineData(Direction.Dropright)]
    [InlineData(Direction.Dropup)]
    [InlineData(Direction.Dropdown)]
    public void Direction_Ok(Direction direction)
    {
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>(pb =>
        {
            pb.Add(a => a.MenuAlignment, Alignment.Center);
            pb.Add(a => a.Direction, direction);
        });
        Assert.Contains(direction.ToDescriptionString(), cut.Markup);
    }

    [Fact]
    public void MenuAlignment()
    {
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>(pb =>
        {
            pb.Add(a => a.MenuAlignment, Alignment.Left);
        });
        Assert.DoesNotContain("dropdown-menu-end", cut.Markup);
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.MenuAlignment, Alignment.Right);
        });
        Assert.Contains("dropdown-menu-end", cut.Markup);
    }

    [Fact]
    public async Task IsFixedButtonText()
    {
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.Items, new List<SelectedItem>
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.IsFixedButtonText, true);
        });

        var items = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() =>
        {
            items[0].Click();
        });
        var button = cut.Find("button");
        Assert.Equal("Test1", button.TextContent);

        // 设置 IsFixed false
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.IsFixedButtonText, false);
        });
        items = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() =>
        {
            items[1].Click();
        });
        button = cut.Find("button");
        Assert.Equal("Test2", button.TextContent);

        // ShowFixedButtonTextInDropdown
        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.ShowFixedButtonTextInDropdown, true);
        });
        items = cut.FindAll(".dropdown-item");
        Assert.Equal(2, items.Count);
    }

    [Fact]
    public void DisplayText_OK()
    {
        var foo = new Foo();
        var cut = Context.RenderComponent<ValidateForm>(pb =>
        {
            pb.Add(a => a.Model, foo);
            pb.AddChildContent<Dropdown<EnumEducation?>>(pb =>
            {
                pb.Add(a => a.Value, foo.Education);
                pb.Add(a => a.ValueExpression, Utility.GenerateValueExpression(foo, nameof(Foo.Education), typeof(EnumEducation?)));
            });
        });
        cut.Contains("class=\"form-label\"");
    }

    [Fact]
    public void SelectOption_Ok()
    {
        var cut = Context.RenderComponent<SelectOption>(pb =>
        {
            pb.Add(a => a.Text, "Test-SelectOption");
            pb.Add(a => a.GroupName, "Test-GroupName");
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.Active, true);
            pb.Add(a => a.Value, "");
        });
    }

    [Fact]
    public void Enum_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>();
        Assert.Equal(2, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public void NullableEnum_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<EnumEducation?>>(pb =>
        {
            pb.Add(a => a.AdditionalAttributes, new Dictionary<string, object>()
            {
                ["placeholder"] = ""
            });
        });
        Assert.Equal(2, cut.FindAll(".dropdown-item").Count);
    }

    [Fact]
    public async Task OnSelectedItemChanged_OK()
    {
        var triggered = false;
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1"),
                new("2", "Test2")
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.OnSelectedItemChanged, item =>
            {
                triggered = true;
                return Task.CompletedTask;
            });
        });
        var items = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => items[1].Click());
        Assert.True(triggered);
    }

    [Fact]
    public async Task OnValueChanged_Ok()
    {
        var triggered = false;
        var cut = Context.RenderComponent<Dropdown<EnumEducation>>(pb =>
        {
            pb.Add(a => a.OnValueChanged, item =>
            {
                triggered = true;
                return Task.CompletedTask;
            });
        });
        var items = cut.FindAll(".dropdown-item");
        await cut.InvokeAsync(() => items[1].Click());
        Assert.True(triggered);
    }

    [Fact]
    public void Color_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.Color, Color.Danger);
        });
        Assert.Contains("btn-danger", cut.Markup);
    }

    [Fact]
    public void ItemTemplate_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1") { GroupName = "Test1" },
                new("2", "Test2") { GroupName = "Test2" }
            });
            pb.Add(a => a.Value, "2");
            pb.Add(a => a.ItemTemplate, item => builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "class", "item-template");
                builder.AddContent(2, item.Text);
                builder.CloseComponent();
            });
        });
        cut.Contains("item-template");
    }

    [Fact]
    public void Disabled_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.IsDisabled, false);
            pb.Add(a => a.Items, new SelectedItem[]
            {
                new("1", "Test1") { IsDisabled = true },
                new("2", "Test2")
            });
        });
        cut.Contains("<div class=\"dropdown-item disabled\">Test1</div>");
    }

    [Fact]
    public void ItemsTemplate_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.ItemsTemplate, new RenderFragment(builder =>
            {
                builder.AddContent(0, new MarkupString("<div>test-items-template</div>"));
            }));
        });
        cut.Contains("<div>test-items-template</div>");
    }

    [Fact]
    public void ButtonTemplate_Ok()
    {
        var cut = Context.RenderComponent<Dropdown<string>>(pb =>
        {
            pb.Add(a => a.ButtonTemplate, new RenderFragment<SelectedItem?>(item => builder =>
            {
                builder.AddContent(0, new MarkupString("<span>test-button-template</span>"));
            }));
        });
        cut.Contains("<span>test-button-template</span>");
    }
}
