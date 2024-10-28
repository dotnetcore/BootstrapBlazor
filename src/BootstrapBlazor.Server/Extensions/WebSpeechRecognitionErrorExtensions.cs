﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// WebSpeechRecognitionError 扩展方法
/// </summary>
public static class WebSpeechRecognitionErrorExtensions
{
    /// <summary>
    /// 生成错误消息方法
    /// </summary>
    /// <param name="error"></param>
    /// <param name="localizer"></param>
    public static void ParseErrorMessage(this WebSpeechRecognitionError error, IStringLocalizer localizer)
    {
        if (error.Error == "no-speech")
        {
            error.Message = localizer["RecognitionErrorNoSpeech"];
        }
        else if (error.Error == "aborted")
        {
            error.Message = localizer["RecognitionErrorAborted"];
        }
        else if (error.Error == "audio-capture")
        {
            error.Message = localizer["RecognitionErrorAudioCapture"];
        }
        else if (error.Error == "network")
        {
            error.Message = localizer["RecognitionErrorNetwork"];
        }
        else if (error.Error == "not-allowed")
        {
            error.Message = localizer["RecognitionErrorNotAllowed"];
        }
        else if (error.Error == "service-not-allowed")
        {
            error.Message = localizer["RecognitionErrorServiceNotAllowed"];
        }
        else if (error.Error == "bad-grammar")
        {
            error.Message = localizer["RecognitionErrorBadGrammar"];
        }
        else if (error.Error == "language-not-supported")
        {
            error.Message = localizer["RecognitionErrorLanguageNotSupported"];
        }
        else if (error.Error == "not-support")
        {
            error.Message = localizer["RecognitionErrorNotSupported"];
        }
    }
}
