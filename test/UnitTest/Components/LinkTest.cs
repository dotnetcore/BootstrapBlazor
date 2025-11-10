// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class LinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Link_Ok()
    {
        var cut = Context.RenderComponent<Link>(pb =>
        {
            pb.Add(a => a.Href, "http://www.blazor.zone");
            pb.Add(a => a.Rel, "stylesheet-test");
        });
        var versionService = cut.Services.GetRequiredService<IVersionService>();
        Assert.Equal($"<link href=\"http://www.blazor.zone?v={versionService.GetVersion()}\" rel=\"stylesheet-test\" />", cut.Markup);
    }

    [Fact]
    public void Link_Version()
    {
        var cut = Context.RenderComponent<Link>(pb =>
        {
            pb.Add(a => a.Href, "http://www.blazor.zone");
            pb.Add(a => a.Version, "20220202");
        });
        Assert.Equal($"<link href=\"http://www.blazor.zone?v=20220202\" rel=\"stylesheet\" />", cut.Markup);
    }

    [Fact]
    public void Link_IsAddToHead_True()
    {
        var cut = Context.RenderComponent<Link>(pb =>
        {
            pb.Add(a => a.Href, "http://www.blazor.zone");
            pb.Add(a => a.Version, "20220202");
            pb.Add(a => a.IsAddToHead, true);
        });
        Assert.Equal(string.Empty, cut.Markup);
    }
}
