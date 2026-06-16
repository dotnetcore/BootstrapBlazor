// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.

using System.Text.RegularExpressions;

namespace BootstrapBlazor.LLMsDocsGenerator;

internal static partial class TextHelpers
{
    public static string RemoveNonAscii(string? text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? ""
            : NonAsciiRegex().Replace(text, "");
    }

    public static string NormalizeWhitespace(string? text)
    {
        return string.IsNullOrWhiteSpace(text)
            ? ""
            : WhitespaceRegex().Replace(text.Trim(), " ");
    }

    public static string EscapeMarkdownCell(string text)
    {
        return text
            .Replace("|", "\\|", StringComparison.Ordinal)
            .Replace("\r", "", StringComparison.Ordinal)
            .Replace("\n", " ", StringComparison.Ordinal);
    }

    public static string Truncate(string text, int maxLength)
    {
        if (text.Length <= maxLength)
        {
            return text;
        }

        return text[..Math.Max(0, maxLength - 3)] + "...";
    }

    [GeneratedRegex(@"[^\u0009\u000A\u000D\u0020-\u007E]")]
    private static partial Regex NonAsciiRegex();

    [GeneratedRegex(@"\s+")]
    private static partial Regex WhitespaceRegex();
}
