// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">IP 定位示例。</para>
/// <para lang="en">IP locator sample.</para>
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
    private ISampleLocatorService? LocatorService { get; set; }

    [Inject]
    [NotNull]
    private ILogger<Locators>? Logger { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    private string? Ip { get; set; }

    private string? Location { get; set; }

    private string ProviderName { get; set; } = nameof(BaiduIpLocatorProvider);

    private List<SelectedItem> _providers = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        try
        {
            _providers = (await LocatorService.GetProvidersAsync()).Select(name => new SelectedItem(name, name)).ToList();
            if (_providers.Count > 0 && !_providers.Any(provider => provider.Value == ProviderName))
            {
                ProviderName = _providers[0].Value;
            }
        }
        catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or OperationCanceledException)
        {
            Logger.LogError(ex, "Failed to load locator providers.");
            await ToastService.Error(Localizer["LocatorsTitle"], Localizer["RequestFailed"]);
        }
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
        if (!IPAddress.TryParse(Ip, out _) || !_providers.Any(provider => provider.Value == ProviderName))
        {
            await ToastService.Information(Localizer["LocatorsTitle"], Localizer["InvalidQuery"]);
            return;
        }
        Location = null;
        try
        {
            var result = await LocatorService.LocateAsync(ProviderName, Ip);
            Location = string.IsNullOrEmpty(result.ErrorMessage) ? result.Location : result.ErrorMessage;
        }
        catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or OperationCanceledException)
        {
            Logger.LogError(ex, "IP location sample request failed.");
            await ToastService.Error(Localizer["LocatorsTitle"], Localizer["RequestFailed"]);
        }
    }

    private Task OnProviderNameChanged(string v)
    {
        ProviderName = v;
        return Task.CompletedTask;
    }
}
