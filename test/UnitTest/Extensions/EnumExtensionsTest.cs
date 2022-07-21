// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;

namespace UnitTest.Extensions;

public class EnumExtensionsTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ToDisplayName_Ok()
    {
        var dn = EnumEducation.Middle.ToDisplayName();
        Assert.Equal("中学", dn);
    }

    [Fact]
    public void ToDescriptionString()
    {
        Type? type = null;
        var actual = type.ToDescriptionString(nameof(SortOrder.Desc));
        Assert.Equal("", actual);

        type = typeof(EnumEducation);
        actual = type.ToDescriptionString(null);
        Assert.Equal("", actual);
    }

    [Fact]
    public void ToSelectList_Ok()
    {
        var type = typeof(EnumEducation);
        var ret = type.ToSelectList();

        Assert.Equal(2, ret.Count);
        Assert.Equal("小学", ret[0].Text);
        Assert.Equal("中学", ret[1].Text);

        ret = type.ToSelectList(new SelectedItem("", "全部"));
        Assert.Equal(3, ret.Count);
        Assert.Equal("小学", ret[1].Text);
        Assert.Equal("中学", ret[2].Text);
    }
}
