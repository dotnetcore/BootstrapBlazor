// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class ScriptTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Script_Ok()
    {
        var cut = Context.RenderComponent<Script>(pb =>
        {
            pb.Add(a => a.Src, "http://www.blazor.zone");
        });
        var versionService = cut.Services.GetRequiredService<IVersionService>();
        Assert.Equal($"<script src=\"http://www.blazor.zone?v={versionService.GetVersion()}\"></script>", cut.Markup);
    }

    [Fact]
    public void Script_Version()
    {
        var cut = Context.RenderComponent<Script>(pb =>
        {
            pb.Add(a => a.Src, "http://www.blazor.zone");
            pb.Add(a => a.Version, "20220202");
        });
        Assert.Equal($"<script src=\"http://www.blazor.zone?v=20220202\"></script>", cut.Markup);
    }
}
