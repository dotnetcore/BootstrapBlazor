// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// <para lang="zh">ButtonUpload 按钮上传示例。</para>
/// <para lang="en">ButtonUpload component examples.</para>
/// </summary>
public partial class UploadButtons : IDisposable
{
    [Inject]
    [NotNull]
    private IStringLocalizer<UploadButtons>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private ToastService? ToastService { get; set; }

    [Inject]
    [NotNull]
    private IEditorUploadService? UploadService { get; set; }

    [Inject]
    [NotNull]
    private NavigationManager? NavigationManager { get; set; }

    private static readonly Random Random = new();
    private CancellationTokenSource? _token;

    private bool _isMultiple = true;
    private bool _showProgress = true;
    private bool _showUploadFileList = true;
    private bool _showDownloadButton = true;
    private bool _isDirectory = false;
    private bool _isDisabled = false;

    private async Task OnClickToUpload(UploadFile file)
    {
        // 示例代码，模拟 80% 几率保存成功
        // Simulate an 80% chance of proceeding with the upload.
        var error = Random.Next(1, 100) > 80;
        if (error)
        {
            file.Code = 1;
            file.Error = Localizer["UploadsError"];
        }
        else
        {
            await SaveToFile(file);
        }
    }

    private async Task OnDownload(UploadFile item)
    {
        await ToastService.Success("文件下载", $"下载 {item.FileName} 成功");
    }

    private async Task<bool> OnDelete(UploadFile item)
    {
        await ToastService.Success("文件操作", $"删除文件 {item.FileName} 成功");
        return true;
    }

    private async Task SaveToFile(UploadFile file)
    {
        // Server 直接保存文件，WebAssembly 通过 Web API 保存并返回预览地址。
        // Server saves directly; WebAssembly uses the Web API to save and obtain a preview URL.
        _token ??= new CancellationTokenSource();
        var token = _token.Token;
        try
        {
            var url = await UploadService.UploadAsync(file, token);
            // 保存成功后设置服务器生成的文件名与预览地址。
            // Set the server-generated file name and preview URL after a successful save.
            file.FileName = Path.GetFileName(url);
            file.PrevUrl = NavigationManager.ToAbsoluteUri(url).AbsoluteUri;
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested)
        {
            file.Code = 1;
            file.Error = Localizer["UploadCancelled"];
        }
        catch (Exception ex)
        {
            file.Code = 1;
            file.Error = $"{Localizer["UploadsSaveFileError"]}: {ex.Message}";
            await ToastService.Error(Localizer["UploadFile"], file.Error);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        _token?.Cancel();
        _token?.Dispose();
        _token = null;
        GC.SuppressFinalize(this);
    }
}
