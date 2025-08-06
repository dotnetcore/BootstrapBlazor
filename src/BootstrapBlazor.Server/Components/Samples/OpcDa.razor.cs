// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcDa;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// OpcDa 示例
/// </summary>
public partial class OpcDa : ComponentBase
{
    [Inject]
    [NotNull]
    private IOpcDaServer? OpcDaServer { get; set; }

    private string? _serverName = "opcda://localhost/Kepware.KEPServerEX.V6/Mock";

    private const string Tag1 = "Channel1.Device1.Tag1";
    private const string Tag2 = "Channel1.Device1.Tag2";
    private const string Tag3 = "Channel1.Device1.Tag3";
    private const string Tag4 = "Channel1.Device1.Tag4";

    private string? _tagValue1;
    private string? _tagValue2;
    private string? _tagValue3;
    private string? _tagValue4;

    private IOpcSubscription? _subscription;

    private bool _subscribed;

    private void OnConnect()
    {
        if (!string.IsNullOrEmpty(_serverName))
        {
            OpcDaServer.Connect(_serverName);
        }
    }

    private void OnDisConnect()
    {
        OnCancelSubscription();
        OpcDaServer.Disconnect();
    }

    private void OnRead()
    {
        var values = OpcDaServer.Read(Tag1, Tag2);
        _tagValue1 = values.ElementAt(0).Value?.ToString();

        var v = (int)values.ElementAt(1).Value! / 100d;
        _tagValue2 = v.ToString(CultureInfo.InvariantCulture);
    }

    private void OnCreateSubscription()
    {
        _subscribed = true;
        _subscription = OpcDaServer.CreateSubscription("Subscription1", 1000, true);
        _subscription.DataChanged += UpdateValues;
        _subscription.AddItems([Tag3, Tag4]);
    }

    private void OnCancelSubscription()
    {
        _subscribed = false;
        if (_subscription != null)
        {
            _subscription.DataChanged -= UpdateValues;
            OpcDaServer.CancelSubscription(_subscription);
        }
    }

    private void UpdateValues(List<OpcReadItem> items)
    {
        _tagValue3 = items[0].Value?.ToString();
        var v = (int)items[1].Value! / 100d;
        _tagValue4 = v.ToString(CultureInfo.InvariantCulture);

        InvokeAsync(StateHasChanged);
    }
}

