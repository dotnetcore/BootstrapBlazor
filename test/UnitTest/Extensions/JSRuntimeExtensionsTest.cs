// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
