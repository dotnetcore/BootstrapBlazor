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

    [Fact]
    public async Task OnCountChanged_Ok()
    {
        var cut = Context.Render<FilterProvider>(pb =>
        {
            pb.Add(a => a.ShowMoreButton, true);
            pb.AddChildContent<MockFilter>();
        });
        var filter = cut.FindComponent<MockFilter>().Instance;

        // 只有一个过滤条件
        Assert.Equal(0, filter.GetFilterContext()!.Count);

        var conditions = new FilterKeyValueAction
        {
            Filters =
            [
                new() { FieldValue = "first" },
                new() { FieldValue = "second" }
            ]
        };

        // 设置后有两个过滤条件
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(conditions));
        Assert.Equal(1, filter.GetFilterContext()!.Count);

        // 调用重置方法后过滤条件数量为 0
        await cut.InvokeAsync(filter.Reset);
        Assert.Equal(0, filter.GetFilterContext()!.Count);
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task SetCount_WithoutCallback(bool hasContext)
    {
        var cut = Context.Render<MockFilter>(pb =>
        {
            if (hasContext)
            {
                pb.AddCascadingValue(new FilterContext());
            }
        });

        await cut.InvokeAsync(() => cut.Instance.SetFilterConditionsAsync(new FilterKeyValueAction
        {
            Filters =
            [
                new() { FieldValue = "first" },
                new() { FieldValue = "second" }
            ]
        }));

        Assert.Equal(1, cut.Instance.Count);

        await cut.InvokeAsync(cut.Instance.Reset);

        Assert.Equal(0, cut.Instance.Count);
    }

    class MockFilter : StringFilter
    {
        public FilterContext? GetFilterContext() => FilterContext;
    }
}
