// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// WiFi content class
/// </summary>
public class WiFiContent
{
    /// <summary>
    /// Gets or sets the authentication type.
    /// </summary>
    public WiFiAuthentication Authentication { get; set; } = WiFiAuthentication.WPA;

    /// <summary>
    /// Gets or sets the SSID.
    /// </summary>
    [Required]
    public string? SSID { get; set; }

    /// <summary>
    /// Gets or sets the password.
    /// </summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return $"WIFI:S:{EscapeInput(SSID ?? "")};T:{Authentication};P:{Password};;";
    }

    private static string EscapeInput(string content)
    {
        char[] array = ['\\', ';', ',', ':'];
        foreach (char c in array)
        {
            content = content.Replace(c.ToString(), "\\" + c);
        }
        return content;
    }
}
