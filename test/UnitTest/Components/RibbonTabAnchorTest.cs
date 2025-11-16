// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Bunit.TestDoubles;

namespace UnitTest.Components;

/// <summary>
/// 
/// </summary>
public class RibbonTabAnchorTest : BootstrapBlazorTestBase
{
    [Fact]
    public void IsSupportAnchor_Ok()
    {
        var item1 = new RibbonTabItem() { Text = "test1" };
        item1.Items.Add(new RibbonTabItem() { Text = "Item" });
        var item2 = new RibbonTabItem() { Text = "test2" };
        item2.Items.Add(new RibbonTabItem() { Text = "Item" });

        var cut = Context.Render<RibbonTab>(pb =>
        {
            pb.Add(a => a.IsSupportAnchor, true);
            pb.Add(a => a.EncodeAnchorCallback, (url, text) =>
            {
                return $"{url}#{text}-anchor";
            });
            pb.Add(a => a.DecodeAnchorCallback, url => url.Split('#').LastOrDefault()?.Split('-').FirstOrDefault());
            pb.Add(a => a.Items, [item1, item2]);
        });

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".tabs-item");
            item.Click();
        });

        var nav = Context.Services.GetRequiredService<BunitNavigationManager>();
        Assert.Equal("http://localhost/#test1-anchor", nav.Uri);

        nav.NavigateTo("http://localhost/#test2-anchor");
        cut.Render();
        var item = cut.FindComponents<TabItem>();
        Assert.True(item[1].Instance.IsActive);

        cut.Render(pb =>
        {
            pb.Add(a => a.EncodeAnchorCallback, null);
            pb.Add(a => a.DecodeAnchorCallback, null);
        });

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".tabs-item");
            item.Click();
        });
        Assert.Equal("http://localhost/#test1", nav.Uri);

        nav.NavigateTo("http://localhost/#test2");
        cut.Render();
        item = cut.FindComponents<TabItem>();
        Assert.True(item[1].Instance.IsActive);
    }
}
