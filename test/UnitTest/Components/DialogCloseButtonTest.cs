// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DialogCloseButtonTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Icon_Ok()
    {
        var cut = Context.Render<DialogCloseButton>();
        Assert.Equal("fa-solid fa-xmark", cut.Instance.Icon);
        cut.Contains("fa-solid fa-xmark");

        cut.Render(pb => pb.Add(p => p.Icon, "test-icon"));
        Assert.Equal("test-icon", cut.Instance.Icon);
        cut.Contains("test-icon");
    }
}
