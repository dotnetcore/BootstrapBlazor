// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Options;
using System.Globalization;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// Home 模板
/// </summary>
public partial class HomeLayout
{
    private static string Version => Environment.Version.ToString();

    [NotNull]
    private string? OS { get; set; }

    private string? Runtime { get; set; }

    private string SelectedCulture { get; set; } = CultureInfo.CurrentUICulture.Name;

    private CancellationTokenSource DisposeTokenSource { get; } = new();

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? BootstrapBlazorOptions { get; set; }

    private ConnectionHubOptions _options = default!;

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    protected override void OnInitialized()
    {
        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            OS = "Windows";
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.OSX))
        {
            OS = "OSX";
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
        {
            OS = "Linux";
        }
        else
        {
            OS = "Unknown";
        }

        _options = BootstrapBlazorOptions.Value.ConnectionHubOptions;
        UpdateRuntime();
    }

    /// <summary>
    /// <inheritdoc />
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            _ = Task.Run(async () =>
            {
                while (!DisposeTokenSource.IsCancellationRequested)
                {
                    try
                    {
                        await Task.Delay(1000, DisposeTokenSource.Token);
                    }
                    catch (TaskCanceledException)
                    {

                    }
                    if (!DisposeTokenSource.IsCancellationRequested)
                    {
                        UpdateRuntime();
                        await InvokeAsync(StateHasChanged);
                    }
                }
            });
        }
    }

    private void UpdateRuntime()
    {
        var ts = DateTimeOffset.Now - Cache.GetStartTime();
        Runtime = ts.ToString("dd\\.hh\\:mm\\:ss");
    }

    private Task SetLang(string cultureName)
    {
        // 使用 api 方式 适用于 Server-Side 模式
        if (SelectedCulture != cultureName)
        {
            var uri = new Uri(NavigationManager.Uri).GetComponents(UriComponents.PathAndQuery, UriFormat.Unescaped);
            var query = $"?culture={Uri.EscapeDataString(cultureName)}&redirectUri={Uri.EscapeDataString(uri)}";

            // use a path that matches your culture redirect controller from the previous steps
            NavigationManager.NavigateTo("/Culture/SetCulture" + query, forceLoad: true);
        }

        return Task.CompletedTask;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            DisposeTokenSource.Cancel();
            DisposeTokenSource.Dispose();
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
