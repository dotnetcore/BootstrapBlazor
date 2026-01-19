// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Lights
/// </summary>
public partial class Lights
{
    [NotNull]
    private string? Title { get; set; }

    private void OnSetTitle()
    {
        Title = Localizer["TooltipText"];
    }

    private void OnRemoveTitle()
    {
        Title = "";
    }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Title = Localizer["TooltipText"];
    }

    private Color Color { get; set; } = Color.Primary;

    private CancellationTokenSource UpdateColorTokenSource { get; } = new CancellationTokenSource();

    /// <summary>
    /// OnAfterRender
    /// </summary>
    /// <param name = "firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        try
        {
            await Task.Delay(2000, UpdateColorTokenSource.Token);
            Color = Color switch
            {
                Color.Primary => Color.Success,
                Color.Success => Color.Info,
                Color.Info => Color.Warning,
                Color.Warning => Color.Danger,
                Color.Danger => Color.Secondary,
                _ => Color.Primary
            };
            StateHasChanged();
        }
        catch (TaskCanceledException)
        {
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            UpdateColorTokenSource.Cancel();
            UpdateColorTokenSource.Dispose();
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
