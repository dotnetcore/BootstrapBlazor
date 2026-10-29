// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// Uses same-origin Server APIs for WebAssembly speech samples.
/// </summary>
public sealed class HttpSampleSpeechService(HttpClient client) : ISampleSpeechService
{
    /// <inheritdoc />
    public async Task<SampleSpeechRecognitionResult> RecognizeAsync(byte[] audio, CancellationToken cancellationToken = default)
    {
        using var content = new ByteArrayContent(audio);
        content.Headers.ContentType = new("application/octet-stream");
        using var response = await client.PostAsync("api/speech/recognize", content, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<SampleSpeechRecognitionResult>(cancellationToken)
            ?? throw new InvalidOperationException("The server did not return speech recognition results.");
    }

    /// <inheritdoc />
    public async Task<byte[]> SynthesizeAsync(string text, CancellationToken cancellationToken = default)
    {
        using var response = await client.PostAsJsonAsync("api/speech/synthesize", new SampleSpeechSynthesisRequest { Text = text }, cancellationToken);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsByteArrayAsync(cancellationToken);
    }
}

/// <summary>
/// Request for speech synthesis.
/// </summary>
public sealed class SampleSpeechSynthesisRequest
{
    /// <summary>
    /// Gets or sets the text to synthesize.
    /// </summary>
    public string Text { get; set; } = "";
}
