// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Diagnostics;

namespace BootstrapBlazor.Components;

class DefaultJSVersionService(IOptions<BootstrapBlazorOptions> options) : IVersionService
{
    private string? Version { get; set; }

    private string? ConfigVersion => options.Value.JSModuleVersion;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public string GetVersion()
    {
        Version ??= ConfigVersion ?? GetVersionFromAssembly();
        return Version;

        [ExcludeFromCodeCoverage]
        static string GetVersionFromAssembly()
        {
            string? ver = null;
            try
            {
                if (OperatingSystem.IsBrowser())
                {
                    ver = GetAssemblyVersion();
                }
                else
                {
                    ver = string.IsNullOrEmpty(typeof(BootstrapComponentBase).Assembly.Location)
                        ? GetAssemblyVersion()
                        : FileVersionInfo.GetVersionInfo(typeof(BootstrapComponentBase).Assembly.Location).ProductVersion;
                }
            }
            catch { }
            return FormatVersion();

            [ExcludeFromCodeCoverage]
            string? GetAssemblyVersion() => typeof(BootstrapComponentBase).Assembly.GetName().Version?.ToString(3);

            [ExcludeFromCodeCoverage]
            string FormatVersion()
            {
                if (!string.IsNullOrEmpty(ver))
                {
                    var index = ver.IndexOf('+');
                    if (index > 0)
                    {
                        ver = ver[..index];
                    }
                }
                return ver ?? "8.0.0.0";
            }
        }
    }
}
