// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class DynamicElementTest
{
    [Fact]
    public void Key_OK()
    {
        var context = new BunitContext();
        var cut = context.Render<DynamicElement>(pb =>
        {
            pb.Add(s => s.Key, Guid.NewGuid());
        });

        Assert.Equal("<div></div>", cut.Markup);
    }
}
