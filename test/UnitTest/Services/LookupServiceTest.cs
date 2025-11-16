// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class LookupServiceTest
{
    [Fact]
    public void LookupService_Null()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddBootstrapBlazor();

        var provider = serviceCollection.BuildServiceProvider();
        var service = provider.GetRequiredService<ILookupService>();
        var data = service.GetItemsByKey("", "");
        Assert.Null(data);
    }
}
