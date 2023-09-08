// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit;

namespace UnitTest.Components;

public class StepTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Step_Items()
    {
        var cut = Context.RenderComponent<Step>(pb =>
        {
            pb.Add(a => a.Items, GetStepItems);
        });
        cut.Contains("class=\"step\"");
        cut.Contains("step-header");
        cut.Contains("step-body");

        var headers = cut.FindAll(".step-item");
        Assert.Equal(3, headers.Count);

        var body = cut.FindAll(".step-body-item");
        Assert.Equal(3, body.Count);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Items, null);
        });
        cut.Contains("class=\"step\"");
        cut.Contains("step-header");
        cut.Contains("step-body");

        headers = cut.FindAll(".step-item");
        Assert.Empty(headers);

        body = cut.FindAll(".step-body-item");
        Assert.Empty(body);
    }

    [Fact]
    public void Step_IsVertical()
    {
        var cut = Context.RenderComponent<Step>(pb =>
        {
            pb.Add(a => a.Items, GetStepItems);
            pb.Add(a => a.IsVertical, true);
        });
        cut.Contains("class=\"step step-vertical\"");
    }

    [Fact]
    public void Step_ChildContent()
    {
        var cut = Context.RenderComponent<Step>(pb =>
        {
            pb.Add(a => a.StepIndex, 1);
            pb.Add(a => a.ChildContent, builder =>
            {
                builder.OpenComponent<StepItem>(0);
                builder.AddAttribute(3, nameof(StepItem.FinishedIcon), "test-item-finish-icon1");
                builder.AddAttribute(4, nameof(StepItem.Title), "test-item-title");
                builder.AddAttribute(5, nameof(StepItem.Description), "test-item-desc");
                builder.AddAttribute(6, nameof(StepItem.ChildContent), new RenderFragment(b =>
                {
                    b.AddContent(7, "test-item-content");
                }));
                builder.CloseComponent();

                builder.OpenComponent<StepItem>(10);
                builder.AddAttribute(11, nameof(StepItem.HeaderTemplate), new RenderFragment<StepOption>(option => b =>
                {
                    b.AddContent(12, "test-item-header-template");
                }));
                builder.AddAttribute(13, nameof(StepItem.TitleTemplate), new RenderFragment<StepOption>(option => b =>
                {
                    b.AddContent(14, "test-item-title-template");
                }));
                builder.AddAttribute(15, nameof(StepItem.FinishedIcon), "test-item-finish-icon1");
                builder.CloseComponent();

                builder.OpenComponent<StepItem>(20);
                builder.AddAttribute(21, nameof(StepItem.Text), "Text1");
                builder.AddAttribute(22, nameof(StepItem.Icon), "test-item-icon1");
                builder.CloseComponent();
            });
        });
        var headers = cut.FindAll(".step-item");
        Assert.True(headers[1].ClassList.Contains("active"));

        cut.Contains("Text1");
        cut.Contains("test-item-icon1");
        cut.Contains("test-item-finish-icon1");
        cut.Contains("test-item-title");
        cut.Contains("test-item-desc");
        cut.Contains("test-item-content");
        cut.Contains("test-item-header-template");
        cut.Contains("test-item-title-template");
    }

    [Fact]
    public void Step_Method()
    {
        var cut = Context.RenderComponent<Step>(pb =>
        {
            pb.Add(a => a.Items, GetStepItems);
            pb.Add(a => a.StepIndex, 1);
        });
        var step = cut.Instance;

        cut.InvokeAsync(() => step.Next());
        var headers = cut.FindAll(".step-item");
        Assert.True(headers[2].ClassList.Contains("active"));

        cut.InvokeAsync(() => step.Prev());
        headers = cut.FindAll(".step-item");
        Assert.True(headers[1].ClassList.Contains("active"));

        cut.InvokeAsync(() => step.Reset());
        headers = cut.FindAll(".step-item");
        Assert.True(headers[0].ClassList.Contains("active"));

        cut.InvokeAsync(() => step.Insert(1, new StepOption() { Text = "test1" }));
        Assert.Equal(4, cut.Instance.Items.Count);
    }

    [Fact]
    public void StepItem_Ok()
    {
        var cut = Context.RenderComponent<StepItem>(pb =>
        {
            pb.Add(a => a.Text, "test1");
        });
        Assert.Equal("test1", cut.Instance.Text);
    }

    private static List<StepOption> GetStepItems => new()
    {
        new StepOption()
        {
            Template = builder => builder.AddContent(0, "SteItem1")
        },
        new StepOption()
        {
            Template = builder => builder.AddContent(0, "SteItem2")
        },
        new StepOption()
        {
            Template = builder => builder.AddContent(0, "SteItem3")
        }
    };
}
