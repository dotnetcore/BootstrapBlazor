// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics;

namespace BootstrapBlazor.Components;

class DefaultJSVersionService : IVersionService
{
    private string? Version { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string GetVersion()
    {
        Version ??= GetVersionImpl();
        return Version;

        [ExcludeFromCodeCoverage]
        string GetVersionImpl()
        {
            string? ver;
            if (OperatingSystem.IsBrowser())
            {
                ver = typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString();
            }
            else
            {
                ver = FileVersionInfo.GetVersionInfo(typeof(BootstrapComponentBase).Assembly.Location).ProductVersion;
            }
            return ver ?? "7.0.0.0";
        }
    }
}
