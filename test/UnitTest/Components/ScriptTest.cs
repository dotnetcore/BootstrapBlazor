// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class ScriptTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Script_Ok()
    {
        var cut = Context.Render<Script>(pb =>
        {
            pb.Add(a => a.Src, "http://www.blazor.zone");
        });
        var versionService = cut.Services.GetRequiredService<IVersionService>();
        Assert.Equal($"<script src=\"http://www.blazor.zone?v={versionService.GetVersion()}\"></script>", cut.Markup);
    }

    [Fact]
    public void Script_Version()
    {
        var cut = Context.Render<Script>(pb =>
        {
            pb.Add(a => a.Src, "http://www.blazor.zone");
            pb.Add(a => a.Version, "20220202");
        });
        Assert.Equal($"<script src=\"http://www.blazor.zone?v=20220202\"></script>", cut.Markup);
    }
}
