// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Tutorials;

/// <summary>
/// Online sheet sample code
/// </summary>
public partial class OnlineSheet : IDisposable
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

    private async Task OnReadyAsync()
    {
        _inited = true;
        await ToastService.Information(Localizer["ToastOnReadyTitle"], Localizer["ToastOnReadyContent"]);
        DispatchService.Subscribe(Dispatch);
    }

    private async Task Dispatch(DispatchEntry<Contributor> entry)
    {
        if (!_inited)
        {
            return;
        }

        if (entry.Entry != null)
        {
            await ToastService.Show(new ToastOption()
            {
                Title = "Dispatch 服务测试",
                ChildContent = BootstrapDynamicComponent.CreateComponent<OnlineContributor>(new Dictionary<string, object?>()
                {
                    { "Contributor", entry.Entry }
                }).Render(),
                Category = ToastCategory.Information,
                Delay = 3000,
                ForceDelay = true
            });

            DispatchService.UnSubscribe(Dispatch);

            await _sheetExcel.PushDataAsync(entry.Entry.Data);
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            DispatchService.UnSubscribe(Dispatch);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

