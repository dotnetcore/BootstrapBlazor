// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcUa;
using Opc.Ua;

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">OpcUa 示例。</para>
/// <para lang="en">OpcUa sample.</para>
/// </summary>
public partial class OpcUa : ComponentBase, IAsyncDisposable
{
    [Inject]
    [NotNull]
    private IStringLocalizer<OpcUa>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IOpcUaClient? OpcUaClient { get; set; }

    private string? _endpoint = "opc.tcp://localhost:49320";

    private string? _nodeId = "ns=2;s=Channel1.Device1.Tag1";

    private string? _value;

    private IOpcUaSubscription? _subscription;
    private bool _disposed;

    private List<TreeViewItem<OpcUaBrowseElement>> _roots = [];

    private async Task OnConnect()
    {
        if (!string.IsNullOrEmpty(_endpoint))
        {
            await OpcUaClient.ConnectAsync(_endpoint);
        }
    }

    private async Task OnDisconnect()
    {
        await OnCancelSubscription();
        await OpcUaClient.DisconnectAsync();
    }

    private async Task OnRead()
    {
        if (!string.IsNullOrEmpty(_nodeId))
        {
            var items = await OpcUaClient.ReadAsync([_nodeId]);
            _value = items.FirstOrDefault()?.Value?.ToString();
        }
    }

    private async Task OnCreateSubscription()
    {
        if (!string.IsNullOrEmpty(_nodeId))
        {
            var subscription = await OpcUaClient.CreateSubscriptionAsync("Subscription1");
            if (_disposed)
            {
                await OpcUaClient.CancelSubscriptionAsync(subscription);
                return;
            }
            _subscription = subscription;
            subscription.DataChanged = UpdateValues;
            await subscription.AddItemsAsync([_nodeId]);
        }
    }

    private async Task OnCancelSubscription()
    {
        var subscription = _subscription;
        _subscription = null;
        if (subscription != null)
        {
            subscription.DataChanged = null;
            await OpcUaClient.CancelSubscriptionAsync(subscription);
        }
    }

    private void UpdateValues(IReadOnlyList<OpcUaReadItem> items)
    {
        _ = InvokeAsync(() =>
        {
            if (_disposed || _subscription == null)
            {
                return;
            }
            _value = items.FirstOrDefault()?.Value?.ToString();
            StateHasChanged();
        });
    }

    private async Task OnBrowse()
    {
        var elements = await OpcUaClient.BrowseAsync(ObjectIds.ObjectsFolder.ToString());
        _roots = [.. elements.Select(CreateTreeItem)];
    }

    private async Task<IEnumerable<TreeViewItem<OpcUaBrowseElement>>> OnExpandNodeAsync(TreeViewItem<OpcUaBrowseElement> element)
    {
        var children = await OpcUaClient.BrowseAsync(element.Value.NodeId);
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

    /// <inheritdoc />
    public async ValueTask DisposeAsync()
    {
        _disposed = true;
        await OnCancelSubscription();
        GC.SuppressFinalize(this);
    }
}
