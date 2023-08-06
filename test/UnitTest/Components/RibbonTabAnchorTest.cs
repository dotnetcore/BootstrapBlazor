// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

/// <summary>
/// 
/// </summary>
public class RibbonTabAnchorTest : RibbonTabTestBase
{
    [Fact]
    public void IsSupportAnchor_Ok()
    {
        var cut = Context.RenderComponent<RibbonTab>(pb =>
        {
            pb.Add(a => a.IsSupportAnchor, true);
            pb.Add(a => a.EncodeAnchorCallback, (url, text) =>
            {
                return $"{url}#{text}-anchor";
            });
            pb.Add(a => a.DecodeAnchorCallback, url => url.Split('#').LastOrDefault()?.Split('-').FirstOrDefault());
            pb.Add(a => a.Items, new RibbonTabItem[]
            {
                new RibbonTabItem()
                {
                    Text = "test1",
                    Items = new RibbonTabItem[]
                    {
                        new RibbonTabItem()
                        {
                            Text = "Item"
                        }
                    }
                },
                new RibbonTabItem()
                {
                    Text = "test2",
                    Items = new RibbonTabItem[]
                    {
                        new RibbonTabItem()
                        {
                            Text = "Item"
                        }
                    }
                }
            });
        });

        cut.InvokeAsync(() =>
        {
            var item = cut.Find(".tabs-item");
            item.Click();
        });

        var nav = Context.Services.GetRequiredService<FakeNavigationManager>();
        Assert.Equal("http://localhost/#test1-anchor", nav.Uri);

        nav.NavigateTo("http://localhost/#test2-anchor");
        cut.SetParametersAndRender();
        var item = cut.FindComponents<TabItem>();
        Assert.True(item[1].Instance.IsActive);

        cut.SetParametersAndRender(pb =>
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
        cut.SetParametersAndRender();
        item = cut.FindComponents<TabItem>();
        Assert.True(item[1].Instance.IsActive);
    }
}
