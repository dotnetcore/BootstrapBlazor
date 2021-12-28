// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using System.Globalization;
using UnitTest.Core;
using Xunit;

namespace UnitTest.Localization;

public class EnumExtensionsTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ToDisplayName_Ok()
    {
        var dn = EnumEducation.Middel.ToDisplayName();
        Assert.Equal("中学", dn);

        CultureInfo.CurrentUICulture = new CultureInfo("en-US");
        dn = EnumEducation.Middel.ToDisplayName();
        Assert.Equal(EnumEducation.Middel.ToString(), dn);
    }
}
