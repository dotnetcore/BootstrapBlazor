// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Baidu.Aip.Speech;
using BootstrapBlazor.Components;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// Keeps Baidu Speech credentials and SDK calls on the Server.
/// </summary>
public sealed class SampleSpeechService(
    IOptionsMonitor<BaiduSpeechOption> options) : ISampleSpeechService
{
    /// <inheritdoc />
    public async Task<SampleSpeechRecognitionResult> RecognizeAsync(byte[] audio, CancellationToken cancellationToken = default)
    {
        if (audio.Length is < 44 or > ISampleSpeechService.MaxAudioSize ||
            !audio.AsSpan(0, 4).SequenceEqual("RIFF"u8) ||
            !audio.AsSpan(8, 4).SequenceEqual("WAVE"u8))
        {
            throw new ArgumentException("A valid WAV recording up to 4 MB is required.", nameof(audio));
        }

        var option = options.CurrentValue;
        var client = new Asr(option.AppId, option.ApiKey, option.Secret);
        var response = await Task.Run(() => client.Recognize(audio, "wav", 16000, null), cancellationToken);
        var errorCode = response.Value<int?>("err_no") ?? throw new InvalidOperationException("The speech provider returned an invalid response.");
        var result = (string?)response["result"]?.FirstOrDefault();
        return new SampleSpeechRecognitionResult
        {
            ErrorCode = errorCode,
            ErrorMessage = errorCode == 0 ? null : response.Value<string>("err_msg") ?? "Baidu Speech recognition failed.",
            Text = result
        };
    }

    /// <inheritdoc />
    public async Task<byte[]> SynthesizeAsync(string text, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length > ISampleSpeechService.MaxTextLength)
        {
            throw new ArgumentException("Text must contain between 1 and 1024 characters.", nameof(text));
        }

        var option = options.CurrentValue;
        var client = new Tts(option.ApiKey, option.Secret);
        var response = await Task.Run(() => client.Synthesis(text, new Dictionary<string, object>
        {
            ["spd"] = option.Speed,
            ["aue"] = 3
        }), cancellationToken);
        if (!response.Success || response.Data is not { Length: > 0 })
        {
            throw new InvalidOperationException(response.ErrorMsg ?? "The speech provider returned no audio.");
        }
        return response.Data;
    }
}
