// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace BootstrapBlazor.Components;

class DefaultJSVersionService : IVersionService
{
    private string? Version { get; set; }

    private string? ConfigVersion { get; set; }

    public DefaultJSVersionService(IOptions<BootstrapBlazorOptions> options)
    {
        ConfigVersion = options.Value.JSModuleVersion;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public string GetVersion()
    {
        Version ??= ConfigVersion ?? GetVersionImpl();
        return Version;

        [ExcludeFromCodeCoverage]
        string GetVersionImpl()
        {
            string? ver = null;
            try
            {
                if (OperatingSystem.IsBrowser())
                {
                    ver = typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString();
                }
                else
                {
                    if (string.IsNullOrEmpty(typeof(BootstrapComponentBase).Assembly.Location))
                    {
                        // 发布单文件
                        ver = typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString();
                    }
                    else
                    {
                        ver = FileVersionInfo.GetVersionInfo(typeof(BootstrapComponentBase).Assembly.Location).ProductVersion;
                    }
                }
            }
            catch { }
            ver = ver ?? "7.0.0.0";
            ver = ver.Substring(0, ver.LastIndexOf("."));
            return ver;
        }
    }
}
