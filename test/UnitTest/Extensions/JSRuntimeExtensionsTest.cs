// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Extensions;

public class JSRuntimeExtensionsTest
{
    [Fact]
    public void Parameter_Ok()
    {
        var actual = Convert("", null, 1);
        Assert.Equal("", actual[0]);
        Assert.Null(actual[1]);
        Assert.Equal(1, actual[2]);

        object?[] Convert(params object?[]? args)
        {
            var paras = new List<object?>();
            if (args != null)
            {
                paras.AddRange(args);
            }
            return paras.ToArray();
        }
    }
}
