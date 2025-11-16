// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class RedirectTest : TestBase
{
    [Fact]
    public void Url_Ok()
    {
        var cut = Context.Render<Redirect>(pb =>
        {
            pb.Add(a => a.Url, "Account/Test");
            pb.Add(a => a.ForceLoad, true);
        });
        Assert.Equal("Account/Test", cut.Instance.Url);
    }
}
