// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcUa;
using Opc.Ua;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// OpcUa 示例
/// </summary>
public partial class OpcUa : ComponentBase
{
    [Inject]
    [NotNull]
    private IOpcUaServer? OpcUaServer { get; set; }

    private string? _endpoint = "opc.tcp://localhost:49320";

    private string? _nodeId = "ns=2;s=Channel1.Device1.Tag1";

    private string? _value;

    private IOpcUaSubscription? _subscription;

    private List<TreeViewItem<OpcUaBrowseElement>> _roots = [];

    private async Task OnConnect()
    {
        if (!string.IsNullOrEmpty(_endpoint))
        {
            await OpcUaServer.ConnectAsync(_endpoint);
        }
    }

    private async Task OnDisconnect()
    {
        await OnCancelSubscription();
        await OpcUaServer.DisconnectAsync();
    }

    private async Task OnRead()
    {
        if (!string.IsNullOrEmpty(_nodeId))
        {
            var items = await OpcUaServer.ReadAsync([_nodeId]);
            _value = items.FirstOrDefault()?.Value?.ToString();
        }
    }

    private async Task OnCreateSubscription()
    {
        if (!string.IsNullOrEmpty(_nodeId))
        {
            _subscription = await OpcUaServer.CreateSubscriptionAsync("Subscription1");
            _subscription.DataChanged = UpdateValues;
            await _subscription.AddItemsAsync([_nodeId]);
        }
    }

    private async Task OnCancelSubscription()
    {
        if (_subscription != null)
        {
            _subscription.DataChanged = null;
            await OpcUaServer.CancelSubscriptionAsync(_subscription);
            _subscription = null;
        }
    }

    private void UpdateValues(IReadOnlyList<OpcUaReadItem> items)
    {
        _ = InvokeAsync(() =>
        {
            _value = items.FirstOrDefault()?.Value?.ToString();
            StateHasChanged();
        });
    }

    private async Task OnBrowse()
    {
        var elements = await OpcUaServer.BrowseAsync(ObjectIds.ObjectsFolder.ToString());
        _roots = [.. elements.Select(CreateTreeItem)];
    }

    private async Task<IEnumerable<TreeViewItem<OpcUaBrowseElement>>> OnExpandNodeAsync(TreeViewItem<OpcUaBrowseElement> element)
    {
        var children = await OpcUaServer.BrowseAsync(element.Value.NodeId);
        var items = children.Select(CreateTreeItem).ToList();
        if (items.Count == 0)
        {
            element.HasChildren = false;
        }
        return items;
    }

    private static TreeViewItem<OpcUaBrowseElement> CreateTreeItem(OpcUaBrowseElement element) => new(element)
    {
        Text = element.DisplayName,
        HasChildren = element.NodeClass != NodeClass.Variable,
        Icon = element.NodeClass == NodeClass.Variable ? "fa-solid fa-fw fa-wrench" : "fa-solid fa-fw fa-cube"
    };
}
