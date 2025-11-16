// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class FilterProviderTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task FilterProvider_Ok()
    {
        var cut = Context.Render<FilterProvider>(pb =>
        {
            pb.Add(a => a.ShowMoreButton, true);
            pb.AddChildContent<MockFilter>();
        });

        var filter = cut.FindComponent<MockFilter>();
        var context = filter.Instance.GetFilterContext();
        Assert.NotNull(context);
        Assert.Equal(0, context.Count);

        // 点击 +
        var plus = cut.Find(".card-footer button");
        await cut.InvokeAsync(() => plus.Click());
        context = filter.Instance.GetFilterContext();
        Assert.NotNull(context);
        Assert.Equal(1, context.Count);
    }

    class MockFilter : StringFilter
    {
        public FilterContext? GetFilterContext() => FilterContext;
    }
}
