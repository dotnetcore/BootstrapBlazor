// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Options;

public class PdfOptionsTest
{
    [Fact]
    public void Test_PdfOptions_Ok()
    {
        var options = new PdfOptions()
        {
            Scale = 1.0m,
            DisplayHeaderFooter = true,
            PrintBackground = true,
            Landscape = true,
            Format = PaperFormat.A1,
            MarginOptions = new MarginOptions()
            {
                Top = "10mm",
                Bottom = "10mm",
                Left = "10mm",
                Right = "10mm"
            }
        };
        Assert.Equal(1.0m, options.Scale);
        Assert.True(options.DisplayHeaderFooter);
        Assert.True(options.PrintBackground);
        Assert.True(options.Landscape);
        Assert.Equal(PaperFormat.A1, options.Format);
        Assert.Equal("10mm", options.MarginOptions.Top);
        Assert.Equal("10mm", options.MarginOptions.Bottom);
        Assert.Equal("10mm", options.MarginOptions.Left);
        Assert.Equal("10mm", options.MarginOptions.Right);
    }
}
