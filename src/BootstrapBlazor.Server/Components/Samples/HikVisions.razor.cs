// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 海康威视网络摄像机组件
/// </summary>
public partial class HikVisions
{
    private HikVision _hikVision = default!;

    private async Task OnLogin()
    {
        await _hikVision.Login("47.121.113.151", 9980, "admin", "vhbn8888", LoginType.Http);
    }

    private async Task OnLogout()
    {
        await _hikVision.Logout();
    }

    private async Task OnStartRealPlay()
    {
        await _hikVision.StartRealPlay();
    }

    private async Task OnStopRealPlay()
    {
        await _hikVision.StopRealPlay();
    }
}
