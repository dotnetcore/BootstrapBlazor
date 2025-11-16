// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Diagnostics;
using System.Text;

namespace UnitTest.Performance;

public class StringExtensionsTest(ReadRazorFile reader, ITestOutputHelper logger) : IClassFixture<ReadRazorFile>
{
    private string Payload { get; set; } = reader.FileContent;

    private List<KeyValuePair<string, string>> Localizers { get; } = reader.Localizers;

    private ITestOutputHelper Logger { get; } = logger;

    private const int Count = 2;

    [Fact]
    public void Replace_Ok()
    {
        var sw = Stopwatch.StartNew();
        for (var index = 0; index < Count; index++)
        {
            Loop(Payload);
        }
        sw.Stop();
        Logger.WriteLine($"String: {sw.Elapsed}");

        void Loop(string payload)
        {
            Localizers.ForEach(kv =>
            {
                payload = payload.Replace($"@(((MarkupString)Localizer[\"{kv.Key}\"].Value).ToString())", kv.Value);
                payload = payload.Replace($"@((MarkupString)Localizer[\"{kv.Key}\"].Value)", kv.Value);
                payload = payload.Replace($"@Localizer[\"{kv.Key}\"]", kv.Value);
            });
            payload = payload.Replace("@@", "@");
            payload = payload.Replace("&lt;", "<");
            payload = payload.Replace("&gt;", ">");
        }
    }

    [Fact()]
    public void Replace_Bad()
    {
        var segment = Payload.AsMemory();
        var sw = Stopwatch.StartNew();
        for (var index = 0; index < Count; index++)
        {
            LoopSpan(segment);
        }
        sw.Stop();
        Logger.WriteLine($"Span: {sw.Elapsed}");

        void LoopSpan(ReadOnlyMemory<char> payload)
        {
            Localizers.ForEach(kv =>
            {
                payload = payload.Replace($"@(((MarkupString)Localizer[\"{kv.Key}\"].Value).ToString())".AsSpan(), kv.Value);
                payload = payload.Replace($"@((MarkupString)Localizer[\"{kv.Key}\"].Value)".AsSpan(), kv.Value);
                payload = payload.Replace($"@Localizer[\"{kv.Key}\"]".AsSpan(), kv.Value);
            });
            payload = payload.Replace("@@".AsSpan(), "@");
            payload = payload.Replace("&lt;".AsSpan(), "<");
            payload = payload.Replace("&gt;".AsSpan(), ">");
        }
    }
}

internal static class StringExtensions
{
    public static ReadOnlyMemory<char> Replace(this ReadOnlyMemory<char> source, ReadOnlySpan<char> oldValue, string newValue)
    {
        var sb = new StringBuilder(100 * 1024);
        while (!source.IsEmpty)
        {
            var index = source.Span.IndexOf(oldValue);
            if (index > -1)
            {
                sb.Append(source[0..index]);
                sb.Append(newValue.AsSpan());
                source = source[(index + oldValue.Length)..];
            }
            else
            {
                sb.Append(source);
                break;
            }
        }
        return sb.ToString().AsMemory();
    }
}

/// <summary>
/// 
/// </summary>
public class ReadRazorFile
{
    public string FileContent { get; }

    public List<KeyValuePair<string, string>> Localizers { get; }

    public ReadRazorFile()
    {
        FileContent = ReadFile();
        Localizers = new List<KeyValuePair<string, string>>(BuildLocalizer());

        string ReadFile()
        {
            var dirSeparator = Path.DirectorySeparatorChar;
            var rootFolder = $"..{dirSeparator}..{dirSeparator}..{dirSeparator}..{dirSeparator}..{dirSeparator}";
            var razorFile = $"src{dirSeparator}BootstrapBlazor.Server{dirSeparator}Components{dirSeparator}Samples{dirSeparator}Alerts.razor";
            var file = Path.Combine(AppContext.BaseDirectory, rootFolder, razorFile);
            return File.Exists(file) ? File.ReadAllText(file) : "";
        }

        IEnumerable<KeyValuePair<string, string>> BuildLocalizer()
        {
            var localizers = new List<KeyValuePair<string, string>>
            {
                new("Title", "Alert 警告"),
                new("SubTitle", "用于页面中展示重要的提示信息。"),
                new("BaseUsageText", "基本用法"),
                new("IntroText1", "页面中的非浮层元素，不会自动消失。"),
                new("AlertPrimaryText", "主要的警告框"),
                new("AlertSecondaryText", "次要的警告框"),
                new("AlertSuccessText", "成功的警告框"),
                new("AlertDangerText", "危险的警告框"),
                new("AlertWarningText", "警告的警告框"),
                new("AlertInfoText", "信息的警告框"),
                new("AlertDarkText", "黑暗的警告框"),
                new("CloseButtonUsageText", "关闭按钮"),
                new("IntroText2", "提供关闭按钮的警告框"),
                new("WithIconUsageText", "带 Icon"),
                new("IntroText3", "表示某种状态时提升可读性。"),
                new("ShowBarUsageText", "显示左侧 Bar"),
                new("IntroText4", "作为 <code>Tip</code> 使用")
            };
            return localizers;
        }
    }
}
