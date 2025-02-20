// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Locators
/// </summary>
public partial class Locators
{
    [Inject]
    [NotNull]
    IStringLocalizer<Locators>? Localizer { get; set; }

    [Inject]
    [NotNull]
    WebClientService? ClientService { get; set; }

    [Inject]
    [NotNull]
    IIpLocatorFactory? IpLocatorFactory { get; set; }

    [Inject]
    [NotNull]
    IEnumerable<IIpLocatorProvider>? IpLocatorProviders { get; set; }

    private string? Ip { get; set; }

    private string? Location { get; set; }

    private string ProviderName { get; set; } = nameof(BaiduIpLocatorProviderV2);

    [NotNull]
    private List<SelectedItem>? _providers = null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _providers = [.. IpLocatorProviders.Select(provider => new SelectedItem
        {
            Text = provider.GetType().Name,
            Value = provider.GetType().Name
        })];
    }

    /// <summary>
    /// OnAfterRenderAsync
    /// </summary>
    /// <param name = "firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            var clientInfo = await ClientService.GetClientInfo();
            Ip = clientInfo.Ip;
            StateHasChanged();
        }
    }

    private async Task OnClick()
    {
        if (!string.IsNullOrEmpty(Ip))
        {
            var provider = IpLocatorFactory.Create(ProviderName);
            Location = await provider.Locate(Ip);
        }
    }

    private Task OnProviderNameChanged(string v)
    {
        ProviderName = v;
        return Task.CompletedTask;
    }
}
