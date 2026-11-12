// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.OnlineSheet;

static class MockOnlineContributor
{
    public static Task Trigger(IDispatchService<Contributor> dispatchService, string desc, string cellValue)
    {
        return dispatchService.DispatchAsync(new DispatchEntry<Contributor>()
        {
            Name = "OnlineSheet-Demo",
            Entry = new Contributor()
            {
                Name = "Argo Zhang",
                Avatar = "/images/Argo-C.png",
                Description = desc,
                Data = new UniverSheetData()
                {
                    CommandName = "UpdateRange",
                    Data = new
                    {
                        Range = "A8",
                        Value = cellValue
                    }
                }
            }
        });
    }
}
