// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class LookupServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task LookupService_Null()
    {
        var service = Context.Services.GetRequiredService<ILookupService>();
        var data = await service.GetItemsAsync("", "");
        Assert.Null(data);
    }
}
