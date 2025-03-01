// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class BootstrapBlazorRootOutletTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Content_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.Add(a => a.EnableErrorLogger, false);
            pb.AddChildContent<BootstrapBlazorRootContent>(pbc =>
            {
                pbc.Add(a => a.RootName, "test");
                pbc.AddChildContent("test-content");
            });
        });
        cut.DoesNotContain("test-content");

        var content = cut.FindComponent<BootstrapBlazorRootContent>();
        content.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.RootName, null);
        });
        cut.Contains("test-content");

        content.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.RootId, new object());
        });
        cut.DoesNotContain("test-content");

        content.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.RootId, null);
        });
        cut.Contains("test-content");

        content.SetParametersAndRender(pb =>
        {
            pb.AddChildContent((RenderFragment)null!);
        });

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            content.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.RootName, "test");
                pb.Add(a => a.RootId, new object());
            });
            return Task.CompletedTask;
        });
        Assert.Equal("BootstrapBlazorRootContent requires that 'RootName' and 'RootId' cannot both have non-null values.", exception.Message);
    }

    [Fact]
    public async Task Outlet_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRootOutlet>(pb =>
        {
            pb.Add(a => a.RootId, new object());
        });
        Assert.Empty(cut.Markup);

        cut.SetParametersAndRender(pb =>
        {
            pb.Add(a => a.RootId, null);
            pb.Add(a => a.RootName, "test");
        });
        Assert.Empty(cut.Markup);

        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
        {
            cut.SetParametersAndRender(pb =>
            {
                pb.Add(a => a.RootName, "test");
                pb.Add(a => a.RootId, new object());
            });
            return Task.CompletedTask;
        });
        Assert.Equal("BootstrapBlazorRootOutlet requires that 'RootName' and 'RootId' cannot both have non-null values.", exception.Message);
    }
}
