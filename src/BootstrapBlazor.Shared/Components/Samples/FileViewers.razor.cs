// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">FileViewers 文件预览示例。</para>
/// <para lang="en">FileViewers file preview sample.</para>
/// </summary>
public partial class FileViewers : IDisposable
{
    [Inject, NotNull]
    private IStringLocalizer<FileViewers>? Localizer { get; set; }

    [Inject, NotNull]
    private ISampleFileViewerService? ViewerService { get; set; }

    [Inject, NotNull]
    private ILogger<FileViewers>? Logger { get; set; }

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private const string PreviewStyle = "border: none; width: 100%; height: 300px; padding: var(--bs-card-cap-padding-y) var(--bs-card-cap-padding-x); border: var(--bs-card-border-width) solid var(--bs-card-border-color);";
    private readonly CancellationTokenSource _lifetime = new();
    private readonly List<SelectedItem> Items = [new("sample.xlsx", "sample.xlsx"), new("sample.docx", "sample.docx")];
    private List<AttributeItem> _attributes = [];
    private string? WordHtml { get; set; }
    private string? ExcelHtml { get; set; }
    private string? ReloadHtml { get; set; }
    private string? Url { get; set; } = "sample.xlsx";
    private bool _isLoading = true;

    /// <summary>
    /// <para lang="zh">OnInitialized：加载示例预览和参数文档。</para>
    /// <para lang="en">OnInitialized: loads sample previews and parameter documentation.</para>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        try
        {
            WordHtml = await ViewerService.PreviewAsync("sample.docx", _lifetime.Token);
            ExcelHtml = await ViewerService.PreviewAsync("sample.xlsx", _lifetime.Token);
            ReloadHtml = ExcelHtml;
            _attributes = await ViewerService.GetAttributesAsync(_lifetime.Token);
        }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested)
        {
            Logger.LogDebug("File preview initialization was cancelled on navigation.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to initialize file previews.");
            await ToastService.Error(Localizer["Title"], Localizer["PreviewFailed"]);
        }
        finally
        {
            _isLoading = false;
        }
    }

    private async Task ChangeURL(SelectedItem e)
    {
        Url = e.Value;
        await Apply();
    }

    private async Task Apply()
    {
        if (_isLoading)
        {
            return;
        }
        if (Url is not ("sample.xlsx" or "sample.docx"))
        {
            await ToastService.Information(Localizer["Title"], Localizer["InvalidFile"]);
            return;
        }
        _isLoading = true;
        ReloadHtml = null;
        try
        {
            ReloadHtml = await ViewerService.PreviewAsync(Url, _lifetime.Token);
        }
        catch (OperationCanceledException) when (_lifetime.IsCancellationRequested)
        {
            Logger.LogDebug("File preview was cancelled on navigation.");
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Failed to reload the file preview.");
            await ToastService.Error(Localizer["Title"], Localizer["PreviewFailed"]);
        }
        finally
        {
            _isLoading = false;
        }
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _lifetime.Cancel();
        _lifetime.Dispose();
        GC.SuppressFinalize(this);
    }
}
