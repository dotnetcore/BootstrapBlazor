// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Extensions;

internal static class IpExtensions
{
    public static string MaskIpString(this string ip)
    {
        var ret = ip;
        if (!string.IsNullOrEmpty(ip))
        {
            var segments = ip.Split('.');
            segments[^1] = "###";
            ret = string.Join('.', segments);
        }
        return ret;
    }
}
