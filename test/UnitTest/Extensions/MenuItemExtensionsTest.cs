// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class MenuItemExtensionsTest
{
    [Fact]
    public void CascadingSetActive_Ok()
    {

    }

    [Fact]
    public void GetAllItems_Ok()
    {
        var menus = new List<MenuItem>()
        {
            new()
            {
                Text = "1",
                Items = new List<MenuItem>()
                {
                    new() {
                        Text = "11",
                        Items = new List<MenuItem>()
                        {
                            new()
                            {
                                Text = "111"
                            }
                        }
                    }
                }
            },
            new()
            {
                Text = "2",
                Items = new List<MenuItem>()
                {
                    new() {
                        Text = "21",
                        Items = new List<MenuItem>()
                        {
                            new()
                            {
                                Text = "211"
                            }
                        }
                    }
                }
            }
        };

        var items = menus.GetAllItems();
        Assert.NotNull(items);
        Assert.Equal(6, items.Count());
    }

    [Fact]
    public void GetAllSubItems_Null()
    {
        List<MenuItem>? menus = null;
        var items = menus.GetAllSubItems();
        Assert.NotNull(items);
        Assert.Empty(items);
    }
}
