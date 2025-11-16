// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Utils;

public class OffsetTest
{
    [Fact]
    public void Offset_Ok()
    {
        var offset = new Offset()
        {
            Top = 100,
            Left = 100,
            Height = 100,
            Width = 100
        };
        Assert.Equal(100, offset.Top);
        Assert.Equal(100, offset.Left);
        Assert.Equal(100, offset.Height);
        Assert.Equal(100, offset.Width);
    }
}
