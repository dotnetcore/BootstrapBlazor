// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class LinkTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Link_Ok()
    {
        var cut = Context.RenderComponent<Link>(pb =>
        {
            pb.Add(a => a.Href, "http://www.blazor.zone");
        });
        var versionService = cut.Services.GetRequiredService<IVersionService>();
        Assert.Equal($"<link href=\"http://www.blazor.zone?v={versionService.GetVersion()}\" rel=\"stylesheet\" />", cut.Markup);
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
}
