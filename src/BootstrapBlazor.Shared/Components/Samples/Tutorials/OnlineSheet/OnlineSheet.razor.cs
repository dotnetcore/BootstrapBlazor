// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Tutorials.OnlineSheet;

/// <summary>
/// Online sheet sample code
/// </summary>
public partial class OnlineSheet : IAsyncDisposable
{
    [Inject, NotNull]
    private HttpClient? HttpClient { get; set; }

    [Inject, NotNull]
    private NavigationManager? NavigationManager { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    [Inject, NotNull]
    private IStringLocalizer<OnlineSheet>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IDispatchService<Contributor>? DispatchService { get; set; }

    [NotNull]
    private UniverSheet? _sheetExcel = null;

    private UniverSheetData? _data = null;

    private bool _inited = false;

    private readonly CancellationTokenSource _lifetime = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _data = new UniverSheetData()
        {
            WorkbookData = await HttpClient.GetStringAsync(NavigationManager.ToAbsoluteUri("univer-sheet/report.json"))
        };
    }

    private async Task OnReadyAsync()
    {
        if (_inited)
        {
            return;
        }

        _inited = true;
        var token = _lifetime.Token;
        try
        {
            await DispatchService.SubscribeAsync(Dispatch, token);
            await ToastService.Information(Localizer["ToastOnReadyTitle"], Localizer["ToastOnReadyContent"]);
            await Task.Delay(5000, token);
            await MockOnlineContributor.Trigger(DispatchService, Localizer, token);
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested)
        {
        }
    }

    private Task Dispatch(DispatchEntry<Contributor> entry) => InvokeAsync(async () =>
    {
        if (!_inited)
        {
            return;
        }

        if (entry is { Name: "OnlineSheet-Demo", Entry: not null })
        {
            await ToastService.Show(new ToastOption()
            {
                Title = Localizer["DispatchToastTitle"],
                ChildContent = BootstrapDynamicComponent.CreateComponent<OnlineContributor>(new Dictionary<string, object?>()
                {
                    { "Contributor", entry.Entry }
                }).Render(),
                Category = ToastCategory.Information,
                Delay = 3000,
                ForceDelay = true
            });

            await _sheetExcel.PushDataAsync(entry.Entry.Data);
        }
    });

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        _inited = false;
        _lifetime.Cancel();
        try
        {
            await DispatchService.UnSubscribeAsync(Dispatch);
        }
        finally
        {
            _lifetime.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}
