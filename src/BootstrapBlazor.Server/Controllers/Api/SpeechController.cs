// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// Provides same-origin APIs for WebAssembly Baidu Speech samples.
/// </summary>
[ApiController]
public sealed class SpeechController(ISampleSpeechService speech) : ControllerBase
{
    /// <summary>
    /// Recognizes a WAV recording without persisting it.
    /// </summary>
    [HttpPost("api/speech/recognize")]
    [Consumes("application/octet-stream")]
    [RequestSizeLimit(ISampleSpeechService.MaxAudioSize)]
    public async Task<ActionResult<SampleSpeechRecognitionResult>> Recognize(CancellationToken cancellationToken)
    {
        if (Request.ContentLength > ISampleSpeechService.MaxAudioSize)
        {
            return Problem(detail: "The audio exceeds 4 MB.", statusCode: 413);
        }

        using var audio = new MemoryStream();
        var buffer = new byte[81920];
        int read;
        while ((read = await Request.Body.ReadAsync(buffer, cancellationToken)) > 0)
        {
            if (audio.Length + read > ISampleSpeechService.MaxAudioSize)
            {
                return Problem(detail: "The audio exceeds 4 MB.", statusCode: 413);
            }
            await audio.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
        }
        var bytes = audio.ToArray();
        if (bytes.Length < 44 ||
            !bytes.AsSpan(0, 4).SequenceEqual("RIFF"u8) ||
            !bytes.AsSpan(8, 4).SequenceEqual("WAVE"u8))
        {
            return Problem(detail: "A valid WAV recording is required.", statusCode: 400);
        }

        Response.Headers.CacheControl = "no-store";
        return await speech.RecognizeAsync(bytes, cancellationToken);
    }

    /// <summary>
    /// Synthesizes bounded text into an MP3 response.
    /// </summary>
    [HttpPost("api/speech/synthesize")]
    [Consumes("application/json")]
    [RequestSizeLimit(16 * 1024)]
    public async Task<IActionResult> Synthesize(SampleSpeechSynthesisRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Text) || request.Text.Length > ISampleSpeechService.MaxTextLength)
        {
            return Problem(detail: "Text must contain between 1 and 1024 characters.", statusCode: 400);
        }

        Response.Headers.CacheControl = "no-store";
        var audio = await speech.SynthesizeAsync(request.Text, cancellationToken);
        return File(audio, "audio/mpeg");
    }
}
