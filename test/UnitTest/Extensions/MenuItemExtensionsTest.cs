// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Extensions;

public class MenuItemExtensionsTest
{
    [Fact]
    public void CascadingSetActive_Ok()
    {
        var menu = new MenuItem()
        {
            Parent = new MenuItem()
            {
                ParentId = "0"
            }
        };
        menu.CascadingSetActive(true);

        Assert.True(menu.Parent.IsActive);
        Assert.False(menu.Parent.IsCollapsed);
    }
}
