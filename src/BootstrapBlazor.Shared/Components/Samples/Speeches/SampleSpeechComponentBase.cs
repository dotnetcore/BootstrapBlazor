// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Components.Samples.Speeches;

/// <summary>
/// Provides shared browser interactions for Baidu Speech samples.
/// </summary>
public abstract class SampleSpeechComponentBase : BootstrapComponentBase, IAsyncDisposable
{
    /// <summary>
    /// Gets the sample speech API.
    /// </summary>
    [Inject, NotNull]
    protected ISampleSpeechService? SpeechService { get; set; }

    [Inject, NotNull]
    private ILogger<SampleSpeechComponentBase>? Logger { get; set; }

    private IJSObjectReference? _recognizerModule;
    private IJSObjectReference? _synthesizerModule;
    private DotNetObjectReference<SampleSpeechComponentBase>? _self;
    private Func<RecognizerStatus, string?, Task>? _recognizerCallback;
    private Func<SynthesizerStatus, Task>? _synthesizerCallback;
    private int _recognizerInterval;

    /// <summary>
    /// Starts browser audio recording and sends the result to the recognition service.
    /// </summary>
    protected async Task RecognizeOnceAsync(Func<RecognizerStatus, string?, Task> callback, int interval)
    {
        _recognizerCallback = callback;
        _recognizerInterval = interval;
        _self ??= DotNetObjectReference.Create(this);
        try
        {
            _recognizerModule ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "/_content/BootstrapBlazor.BaiduSpeech/js/recognizer.js");
            await _recognizerModule.InvokeVoidAsync("bb_baidu_speech_recognizeOnce", _self,
                nameof(OnRecognizerCallback), interval);
        }
        catch (JSException ex)
        {
            Logger.LogError(ex, "Could not start speech recording.");
            throw;
        }
    }

    /// <summary>
    /// Stops browser audio recording.
    /// </summary>
    protected async Task CloseRecognitionAsync()
    {
        if (_recognizerModule != null && _self != null && _recognizerCallback != null)
        {
            try
            {
                await _recognizerModule.InvokeVoidAsync("bb_baidu_speech_close", _self,
                    nameof(OnRecognizerCallback), _recognizerInterval);
            }
            catch (JSException ex)
            {
                Logger.LogError(ex, "Could not stop speech recording.");
                throw;
            }
        }
    }

    /// <summary>
    /// Synthesizes text and plays the returned audio.
    /// </summary>
    protected async Task SynthesizeOnceAsync(string text, Func<SynthesizerStatus, Task> callback)
    {
        _synthesizerCallback = callback;
        _self ??= DotNetObjectReference.Create(this);
        var audio = await SpeechService.SynthesizeAsync(text);
        try
        {
            _synthesizerModule ??= await JSRuntime.InvokeAsync<IJSObjectReference>("import",
                "/_content/BootstrapBlazor.BaiduSpeech/js/synthesizer.js");
            await _synthesizerModule.InvokeVoidAsync("bb_baidu_speech_synthesizerOnce", _self,
                nameof(OnSynthesizerCallback), audio);
        }
        catch (JSException ex)
        {
            Logger.LogError(ex, "Could not play synthesized speech.");
            throw;
        }
    }

    /// <summary>
    /// Stops browser audio playback.
    /// </summary>
    protected async Task CloseSynthesisAsync()
    {
        if (_synthesizerModule != null && _self != null && _synthesizerCallback != null)
        {
            try
            {
                await _synthesizerModule.InvokeVoidAsync("bb_baidu_close_synthesizer", _self,
                    nameof(OnSynthesizerCallback));
            }
            catch (JSException ex)
            {
                Logger.LogError(ex, "Could not stop synthesized speech.");
                throw;
            }
        }
    }

    /// <summary>
    /// Receives recording status and audio from the Baidu Speech browser module.
    /// </summary>
    [JSInvokable]
    public async Task OnRecognizerCallback(RecognizerStatus status, byte[]? audio)
    {
        string? result = null;
        if (status == RecognizerStatus.Finished)
        {
            try
            {
                if (audio == null || audio.Length == 0)
                {
                    status = RecognizerStatus.Error;
                    result = "No audio was recorded.";
                }
                else
                {
                    var response = await SpeechService.RecognizeAsync(audio);
                    if (response.ErrorCode == 0)
                    {
                        result = response.Text;
                    }
                    else
                    {
                        status = RecognizerStatus.Error;
                        result = $"{response.ErrorCode}: {response.ErrorMessage}";
                    }
                }
            }
            catch (Exception ex) when (ex is HttpRequestException or InvalidOperationException or ArgumentException)
            {
                Logger.LogError(ex, "Speech recognition sample request failed.");
                status = RecognizerStatus.Error;
                result = "Speech recognition request failed.";
            }
        }

        if (_recognizerCallback != null)
        {
            await _recognizerCallback(status, result);
        }
    }

    /// <summary>
    /// Receives playback status from the Baidu Speech browser module.
    /// </summary>
    [JSInvokable]
    public Task OnSynthesizerCallback(SynthesizerStatus status) =>
        _synthesizerCallback?.Invoke(status) ?? Task.CompletedTask;

    /// <summary>
    /// Releases the imported JavaScript modules and callback reference.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        _self?.Dispose();
        _self = null;

        if (_recognizerModule != null)
        {
            await _recognizerModule.DisposeAsync();
            _recognizerModule = null;
        }

        if (_synthesizerModule != null)
        {
            await _synthesizerModule.DisposeAsync();
            _synthesizerModule = null;
        }

        GC.SuppressFinalize(this);
    }
}
