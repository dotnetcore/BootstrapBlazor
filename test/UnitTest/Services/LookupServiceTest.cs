// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class LookupServiceTest : MessageTestBase
{
    [Fact]
    public void LookupService_Null()
    {
        var service = Context.Services.GetRequiredService<ILookupService>();
        Assert.Null(service.GetItemsByKey(""));
    }
}
