// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcDa;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// OpcDa 示例
/// </summary>
public partial class OpcDa : ComponentBase
{
    [Inject]
    [NotNull]
    private IOpcServer? OpcServer { get; set; }

    private string? _serverName = "opcda://localhost/Kepware.KEPServerEX.V6/Mock";

    private const string Tag1 = "Channel1.Device1.Tag1";
    private const string Tag2 = "Channel1.Device1.Tag2";

    private string? _tagValue1;
    private string? _tagValue2;

    private void OnConnect()
    {
        if (!string.IsNullOrEmpty(_serverName))
        {
            OpcServer.Connect(_serverName);
        }
    }

    private void OnDisConnect()
    {
        OpcServer.Disconnect();
    }

    private void OnRead()
    {
        var values = OpcServer.Read(Tag1, Tag2);
        _tagValue1 = values.ElementAt(0).Value?.ToString();
        _tagValue2 = values.ElementAt(1).Value?.ToString();
    }
}

