// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
                    new MenuItem() {
                        Text = "11",
                        Items = new List<MenuItem>()
                        {
                            new MenuItem()
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
                    new MenuItem() {
                        Text = "21",
                        Items = new List<MenuItem>()
                        {
                            new MenuItem()
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
