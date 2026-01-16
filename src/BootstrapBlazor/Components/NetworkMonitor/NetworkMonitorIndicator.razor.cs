// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Represents a network monitor indicator with customizable tooltip settings.
///</para>
/// <para lang="en">Represents a network monitor indicator with customizable tooltip settings.
///</para>
/// </summary>
/// <remarks>This component allows you to configure the text, placement, and trigger behavior of a tooltip that
/// appears when interacting with the network monitor indicator. The tooltip can be customized to provide additional
/// information to users.</remarks>
public partial class NetworkMonitorIndicator : IDisposable
{
    /// <summary>
    /// <para lang="zh">获得/设置 Popover 弹窗标题 默认为 null
    ///</para>
    /// <para lang="en">Gets or sets Popover 弹窗标题 Default is为 null
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Popover 显示位置 默认为 Top
    ///</para>
    /// <para lang="en">Gets or sets Popover display位置 Default is为 Top
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Placement PopoverPlacement { get; set; } = Placement.Top;

    /// <summary>
    /// <para lang="zh">获得/设置 Popover 触发方式 默认为 hover focus
    ///</para>
    /// <para lang="en">Gets or sets Popover 触发方式 Default is为 hover focus
    ///</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? Trigger { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<NetworkMonitorIndicator>? Localizer { get; set; }

    [Inject, NotNull]
    private INetworkMonitorService? NetworkMonitorService { get; set; }

    private NetworkMonitorState _state = new();
    private string _networkTypeString = "";
    private string _downlinkString = "";
    private string _rttString = "";

    private string? ClassString => CssBuilder.Default("bb-nt-indicator")
        .AddClass("bb-nt-indicator-4g", _state.NetworkType == "4g")
        .AddClass("bb-nt-indicator-3g", _state.NetworkType == "3g")
        .AddClass("bb-nt-indicator-2g", _state.NetworkType == "2g")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await NetworkMonitorService.RegisterStateChangedCallback(this, OnNetworkStateChanged);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Trigger ??= "hover focus";
        Title ??= Localizer["Title"];

        _networkTypeString = Localizer["NetworkType"];
        _downlinkString = Localizer["Downlink"];
        _rttString = Localizer["RTT"];
    }

    private async Task OnNetworkStateChanged(NetworkMonitorState state)
    {
        _state = state;
        await InvokeAsync(StateHasChanged);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            NetworkMonitorService.UnregisterStateChangedCallback(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
