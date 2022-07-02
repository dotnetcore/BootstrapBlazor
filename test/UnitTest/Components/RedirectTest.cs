// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class RedirectTest : TestBase
{
    [Fact]
    public void Url_Ok()
    {
        var cut = Context.RenderComponent<Redirect>(pb =>
        {
            pb.Add(a => a.Url, "Account/Test");
        });
        Assert.Equal("Account/Test", cut.Instance.Url);
    }
}
