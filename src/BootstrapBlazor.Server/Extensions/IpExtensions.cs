// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Extensions;

internal static class IpExtensions
{
    public static string MaskIpString(this string ip)
    {
        var ret = ip;
        if (!string.IsNullOrEmpty(ip))
        {
            var index = ip.LastIndexOf('.');
            if (index > -1)
            {
                var mask = ip[index..];
                ret = ip.Replace(mask, ".###");
            }
        }
        return ret;
    }
}
