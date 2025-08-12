// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

static class MockOnlineContributor
{
    public static void Trigger(IDispatchService<Contributor> dispatchService)
    {
        dispatchService.Dispatch(new DispatchEntry<Contributor>()
        {
            Name = "OnlineSheet-Demo",
            Entry = new Contributor()
            {
                Name = "Argo Zhang",
                Avatar = "/images/Argo-C.png",
                Description = "正在更新单元格 A8",
                Data = new UniverSheetData()
                {
                    CommandName = "UpdateRange",
                    Data = new
                    {
                        Range = "A8",
                        Value = $"{DateTime.Now: yyyy-MM-dd HH:mm:ss} Argo 更新此单元格"
                    }
                }
            }
        });
    }
}
