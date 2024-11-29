// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BootstrapBlazor.Shared.Components.Samples.Tutorials.Translation;

class LanguageWriter : IDisposable
{
    private readonly MemoryStream _stream = default!;

    private readonly Utf8JsonWriter _writer = default!;

    public static JsonWriterOptions EncoderOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        Indented = true
    };

    public LanguageWriter()
    {
        _stream = new MemoryStream();

        _writer = new Utf8JsonWriter(_stream, EncoderOptions);
        _writer.WriteStartObject();
    }

    public Utf8JsonWriter Writer => _writer;

    public string? LastSectionName { get; set; }

    public async Task<Stream> SaveAsync()
    {
        if (!string.IsNullOrEmpty(LastSectionName))
        {
            // end section 
            _writer.WriteEndObject();
        }

        // end object
        _writer.WriteEndObject();
        await _writer.FlushAsync();
        _stream.Position = 0;
        return _stream;
    }

    public async Task SaveAsync(string fileName)
    {
        await SaveAsync();
        await using var fileWriter = File.CreateText(fileName);
        await _stream.CopyToAsync(fileWriter.BaseStream);
        await fileWriter.WriteLineAsync();
        fileWriter.Close();
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _writer.Dispose();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}

static class LanguageWriterExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="sectionName"></param>
    /// <param name="itemName"></param>
    /// <param name="value"></param>
    public static void WriteSectionItem(this LanguageWriter writer, string sectionName, string itemName, string value)
    {
        if (writer.LastSectionName != sectionName)
        {
            if (!string.IsNullOrEmpty(writer.LastSectionName))
            {
                writer.Writer.WriteEndObject();
            }

            writer.LastSectionName = sectionName;
            writer.Writer.WriteStartObject(sectionName);
        }

        writer.Writer.WriteString(itemName, value);
    }
}
