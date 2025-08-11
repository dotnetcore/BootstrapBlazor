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
    private IOpcDaServer? OpcDaServer { get; set; }

    private string? _serverName = "opcda://localhost/Kepware.KEPServerEX.V6";

    private const string Tag1 = "Channel1.Device1.Tag1";
    private const string Tag2 = "Channel1.Device1.Tag2";

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
        var items = OpcDaServer.Read(Tag1, Tag2);
        var value1 = items.FirstOrDefault(i => i.Name == Tag1).Value;
        if (value1 != null)
        {
            _tagValue1 = value1.ToString();
        }
        var value2 = items.FirstOrDefault(i => i.Name == Tag2).Value;
        if (value2 != null)
        {
            _tagValue2 = value2.ToString();
        }
    }

    private void OnCreateSubscription()
    {
        _subscribed = true;
        _subscription = OpcDaServer.CreateSubscription("Subscription1", 1000, true);
        _subscription.DataChanged += UpdateValues;
        _subscription.AddItems([Tag1, Tag2]);
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
        var value1 = items.Find(i => i.Name == Tag1).Value;
        if (value1 != null)
        {
            _tagValue3 = value1.ToString();
        }
        var value2 = items.Find(i => i.Name == Tag2).Value;
        if (value2 != null)
        {
            _tagValue4 = value2.ToString();
        }

        InvokeAsync(StateHasChanged);
    }

    private List<TreeViewItem<OpcBrowseElement>> _roots = [];

    private void OnBrowse()
    {
        var elements = OpcDaServer.Browse("", new OpcBrowseFilters(), out _);
        _roots = [.. elements.Select(element => new TreeViewItem<OpcBrowseElement>(element)
        {
            Text = element.Name,
            HasChildren = element.HasChildren,
            Icon = "fa-solid fa-fw fa-cubes"
        })];
    }

    private Task<IEnumerable<TreeViewItem<OpcBrowseElement>>> OnExpandNodeAsync(TreeViewItem<OpcBrowseElement> element)
    {
        var children = OpcDaServer.Browse(element.Value.ItemName, new OpcBrowseFilters(), out _);
        var items = children.Select(i => new TreeViewItem<OpcBrowseElement>(i)
        {
            Text = i.Name,
            HasChildren = i.HasChildren,
            Icon = i.HasChildren ? "fa-solid fa-fw fa-cube" : "fa-solid fa-fw fa-wrench"
        });
        if (!items.Any())
        {
            element.HasChildren = false;
        }
        return Task.FromResult(items);
    }
}
