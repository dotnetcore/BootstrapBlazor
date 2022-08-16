// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
