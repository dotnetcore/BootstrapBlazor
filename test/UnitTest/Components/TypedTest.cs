// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TypedTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<Typed>(pb =>
        {
            pb.Add(a => a.Text, "Test");
        });
        cut.MarkupMatches("<span diff:ignore></span>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Text, "Test1");
        });
    }

    [Fact]
    public void Options_Ok()
    {
        var cut = Context.RenderComponent<Typed>(pb =>
        {
            pb.Add(a => a.Options, new TypedOptions() { Text = ["test1", "test2"], TypeSpeed = 70 });
        });
        cut.MarkupMatches("<span diff:ignore></span>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Options, new TypedOptions() { Text = null, TypeSpeed = 70 });
        });
        cut.MarkupMatches("<span diff:ignore></span>");

        cut.SetParametersAndRender();

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Options, new TypedOptions() { Text = ["test1", "test2", "test3"], TypeSpeed = 70 });
        });
        cut.MarkupMatches("<span diff:ignore></span>");

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.Options, null);
        });
    }

    [Fact]
    public async Task TriggerComplete_Ok()
    {
        var triggered = false;
        Task onCompleteCallback()
        {
            triggered = true;
            return Task.CompletedTask;
        }
        var cut = Context.RenderComponent<Typed>(pb =>
        {
            pb.Add(a => a.Options, new TypedOptions() { Text = ["test1", "test2"], TypeSpeed = 70 });
            pb.Add(a => a.OnCompleteAsync, onCompleteCallback);
        });
        cut.MarkupMatches("<span diff:ignore></span>");

        await cut.InvokeAsync(() => cut.Instance.TriggerComplete());
        Assert.True(triggered);
    }

    [Fact]
    public void TypedOptions_Ok()
    {
        // Arrange
        var options = new TypedOptions()
        {
            Text = ["test1", "test2"],
            TypeSpeed = 70,
            BackSpeed = 50,
            BackDelay = 200,
            Loop = true,
            LoopCount = 2,
            ShowCursor = true,
            CursorChar = "|",
            ContentType = "html",
            Shuffle = false,
            SmartBackspace = true
        };

        // Assert
        Assert.NotNull(options.Text);
        Assert.Equal(2, options.Text.Count);
        Assert.Equal("test1", options.Text[0]);
        Assert.Equal("test2", options.Text[1]);
        Assert.Equal(70, options.TypeSpeed);
        Assert.Equal(50, options.BackSpeed);
        Assert.Equal(200, options.BackDelay);
        Assert.True(options.Loop);
        Assert.Equal(2, options.LoopCount);
        Assert.True(options.ShowCursor);
        Assert.False(options.Shuffle);
        Assert.True(options.SmartBackspace);
        Assert.Equal("|", options.CursorChar);
        Assert.Equal("html", options.ContentType);
    }

    [Fact]
    public void Equal_Ok()
    {
        var options = new TypedOptions() { Text = ["test1", "test2"], TypeSpeed = 70 };
        Assert.False(options.Equals(null));

        var options2 = new TypedOptions() { Text = ["test1", "test2", "test3"], TypeSpeed = 70 };
        Assert.False(options.Equals(options2));

        var options3 = new TypedOptions() { Text = ["test1", "test2"], TypeSpeed = 70 };
        Assert.True(options.Equals(options3));

        Assert.True(options.Equals((object)options3));
        Assert.False(options.Equals(new object()));
        Assert.True(options.GetHashCode() > 0);
    }
}
