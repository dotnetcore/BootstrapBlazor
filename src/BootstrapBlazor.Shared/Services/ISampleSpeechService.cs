// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">为语音示例提供识别和合成服务。</para>
/// <para lang="en">Provides recognition and synthesis services for speech samples.</para>
/// </summary>
public interface ISampleSpeechService
{
    /// <summary>
    /// Maximum uploaded WAV audio size.
    /// </summary>
    const int MaxAudioSize = 4 * 1024 * 1024;

    /// <summary>
    /// Maximum text length sent for synthesis.
    /// </summary>
    const int MaxTextLength = 1024;

    /// <summary>
    /// Recognizes a WAV recording.
    /// </summary>
    Task<SampleSpeechRecognitionResult> RecognizeAsync(byte[] audio, CancellationToken cancellationToken = default);

    /// <summary>
    /// Synthesizes text into MP3 audio.
    /// </summary>
    Task<byte[]> SynthesizeAsync(string text, CancellationToken cancellationToken = default);
}
