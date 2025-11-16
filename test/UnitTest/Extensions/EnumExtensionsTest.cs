// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

        actual = type.ToDescriptionString("Test");
        Assert.Equal("Test", actual);
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
