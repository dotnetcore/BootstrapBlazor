// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials.OnlineSheet;

/// <summary>
/// Online sheet sample code
/// </summary>
public partial class OnlineSheet : IAsyncDisposable
{
    [Inject, NotNull]
    private IWebHostEnvironment? WebHost { get; set; }

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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        var reportFile = Path.Combine(WebHost.WebRootPath, "univer-sheet", "report.json");
        if (File.Exists(reportFile))
        {
            var sheetData = File.ReadAllText(reportFile);

            _data = new UniverSheetData()
            {
                WorkbookData = sheetData
            };
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (firstRender)
        {
            await Task.Delay(5000);
            await MockOnlineContributor.Trigger(DispatchService, Localizer);
        }
    }

    private async Task OnReadyAsync()
    {
        _inited = true;
        await ToastService.Information(Localizer["ToastOnReadyTitle"], Localizer["ToastOnReadyContent"]);
        await DispatchService.SubscribeAsync(Dispatch);
    }

    private async Task Dispatch(DispatchEntry<Contributor> entry)
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
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DispatchService.UnSubscribeAsync(Dispatch);
        GC.SuppressFinalize(this);
    }
}
